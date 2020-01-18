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
    public class TVAsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /TVAs/
        public ActionResult Index()
        {
            return View(db.TVA.ToList());
        }

        // GET: /TVAs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVA tVA = db.TVA.Find(id);
            if (tVA == null)
            {
                return HttpNotFound();
            }
            return View(tVA);
        }

        // GET: /TVAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TVAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Valeur_TVA")] TVA tVA)
        {
            if (ModelState.IsValid)
            {
                db.TVA.Add(tVA);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }

            return View(tVA);
        }

        // GET: /TVAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVA tVA = db.TVA.Find(id);
            if (tVA == null)
            {
                return HttpNotFound();
            }
            return View(tVA);
        }

        // POST: /TVAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Valeur_TVA")] TVA tVA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tVA);
        }

        // GET: /TVAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TVA tVA = db.TVA.Find(id);
            if (tVA == null)
            {
                return HttpNotFound();
            }
            return View(tVA);
        }

        // POST: /TVAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TVA tVA = db.TVA.Find(id);
            db.TVA.Remove(tVA);
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
