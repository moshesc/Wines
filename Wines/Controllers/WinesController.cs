using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wines.Models;

namespace Wines.Controllers
{
    public class WinesController : Controller
    {
        private WinesContext db = new WinesContext();

        // GET: Wines
        public ActionResult Index(string wineByCountry, string searchString)
        {
            var CountryLst = new List<string>();

            var CountryQry = from d in db.Wines
                             orderby d.Country
                             select d.Country;

            CountryLst.AddRange(CountryQry.Distinct());
            ViewBag.wineByCountry = new SelectList(CountryLst);

            var wines = from m in db.Wines
                         select m;

            if (!string.IsNullOrEmpty(wineByCountry))
            {
                wines = wines.Where(x => x.Country == wineByCountry);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                wines = wines.Where(s => s.WineByName.Contains(searchString));
            }

            return View(wines);
        }

        // GET: Wines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // GET: Wines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Country,WineByRegion,Grape1,WineByStopper,WineByName,Vintage,Producer,ABV,GoWellWith,TestingNote,WineImage,FoodImage,RegionImage,CountryImage")] Wine wine)
        {
            if (wine.WineImage == null)
            {
                wine.WineImage = "http://img.tesco.com/Groceries/pi/533/8436014670533/IDShot_540x540.jpg";
            }
            if (ModelState.IsValid)
            {
                db.Wines.Add(wine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wine);
        }

        // GET: Wines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Country,WineByRegion,Grape1,WineByStopper,WineByName,Vintage,Producer,ABV,GoWellWith,TestingNote,WineImage,FoodImage,RegionImage,CountryImage")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wine);
        }

        // GET: Wines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wine wine = db.Wines.Find(id);
            db.Wines.Remove(wine);
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
