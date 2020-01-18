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
    public class FournisseursController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Fournisseurs/
        public ActionResult Index()
        {
            return View(db.Fournisseur.ToList());
        }

        // GET: /Fournisseurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fournisseur fournisseur = db.Fournisseur.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // GET: /Fournisseurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Fournisseurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FRSID,NOM,ADRESSE,TEL,FAX,EMAIL,RC,MF")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                db.Fournisseur.Add(fournisseur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fournisseur);
        }

        // GET: /Fournisseurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fournisseur fournisseur = db.Fournisseur.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // POST: /Fournisseurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FRSID,NOM,ADRESSE,TEL,FAX,EMAIL,RC,MF")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fournisseur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fournisseur);
        }

        // GET: /Fournisseurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fournisseur fournisseur = db.Fournisseur.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        // POST: /Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fournisseur fournisseur = db.Fournisseur.Find(id);
            db.Fournisseur.Remove(fournisseur);
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
