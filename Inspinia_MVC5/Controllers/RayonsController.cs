using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class RayonsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Rayons
        public ActionResult Index()
        {
            return View(db.Rayons.ToList());
        }

        // GET: Rayons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rayons rayons = db.Rayons.Find(id);
            if (rayons == null)
            {
                return HttpNotFound();
            }
            return View(rayons);
        }

        // GET: Rayons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rayons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rayon")] Rayons rayons)
        {
            if (ModelState.IsValid)
            {
                db.Rayons.Add(rayons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rayons);
        }

        // GET: Rayons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rayons rayons = db.Rayons.Find(id);
            if (rayons == null)
            {
                return HttpNotFound();
            }
            return View(rayons);
        }

        // POST: Rayons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rayon")] Rayons rayons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rayons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rayons);
        }

        // GET: Rayons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rayons rayons = db.Rayons.Find(id);
            if (rayons == null)
            {
                return HttpNotFound();
            }
            return View(rayons);
        }

        // POST: Rayons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rayons rayons = db.Rayons.Find(id);
            db.Rayons.Remove(rayons);
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
