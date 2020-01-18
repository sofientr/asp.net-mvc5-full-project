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
    public class SERVICESController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: SERVICES
        public ActionResult Index()
        {

            return View(db.SERVICES.ToList());
        }

        // GET: SERVICES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICES sERVICES = db.SERVICES.Find(id);
            if (sERVICES == null)
            {
                return HttpNotFound();
            }
            return View(sERVICES);
        }

        // GET: SERVICES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SERVICES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,REF,DES_SERVICE")] SERVICES sERVICES)
        {
            if (ModelState.IsValid)
            {
                db.SERVICES.Add(sERVICES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sERVICES);
        }

        // GET: SERVICES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICES sERVICES = db.SERVICES.Find(id);
            if (sERVICES == null)
            {
                return HttpNotFound();
            }
            return View(sERVICES);
        }

        // POST: SERVICES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,REF,DES_SERVICE")] SERVICES sERVICES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sERVICES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sERVICES);
        }

        // GET: SERVICES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVICES sERVICES = db.SERVICES.Find(id);
            if (sERVICES == null)
            {
                return HttpNotFound();
            }
            return View(sERVICES);
        }

        // POST: SERVICES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SERVICES sERVICES = db.SERVICES.Find(id);
            db.SERVICES.Remove(sERVICES);
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
