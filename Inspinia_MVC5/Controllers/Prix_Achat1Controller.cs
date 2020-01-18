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
    public class Prix_Achat1Controller : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Prix_Achat1/Rayons/5
        public ActionResult Rayons(string id)
        {
            ViewBag.id = id;
            return PartialView("Rayons");
        }
        // GET: Prix_Achat1
        public ActionResult Index()
        {
            Session["DepotRayon"] = null;
            var prix_Achat = db.Prix_Achat.Include(p => p.Categorie1).Include(p => p.Devise1).Include(p => p.FOURNISSEURS).Include(p => p.Marque1).Include(p => p.Sous_Categorie1).Include(p => p.TVA).Include(p => p.Unite1);
            return View(prix_Achat.ToList());
        }
        public JsonResult GetAlldépot()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Dépot> list = db.Dépot.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllrayon()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Rayons> list = db.Rayons.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: Prix_Achat1/Details/5
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
            ViewBag.lib = prix_Achat.Libelle;
            ViewBag.id = prix_Achat.Product_ID;
            return View(prix_Achat);
        }

        // GET: Prix_Achat1/Create
        public ActionResult Create()
        {
            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle");
            ViewBag.DEVIS_FRS = new SelectList(db.DEVIS_FOURNISSEURS, "ID", "Designation");
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise");
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "NOM");
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque");
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle");
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA");
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite");
            return View();
        }

        // POST: Prix_Achat1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Designation,Marque,Categorie,Sous_Categorie,Unite,PU_HT_Sans_Remise,Remise,PU_HT_Avec_Remise,Devise,Valeur_TVA,PU_TTC,Fournisseur,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix,QUANTITE,QUANTITE_REPTURE_STOCK,Libelle,DEVIS_FRS,Stock")] Prix_Achat prix_Achat)
        {
            prix_Achat.Stock = prix_Achat.QUANTITE;
            prix_Achat.Remise = 0;
            prix_Achat.Valeur_TVA = "0";
            if (ModelState.IsValid)
            {
                db.Prix_Achat.Add(prix_Achat);
                db.SaveChanges();
                DEVIS_FOURNISSEURS devisFrs = new DEVIS_FOURNISSEURS();
                string Numero = string.Empty;
                DateTime d = DateTime.Today;
                int Max = 0;
                int idste = (int)Session["SoclogoId"];
                PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 2).FirstOrDefault();
                if (PrefixeTable == null)
                {
                    if (db.DEVIS_FOURNISSEURS.Where(f => f.Societes == idste).ToList().Count != 0)
                    {
                        Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
                    }
                    Max++;

                    Numero = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                    while (db.DEVIS_FOURNISSEURS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
                    {
                        Max++;
                        Numero = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                    }
                }
                else
                {
                    if (db.DEVIS_FOURNISSEURS.Where(f => f.Societes == idste).ToList().Count != 0)
                    {
                        Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
                    }
                    Max++;
                    string PRF = PrefixeTable.Prefixe;
                    string numPre = PRF.Replace("0000", Max.ToString("0000"));
                    string count = "";
                    string count1 = "";
                    foreach (char c in numPre)
                    {
                        if (c == 'y')
                        {
                            count += c;
                        }
                    }
                    foreach (char c in numPre)
                    {
                        if (c == 'm')
                        {
                            count1 += c;
                        }
                    }
                    string date1 = d.ToString(count);
                    string date2 = d.ToString(count1);
                    Numero = numPre.Replace(count, date1);
                    Numero = Numero.Replace(count1, date2);
                    while (db.DEVIS_FOURNISSEURS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
                    {
                        PRF = PrefixeTable.Prefixe;
                        numPre = PRF.Replace("0000", Max.ToString("0000"));
                        count = "";
                        count1 = "";
                        foreach (char c in numPre)
                        {
                            if (c == 'y')
                            {
                                count += c;
                            }
                        }
                        foreach (char c in numPre)
                        {
                            if (c == 'm')
                            {
                                count1 += c;
                            }
                        }
                        date1 = d.ToString(count);
                        date2 = d.ToString(count1);
                        Numero = numPre.Replace(count, date1);
                        Numero = Numero.Replace(count1, date2);
                    }

                }
                devisFrs.CODE = Numero;
                devisFrs.DATE = d;
                devisFrs.Societes = idste;
                devisFrs.FOURNISSEUR = db.FOURNISSEURS.Where(f => f.ID == prix_Achat.Fournisseur).FirstOrDefault().ID;

				devisFrs.REMISE = 0;
				devisFrs.TTVA = 0;
				devisFrs.TNET = (decimal)prix_Achat.QUANTITE * (decimal)prix_Achat.PU_HT_Sans_Remise;
				devisFrs.TTC = devisFrs.TNET;
				devisFrs.THT = devisFrs.TTC;
				devisFrs.NHT = devisFrs.THT;
				


				db.DEVIS_FOURNISSEURS.Add(devisFrs);
                db.SaveChanges();
                LIGNES_DEVIS_FOURNISSEURS ligne = new LIGNES_DEVIS_FOURNISSEURS();
                ligne.DESIGNATION_PRODUIT = prix_Achat.Designation;
                ligne.REMISE = prix_Achat.Remise;
                ligne.TVA =int.Parse(prix_Achat.Valeur_TVA);
                ligne.Libelle_Prd = prix_Achat.Libelle;
                ligne.Marque = prix_Achat.Marque;
                ligne.Devise = prix_Achat.Devise;
                ligne.Categorie = db.Categorie.Where(f => f.CentreID == prix_Achat.Categorie).FirstOrDefault().Libelle;
                ligne.Unite = ligne.Unite;
                ligne.Sous_Categorie = db.Sous_Categorie.Where(f => f.CatID == prix_Achat.Sous_Categorie).FirstOrDefault().Libelle;
                ligne.QUANTITE = (decimal)prix_Achat.QUANTITE;
                ligne.PRIX_UNITAIRE_HT = (decimal)prix_Achat.PU_HT_Sans_Remise;
                ligne.DEVIS_CLIENT = devisFrs.ID;
                db.LIGNES_DEVIS_FOURNISSEURS.Add(ligne);
                db.SaveChanges();
                Détails_Articles details = new Détails_Articles();
                details.Fournisseur = devisFrs.FOURNISSEUR;
                details.Quantite = (decimal)prix_Achat.QUANTITE;
                details.Description = prix_Achat.Designation;
                details.IdPrixAchat = prix_Achat.Product_ID;
                db.Détails_Articles.Add(details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle", prix_Achat.Categorie);
            ViewBag.DEVIS_FRS = new SelectList(db.DEVIS_FOURNISSEURS, "ID", "Designation", prix_Achat.BLfRS);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "CODE", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            return View(prix_Achat);
        }

        // GET: Prix_Achat1/Edit/5
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
            ViewBag.DEVIS_FRS = new SelectList(db.DEVIS_FOURNISSEURS, "ID", "Designation", prix_Achat.BLfRS);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "CODE", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            ViewBag.stockAlert = prix_Achat.Stock_Alerte;
            ViewBag.id = prix_Achat.Product_ID;
            List<DepotRayon> depotart = new List<DepotRayon>();
            int i = 0;
            List<Art_Depot> artdepot = db.Art_Depot.Where(f => f.Id_Art == prix_Achat.Product_ID).ToList();
            if(artdepot!=null)
            { 
                foreach(Art_Depot art in artdepot)
                {
                    List<Art_Depot_Rayon> artdeppotrayon = db.Art_Depot_Rayon.Where(f => f.Id_Art_Depot == art.Id).ToList();
                    foreach(Art_Depot_Rayon artdep in artdeppotrayon)
                    { i++;
                        DepotRayon depray = new DepotRayon();
                        depray.ID = i;
                        depray.depot = (int)art.Id_Dépot;
                        depray.rayon = (int)artdep.Id_Rayons;
                        depray.QUANTITE = (decimal)artdep.Stock_Rayon;
                        depotart.Add(depray);
                    }

                }
               

            }
            Session["DepotRayon"] = depotart;
            return View(prix_Achat);
        }

        // POST: Prix_Achat1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Designation,Marque,Categorie,Sous_Categorie,Unite,PU_HT_Sans_Remise,Remise,PU_HT_Avec_Remise,Devise,Valeur_TVA,PU_TTC,Fournisseur,Date_offre_de_prix,Duree_de_validite,N_Offre_de_Prix,QUANTITE,QUANTITE_REPTURE_STOCK,Libelle,DEVIS_FRS,Stock")] Prix_Achat prix_Achat)
        {
            string stockalert = Request["stockalert"] != null ? Request["stockalert"].ToString() : string.Empty;

            prix_Achat.Stock_Alerte = double.Parse(stockalert);
            if (ModelState.IsValid)
            {
                Prix_Achat prixAchat1 = db.Prix_Achat.Where(f => f.Product_ID == prix_Achat.Product_ID).FirstOrDefault();
                prixAchat1.Stock = prix_Achat.Stock;
                //db.Entry(prix_Achat).State = EntityState.Modified;
                db.Prix_Achat.Add(prixAchat1);
                db.SaveChanges();
                List<DepotRayon> DepotRayon = new List<DepotRayon>();
               
                List<Art_Depot> artdepot = db.Art_Depot.Where(f => f.Id_Art == prix_Achat.Product_ID).ToList();
                if (artdepot != null)
                {
                    foreach (Art_Depot art in artdepot)
                    {
                        List<Art_Depot_Rayon> artdeppotrayon = db.Art_Depot_Rayon.Where(f => f.Id_Art_Depot == art.Id).ToList();
                        foreach (Art_Depot_Rayon artdep in artdeppotrayon)
                        {
                            db.Art_Depot_Rayon.Remove(artdep);
                        }
                        db.Art_Depot.Remove(art);
                    }


                }
                if (Session["DepotRayon"] != null)
                {
                    DepotRayon = (List<DepotRayon>)Session["DepotRayon"];
                }
                foreach (DepotRayon Ligne in DepotRayon)
                {

                    List<DepotRayon> DepotRayon1 = DepotRayon.Where(f => f.depot == Ligne.depot).ToList();
                    if (DepotRayon1 != null)
                    {

                        decimal qte1 = 0;
                        foreach (DepotRayon rayon in DepotRayon1)
                        {
                            qte1 = qte1 + rayon.QUANTITE;
                        }
                        Art_Depot artdepot1 = new Art_Depot();
                        artdepot1.Id_Dépot = Ligne.depot;
                        artdepot1.Id_Art = prix_Achat.Product_ID;
                        artdepot1.Stock_Art_Dépot = qte1;
                        db.Art_Depot.Add(artdepot1);
                        db.SaveChanges();
                        foreach (DepotRayon rayon in DepotRayon1)
                        {
                            Art_Depot_Rayon artdepotrayon11 = new Art_Depot_Rayon();
                            artdepotrayon11.Id_Art_Depot = artdepot1.Id;
                            artdepotrayon11.Id_Rayons = rayon.rayon;
                            artdepotrayon11.Stock_Rayon = rayon.QUANTITE;
                            db.Art_Depot_Rayon.Add(artdepotrayon11);
                            db.SaveChanges();
                        }
                    }
                  
                }
                Session["DepotRayon"] = null;
                return RedirectToAction("Index");

            }
            ViewBag.Categorie = new SelectList(db.Categorie, "CentreID", "Libelle", prix_Achat.Categorie);
            ViewBag.DEVIS_FRS = new SelectList(db.DEVIS_FOURNISSEURS, "ID", "Designation", prix_Achat.BLfRS);
            ViewBag.Devise = new SelectList(db.Devise, "Nom_Devise", "Nom_Devise", prix_Achat.Devise);
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "CODE", prix_Achat.Fournisseur);
            ViewBag.Marque = new SelectList(db.Marque, "Nom_marque", "Nom_marque", prix_Achat.Marque);
            ViewBag.Sous_Categorie = new SelectList(db.Sous_Categorie, "CatID", "Libelle", prix_Achat.Sous_Categorie);
            ViewBag.Valeur_TVA = new SelectList(db.TVA, "Valeur_TVA", "Valeur_TVA", prix_Achat.Valeur_TVA);
            ViewBag.Unite = new SelectList(db.Unite, "Valeur_Unite", "Valeur_Unite", prix_Achat.Unite);
            
                return View(prix_Achat);
        }
        public string EditStock(string id,string stock)
        {
            int idd = int.Parse(id);
            double stock1 = double.Parse(stock);
            Prix_Achat prixAchat1 = db.Prix_Achat.Where(f => f.Product_ID == idd).FirstOrDefault();
            prixAchat1.Stock = stock1;
            db.SaveChanges();
            return string.Empty;
        }
            // GET: Prix_Achat1/Delete/5
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

        // POST: Prix_Achat1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prix_Achat prix_Achat = db.Prix_Achat.Find(id);
            db.Prix_Achat.Remove(prix_Achat);
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
        public string AddLinedépot(string stock, string depot, string rayon, string qte)
        {
            List<DepotRayon> ListeDesPoduits = new List<DepotRayon>();
            DepotRayon ligne = new DepotRayon();
            if (Session["DepotRayon"] != null)
            {
                ListeDesPoduits = (List<DepotRayon>)Session["DepotRayon"];
                ligne.ID = ListeDesPoduits.Count() + 1;

            }
            else
            {
                ligne.ID = 1;
            }
            ligne.depot = int.Parse(depot);
            ligne.rayon = int.Parse(rayon);
            ligne.QUANTITE = decimal.Parse(qte, CultureInfo.InvariantCulture);
            ListeDesPoduits.Add(ligne);
            decimal qteStock = 0;
            decimal stock1 = decimal.Parse(stock);
            foreach(DepotRayon rayondep in ListeDesPoduits)
            {
                qteStock += rayondep.QUANTITE;
            }
            if(qteStock> stock1)
            {
                ListeDesPoduits.Remove(ligne);
                return "NO";
            }
            else
            { 
            Session["DepotRayon"] = ListeDesPoduits;
            return string.Empty;
            }
        }
        public JsonResult GetDepotRayon(string depot,string rayon)
        {
            int iddepot = int.Parse(depot);
            int idrayon = int.Parse(rayon);
            string libdepo = db.Dépot.Where(f => f.Id == iddepot).FirstOrDefault().Dépot1;
            string librayon = db.Rayons.Where(f => f.Id == idrayon).FirstOrDefault().Rayon;
            dynamic Result = new
            {
                libdepo = libdepo,
                librayon = librayon
            };
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
		public JsonResult GetAllFournisseur()
		{
			int idste = (int)Session["SoclogoId"];
			db.Configuration.ProxyCreationEnabled = false;
			List<FOURNISSEURS> Listefournisseur = db.FOURNISSEURS.Where(f => f.Id_Ste == idste).ToList();
			return Json(Listefournisseur, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllDepotRayon()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<DepotRayon> DepotRayon = (List<DepotRayon>)Session["DepotRayon"];
            return Json(DepotRayon, JsonRequestBehavior.AllowGet);
        }
        public string DeleteLineDepotRayon(string parampassed)
        {
            List<DepotRayon> DepotRayon = new List<DepotRayon>();
            if (Session["DepotRayon"] != null)
            {
                DepotRayon = (List<DepotRayon>)Session["DepotRayon"];
            }
            int ID = int.Parse(parampassed);
            DepotRayon ligne = DepotRayon.Where(pr => pr.ID == ID).FirstOrDefault();
            DepotRayon.Remove(ligne);
            return string.Empty;
        }
    }
}
