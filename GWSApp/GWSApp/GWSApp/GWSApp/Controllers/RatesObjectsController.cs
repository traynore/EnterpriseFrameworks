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
    public class RatesObjectsController : Controller
    {
        private GWSContext db = new GWSContext();

        // GET: RatesObjects
        public ActionResult Index()
        {
            return View(db.RatesObjects.ToList());
        }

        // GET: RatesObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatesObject ratesObject = db.RatesObjects.Find(id);
            if (ratesObject == null)
            {
                return HttpNotFound();
            }
            return View(ratesObject);
        }

        // GET: RatesObjects/Create
        public ActionResult Create()
        {
            int count = db.RatesObjects.Count();
            if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            return View();
        }

        // POST: RatesObjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,BandA,BandB,BandC,BandD,RateA,RateB,RateC,RateD,RateE")] RatesObject ratesObject)
        {
            if (ModelState.IsValid)
            {
                db.RatesObjects.Add(ratesObject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ratesObject);
        }

        // GET: RatesObjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatesObject ratesObject = db.RatesObjects.Find(id);
            if (ratesObject == null)
            {
                return HttpNotFound();
            }
            return View(ratesObject);
        }

        // POST: RatesObjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,BandA,BandB,BandC,BandD,RateA,RateB,RateC,RateD,RateE")] RatesObject ratesObject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ratesObject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ratesObject);
        }

        // GET: RatesObjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatesObject ratesObject = db.RatesObjects.Find(id);
            if (ratesObject == null)
            {
                return HttpNotFound();
            }
            return View(ratesObject);
        }

        // POST: RatesObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RatesObject ratesObject = db.RatesObjects.Find(id);
            db.RatesObjects.Remove(ratesObject);
            db.SaveChanges();
            return RedirectToAction("Index");
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
