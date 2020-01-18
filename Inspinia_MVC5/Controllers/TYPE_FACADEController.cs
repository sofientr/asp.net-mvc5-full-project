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
    public class TYPE_FACADEController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: TYPE_FACADE
        public ActionResult Index()
        {
            return View(db.TYPE_FACADE.ToList());
        }

        // GET: TYPE_FACADE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_FACADE tYPE_FACADE = db.TYPE_FACADE.Find(id);
            if (tYPE_FACADE == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_FACADE);
        }

        // GET: TYPE_FACADE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TYPE_FACADE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TYPE_FACADE1")] TYPE_FACADE tYPE_FACADE)
        {
            if (ModelState.IsValid)
            {
                db.TYPE_FACADE.Add(tYPE_FACADE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tYPE_FACADE);
        }

        // GET: TYPE_FACADE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_FACADE tYPE_FACADE = db.TYPE_FACADE.Find(id);
            if (tYPE_FACADE == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_FACADE);
        }

        // POST: TYPE_FACADE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TYPE_FACADE1")] TYPE_FACADE tYPE_FACADE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tYPE_FACADE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tYPE_FACADE);
        }

        // GET: TYPE_FACADE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_FACADE tYPE_FACADE = db.TYPE_FACADE.Find(id);
            if (tYPE_FACADE == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_FACADE);
        }

        // POST: TYPE_FACADE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TYPE_FACADE tYPE_FACADE = db.TYPE_FACADE.Find(id);
            db.TYPE_FACADE.Remove(tYPE_FACADE);
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
