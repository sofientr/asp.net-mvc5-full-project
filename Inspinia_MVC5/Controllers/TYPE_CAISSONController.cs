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
    public class TYPE_CAISSONController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: TYPE_CAISSON
        public ActionResult Index()
        {
            return View(db.TYPE_CAISSON.ToList());
        }

        // GET: TYPE_CAISSON/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_CAISSON tYPE_CAISSON = db.TYPE_CAISSON.Find(id);
            if (tYPE_CAISSON == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_CAISSON);
        }

        // GET: TYPE_CAISSON/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TYPE_CAISSON/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TYPE_CAISSON1")] TYPE_CAISSON tYPE_CAISSON)
        {
            if (ModelState.IsValid)
            {
                db.TYPE_CAISSON.Add(tYPE_CAISSON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tYPE_CAISSON);
        }

        // GET: TYPE_CAISSON/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_CAISSON tYPE_CAISSON = db.TYPE_CAISSON.Find(id);
            if (tYPE_CAISSON == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_CAISSON);
        }

        // POST: TYPE_CAISSON/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TYPE_CAISSON1")] TYPE_CAISSON tYPE_CAISSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tYPE_CAISSON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tYPE_CAISSON);
        }

        // GET: TYPE_CAISSON/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPE_CAISSON tYPE_CAISSON = db.TYPE_CAISSON.Find(id);
            if (tYPE_CAISSON == null)
            {
                return HttpNotFound();
            }
            return View(tYPE_CAISSON);
        }

        // POST: TYPE_CAISSON/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TYPE_CAISSON tYPE_CAISSON = db.TYPE_CAISSON.Find(id);
            db.TYPE_CAISSON.Remove(tYPE_CAISSON);
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
