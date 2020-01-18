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
    public class CommandesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Commandes/
        public ActionResult Index()
        {
            var commande = db.Commande.Include(c => c.Decaissement).Include(c => c.Facturation).Include(c => c.Fournisseur);
            return View(commande.ToList());
        }

        // GET: /Commandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: /Commandes/Create
        public ActionResult Create()
        {
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "DecaissID");
            ViewBag.CmdID = new SelectList(db.Facturation, "FactID", "NomFact");
            ViewBag.CmdID = new SelectList(db.Fournisseur, "FRSID", "NOM");
            ViewBag.FRSID = new SelectList(db.Fournisseur, "FRSID", "NOM");
            return View();
        }

        // POST: /Commandes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CmdID,Date_Passasation,Prix_Totale,DecaissID,FRSID")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Commande.Add(commande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "Mo_pay", commande.DecaissID);
            ViewBag.CmdID = new SelectList(db.Facturation, "FactID", "NomFact", commande.CmdID);
            ViewBag.CmdID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.CmdID);
            ViewBag.FRSID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.FRSID);
            return View(commande);
        }

        // GET: /Commandes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "Mo_pay", commande.DecaissID);
            ViewBag.CmdID = new SelectList(db.Facturation, "FactID", "NomFact", commande.CmdID);
            ViewBag.CmdID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.CmdID);
            ViewBag.FRSID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.FRSID);
            return View(commande);
        }

        // POST: /Commandes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CmdID,Date_Passasation,Prix_Totale,DecaissID,FRSID")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DecaissID = new SelectList(db.Decaissement, "DecaissID", "Mo_pay", commande.DecaissID);
            ViewBag.CmdID = new SelectList(db.Facturation, "FactID", "NomFact", commande.CmdID);
            ViewBag.CmdID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.CmdID);
            ViewBag.FRSID = new SelectList(db.Fournisseur, "FRSID", "NOM", commande.FRSID);
            return View(commande);
        }

        // GET: /Commandes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commande.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: /Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.Commande.Find(id);
            db.Commande.Remove(commande);
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
