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
    public class CoordonneesSocsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /CoordonneesSocs/
        public ActionResult Index()
        {
            return View(db.CoordonneesSoc.ToList());
        }

        // GET: /CoordonneesSocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordonneesSoc coordonneesSoc = db.CoordonneesSoc.Find(id);
            if (coordonneesSoc == null)
            {
                return HttpNotFound();
            }
            return View(coordonneesSoc);
        }

        // GET: /CoordonneesSocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CoordonneesSocs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SCoorID,Nom_Soc")] CoordonneesSoc coordonneesSoc)
        {
            if (ModelState.IsValid)
            {
                db.CoordonneesSoc.Add(coordonneesSoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coordonneesSoc);
        }

        // GET: /CoordonneesSocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordonneesSoc coordonneesSoc = db.CoordonneesSoc.Find(id);
            if (coordonneesSoc == null)
            {
                return HttpNotFound();
            }
            return View(coordonneesSoc);
        }

        // POST: /CoordonneesSocs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SCoorID,Nom_Soc")] CoordonneesSoc coordonneesSoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordonneesSoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coordonneesSoc);
        }

        // GET: /CoordonneesSocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoordonneesSoc coordonneesSoc = db.CoordonneesSoc.Find(id);
            if (coordonneesSoc == null)
            {
                return HttpNotFound();
            }
            return View(coordonneesSoc);
        }

        // POST: /CoordonneesSocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoordonneesSoc coordonneesSoc = db.CoordonneesSoc.Find(id);
            db.CoordonneesSoc.Remove(coordonneesSoc);
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
