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
    public class LIGNES_FACTURES_FOURNISSEURSController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: LIGNES_FACTURES_FOURNISSEURS
        public ActionResult Index()
        {
            var lIGNES_FACTURES_FOURNISSEURS = db.LIGNES_FACTURES_FOURNISSEURS.Include(l => l.FACTURES_FOURNISSEURS).Include(l => l.Prix_Achat1);
            return View(lIGNES_FACTURES_FOURNISSEURS.ToList());
        }

        // GET: LIGNES_FACTURES_FOURNISSEURS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS = db.LIGNES_FACTURES_FOURNISSEURS.Find(id);
            if (lIGNES_FACTURES_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(lIGNES_FACTURES_FOURNISSEURS);
        }

        // GET: LIGNES_FACTURES_FOURNISSEURS/Create
        public ActionResult Create()
        {
            ViewBag.FACTURE_FOURNISSEUR = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE");
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation");
            return View();
        }

        // POST: LIGNES_FACTURES_FOURNISSEURS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Prix_achat,DESIGNATION_PRODUIT,QUANTITE,PRIX_UNITAIRE_HT,REMISE,TOTALE_REMISE_HT,TOTALE_HT,TVA,TOTALE_TTC,FACTURE_FOURNISSEUR,Categorie,Sous_Categorie,Marque,Unite,Devise,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix,Stock,Libelle_Prd")] LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
                db.LIGNES_FACTURES_FOURNISSEURS.Add(lIGNES_FACTURES_FOURNISSEURS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FACTURE_FOURNISSEUR = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", lIGNES_FACTURES_FOURNISSEURS.FACTURE_FOURNISSEUR);
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_FACTURES_FOURNISSEURS.Prix_achat);
            return View(lIGNES_FACTURES_FOURNISSEURS);
        }

        // GET: LIGNES_FACTURES_FOURNISSEURS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS = db.LIGNES_FACTURES_FOURNISSEURS.Find(id);
            if (lIGNES_FACTURES_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            ViewBag.FACTURE_FOURNISSEUR = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", lIGNES_FACTURES_FOURNISSEURS.FACTURE_FOURNISSEUR);
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_FACTURES_FOURNISSEURS.Prix_achat);
            return View(lIGNES_FACTURES_FOURNISSEURS);
        }

        // POST: LIGNES_FACTURES_FOURNISSEURS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Prix_achat,DESIGNATION_PRODUIT,QUANTITE,PRIX_UNITAIRE_HT,REMISE,TOTALE_REMISE_HT,TOTALE_HT,TVA,TOTALE_TTC,FACTURE_FOURNISSEUR,Categorie,Sous_Categorie,Marque,Unite,Devise,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix,Stock,Libelle_Prd")] LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lIGNES_FACTURES_FOURNISSEURS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FACTURE_FOURNISSEUR = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", lIGNES_FACTURES_FOURNISSEURS.FACTURE_FOURNISSEUR);
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_FACTURES_FOURNISSEURS.Prix_achat);
            return View(lIGNES_FACTURES_FOURNISSEURS);
        }

        // GET: LIGNES_FACTURES_FOURNISSEURS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS = db.LIGNES_FACTURES_FOURNISSEURS.Find(id);
            if (lIGNES_FACTURES_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(lIGNES_FACTURES_FOURNISSEURS);
        }

        // POST: LIGNES_FACTURES_FOURNISSEURS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LIGNES_FACTURES_FOURNISSEURS lIGNES_FACTURES_FOURNISSEURS = db.LIGNES_FACTURES_FOURNISSEURS.Find(id);
            db.LIGNES_FACTURES_FOURNISSEURS.Remove(lIGNES_FACTURES_FOURNISSEURS);
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
