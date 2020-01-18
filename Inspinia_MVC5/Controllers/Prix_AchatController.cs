using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class Prix_AchatController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Prix_Achat/
        public ActionResult Index()
        {
            var prix_achat = db.Prix_Achat.Include(p => p.Categorie1).Include(p => p.Devise1).Include(p => p.FOURNISSEURS).Include(p => p.Marque1).Include(p => p.Sous_Categorie1).Include(p => p.TVA).Include(p => p.Unite1);
            return View(prix_achat.ToList());
        }

        // GET: /Prix_Achat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prix_Achat prix_Achat = db.Prix_Achat.Find(id);
            if (prix_Achat == null)
            {
                return HttpNotFound();
            }
            return View(prix_Achat);
        }
        //// GET: /Prix_Achat/Historique/5
        //public ActionResult Historique(int? id)
        //{
           

        //    var historique_Prix_Achat = new List<Historique_Prix_Achat>();
        //    var hist_Prix_Achat = (from m in db.Historique_Prix_Achat
        //                             where m.Product_ID == id
        //                             orderby m.Historque_ID
        //                           select m
        //                           );
        //    historique_Prix_Achat.AddRange(hist_Prix_Achat.Distinct());
        //    return View(historique_Prix_Achat.ToList());
            

            
        //}

        // GET: /Prix_Achat/Create
        public ActionResult Create()
        {
            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle");
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise");
            ViewBag.Fournisseur = new SelectList(db.Fournisseur, "FRSID", "NOM");
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque");
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle");
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA");
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite");
            return View();
        }
        [WebMethod]
        public string CurrencyConversion(string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = string.Format("https://free.currencyconverterapi.com/api/v5/convert?q={0}_{1}&compact=ultra", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] rate = response.Split(':');
            string val = rate[1].Remove(rate[1].Length - 1);
            return val;
        }
        // POST: /Prix_Achat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Designation,Marque,Categorie,Sous_Categorie,Unite,PU_HT_Sans_Remise,Remise,PU_HT_Avec_Remise,Devise,Valeur_TVA,PU_TTC,Fournisseur,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix")] Prix_Achat prix_Achat)
        {

            if (ModelState.IsValid)
            {
                db.Prix_Achat.Add(prix_Achat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle", prix_Achat.Categorie);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.Fournisseur, "FRSID", "NOM", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            return View(prix_Achat);
        }

        // GET: /Prix_Achat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prix_Achat prix_Achat = db.Prix_Achat.Find(id);
            if (prix_Achat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle", prix_Achat.Categorie);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.Fournisseur, "FRSID", "NOM", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            return View(prix_Achat);
        }

        // POST: /Prix_Achat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Designation,Marque,Categorie,Sous_Categorie,Unite,PU_HT_Sans_Remise,Remise,PU_HT_Avec_Remise,Devise,Valeur_TVA,PU_TTC,Fournisseur,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix")] Prix_Achat prix_Achat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prix_Achat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle", prix_Achat.Categorie);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.Fournisseur, "FRSID", "NOM", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            return View(prix_Achat);
        }

        // GET: /Prix_Achat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prix_Achat prix_Achat = db.Prix_Achat.Find(id);
            if (prix_Achat == null)
            {
                return HttpNotFound();
            }
            return View(prix_Achat);
        }

        // POST: /Prix_Achat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prix_Achat prix_Achat = db.Prix_Achat.Find(id);
            db.Prix_Achat.Remove(prix_Achat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetsousByCategoryId(int id)
        {
            List<Sous_Categorie> s = new List<Sous_Categorie>();
            if (id > 0)
            {
                s = db.Sous_Categorie.Where(p => p.CentreID == id).ToList();

            }

            var result = (from r in s
                          select new
                          {
                              id = r.CatID,
                              name = r.Libelle,
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
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
