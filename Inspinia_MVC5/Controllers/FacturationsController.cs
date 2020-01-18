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
    public class FacturationsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Facturations/
        public ActionResult Index()
        {
            var facturation = db.Facturation.Include(f => f.Commande).Include(f => f.Encaissement).Include(f => f.Projets);
            return View(facturation.ToList());
        }

        // GET: /Facturations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = db.Facturation.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            return View(facturation);
        }

        // GET: /Facturations/Create
        public ActionResult Create()
        {
            ViewBag.FactID = new SelectList(db.Commande, "CmdID", "CmdID");
            ViewBag.EncaissID = new SelectList(db.Encaissement, "EncaissID", "EncaissID");
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "CODE");
            return View();
        }

        // POST: /Facturations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FactID,PrID,EncaissID,DateFact,NomFact")] Facturation facturation)
        {
            if (ModelState.IsValid)
            {
                db.Facturation.Add(facturation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactID = new SelectList(db.Commande, "CmdID", "CmdID", facturation.FactID);
            ViewBag.EncaissID = new SelectList(db.Encaissement, "EncaissID", "EncaissID", facturation.EncaissID);
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "CODE", facturation.PrID);
            return View(facturation);
        }

        // GET: /Facturations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = db.Facturation.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactID = new SelectList(db.Commande, "CmdID", "CmdID", facturation.FactID);
            ViewBag.EncaissID = new SelectList(db.Encaissement, "EncaissID", "EncaissID", facturation.EncaissID);
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "CODE", facturation.PrID);
            return View(facturation);
        }

        // POST: /Facturations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FactID,PrID,EncaissID,DateFact,NomFact")] Facturation facturation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactID = new SelectList(db.Commande, "CmdID", "CmdID", facturation.FactID);
            ViewBag.EncaissID = new SelectList(db.Encaissement, "EncaissID", "EncaissID", facturation.EncaissID);
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "CODE", facturation.PrID);
            return View(facturation);
        }

        // GET: /Facturations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = db.Facturation.Find(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            return View(facturation);
        }

        // POST: /Facturations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturation facturation = db.Facturation.Find(id);
            db.Facturation.Remove(facturation);
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
