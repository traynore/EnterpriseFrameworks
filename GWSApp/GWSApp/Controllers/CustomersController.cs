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
using CsvHelper;
using System.IO;

namespace GWSApp.Controllers
{
    public class CustomersController : Controller
    {
        private GWSContext db = new GWSContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InvoiceNumber,LastName,FirstName,JoinDate,Address1,Address2,Address3,Address4,Telephone,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InvoiceNumber,LastName,FirstName,JoinDate,Address1,Address2,Address3,Address4,Telephone,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ImportCsv()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ImportCsv(HttpPostedFileBase file)
        {

            string path = null;

            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "upload/" + fileName;
                    file.SaveAs(path);

                    var csv = new CsvReader(new StreamReader(path));
                    csv.Configuration.IgnoreHeaderWhiteSpace = true;
                    var customers = csv.GetRecords<ImportCustomer>();

                    //clear the database before saving imported records
                    var customersToDelete = from m in db.Customers
                                         select m;
                    foreach (var customer in customersToDelete)
                    {
                        db.Customers.Remove(customer);
                    }
                    //save imported records to DB
                    foreach (var customer in customers)
                    {
                        Customer newCust = new Customer();
                        newCust.InvoiceNumber = int.Parse(customer.InvNo);
                        newCust.LastName = customer.Surname;
                        newCust.FirstName = customer.Name;
                        newCust.Address1 = customer.Address1;
                        newCust.Address2 = customer.Address2;
                        newCust.Address3 = customer.Address3;
                        newCust.Address4 = customer.Address4;
                        db.Customers.Add(newCust);
                        //convert empty strings to "0" so they can be parsed as numerical data
                        if (customer.Arrears2008 == "")
                        {
                            customer.Arrears2008 = "0";
                        }
                        if (customer.Arrears2009 == "")
                        {
                            customer.Arrears2009 = "0";
                        }
                        if (customer.Arrears2010 == "")
                        {
                            customer.Arrears2010 = "0";
                        }
                        if (customer.Arrears2011 == "")
                        {
                            customer.Arrears2011 = "0";
                        }
                        if (customer.QtyRateA == "")
                        {
                            customer.QtyRateA = "0";
                        }
                        if (customer.QtyRateB == "")
                        {
                            customer.QtyRateB = "0";
                        }
                        if (customer.QtyRateC == "")
                        {
                            customer.QtyRateC = "0";
                        }
                        if (customer.QtyRateD == "")
                        {
                            customer.QtyRateD = "0";
                        }
                        if (customer.QtyRateE == "")
                        {
                            customer.QtyRateE = "0";
                        }
                        if (customer.BSubTotal == "")
                        {
                            customer.BSubTotal = "0";
                        }
                        if (customer.CSubTotal == "")
                        {
                            customer.CSubTotal = "0";
                        }
                        if (customer.DSubTotal == "")
                        {
                            customer.DSubTotal = "0";
                        }
                        if (customer.ESubTotal == "")
                        {
                            customer.ESubTotal = "0";
                        }
                        if (customer.Total == "")
                        {
                            customer.Total = "0";
                        }
                        if (customer.TotalArrears == "")
                        {
                            customer.TotalArrears = "0";
                        }
                        if (customer.GrandTotal == "")
                        {
                            customer.GrandTotal = "0";
                        }
                        if (customer.Paid == "")
                        {
                            customer.Paid = "0";
                        }
                        
                        db.SaveChanges(); //necessary to save here to generate an ID for the customer

                        if (float.Parse(customer.Arrears2008) > 0)
                        {
                            Invoice arrears08 = new Invoice();
                            arrears08.CustomerID = newCust.ID;
                            arrears08.Year = 2008;
                            arrears08.Total = float.Parse(customer.Arrears2008);
                            db.Invoices.Add(arrears08);
                        }
                        if (float.Parse(customer.Arrears2009) > 0)
                        {
                            Invoice arrears09 = new Invoice();
                            arrears09.CustomerID = newCust.ID;
                            arrears09.Year = 2009;
                            arrears09.Total = float.Parse(customer.Arrears2009);
                            db.Invoices.Add(arrears09);
                        }
                        if (float.Parse(customer.Arrears2010) > 0)
                        {
                            Invoice arrears10 = new Invoice();
                            arrears10.CustomerID = newCust.ID;
                            arrears10.Year = 2010;
                            arrears10.Total = float.Parse(customer.Arrears2010);
                            db.Invoices.Add(arrears10);
                        }
                        if (float.Parse(customer.Arrears2011) > 0)
                        {
                            Invoice arrears11 = new Invoice();
                            arrears11.CustomerID = newCust.ID;
                            arrears11.Year = 2011;
                            arrears11.Total = float.Parse(customer.Arrears2011);
                            db.Invoices.Add(arrears11);
                        }

                        // not sure about 2012 arrears
                        if (float.Parse(customer.QtyRateA) > 0) // if customer used any water in last year
                        {
                            MeterReading reading = new MeterReading();
                            reading.CustomerID = newCust.ID;
                            var quantity = float.Parse(customer.QtyRateA)
                                           + float.Parse(customer.QtyRateB)
                                            + float.Parse(customer.QtyRateC)
                                            + float.Parse(customer.QtyRateD)
                                            + float.Parse(customer.QtyRateE);
                            reading.Quantity = (int)quantity;
                            reading.Year = 2013;
                            db.MeterReadings.Add(reading);
                            // import 2013 invoice 
                            Invoice invoice13 = new Invoice();
                            invoice13.CustomerID = newCust.ID;
                            invoice13.Year = 2013;
                            var rateA = float.Parse(customer.QtyRateA);
                            invoice13.QtyRateA = (int)rateA;
                            var rateB = float.Parse(customer.QtyRateB);
                            invoice13.QtyRateB = (int)rateB;
                            var rateC = float.Parse(customer.QtyRateC);
                            invoice13.QtyRateC = (int)rateC;
                            var rateD = float.Parse(customer.QtyRateD);
                            invoice13.QtyRateD = (int)rateD;
                            var rateE = float.Parse(customer.QtyRateE);
                            invoice13.QtyRateE = (int)rateE;
                            invoice13.SubtotalA = 0;
                            invoice13.SubtotalB = float.Parse(customer.BSubTotal);
                            invoice13.SubtotalC = float.Parse(customer.CSubTotal);
                            invoice13.SubtotalD = float.Parse(customer.DSubTotal);
                            invoice13.SubtotalE = float.Parse(customer.ESubTotal);
                            invoice13.Total = float.Parse(customer.Total);
                            invoice13.Arrears = float.Parse(customer.TotalArrears);
                            invoice13.GrandTotal = float.Parse(customer.GrandTotal);
                            invoice13.AmountPaid = float.Parse(customer.Paid);
                            db.Invoices.Add(invoice13);

                        }



                    }
                    db.SaveChanges();

                    ViewBag.FileName = fileName;
                    return View(customers);

                }
            }

            catch
            {
                ViewData["error"] = "Upload Failed";

            }

            return View();

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
