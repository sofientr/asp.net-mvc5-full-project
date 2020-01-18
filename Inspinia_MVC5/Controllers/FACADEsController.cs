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
    public class FACADEsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: FACADEs
        public ActionResult Index()
        {
            return View(db.FACADE.ToList());
        }

        // GET: FACADEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACADE fACADE = db.FACADE.Find(id);
            if (fACADE == null)
            {
                return HttpNotFound();
            }
            return View(fACADE);
        }

        // GET: FACADEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FACADEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,REF_FAC")] FACADE fACADE)
        {
            if (ModelState.IsValid)
            {
                db.FACADE.Add(fACADE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fACADE);
        }

        // GET: FACADEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACADE fACADE = db.FACADE.Find(id);
            if (fACADE == null)
            {
                return HttpNotFound();
            }
            return View(fACADE);
        }

        // POST: FACADEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,REF_FAC")] FACADE fACADE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fACADE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fACADE);
        }

        // GET: FACADEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACADE fACADE = db.FACADE.Find(id);
            if (fACADE == null)
            {
                return HttpNotFound();
            }
            return View(fACADE);
        }

        // POST: FACADEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FACADE fACADE = db.FACADE.Find(id);
            db.FACADE.Remove(fACADE);
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
