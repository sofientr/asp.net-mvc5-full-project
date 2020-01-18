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
    public class DépotController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Dépot
        public ActionResult Index()
        {
            return View(db.Dépot.ToList());
        }

        // GET: Dépot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dépot dépot = db.Dépot.Find(id);
            if (dépot == null)
            {
                return HttpNotFound();
            }
            return View(dépot);
        }

        // GET: Dépot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dépot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Dépot1")] Dépot dépot)
        {
            if (ModelState.IsValid)
            {
                db.Dépot.Add(dépot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dépot);
        }

        // GET: Dépot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dépot dépot = db.Dépot.Find(id);
            if (dépot == null)
            {
                return HttpNotFound();
            }
            return View(dépot);
        }

        // POST: Dépot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Dépot1")] Dépot dépot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dépot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dépot);
        }

        // GET: Dépot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dépot dépot = db.Dépot.Find(id);
            if (dépot == null)
            {
                return HttpNotFound();
            }
            return View(dépot);
        }

        // POST: Dépot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dépot dépot = db.Dépot.Find(id);
            db.Dépot.Remove(dépot);
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
