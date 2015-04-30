using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GWSApp.DAL;
using GWSApp.Models;

namespace GWSApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class MeterReadingsController : Controller
    {
        private GWSContext db = new GWSContext();

        // GET: MeterReadings
        public ActionResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // error message displays if user tried to create multiple meter readings in a single billing period
            if (Request.QueryString["error"] == "error")
            {
                ViewBag.Error = "<p class='text-danger'>Meter Reading for that year and customer exists already. Please Edit instead.</p>";
            }

            var meterReadings = from m in db.MeterReadings.Include(i => i.Customer) select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                meterReadings = meterReadings.Where(s => s.Customer.LastName.Equals(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    meterReadings = meterReadings.OrderByDescending(s => s.Customer.LastName);
                    break;
                default:
                    meterReadings = meterReadings.OrderBy(s => s.Customer.LastName);
                    break;
            }

            return View(meterReadings.ToList());
        }

        // GET: MeterReadings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterReading meterReading = db.MeterReadings.Find(id);
            if (meterReading == null)
            {
                return HttpNotFound();
            }
            return View(meterReading);
        }

        // GET: MeterReadings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName");
            return View();
        }

        // POST: MeterReadings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,Year,Quantity")] MeterReading meterReading)
        {

            if (ModelState.IsValid)
            {

                // check to ensure there is not already a reading for that customer and year

                var readings = from c in db.MeterReadings
                             select c;
                readings = readings.Where(s => s.CustomerID.Equals(meterReading.CustomerID));
                if (readings.Where(t => t.Year.Equals(meterReading.Year)).Count() > 0)
                {
                    return RedirectToAction("Index", new { error = "error" });
                    //return RedirectToAction("Index");
                }

                
                db.MeterReadings.Add(meterReading);
                db.SaveChanges();

                // generate invoice using usage
                CalculateInvoice(meterReading);
                // create a new note
                Note newNote = new Note();
                newNote.CustomerID = meterReading.CustomerID;
                newNote.NoteText = "Meter Reading of " + meterReading.Quantity + " added and Invoice Calculated at " + DateTime.Now + ".";
                db.Notes.Add(newNote);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", meterReading.CustomerID);
            return View(meterReading);
        }

        // GET: MeterReadings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterReading meterReading = db.MeterReadings.Find(id);
            if (meterReading == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", meterReading.CustomerID);
            return View(meterReading);
        }

        // POST: MeterReadings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,Year,Quantity")] MeterReading meterReading)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meterReading).State = EntityState.Modified;
                db.SaveChanges();

                // now that meterreadng has been edited, need to make a fresh invoice
                // find previous invoice and delete
                var invoices = from c in db.Invoices
                               select c;
                invoices = invoices.Where(s => s.CustomerID.Equals(meterReading.CustomerID));
                invoices = invoices.Where(t => t.Year.Equals(meterReading.Year));
                foreach (var invoice in invoices)
                {
                    db.Invoices.Remove(invoice);
                }
                db.SaveChanges();

                //now create fresh invoice with updated reading
                CalculateInvoice(meterReading);

                // create a new note to record changes
                Note newNote = new Note();
                newNote.CustomerID = meterReading.CustomerID;
                newNote.NoteText = "Meter Reading edited to " + meterReading.Quantity + " and fresh Invoice Calculated at " + DateTime.Now + ".";
                db.Notes.Add(newNote);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", meterReading.CustomerID);
            return View(meterReading);

        }

        // GET: MeterReadings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterReading meterReading = db.MeterReadings.Find(id);
            if (meterReading == null)
            {
                return HttpNotFound();
            }
            return View(meterReading);
        }

        // POST: MeterReadings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeterReading meterReading = db.MeterReadings.Find(id);
            db.MeterReadings.Remove(meterReading);
            db.SaveChanges();

            // find invoice and delete
            var invoices = from c in db.Invoices
                           select c;
            invoices = invoices.Where(s => s.CustomerID.Equals(meterReading.CustomerID));
            invoices = invoices.Where(t => t.Year.Equals(meterReading.Year));
            foreach (var invoice in invoices)
            {
                db.Invoices.Remove(invoice);
            }
            db.SaveChanges();

            // create a new note to record changes
            Note newNote = new Note();
            newNote.CustomerID = meterReading.CustomerID;
            newNote.NoteText = "Meter Reading and associated Invoice deleted at " + DateTime.Now + ".";
            db.Notes.Add(newNote);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // invoices are calculated whenever a meterreading is created or changed
        public void CalculateInvoice(MeterReading meterReading)
        {
            // rates are caled from Application state rather than querying DB
            RatesObject rates = (RatesObject)HttpContext.Application["Rates"];
            Invoice newInvoice = new Invoice();
            newInvoice.CustomerID = meterReading.CustomerID;
            newInvoice.Year = meterReading.Year;
            // calculations of usage per band and subtotals; currently hardcoded for 5 bands
            // calculation method based on spreadsheet sheet 2 billing rates, not 2013 data
            if (meterReading.Quantity > 0)
            {
                if (meterReading.Quantity >= rates.BandA)
                {
                    newInvoice.QtyRateA = rates.BandA;  // default 115
                }
                else
                {
                    newInvoice.QtyRateA = meterReading.Quantity;
                }
                newInvoice.SubtotalA = newInvoice.QtyRateA * rates.RateA; //usually zero unless rate for Band A changes
            }

            if (meterReading.Quantity > rates.BandA)
            {
                if (meterReading.Quantity >= rates.BandB)
                {
                    newInvoice.QtyRateB = rates.BandB - rates.BandA; // maximum for band B 400-115 = 285
                }
                else
                {
                    newInvoice.QtyRateB = meterReading.Quantity - rates.BandA;
                }
                newInvoice.SubtotalB = newInvoice.QtyRateB * rates.RateB;
            }

            if (meterReading.Quantity > rates.BandB)
            {
                if (meterReading.Quantity >= rates.BandC)
                {
                    newInvoice.QtyRateC = rates.BandC - rates.BandB; // 800-400 = 400
                }
                else
                {
                    newInvoice.QtyRateC = meterReading.Quantity - rates.BandB;
                }
                newInvoice.SubtotalC = newInvoice.QtyRateC * rates.RateC;
            }

            if (meterReading.Quantity > rates.BandC)
            {
                if (meterReading.Quantity >= rates.BandD)
                {
                    newInvoice.QtyRateD = rates.BandD - rates.BandC; // 1500-800 = 700
                }
                else
                {
                    newInvoice.QtyRateD = meterReading.Quantity - rates.BandC;
                }
                newInvoice.SubtotalD = newInvoice.QtyRateD * rates.RateD;
            }

            if (meterReading.Quantity > rates.BandD)
            {
                newInvoice.QtyRateE = meterReading.Quantity - rates.BandD;
                newInvoice.SubtotalE = newInvoice.QtyRateE * rates.RateE;
            }

            newInvoice.Total = newInvoice.SubtotalA + newInvoice.SubtotalB + newInvoice.SubtotalC + newInvoice.SubtotalD + newInvoice.SubtotalE;
            newInvoice.GrandTotal = newInvoice.Total + newInvoice.Arrears;
            db.Invoices.Add(newInvoice);

            db.SaveChanges();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
