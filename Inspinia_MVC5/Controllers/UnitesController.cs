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
    public class UnitesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Unites/
        public ActionResult Index()
        {
            return View(db.Unite.ToList());
        }

        // GET: /Unites/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unite.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            return View(unite);
        }

        // GET: /Unites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Unites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Valeur_Unite")] Unite unite)
        {
            if (ModelState.IsValid)
            {
                db.Unite.Add(unite);
                try { 
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                catch { return RedirectToAction("Index"); }
            }

            return View(unite);
        }

        // GET: /Unites/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unite.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            return View(unite);
        }

        // POST: /Unites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Valeur_Unite")] Unite unite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unite);
        }

        // GET: /Unites/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unite.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            return View(unite);
        }

        // POST: /Unites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Unite unite = db.Unite.Find(id);
            db.Unite.Remove(unite);
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
