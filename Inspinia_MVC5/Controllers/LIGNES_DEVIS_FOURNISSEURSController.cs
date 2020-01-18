using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class LIGNES_DEVIS_FOURNISSEURSController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: LIGNES_DEVIS_FOURNISSEURS
        public ActionResult Index()
        {
            var lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Include(t => t.DEVIS_FOURNISSEURS);
            return View(lIGNES_DEVIS_FOURNISSEURS.ToList());
        }

        // GET: LIGNES_DEVIS_FOURNISSEURS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Find(id);
            if (lIGNES_DEVIS_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(lIGNES_DEVIS_FOURNISSEURS);
        }

        // GET: LIGNES_DEVIS_FOURNISSEURS/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT");
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT");
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation");
            return View();
        }

        // POST: LIGNES_DEVIS_FOURNISSEURS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESIGNATION_PRODUIT,QUANTITE,PRIX_UNITAIRE_HT,REMISE,TOTALE_REMISE_HT,TOTALE_HT,TVA,TOTALE_TTC,DEVIS_CLIENT,Marque,Categorie,Sous_Categorie,Unite,Devise,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix,Description,Libelle_Prd,Prix_achat")] LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
                db.LIGNES_DEVIS_FOURNISSEURS.Add(lIGNES_DEVIS_FOURNISSEURS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_DEVIS_FOURNISSEURS.Prix_achat);
            return View(lIGNES_DEVIS_FOURNISSEURS);
        }

        // GET: LIGNES_DEVIS_FOURNISSEURS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Find(id);
            if (lIGNES_DEVIS_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.lib = lIGNES_DEVIS_FOURNISSEURS.Libelle_Prd;
            ViewBag.Marque = lIGNES_DEVIS_FOURNISSEURS.Marque;
            ViewBag.Categorie = lIGNES_DEVIS_FOURNISSEURS.Categorie;
            ViewBag.Sous_Categorie = lIGNES_DEVIS_FOURNISSEURS.Sous_Categorie;
            ViewBag.Unite = lIGNES_DEVIS_FOURNISSEURS.Unite;
            ViewBag.Devise = lIGNES_DEVIS_FOURNISSEURS.Devise;

            ViewBag.QUANTITE = lIGNES_DEVIS_FOURNISSEURS.QUANTITE;
            ViewBag.TVA = lIGNES_DEVIS_FOURNISSEURS.TVA;
            ViewBag.TOTALE_TTC = lIGNES_DEVIS_FOURNISSEURS.TOTALE_TTC;
            ViewBag.TOTALE_HT = lIGNES_DEVIS_FOURNISSEURS.TOTALE_HT;
            ViewBag.REMISE = lIGNES_DEVIS_FOURNISSEURS.REMISE;
            ViewBag.PRIX_UNITAIRE_HT = lIGNES_DEVIS_FOURNISSEURS.PRIX_UNITAIRE_HT;
            ViewBag.DEVIS_CLIENT = lIGNES_DEVIS_FOURNISSEURS.DEVIS_CLIENT;

            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_DEVIS_FOURNISSEURS.Prix_achat);
            return View(lIGNES_DEVIS_FOURNISSEURS);
        }

        // POST: LIGNES_DEVIS_FOURNISSEURS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,DESIGNATION_PRODUIT,QUANTITE,PRIX_UNITAIRE_HT,REMISE,TOTALE_HT,TVA,TOTALE_TTC,Marque,Categorie,Sous_Categorie,Unite,Devise,Libelle_Prd,Prix_achat")] LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS)
        public ActionResult Edit([Bind(Include = "ID,DESIGNATION_PRODUIT,Libelle_Prd")] LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS)
        {
            string ID = Request["ID"] != null ? Request["ID"].ToString() : string.Empty;

            string marque = Request["marque"] != null ? Request["marque"].ToString() : string.Empty;
            string categorie = Request["categorie"] != null ? Request["categorie"].ToString() : string.Empty;
            string Sous_Categorie = Request["Sous_Categorie"] != null ? Request["Sous_Categorie"].ToString() : string.Empty;

            string unite = Request["unite"] != null ? Request["unite"].ToString() : string.Empty;
            string devise = Request["devise"] != null ? Request["devise"].ToString() : string.Empty;
            string TVA = Request["TVA"] != null ? Request["TVA"].ToString() : "0";
          
            int idd = int.Parse(ID);
            LIGNES_DEVIS_FOURNISSEURS ligne = db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == idd).FirstOrDefault();
            if (ModelState.IsValid)
            {
                ligne.Categorie = categorie;
                ligne.Devise = devise;
                ligne.Unite = unite;
                ligne.TVA = int.Parse(TVA);
                ligne.Marque = marque;
                ligne.Sous_Categorie = Sous_Categorie;
                ligne.DESIGNATION_PRODUIT = lIGNES_DEVIS_FOURNISSEURS.DESIGNATION_PRODUIT;
                if(lIGNES_DEVIS_FOURNISSEURS.Libelle_Prd!="")
                {
                ligne.Libelle_Prd = lIGNES_DEVIS_FOURNISSEURS.Libelle_Prd;
                }
                //db.Entry(lIGNES_DEVIS_FOURNISSEURS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           

            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.ID = new SelectList(db.LIGNES_DEVIS_FOURNISSEURS, "ID", "DESIGNATION_PRODUIT", lIGNES_DEVIS_FOURNISSEURS.ID);
            ViewBag.Prix_achat = new SelectList(db.Prix_Achat, "Product_ID", "Designation", lIGNES_DEVIS_FOURNISSEURS.Prix_achat);
            return View(lIGNES_DEVIS_FOURNISSEURS);
        }

        // GET: LIGNES_DEVIS_FOURNISSEURS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Find(id);
            if (lIGNES_DEVIS_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(lIGNES_DEVIS_FOURNISSEURS);
        }

        // POST: LIGNES_DEVIS_FOURNISSEURS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LIGNES_DEVIS_FOURNISSEURS lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Find(id);
            List<LIGNES_DEVIS_CLIENTS> LIGNES_DEVIS_CLIENTS = db.LIGNES_DEVIS_CLIENTS.Where(f => f.Art_Devis_Frs == id).ToList();
            List<LIGNES_BONS_LIVRAISONS_CLIENTS> LIGNES_BONS_LIVRAISONS_CLIENTS = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(f => f.Prix_achat == id).ToList();
            if(LIGNES_DEVIS_CLIENTS==null && LIGNES_BONS_LIVRAISONS_CLIENTS==null)
            {
                db.LIGNES_DEVIS_FOURNISSEURS.Remove(lIGNES_DEVIS_FOURNISSEURS);
                db.SaveChanges();

            }
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
