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
    public class ProjetsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Projets/
        public ActionResult Index()
        {
            var projets = db.Projets.Include(p => p.Decaissement).Include(p => p.Tiers);
            return View(projets.ToList());
        }

        public ActionResult IndexParProjet()
        {
            var projets = db.Projets.Include(p => p.Decaissement).Include(p => p.Tiers);
            return View(projets.ToList());
        }
        public ActionResult DepensesProjets()
        {
            var projets = db.Projets.Include(p => p.Decaissement).Include(p => p.Tiers);
            return View(projets.ToList());
        }
        // GET: /Projets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projets projets = db.Projets.Find(id);
            if (projets == null)
            {
                return HttpNotFound();
            }
            return View(projets);
        }

        // GET: /Projets/Create
        public ActionResult Create()
        {
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "DecaissID");
            ViewBag.TiersID = new SelectList(db.Tiers, "TiersID", "TEL");
            return View();
        }

        // POST: /Projets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PrID,CODE,NOM_PROJET,TYPE,DEBUT,FIN,MONTANT_HT,TVA,GARANTIE,Budget,SociID,DecaissID,TiersID")] Projets projets)
        {
            if (ModelState.IsValid)
            {
                db.Projets.Add(projets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "DecaissID", projets.DecaissID);
            ViewBag.TiersID = new SelectList(db.Tiers, "TiersID", "TEL", projets.TiersID);
            return View(projets);
        }

        // GET: /Projets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projets projets = db.Projets.Find(id);
            if (projets == null)
            {
                return HttpNotFound();
            }
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "DecaissID", projets.DecaissID);
            ViewBag.TiersID = new SelectList(db.Tiers, "TiersID", "TEL", projets.TiersID);
            return View(projets);
        }

        // POST: /Projets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PrID,CODE,NOM_PROJET,TYPE,DEBUT,FIN,MONTANT_HT,TVA,GARANTIE,Budget,SociID,DecaissID,TiersID")] Projets projets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "DecaissID", projets.DecaissID);
            ViewBag.TiersID = new SelectList(db.Tiers, "TiersID", "TEL", projets.TiersID);
            return View(projets);
        }

        // GET: /Projets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projets projets = db.Projets.Find(id);
            if (projets == null)
            {
                return HttpNotFound();
            }
            return View(projets);
        }

        // POST: /Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projets projets = db.Projets.Find(id);
            db.Projets.Remove(projets);
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
