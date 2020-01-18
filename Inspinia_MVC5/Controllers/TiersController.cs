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
    public class TiersController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Tiers/
        public ActionResult Index()
        {
            var tiers = db.Tiers.Include(t => t.Societes);
            return View(tiers.ToList());
        }

        public ActionResult ListeTiers()
        {
            var tiers = db.Tiers.Include(t => t.Societes);
            return View(tiers.ToList());
        }

        // GET: /Tiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tiers tiers = db.Tiers.Find(id);
            if (tiers == null)
            {
                return HttpNotFound();
            }
            return View(tiers);
        }

        // GET: /Tiers/Create
        public ActionResult Create()
        {
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM");
            return View();
        }

        // POST: /Tiers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TiersID,SociID,TEL,FAX,NOM,ADRESSE,MAIL")] Tiers tiers)
        {
            if (ModelState.IsValid)
            {
                db.Tiers.Add(tiers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", tiers.SociID);
            return View(tiers);
        }

        // GET: /Tiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tiers tiers = db.Tiers.Find(id);
            if (tiers == null)
            {
                return HttpNotFound();
            }
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", tiers.SociID);
            return View(tiers);
        }

        // POST: /Tiers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "TiersID,SociID,TEL,FAX,NOM,ADRESSE,MAIL")] Tiers tiers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", tiers.SociID);
            return View(tiers);
        }

        // GET: /Tiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tiers tiers = db.Tiers.Find(id);
            if (tiers == null)
            {
                return HttpNotFound();
            }
            return View(tiers);
        }

        // POST: /Tiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tiers tiers = db.Tiers.Find(id);
            db.Tiers.Remove(tiers);
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
