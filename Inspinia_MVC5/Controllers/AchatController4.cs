using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Inspinia_MVC5.Controllers
{
    public class AchatController : Controller
    {
        // GET: Achat
        //
        // GET: /Achat/
        private Tr db = new Tr();
        #region Views
        public ActionResult Fournisseurs()
        {
           
            int count = db.FOURNISSEURS.Select(cmd => cmd.ID).Count();
            ViewBag.Numero = "F" + count;
            FOURNISSEURS frnd = new FOURNISSEURS();
            return PartialView("Fournisseurs", frnd);
        }

        public ActionResult DevisPrdate(string date1, string date2)
        {
            DateTime d1 = DateTime.Parse(date1);
            DateTime d2 = DateTime.Parse(date2);
            List<DEVIS_FOURNISSEURS> Liste = db.DEVIS_FOURNISSEURS.Where(fou => fou.DATE > d1 && fou.DATE < d2).ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  cc = db.COMMANDES_FOURNISSEURS.Where(fou => fou.Devis_Frs == com.ID).FirstOrDefault(),


                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;

            return View(Result);
        }
        public ActionResult Devis(string Mode)
        {
            List<DEVIS_FOURNISSEURS> Liste = db.DEVIS_FOURNISSEURS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  cc = db.COMMANDES_FOURNISSEURS.Where(fou => fou.Devis_Frs == com.ID).FirstOrDefault(),


                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult Commandes(string Mode)
        {
            List<COMMANDES_FOURNISSEURS> Liste = db.COMMANDES_FOURNISSEURS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  cc = db.BONS_RECEPTIONS_FOURNISSEURS.Where(fou => fou.COMMANDE_FOURNISSEUR == com.ID).FirstOrDefault(),

                                  //TIERS = db.Tiers.Where(fou=>fou.frs==com.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult BonReception(string Mode)
        {
            List<BONS_RECEPTIONS_FOURNISSEURS> Liste = db.BONS_RECEPTIONS_FOURNISSEURS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  VALIDE = com.VALIDER,
                                  cc = db.FACTURES_FOURNISSEURS.Where(fou => fou.BON_RECEPTION_FOURNISSEUR == com.ID).FirstOrDefault(),

                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  Etat=com.Etat,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == com.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult BonReceptionvalide(string Mode)
        {
            List<BONS_RECEPTIONS_FOURNISSEURS> Liste = db.BONS_RECEPTIONS_FOURNISSEURS.Where(fou => fou.VALIDER == true).ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  VALIDE = com.VALIDER,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  Etat = com.Etat,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == com.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult BonReceptionNonvalide(string Mode)
        {
            List<BONS_RECEPTIONS_FOURNISSEURS> Liste = db.BONS_RECEPTIONS_FOURNISSEURS.Where(fou => fou.VALIDER == false).ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == com.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  VALIDE = com.VALIDER,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TIERS = com.Tiers,
                                  Etat = com.Etat,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == com.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult FactureByFrs(string Mode, string id)
        {
            int p = int.Parse(id);
            string frs = db.FOURNISSEURS.Where(f => f.ID == p).FirstOrDefault().NOM;
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.Where(fou => fou.FOURNISSEUR == p).ToList();
            decimal totale = 0;
            decimal totalePayee = 0;
            decimal totaleNonPayee = 0;

            foreach (FACTURES_FOURNISSEURS fact in Liste)
            {
                totale += fact.TTC;
                if (fact.PAYEE == true)
                {
                    totalePayee += fact.TTC;
                }
                else
                {
                    totaleNonPayee += fact.TTC;
                }
            }
            String pourcentage = ((totalePayee / totale) * 100).ToString(".###");
            String pourcentage2 = ((totaleNonPayee / totale) * 100).ToString(".###");
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            ViewBag.Totale = totale;
            ViewBag.totalePayee = totalePayee;
            ViewBag.totaleNonPayee = totaleNonPayee;
            ViewBag.Pourcentage = pourcentage;
            ViewBag.pourcentage2 = pourcentage2;
            ViewBag.NOM = frs;

            return View(Result);
        }

        public ActionResult FactureValide(string Mode)
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.Where(fou => fou.VALIDER == true).ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  NUMFact = Fact.Num_Fact,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult FactureNonValide(string Mode)
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.Where(fou => fou.VALIDER == false).ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  NUMFact = Fact.Num_Fact,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult FacturePayee(string Mode)
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.Where(fou => fou.PAYEE == true).ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  NUMFact = Fact.Num_Fact,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult FactureNonPayee(string Mode)
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.Where(fou => fou.PAYEE == false).ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  NUMFact = Fact.Num_Fact,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }

        public ActionResult Facture(string Mode)
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  NUMFact = Fact.Num_Fact,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = Fact.Tiers,
                                  //TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult Avoir(string Mode)
        {
            List<AVOIRS_FOURNISSEURS> Liste = db.AVOIRS_FOURNISSEURS.ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  FOURNISSEUR = db.FOURNISSEURS.Where(fou => fou.ID == Fact.FOURNISSEUR).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TIERS = db.Tiers.Where(fou => fou.frs == Fact.FOURNISSEUR).FirstOrDefault().NOM,


                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        #endregion
        #region Forms
        public ActionResult FormDevis(string Mode, string Code)

        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            DEVIS_FOURNISSEURS DevisClient = new DEVIS_FOURNISSEURS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                //int Max = 0;
                //if (db.DEVIS_FOURNISSEURS.ToList().Count != 0)
                //{
                //    Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                //}
                //Max++;
                Numero = "DVF" /*+ Max.ToString("0000") + "/" + DateTime.Today.ToString("yy")*/;
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                DevisClient = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = DevisClient.CODE;
                List<LIGNES_DEVIS_FOURNISSEURS> ListeLigne = db.LIGNES_DEVIS_FOURNISSEURS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
            foreach (LIGNES_DEVIS_FOURNISSEURS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    if (ListeDesPoduits.Count()!= 0)
                    {

                        NewLine.ID = ListeDesPoduits.Count() + 1;

                    }
                    else
                    {
                        NewLine.ID = 1;
                    }
                    //NewLine.ID = (int)ligne.Prix_achat;
                    //NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Designation;
                    NewLine.LIBELLE = ligne.Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE =ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    //NewLine.STOCK = (int)ligne.STOCK;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (decimal)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA =(int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                    
                }
               
                ViewBag.CODE_CLIENT = DevisClient.FOURNISSEURS.CODE;
                ViewBag.CODESOC = DevisClient.Societes;
            }
            Session["ProduitsDevisFournisseur"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(DevisClient);
        }
        public ActionResult FormCommande(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            COMMANDES_FOURNISSEURS CommandeFournisseur = new COMMANDES_FOURNISSEURS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                //int Max = 0;
                //if (db.COMMANDES_FOURNISSEURS.ToList().Count != 0)
                //{
                //    Max = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                //}
                //Max++;
                Numero = "CDF" /*+ Max.ToString("0000") + "/" + DateTime.Today.ToString("yy")*/;
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                CommandeFournisseur = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = CommandeFournisseur.CODE;
                List<LIGNES_COMMANDES_FOURNISSEURS> ListeLigne = db.LIGNES_COMMANDES_FOURNISSEURS.Where(lcmd => lcmd.COMMANDE_FOURNISSEUR == ID).ToList();
                foreach (LIGNES_COMMANDES_FOURNISSEURS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    if (ListeDesPoduits.Count() != 0)
                    {

                        NewLine.ID = ListeDesPoduits.Count() + 1;

                    }
                    else
                    {
                        NewLine.ID = 1;
                    }
                    //NewLine.ID = (int)ligne.Prix_achat;
                    //NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Designation;
                    NewLine.LIBELLE = ligne.Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_FOURNISSEUR = CommandeFournisseur.FOURNISSEURS.CODE;
                ViewBag.CODESOC = CommandeFournisseur.Societes;
            }
            Session["ProduitsCommandeFournisseur"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(CommandeFournisseur);
        }
        public ActionResult FormBonReception(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            BONS_RECEPTIONS_FOURNISSEURS BonReceptionFournisseur = new BONS_RECEPTIONS_FOURNISSEURS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                //int Max = 0;
                //if (db.BONS_RECEPTIONS_FOURNISSEURS.ToList().Count != 0)
                //{
                //    Max = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                //}
                //Max++;
                Numero = "BRF" /*+ Max.ToString("0000") + "/" + DateTime.Today.ToString("yy")*/;
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                BonReceptionFournisseur = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = BonReceptionFournisseur.CODE;
                List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> ListeLigne = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(lcmd => lcmd.BON_RECEPTION_FOURNISSEUR == ID).ToList();
                foreach (LIGNES_BONS_RECEPTIONS_FOURNISSEURS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    if (ListeDesPoduits.Count() != 0)
                    {

                        NewLine.ID = ListeDesPoduits.Count() + 1;

                    }
                    else
                    {
                        NewLine.ID = 1;
                    }
                    //NewLine.ID = (int)ligne.Prix_achat;
                    //NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Designation;
                    NewLine.STOCK = (decimal)ligne.Stock;
                    NewLine.LIBELLE = ligne.Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_FOURNISSEUR = BonReceptionFournisseur.FOURNISSEURS.CODE;
                ViewBag.CODESOC = BonReceptionFournisseur.Societes;
            }
            Session["ProduitsBonCommandeFournisseur"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(BonReceptionFournisseur);
        }
        public ActionResult FormFacture(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            FACTURES_FOURNISSEURS FactureFournisseur = new FACTURES_FOURNISSEURS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                //int Max = 0;
                //if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
                //{
                //    Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                //}
                //Max++;
                Numero = "FF" /*+ Max.ToString("0000") + "/" + DateTime.Today.ToString("yy")*/;
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                FactureFournisseur = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = FactureFournisseur.CODE;
                List<LIGNES_FACTURES_FOURNISSEURS> ListeLigne = db.LIGNES_FACTURES_FOURNISSEURS.Where(lcmd => lcmd.FACTURE_FOURNISSEUR == ID).ToList();
                foreach (LIGNES_FACTURES_FOURNISSEURS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();

                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Libelle;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    if(ligne.ID_Bl!=null)
                    { 
                    NewLine.ID_Bl = (int)ligne.ID_Bl;
                    }
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_FOURNISSEUR = FactureFournisseur.FOURNISSEURS.CODE;
                ViewBag.CODESOC = FactureFournisseur.Societes;
            }
            Session["ProduitsFactureFournisseur"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(FactureFournisseur);
        }
        public ActionResult FormAvoir(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            AVOIRS_FOURNISSEURS AvoirFournisseur = new AVOIRS_FOURNISSEURS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                //int Max = 0;
                //if (db.AVOIRS_FOURNISSEURS.ToList().Count != 0)
                //{
                //    Max = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                //}
                //Max++;
                Numero = "AVF" /*+ Max.ToString("0000") + "/" + DateTime.Today.ToString("yy")*/;
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                AvoirFournisseur = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = AvoirFournisseur.CODE;
                List<LIGNES_AVOIRS_FOURNISSEURS> ListeLigne = db.LIGNES_AVOIRS_FOURNISSEURS.Where(lcmd => lcmd.AVOIR_FOURNISSEUR == ID).ToList();
                foreach (LIGNES_AVOIRS_FOURNISSEURS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Designation;
                    NewLine.STOCK = (int)db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Stock;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_FOURNISSEUR = AvoirFournisseur.FOURNISSEURS.CODE;
                ViewBag.CODESOC = AvoirFournisseur.Societes;

            }
            Session["ProduitsAvoirFournisseur"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(AvoirFournisseur);
        }
        #endregion
        #region common functions
        public ActionResult getlibelleproduit(string term)
        {
            return Json(db.LIGNES_DEVIS_FOURNISSEURS.Where(c => c.Libelle_Prd.StartsWith(term)).Select(a => new { label = a.Libelle_Prd, id = a.ID }), JsonRequestBehavior.AllowGet);
            //return Json(db.Countries.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getdescriptionProduit(string term)
        {
            return Json(db.LIGNES_DEVIS_FOURNISSEURS.Where(c => c.DESIGNATION_PRODUIT.StartsWith(term)).Select(a => new { label = a.DESIGNATION_PRODUIT, id = a.ID }), JsonRequestBehavior.AllowGet);
            //return Json(db.Countries.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getarticles(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.LIGNES_DEVIS_FOURNISSEURS select new SelectListItem { Text = u.Libelle_Prd, Value = u.ID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text.Trim().ToLower()));
            }

            var namecodes1 = result.Where(s => s.Value.Trim().ToLower().Contains
                        (term.Trim().ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMarque(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.Marque select new SelectListItem { Text = u.Nom_marque, Value = u.ID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }

            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                        (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDevise(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.Devise select new SelectListItem { Text = u.Nom_Devise, Value = u.ID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }

            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                        (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnite(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.Unite select new SelectListItem { Text = u.Valeur_Unite, Value = u.ID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }

            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                        (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategorie(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.Categorie select new SelectListItem { Text = u.Libelle, Value = u.CentreID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }

            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                        (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTva(string term)
        {
            // var codes = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.Libelle_Prd.StartsWith(term)).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in db.TVA select new SelectListItem { Text = u.Valeur_TVA, Value = u.ID.ToString() }).ToList();

            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }

            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                        (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Getsscat(int id, string term)
        {
            List<Sous_Categorie> list = db.Sous_Categorie.Where(i => i.CentreID == id).ToList();
            var result = new List<KeyValuePair<string, string>>();
            var namecodes = new List<SelectListItem>();
            namecodes = (from u in list select new SelectListItem { Text = u.Libelle, Value = u.CatID.ToString() }).ToList();
            foreach (var item in namecodes)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }
            var namecodes1 = result.Where(s => s.Value.ToLower().Contains
                         (term.ToLower())).Select(w => w).ToList();
            return Json(namecodes1, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetItemDetails(int id)
        {
            var codeList = db.LIGNES_DEVIS_FOURNISSEURS.Where(i => i.ID == id).ToList();

            var viewmodel = codeList.Select(x => new
            {
                Id = x.ID,
                descriptionProduit = x.DESIGNATION_PRODUIT,
                marque = x.Marque,
                unite = x.Unite,
                devise = x.Devise,
                categorie = x.Categorie,
                sous_categorie = x.Sous_Categorie,
                QuantiteProduit = x.QUANTITE,
                PUHTProduit = x.PRIX_UNITAIRE_HT,
                RemiseProduit = x.REMISE,
                PTHTProduit = x.TOTALE_HT,
                TVAProduit = x.TVA,
                TTCProduit = x.TOTALE_TTC,



            });

            return Json(viewmodel);
        }

        public JsonResult GetAllLineDevis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
           
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Prix_Achat> ListeProduit = db.Prix_Achat.ToList();
            return Json(ListeProduit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllFournisseur()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<FOURNISSEURS> Listefournisseur = db.FOURNISSEURS.ToList();
            return Json(Listefournisseur, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllTVA()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TVA> ListeTVA = db.TVA.ToList();
            return Json(ListeTVA, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMarque()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Marque> Listemarque = db.Marque.ToList();
            return Json(Listemarque, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllUnite()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Unite> Listeunite = db.Unite.ToList();
            return Json(Listeunite, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllDevise()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Devise> Listedevise = db.Devise.ToList();
            return Json(Listedevise, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCategorie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Categorie> Listecategorie = db.Categorie.ToList();
            return Json(Listecategorie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSousCategorie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Sous_Categorie> Listecategorie = db.Sous_Categorie.ToList();
            return Json(Listecategorie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductByID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            Prix_Achat produit = db.Prix_Achat.Where(pr => pr.Product_ID == id).FirstOrDefault();
            return Json(produit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFournisseuryID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            FOURNISSEURS fournisseur = db.FOURNISSEURS.Where(pr => pr.ID == id).FirstOrDefault();
            return Json(fournisseur, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNumeroDevis(string DATE, string Mode, string num)
        {
            DateTime d = DateTime.Parse(DATE);
            string[] code2 = DATE.Split('/');
            string d2;
            int Nb = 0;

            if (code2[1]== "10")
            {
                int yy = int.Parse(code2[2]) + 1;
                int mm = 1;
                d2 = d.Day.ToString() + "/" + mm.ToString() + "/" + yy.ToString();

            }
            else if (code2[1] == "11")
            {
                int yy = int.Parse(code2[2]) + 1;
                int mm = 2;
                 d2 = d.Day.ToString() + "/" + mm.ToString() + "/" + yy.ToString();

            }
            else if(code2[1] == "12")
            {
                int yy = int.Parse(code2[2]) + 1;
                int mm = 3;
                 d2 = d.Day.ToString() + "/" + mm.ToString() + "/" + yy.ToString();

            }
            else
            { 
            int mm = int.Parse(code2[1]) + 3;
             d2 = d.Day.ToString() + "/" + mm.ToString() + "/" + d.Year.ToString();
            }
            //DateTime date2 = DateTime.Parse(d2);
            string Numero1;
            db.Configuration.ProxyCreationEnabled = false;
            if (Mode == "Edit")
            {
                string[] code = num.Split('/');
                int y = int.Parse(code[1]);
                string an = d.Year.ToString();
                string[] an1 = an.Split('0');
                int an2 = int.Parse(an1[1]);
               if(an2 == y)
                {
                   Numero1 = num;
                }
                else
                {
                    int Max = 0;
                    if (db.DEVIS_FOURNISSEURS.ToList().Count != 0)
                    {
                        Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                    }
                    Max++;
                    Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                    List<DEVIS_FOURNISSEURS> frs = db.DEVIS_FOURNISSEURS.ToList();
                    foreach (DEVIS_FOURNISSEURS f in frs)
                    {
                        string[] con = f.CODE.Split('F');
                        string[] con11 = con[1].Split('/');
                        int con1 = int.Parse(con11[0]);
                        if (con1 == Max)
                        {
                            Nb++;
                        }

                    }
                    if (Nb > 0)
                    {
                        Max++;

                        Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                    }
                    else
                    {
                        Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");

                    }

                }
            
            }
            else
            {
                int Max = 0;
                if (db.DEVIS_FOURNISSEURS.ToList().Count != 0)
                {
                    Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                List<DEVIS_FOURNISSEURS> frs = db.DEVIS_FOURNISSEURS.ToList();
                foreach (DEVIS_FOURNISSEURS f in frs)
                {
                    string[] con = f.CODE.Split('F');
                    string[] con11 = con[1].Split('/');
                    int con1 = int.Parse(con11[0]);
                    if (con1 == Max)
                    {
                        Nb++;
                    }

                }
                if (Nb > 0)
                {
                    Max++;

                    Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                }
                else
                {
                    Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");

                }
            }
            dynamic Result = new
            {
                Date = d2,
                Numero1 = Numero1
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetNumeroDevis(string DATE)
        //{
        //    DateTime d = DateTime.Parse(DATE);
        //    string Numero1;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    int Max = 0;
        //    if (db.DEVIS_FOURNISSEURS.ToList().Count != 0)
        //    {
        //        Max = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
        //    }
        //    Max++;
        //    Numero1 = "DVF" + Max.ToString("0000") + "/" + d.ToString("yy");
        //    return Json(Numero1, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetNumeroBr(string DATE)
        {
            DateTime d = DateTime.Parse(DATE);
            string Numero1;
            db.Configuration.ProxyCreationEnabled = false;
            int Max = 0;
            if (db.BONS_RECEPTIONS_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero1 = "BRF" + Max.ToString("0000") + "/" + d.ToString("yy");
            return Json(Numero1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNumeroCmd(string DATE)
        {
            DateTime d = DateTime.Parse(DATE);
            string Numero1;
            db.Configuration.ProxyCreationEnabled = false;
            int Max = 0;
            if (db.COMMANDES_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero1 = "CDF" + Max.ToString("0000") + "/" + d.ToString("yy");
            return Json(Numero1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNumeroFac(string DATE)
        {
            DateTime d = DateTime.Parse(DATE);
            string Numero1;
            db.Configuration.ProxyCreationEnabled = false;
            int Max = 0;
            if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero1 = "FF" + Max.ToString("0000") + "/" + d.ToString("yy");
            return Json(Numero1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNumeroAv(string DATE)
        {
            DateTime d = DateTime.Parse(DATE);
            string Numero1;
            db.Configuration.ProxyCreationEnabled = false;
            int Max = 0;
            if (db.AVOIRS_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero1 = "AVF" + Max.ToString("0000") + "/" + d.ToString("yy");
            return Json(Numero1, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetAllSocietes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Societes> ListeSocietes = db.Societes.ToList();
            return Json(ListeSocietes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTiersBySocietesId(int id)
        {
            List<Tiers> s = new List<Tiers>();
            if (id > 0)
            {
                s = db.Tiers.Where(p => p.frs == id).ToList();

            }

            var result = (from r in s
                          select new
                          {
                              id = r.TiersID,
                              name = r.NOM,
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSocByID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            Societes societe = db.Societes.Where(pr => pr.SociID == id).FirstOrDefault();
            return Json(societe, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Delete
        public string DeleteDevis(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_DEVIS_FOURNISSEURS.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DEVIS_FOURNISSEURS.Remove(p));
            db.SaveChanges();
            DEVIS_FOURNISSEURS Commande = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.DEVIS_FOURNISSEURS.Remove(Commande);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteCommande(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_COMMANDES_FOURNISSEURS.Where(p => p.COMMANDE_FOURNISSEUR == ID).ToList().ForEach(p => db.LIGNES_COMMANDES_FOURNISSEURS.Remove(p));
            db.SaveChanges();
            COMMANDES_FOURNISSEURS Commande = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.COMMANDES_FOURNISSEURS.Remove(Commande);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteBonReception(string parampassed)
        {
            int ID = int.Parse(parampassed);
            BONS_RECEPTIONS_FOURNISSEURS BonReception = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (BonReception.VALIDER == true)
            {
                List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> list = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(p => p.BON_RECEPTION_FOURNISSEUR == ID).ToList();
                List <Prix_Achat> list2 = db.Prix_Achat.ToList();
                foreach(LIGNES_BONS_RECEPTIONS_FOURNISSEURS ligne1 in list)
                {
                    foreach(Prix_Achat pa in list2)
                    {
                        if(ligne1.Libelle_Prd==pa.Libelle)
                        {
                            pa.QUANTITE = pa.QUANTITE - ligne1.QUANTITE;
                        }
                    }

                }
                db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(p => p.BON_RECEPTION_FOURNISSEUR == ID).ToList().ForEach(p => db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                db.BONS_RECEPTIONS_FOURNISSEURS.Remove(BonReception);
                db.SaveChanges();
            }
            else
            {
                db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(p => p.BON_RECEPTION_FOURNISSEUR == ID).ToList().ForEach(p => db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                //BONS_RECEPTIONS_FOURNISSEURS BonReception = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                db.BONS_RECEPTIONS_FOURNISSEURS.Remove(BonReception);
                db.SaveChanges();
            }
            return string.Empty;
        }
        public string DeleteFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.FACTURE_FOURNISSEUR == ID).ToList().ForEach(p => db.LIGNES_FACTURES_FOURNISSEURS.Remove(p));
            db.SaveChanges();
            FACTURES_FOURNISSEURS Facture = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.FACTURES_FOURNISSEURS.Remove(Facture);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteAvoir(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_AVOIRS_FOURNISSEURS.Where(p => p.AVOIR_FOURNISSEUR == ID).ToList().ForEach(p => db.LIGNES_AVOIRS_FOURNISSEURS.Remove(p));
            db.SaveChanges();
            AVOIRS_FOURNISSEURS Facture = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.AVOIRS_FOURNISSEURS.Remove(Facture);
            db.SaveChanges();
            return string.Empty;
        }
        #endregion
        #region Print
        public ActionResult PrintRetenue(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            FACTURES_FOURNISSEURS UnDevis = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            string matfisc2 = UnDevis.FOURNISSEURS.ID_FISCAL;
            string matfisc = matfisc2.Replace("/", "");
            string id_fisc = "";
            int i = 0;
            while ((matfisc[i] == ('0')) || (matfisc[i] == ('1')) || (matfisc[i] == ('2')) || (matfisc[i] == ('3')) || (matfisc[i] == ('4')) || (matfisc[i] == ('5')) || (matfisc[i] == ('6')) || (matfisc[i] == ('7')) || (matfisc[i] == ('8')) || (matfisc[i] == ('9')))
            {

                id_fisc += matfisc[i];
                i++;
            }

            int len = id_fisc.Length;
            int len2 = matfisc.Length;
            int len3 = len2 - len;
            string s2 = matfisc.Substring(len, len3);
            string s3 = s2.Substring(0, 1);
            id_fisc += s3;
            string s4 = s2.Substring(1, 1);
            string s5 = s2.Substring(2, 1);
            int len4 = (id_fisc.Length) + 2;
            int len5 = len2 - len4;
            string s6 = s2.Substring(3, len5);
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UnDevis.Num_Fact,
                             DATE = UnDevis.DATE.ToShortDateString(),
                             NOM = UnDevis.FOURNISSEURS.NOM,
                             ADRESSE = UnDevis.FOURNISSEURS.ADRESSE,
                             ID_FISCAL = id_fisc,
                             AI = s4,
                             NIS = s5,
                             RC = s6,
                             TNET = UnDevis.TNET ?? 0,
                             THT = UnDevis.Redevance ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/Retenue.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Retenue Fournisseur";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult InvoicePrint(string CODE)
        {
            int ID = int.Parse(CODE);
            DEVIS_FOURNISSEURS DVF = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_DEVIS_FOURNISSEURS> lIGNES_DEVIS_FOURNISSEURS = db.LIGNES_DEVIS_FOURNISSEURS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();
            ViewBag.NOM = DVF.FOURNISSEURS.NOM;
            ViewBag.ADRESSE = DVF.FOURNISSEURS.ADRESSE;
            ViewBag.TEL = DVF.FOURNISSEURS.TELEPHONE;
            ViewBag.FAX = DVF.FOURNISSEURS.FAX;
            ViewBag.DATE = DVF.DATE;
            ViewBag.DATE2 = DateTime.Now;
            ViewBag.TTVA = DVF.TTVA;
            ViewBag.TTTC = DVF.TTC;
            ViewBag.TTNET = DVF.NHT;
            ViewBag.Code = DVF.CODE;
            return View(lIGNES_DEVIS_FOURNISSEURS);



        }
        public ActionResult PrintCommandeClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            COMMANDES_FOURNISSEURS UneCommande = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_COMMANDES_FOURNISSEURS> Liste = db.LIGNES_COMMANDES_FOURNISSEURS.Where(lcmd => lcmd.COMMANDE_FOURNISSEUR == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UneCommande.CODE,
                             DATE = UneCommande.DATE.ToShortDateString(),
                             MODE_PAIEMENT = UneCommande.MODE_PAIEMENT,
                             Expr3 = UneCommande.FOURNISSEURS.NOM,
                             CODE_ACCES = UneCommande.FOURNISSEURS.CODE,
                             //Expr2 = db.Tiers.Where(Fd => Fd.TiersID == UneCommande.Tiers).FirstOrDefault().NOM,
                             //ADRESSE = db.Tiers.Where(Fd => Fd.TiersID == UneCommande.Tiers).FirstOrDefault().ADRESSE,
                             //FAX = db.Tiers.Where(Fd => Fd.TiersID == UneCommande.Tiers).FirstOrDefault().FAX,
                             //TEL = db.Tiers.Where(Fd => Fd.TiersID == UneCommande.Tiers).FirstOrDefault().TEL,
                             //NOM = db.Direction.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().Nom,
                             ADRESSE = UneCommande.FOURNISSEURS.ADRESSE,
                             FAX = UneCommande.FOURNISSEURS.FAX,
                             TEL = UneCommande.FOURNISSEURS.TELEPHONE,
                             NOM = db.Direction.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().Nom,
                             //CODE = UnDevis.CLIENTS.CODE,
                             //NOM = UnDevis.CLIENTS.NOM,
                             ////CODE_PRODUIT = cmd.CODE_PRODUIT,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             Marque = cmd.Marque,
                             Categorie = cmd.Categorie,
                             Sous_Categorie = cmd.Sous_Categorie,
                             Unite = cmd.Unite,
                             RC = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().ID_FISCAL

                             //MODE_PAIEMENT = UneCommande.MODE_PAIEMENT,
                             //THT = UneCommande.THT,
                             //TTVA = UneCommande.TTVA,
                             //TTC = UneCommande.TTC,
                             //NHT = UneCommande.NHT,
                             //TNET = UneCommande.TNET,
                             //REMISE = UneCommande.REMISE,
                             //CHIFFRE = convert.NumberToCurrencyText(UneCommande.TNET.ToString()),
                             //RC = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().RC,
                             //CTVA = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().ID_FISCALE
                         };

            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/CommandeFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commandes Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintBonReceptionClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            BONS_RECEPTIONS_FOURNISSEURS UneCommande = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> Liste = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(lcmd => lcmd.BON_RECEPTION_FOURNISSEUR == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UneCommande.CODE,
                             MODE_PAIEMENT = UneCommande.MODE_PAIEMENT,
                             DATE = UneCommande.DATE.ToShortDateString(),
                             //CODE = UnDevis.CLIENTS.CODE,
                             NOM = UneCommande.Societes1.NOM,
                             ADRESSE = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().ADRESSE,
                             TEL = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().TEL,
                             Expr3 = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().NOM,
                             CODE_ACCES = db.Societes.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().CODE_ACCES,
                             //Direction = UneCommande.Societes1.Direction,
                             Expr4 = db.Direction.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().Nom,
                             FAX = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().FAX,
                             //CODE_PRODUIT = cmd.CODE_PRODUIT,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             RC = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UneCommande.Societes).FirstOrDefault().ID_FISCAL
                             //CTVA = BD.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().ID_FISCALE
                         };

            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/BonReceptionFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Bons Reception Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintBonFactureByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            FACTURES_FOURNISSEURS UneFacture = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_FACTURES_FOURNISSEURS> Liste = db.LIGNES_FACTURES_FOURNISSEURS.Where(lcmd => lcmd.FACTURE_FOURNISSEUR == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UneFacture.CODE,
                             MODE_PAIEMENT = UneFacture.MODE_PAIEMENT,
                             DATE = UneFacture.DATE.ToShortDateString(),
                             //CODE= UnDevis.CLIENTS.CODE,
                             Expr6 = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().NOM,
                             ADRESSE = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().ADRESSE,
                             TEL = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().TEL,
                             FAX = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().FAX,
                             NOM = UneFacture.Societes1.NOM,
                             Direction = UneFacture.Societes1.Direction,

                             CODE_ACCES = db.Societes.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().CODE_ACCES,
                             Unite = cmd.Unite,
                             TIMBRE = UneFacture.TIMBRE ?? 0,
                             THT = UneFacture.THT ?? 0,
                             //ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
                             TTC = UneFacture.TTC,
                             TTVA = UneFacture.TTVA ?? 0,

                             //TIMBRE =UnDevis.TIMBRE,
                             // TTC = UnDevis.TTC ,
                             //TTVA=UnDevis.TTVA,
                             //TTVA=UnDevis.TTVA,
                             ID_Bl=cmd.ID_Bl,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
                             REMISE = cmd.REMISE ?? 0,
                             TOTALE_HT = cmd.TOTALE_HT ?? 0,
                             TVA = cmd.TVA ?? 0,
                             TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
                             RC = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UneFacture.Societes).FirstOrDefault().ID_FISCAL

                         };

            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/FactureFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Facture Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAvoirByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            AVOIRS_FOURNISSEURS UnAvoir = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_AVOIRS_FOURNISSEURS> Liste = db.LIGNES_AVOIRS_FOURNISSEURS.Where(lcmd => lcmd.AVOIR_FOURNISSEUR == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             Expr1 = UnAvoir.CODE,
                             DATE = UnAvoir.DATE.ToShortDateString(),
                             CODE = UnAvoir.FOURNISSEURS.CODE,
                             NOM = UnAvoir.FOURNISSEURS.NOM,
                             // CODE_PRODUIT = cmd.CODE_PRODUIT,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
                             Expr2 = cmd.REMISE ?? 0,
                             TOTALE_HT = cmd.TOTALE_HT ?? 0,
                             TVA = cmd.TVA ?? 0,
                             TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
                             ////MODE_PAIEMENT = UnAvoir.MODE_PAIEMENT,
                             ////THT = UnAvoir.THT,
                             ////TTVA = UnAvoir.TTVA,
                             ////TTC = UnAvoir.TTC,
                             ////NHT = UnAvoir.NHT,
                             ////TNET = UnAvoir.TNET,
                             ////REMISE = UnAvoir.REMISE,
                             ////CHIFFRE = convert.NumberToCurrencyText(UnAvoir.TNET.ToString()),
                             //RC = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().RC,
                             //CTVA = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().ID_FISCALE,
                         };

            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/AvoirFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Avoirs Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllDevisFournisseur()
        {
            List<DEVIS_FOURNISSEURS> Liste = db.DEVIS_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = cmd.Societes1.NOM,
                             //FOURNISSEUR = db.FOURNISSEURS.Where(fd => fd.ID == cmd.FOURNISSEUR).FirstOrDefault().NOM,
                             ////FOURNISSEUR = cmd.CLIENTS.NOM,
                             FOURNISSEUR=cmd.FOURNISSEURS.NOM,
                             //Societes=cmd.Societes,
                             //Tiers=cmd.Tiers,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             //QUANTITE=cmd.
                             DATE = cmd.DATE.ToShortDateString(),
                             //TTC=cmd.TTC,
                             //PAYEE = cmd.PAYEE,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             THT = cmd.THT ?? 0,
                             NHT = cmd.NHT ?? 0,
                             //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //T_TVA = cmd.TTVA,
                             //TTC = cmd.TTC,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeDevisFrsPardate.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Devis Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllDevisFournisseurParFrs()
        {
            List<DEVIS_FOURNISSEURS> Liste = db.DEVIS_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = cmd.Societes1.NOM,
                             //FOURNISSEUR = db.FOURNISSEURS.Where(fd => fd.ID == cmd.FOURNISSEUR).FirstOrDefault().NOM,
                             ////FOURNISSEUR = cmd.CLIENTS.NOM,
                             FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             //Societes=cmd.Societes,
                             //Tiers=cmd.Tiers,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             //QUANTITE=cmd.
                             DATE = cmd.DATE.ToShortDateString(),
                             //TTC=cmd.TTC,
                             //PAYEE = cmd.PAYEE,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             THT = cmd.THT ?? 0,
                             NHT = cmd.NHT ?? 0,
                             //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //T_TVA = cmd.TTVA,
                             //TTC = cmd.TTC,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeDevisFrsParfrs.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Devis Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllArticles()
        {
            List<Prix_Achat> Liste = db.Prix_Achat.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             Product_ID = cmd.Product_ID,
                             //Libelle = cmd.Libelle,
                             Marque = cmd.Marque,
                             //Sous_Categorie = cmd.Sous_Categorie,
                             Categorie = cmd.Categorie,
                             //Stock = cmd.Stock,
                             

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeArticleEnrepture.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Articles En Repture";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllCommandeFournisseur()
        {
            List<COMMANDES_FOURNISSEURS> Liste = db.COMMANDES_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             TTC = cmd.TTC
                         };

            ReportDocument rptH = new ReportDocument();

            string FileName = Server.MapPath("/Reports/CrystalReport1.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commandes Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");



        }
        public ActionResult PrintAllBonReceptionFournisseur()
        {
            List<BONS_RECEPTIONS_FOURNISSEURS> Liste = db.BONS_RECEPTIONS_FOURNISSEURS.ToList();
            List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> Liste1 = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.ToList();

            dynamic dt = from cmd in Liste1
                         select new
                         {
                             Prix_achat = cmd.Prix_achat,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             Marque = cmd.Marque,
                             //VALIDER = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             Categorie = cmd.Categorie,
                             Sous_Categorie = cmd.Sous_Categorie,
                             QUANTITE = cmd.QUANTITE,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeBonReceptionFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Bons Reception Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureFournisseur()
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             Societes = cmd.Societes,
                             Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             Expr4 = db.Societes.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             ////VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             ////PAYEE = cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             THT = cmd.THT ?? 0,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureFournisseurPardate()
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             //Societes = cmd.Societes,
                             Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             Expr4 = db.Societes.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             ////VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             THT = cmd.THT ?? 0,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureFournisseurPardate.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureFournisseurParetat()
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             //Societes = cmd.Societes,
                             Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             Expr4 = db.Societes.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             ////VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             THT = cmd.THT ?? 0,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureFournisseurParetat.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllAvoirFournisseur()
        {
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             FOURNISSEUR = cmd.FOURNISSEURS.NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             PAYEE = cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             NET_HT = cmd.NHT,
                             T_TVA = cmd.TTVA,
                             TTC = cmd.TTC,
                             NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeAvoirFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Avoirs Fournisseurs";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        #endregion
        #region specifique fonctions
        public string verifieValiditeBl(string id)
        {
            int ID = int.Parse(id);
            BONS_RECEPTIONS_FOURNISSEURS facture = new BONS_RECEPTIONS_FOURNISSEURS();
            facture = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (facture.VALIDER)
            {
                return facture.ID.ToString();
            }
            else
            {
                return "NO";
            }

        }
        public string devisencours(string id)
        {
            int ID = int.Parse(id);
            DEVIS_FOURNISSEURS cmdclt = new DEVIS_FOURNISSEURS();

            COMMANDES_FOURNISSEURS blcmd = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.Devis_Frs == ID).FirstOrDefault();
            if (blcmd == null)
            {
                return cmdclt.ID.ToString();
            }
            else
            {
                return "NO";
            }

        }
        public string validateBonRecepetion(string parampassed)
        {
            int ID = int.Parse(parampassed);
            BONS_RECEPTIONS_FOURNISSEURS BonReception = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> liste = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.BON_RECEPTION_FOURNISSEUR == ID).ToList();

            foreach (LIGNES_BONS_RECEPTIONS_FOURNISSEURS ligne in liste)
            {
                Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.Libelle_Prd).FirstOrDefault();
                Prix_Achat prixAchat = new Prix_Achat();
                if (Produit == null)
                {
                    prixAchat.Libelle = ligne.Libelle_Prd;
                    prixAchat.Designation = ligne.DESIGNATION_PRODUIT;
                    prixAchat.Marque = ligne.Marque;
                    prixAchat.Devise = ligne.Devise;
                    prixAchat.Unite = ligne.Unite;
                    if (ligne.Sous_Categorie == "")
                    {
                        prixAchat.Sous_Categorie = null;
                    }
                    else
                    {
                        List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
                        foreach (Sous_Categorie sc in List)
                        {
                            if (sc.Libelle == ligne.Sous_Categorie)
                            {
                                prixAchat.Sous_Categorie = sc.CatID;
                            }
                        }
                    }
                    prixAchat.Stock = (double)ligne.QUANTITE;
                    prixAchat.Remise = (int)ligne.REMISE;
                    prixAchat.PU_HT_Sans_Remise = (double)ligne.PRIX_UNITAIRE_HT;
                    prixAchat.Valeur_TVA = (ligne.TVA).ToString();
                    prixAchat.PU_TTC = (double)ligne.TOTALE_TTC;
                    prixAchat.Fournisseur = BonReception.FOURNISSEUR;
                    db.Prix_Achat.Add(prixAchat);
                    db.SaveChanges();
                    ligne.Prix_achat = prixAchat.Product_ID;
                }
                else
                {
                    Produit.Stock += (double)ligne.QUANTITE;
                    ligne.Prix_achat = Produit.Product_ID;
                }
            }
            BonReception.VALIDER = true;
            db.SaveChanges();
            return string.Empty;
        }
        public string validateFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_FOURNISSEURS Facture = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            Facture.VALIDER = true;
            db.SaveChanges();
            List<LIGNES_FACTURES_FOURNISSEURS> liste = db.LIGNES_FACTURES_FOURNISSEURS.Where(cmd => cmd.FACTURE_FOURNISSEUR == ID).ToList();
            foreach(LIGNES_FACTURES_FOURNISSEURS ligne in liste)
            {
                BONS_RECEPTIONS_FOURNISSEURS Br= db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ligne.ID_Bl).FirstOrDefault();
                Br.Etat = true;
                db.SaveChanges();
            }
           
            return string.Empty;
        }
        public string PayeFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_FOURNISSEURS Facture = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Facture.VALIDER)
            {
                Facture.PAYEE = true;
            }
            db.SaveChanges();
            return string.Empty;
        }
        public string validateAvoir(string parampassed)
        {
            int ID = int.Parse(parampassed);
            AVOIRS_FOURNISSEURS Facture = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            Facture.VALIDER = true;
            db.SaveChanges();
            return string.Empty;
        }
        public string DevisVersCommande(string parampassed)
        {
            int ID = int.Parse(parampassed);
            DEVIS_FOURNISSEURS Element = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_DEVIS_FOURNISSEURS> Liste = db.LIGNES_DEVIS_FOURNISSEURS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();
            COMMANDES_FOURNISSEURS NewElement = new COMMANDES_FOURNISSEURS();
            string Numero = string.Empty;
            DateTime d = Element.DATE;
            int Max = 0;
            if (db.COMMANDES_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "CDC" + Max.ToString("0000") + "/" + d.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            //NewElement.Designation = Element.Designation;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            //NewElement.Designation = Element.Designation;
            NewElement.FOURNISSEUR = Element.FOURNISSEUR;
            NewElement.Societes = (int)Element.Societes;
            if (Element.Tiers != null)
            {
                NewElement.Tiers = (int)Element.Tiers;
            }
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TTC = Element.TTC;
            NewElement.TNET = Element.TNET;
            NewElement.VALIDER = false;
            NewElement.REMISE = Element.REMISE;
            NewElement.Devis_Frs = Element.ID;
            NewElement.DEVIS_FOURNISSEURS = Element;
            //NewElement.DEVI= Element.ID;
            //NewElement.D = Element;
            NewElement.FOURNISSEURS = Element.FOURNISSEURS;
            NewElement.Societes1 = Element.Societes1;
            db.COMMANDES_FOURNISSEURS.Add(NewElement);
            db.SaveChanges();
            foreach (LIGNES_DEVIS_FOURNISSEURS Ligne in Liste)
            {
                LIGNES_COMMANDES_FOURNISSEURS NewLine = new LIGNES_COMMANDES_FOURNISSEURS();
                //NewLine.Prix_achat = Ligne.Prix_achat;
                //NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                NewLine.Libelle_Prd = Ligne.Libelle_Prd;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                NewLine.Marque = Ligne.Marque;
                NewLine.Devise = Ligne.Devise;
                NewLine.Unite = Ligne.Unite;
                NewLine.Categorie = Ligne.Categorie;
                NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                NewLine.QUANTITE = (double)Ligne.QUANTITE;
                //NewLine.STOCK = Ligne.STOCK;
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                NewLine.REMISE = Ligne.REMISE;
                NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                NewLine.COMMANDE_FOURNISSEUR = NewElement.ID;
                NewLine.COMMANDES_FOURNISSEURS = NewElement;
                //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                db.LIGNES_COMMANDES_FOURNISSEURS.Add(NewLine);
                db.SaveChanges();
                //AddMouvementProduit("COMMANDE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }

            return NewElement.ID.ToString();
        }
        public string CommandeVersBonReception(string parampassed)
        {
            int ID = int.Parse(parampassed);
            COMMANDES_FOURNISSEURS Element = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_COMMANDES_FOURNISSEURS> Liste = db.LIGNES_COMMANDES_FOURNISSEURS.Where(cmd => cmd.COMMANDE_FOURNISSEUR == ID).ToList();
            BONS_RECEPTIONS_FOURNISSEURS NewElement = new BONS_RECEPTIONS_FOURNISSEURS();
            string Numero = string.Empty;
            DateTime d = Element.DATE;

            int Max = 0;
            if (db.BONS_RECEPTIONS_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "BRF" + Max.ToString("0000") + "/" + d.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            NewElement.FOURNISSEUR = Element.FOURNISSEUR;
            NewElement.Societes = Element.Societes;
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TTC = Element.TTC;
            NewElement.TNET = Element.TNET;
            NewElement.VALIDER = Element.VALIDER;
            NewElement.REMISE = Element.REMISE;
            NewElement.COMMANDE_FOURNISSEUR = Element.ID;
            NewElement.COMMANDES_FOURNISSEURS = Element;
            NewElement.Societes1 = NewElement.Societes1;
            NewElement.FOURNISSEURS = Element.FOURNISSEURS;
            db.BONS_RECEPTIONS_FOURNISSEURS.Add(NewElement);
            db.SaveChanges();
            foreach (LIGNES_COMMANDES_FOURNISSEURS Ligne in Liste)
            {
                LIGNES_BONS_RECEPTIONS_FOURNISSEURS NewLine = new LIGNES_BONS_RECEPTIONS_FOURNISSEURS();
                Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.Libelle_Prd).FirstOrDefault();
                ////Prix_Achat prixAchat = new Prix_Achat();

                //NewLine.Prix_achat = (int)Ligne.Prix_achat;
                NewLine.Libelle_Prd = Ligne.Libelle_Prd;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                NewLine.Marque = Ligne.Marque;
                NewLine.Devise = Ligne.Devise;
                NewLine.Unite = Ligne.Unite;
                NewLine.Categorie = Ligne.Categorie;
                NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                //NewLine.Stock= Ligne.QUANTITE;
                NewLine.QUANTITE = Ligne.QUANTITE;
                if (Produit == null)
                {
                    NewLine.Stock = Ligne.QUANTITE;
                }
                else
                {
                    NewLine.Stock = Produit.Stock + Ligne.QUANTITE;
                }
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                NewLine.REMISE = Ligne.REMISE;
                NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                NewLine.BON_RECEPTION_FOURNISSEUR = NewElement.ID;
                NewLine.BONS_RECEPTIONS_FOURNISSEURS = NewElement;
                //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Add(NewLine);
                db.SaveChanges();
                //AddMouvementProduit("BON_RECEPTION", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }
            return NewElement.ID.ToString();
        }
        public string CommandeVersfacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            COMMANDES_FOURNISSEURS Element = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_COMMANDES_FOURNISSEURS> Liste = db.LIGNES_COMMANDES_FOURNISSEURS.Where(cmd => cmd.COMMANDE_FOURNISSEUR == ID).ToList();
            FACTURES_FOURNISSEURS NewElement = new FACTURES_FOURNISSEURS();
            string Numero = string.Empty;

            int Max = 0;
            if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "FF" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            NewElement.FOURNISSEUR = Element.FOURNISSEUR;
            NewElement.Societes = Element.Societes;
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.FODEC = decimal.Parse("0");
            NewElement.Redevance = decimal.Parse("0");
            NewElement.TIMBRE = decimal.Parse("0,6");
            NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
            NewElement.TNET = Element.TNET + NewElement.TIMBRE;
            NewElement.VALIDER = false;
            NewElement.REMISE = Element.REMISE;
            NewElement.COMMANDE_FOURNISSEUR = Element.ID;
            NewElement.COMMANDES_FOURNISSEURS = Element;
            NewElement.FOURNISSEURS = Element.FOURNISSEURS;
            NewElement.Societes1 = Element.Societes1;
            db.FACTURES_FOURNISSEURS.Add(NewElement);
            db.SaveChanges();
            foreach (LIGNES_COMMANDES_FOURNISSEURS Ligne in Liste)
            {
                LIGNES_FACTURES_FOURNISSEURS NewLine = new LIGNES_FACTURES_FOURNISSEURS();
                NewLine.Prix_achat =(int)Ligne.Prix_achat;
                NewLine.Libelle_Prd = Ligne.Libelle_Prd;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                NewLine.Marque = Ligne.Marque;
                NewLine.Devise = Ligne.Devise;
                NewLine.Unite = Ligne.Unite;
                NewLine.Categorie = Ligne.Categorie;
                NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                NewLine.QUANTITE = Ligne.QUANTITE;
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                NewLine.REMISE = Ligne.REMISE;
                NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                NewLine.FACTURE_FOURNISSEUR = NewElement.ID;
                //NewLine.F = NewElement;
                //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                db.LIGNES_FACTURES_FOURNISSEURS.Add(NewLine);
                db.SaveChanges();
                //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }
            return NewElement.ID.ToString();
        }
        public string BonReceptionVersfacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            BONS_RECEPTIONS_FOURNISSEURS Element = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Element.VALIDER)
            {
                List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> Liste = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.BON_RECEPTION_FOURNISSEUR == ID).ToList();
                FACTURES_FOURNISSEURS NewElement = new FACTURES_FOURNISSEURS();
                string Numero = string.Empty;
                DateTime d = Element.DATE;

                int Max = 0;
                if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
                {
                    Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "FF" + Max.ToString("0000") + "/" + d.ToString("yy");
                NewElement.CODE = Numero;
                NewElement.Num_Fact = "0";
                NewElement.DATE = Element.DATE;
                NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
                NewElement.FOURNISSEUR = Element.FOURNISSEUR;
                NewElement.Societes = Element.Societes;
                NewElement.THT = Element.THT;
                NewElement.TTVA = Element.TTVA;
                NewElement.NHT = Element.NHT;
                NewElement.TIMBRE = decimal.Parse("0,6");
                NewElement.FODEC = decimal.Parse("0");
                NewElement.Redevance = decimal.Parse("0");
                NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
                NewElement.TNET = Element.TNET + NewElement.TIMBRE;
                NewElement.VALIDER = false;
                NewElement.REMISE = Element.REMISE;
                NewElement.BON_RECEPTION_FOURNISSEUR = Element.ID;
                NewElement.BONS_RECEPTIONS_FOURNISSEURS = Element;
                NewElement.FOURNISSEURS = Element.FOURNISSEURS;
                NewElement.Societes1 = Element.Societes1;
                db.FACTURES_FOURNISSEURS.Add(NewElement);
                db.SaveChanges();
                foreach (LIGNES_BONS_RECEPTIONS_FOURNISSEURS Ligne in Liste)
                {
                    LIGNES_FACTURES_FOURNISSEURS NewLine = new LIGNES_FACTURES_FOURNISSEURS();
                    NewLine.Prix_achat = (int)Ligne.Prix_achat;
                    NewLine.Libelle_Prd = Ligne.Libelle_Prd;
                    NewLine.ID_Bl = Element.ID;
                    NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                    NewLine.Marque = Ligne.Marque;
                    NewLine.Devise = Ligne.Devise;
                    NewLine.Unite = Ligne.Unite;
                    NewLine.Categorie = Ligne.Categorie;
                    NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                    NewLine.QUANTITE = Ligne.QUANTITE;
                    NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = Ligne.REMISE;
                    NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                    NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                    NewLine.TVA = Ligne.TVA;
                    NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                    NewLine.FACTURE_FOURNISSEUR = NewElement.ID;
                    //NewLine.FACTURES_FOURNISSEURS = NewElement;
                    //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                    db.LIGNES_FACTURES_FOURNISSEURS.Add(NewLine);
                    db.SaveChanges();
                    //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
                }
                return NewElement.ID.ToString();
            }
            return "NO";
        }
        [HttpPost]
        public ActionResult BonLivraisonParVersfacture(FormCollection formCollection)
        {
            string[] ids = formCollection["affComId"].Split(new char[] { ',' });
            string Idd = ids[0];
            int ID = int.Parse(Idd);

            foreach (string Id in ids)
            {
                int Idf = int.Parse(Id);
                //List<FACTURES_FOURNISSEURS> listfact = db.FACTURES_FOURNISSEURS.ToList();
                //List<LIGNES_FACTURES_FOURNISSEURS> listlignesfac = db.LIGNES_FACTURES_FOURNISSEURS.ToList();
                //foreach (FACTURES_FOURNISSEURS fact in listfact)
                //{
                //    if (fact.BON_RECEPTION_FOURNISSEUR == Idf)
                //    {
                //        db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.FACTURE_FOURNISSEUR == fact.ID).ToList().ForEach(p => db.LIGNES_FACTURES_FOURNISSEURS.Remove(p));
                //        db.SaveChanges();
                //        db.FACTURES_FOURNISSEURS.Remove(fact);
                //        db.SaveChanges();
                //    }

                //}
                //foreach (LIGNES_FACTURES_FOURNISSEURS lignebl in listlignesfac)
                //{
                //    if (lignebl.ID_Bl == Idf)
                //    {
                List<LIGNES_FACTURES_FOURNISSEURS> listlignesfac = db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.ID_Bl == Idf).ToList();
                if(listlignesfac.Count()!=0)
                {
                        LIGNES_FACTURES_FOURNISSEURS ligne = db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.ID_Bl == Idf).FirstOrDefault();
                        FACTURES_FOURNISSEURS factfrs = db.FACTURES_FOURNISSEURS.Where(p => p.ID == ligne.FACTURE_FOURNISSEUR).FirstOrDefault();
                        db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.ID_Bl == Idf).ToList().ForEach(p => db.LIGNES_FACTURES_FOURNISSEURS.Remove(p));
                        db.FACTURES_FOURNISSEURS.Remove(factfrs);
                }
                //    }
                //}
            }
            FACTURES_FOURNISSEURS NewElement = new FACTURES_FOURNISSEURS();
            BONS_RECEPTIONS_FOURNISSEURS Element = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            string Numero = string.Empty;
            DateTime d = Element.DATE;

            int Max = 0;
            if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
            {
                Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "FF" + Max.ToString("0000") + "/" + d.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.Num_Fact = "0";
            NewElement.DATE = Element.DATE;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            //NewElement.Designation = Element.Designation;

            NewElement.FOURNISSEUR = Element.FOURNISSEUR;
            NewElement.Societes = Element.Societes;
            // NewElement.Tiers = Element.Tiers;
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TIMBRE = decimal.Parse("0,6");
            NewElement.FODEC = decimal.Parse("0");
            NewElement.Redevance = decimal.Parse("0");
            NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
            NewElement.TNET = Element.TNET + NewElement.TIMBRE;
            NewElement.VALIDER = false;
            NewElement.PAYEE = false;

            NewElement.REMISE = Element.REMISE;
            NewElement.BON_RECEPTION_FOURNISSEUR = Element.ID;
            NewElement.BONS_RECEPTIONS_FOURNISSEURS = Element;
            NewElement.FOURNISSEURS = Element.FOURNISSEURS;
            //NewElement.Societes1 = Element.Societes1;
            db.FACTURES_FOURNISSEURS.Add(NewElement);
            db.SaveChanges();

            foreach (string Id in ids)
            {
                int Idf = int.Parse(Id);


                List<LIGNES_BONS_RECEPTIONS_FOURNISSEURS> Liste = db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.BON_RECEPTION_FOURNISSEUR == Idf).ToList();

                foreach (LIGNES_BONS_RECEPTIONS_FOURNISSEURS ligne in Liste)
                {

                    LIGNES_FACTURES_FOURNISSEURS NewLine = new LIGNES_FACTURES_FOURNISSEURS();
                    NewLine.Prix_achat = (int)ligne.Prix_achat;
                    // NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                    NewLine.ID_Bl = Idf;
                    NewLine.DESIGNATION_PRODUIT = ligne.DESIGNATION_PRODUIT;
                    NewLine.Marque = ligne.Marque;
                    NewLine.Devise = ligne.Devise;
                    NewLine.Unite = ligne.Unite;
                    NewLine.Categorie = ligne.Categorie;
                    NewLine.Sous_Categorie = ligne.Sous_Categorie;
                    NewLine.QUANTITE = ligne.QUANTITE;
                    NewLine.Stock = (int)ligne.Stock;
                    NewLine.PRIX_UNITAIRE_HT = ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = ligne.REMISE;
                    //NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                    NewLine.TOTALE_HT = ligne.TOTALE_HT;
                    NewLine.TVA = ligne.TVA;
                    NewLine.TOTALE_TTC = ligne.TOTALE_TTC;
                    NewLine.FACTURE_FOURNISSEUR = NewElement.ID;
                    //NewLine.FACTURES_FOURNISSEURS = NewElement;
                    //NewLine.Prix_Achat1 = Ligne.;
                    db.LIGNES_FACTURES_FOURNISSEURS.Add(NewLine);
                    db.SaveChanges();
                    //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
                }
            }
            return RedirectToAction("FormFacture", "Achat", new { @Mode = "Edit", @Code = NewElement.ID.ToString() });

        }
        public string FactureVersAvoir(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_FOURNISSEURS Element = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Element.VALIDER)
            {
                List<LIGNES_FACTURES_FOURNISSEURS> Liste = db.LIGNES_FACTURES_FOURNISSEURS.Where(cmd => cmd.FACTURE_FOURNISSEUR == ID).ToList();
                AVOIRS_FOURNISSEURS NewElement = new AVOIRS_FOURNISSEURS();
                string Numero = string.Empty;
                DateTime d = Element.DATE;

                int Max = 0;
                if (db.FACTURES_FOURNISSEURS.ToList().Count != 0)
                {
                    Max = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "AVF" + Max.ToString("0000") + "/" + d.ToString("yy");
                NewElement.CODE = Numero;
                NewElement.DATE = Element.DATE;
                NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
                NewElement.FOURNISSEUR = Element.FOURNISSEUR;
                NewElement.Societes = Element.Societes;
                NewElement.THT = Element.THT;
                NewElement.TTVA = Element.TTVA;
                NewElement.NHT = Element.NHT;
                NewElement.TTC = Element.TTC;
                NewElement.TNET = Element.TNET;
                NewElement.VALIDER = false;
                NewElement.REMISE = Element.REMISE;
                NewElement.FACTURE_FOURNISSEUR = Element.ID;
                NewElement.FACTURES_FOURNISSEURS = Element;
                NewElement.FOURNISSEURS = Element.FOURNISSEURS;
                NewElement.Societes1 = Element.Societes1;
                db.AVOIRS_FOURNISSEURS.Add(NewElement);
                db.SaveChanges();
                foreach (LIGNES_FACTURES_FOURNISSEURS Ligne in Liste)
                {
                    LIGNES_AVOIRS_FOURNISSEURS NewLine = new LIGNES_AVOIRS_FOURNISSEURS();
                    NewLine.Prix_achat = Ligne.Prix_achat;
                    NewLine.Libelle_Prd = Ligne.Libelle_Prd;
                    NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                    NewLine.Marque = Ligne.Marque;
                    NewLine.Devise = Ligne.Devise;
                    NewLine.Unite = Ligne.Unite;
                    NewLine.Categorie = Ligne.Categorie;
                    NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                    NewLine.QUANTITE = Ligne.QUANTITE;
                    NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = Ligne.REMISE;
                    NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                    NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                    NewLine.TVA = Ligne.TVA;
                    NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                    NewLine.AVOIR_FOURNISSEUR = NewElement.ID;
                    NewLine.AVOIRS_FOURNISSEURS = NewElement;
                    //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                    db.LIGNES_AVOIRS_FOURNISSEURS.Add(NewLine);
                    db.SaveChanges();
                    //AddMouvementProduit("AVOIR", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
                }
                return NewElement.ID.ToString();
            }
            return "NO";
        }
        #endregion
        public string AddLineDevis(/*string ID_Produit, */string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {

            if (marque == "") { return "NOMARQUE"; }
            if (LIB_Produi == "") { return "LibelleProdui"; }
            
            if (devise == "") { return "devise"; }
            if (unite == "") { return "unite"; }
            if (categorie == "") { return "categorie"; }
            //if (sous_categorie == "") { return "sous_categorie"; }
            if (Quantite_Produit == "") { return "Quantite_Produit"; }
            if (PUHT_Produit == "") { return "PUHT_Produit"; }
            if (Remise_Produit == "") { return "Remise_Produit"; }
            if (PTHT_Produit == "") { return "PTHT_Produit"; }
            if (TVA_Produit == "") { return "TVA_Produit"; }
            if (TTC_Produit == "") { return "TTC_Produit"; }
            //if (Description_Produit == "") { return "Description_Produit"; }
            string marq = marque.Trim();

            Marque M = db.Marque.Where(fou => fou.Nom_marque == marq).FirstOrDefault();
            if (M == null)
            {
                Marque mm = new Marque();
                mm.Nom_marque = marque;
                db.Marque.Add(mm);
                db.SaveChanges();

            }
            string unit = unite.Trim();

            Unite U = db.Unite.Where(fou => fou.Valeur_Unite == unit).FirstOrDefault();
            if (U == null)
            {
                Unite uu = new Unite();
                uu.Valeur_Unite = marque;
                db.Unite.Add(uu);
                db.SaveChanges();

            }
            string dev = devise.Trim();

            Devise D = db.Devise.Where(fou => fou.Nom_Devise == dev).FirstOrDefault();
            if (D == null)
            {
                Devise dd = new Devise();
                dd.Nom_Devise = devise;
                db.Devise.Add(dd);
                db.SaveChanges();

            }
            string cat = categorie.Trim();
            Categorie C = db.Categorie.Where(fou => fou.Libelle == cat).FirstOrDefault();
            if (C == null)
            {
                Categorie cc = new Categorie();
                cc.Libelle = categorie;
                db.Categorie.Add(cc);
                db.SaveChanges();
                Sous_Categorie sc = new Sous_Categorie();
                sc.Libelle = sous_categorie;
                sc.CentreID = cc.CentreID;
                db.Sous_Categorie.Add(sc);
                db.SaveChanges();



            }
            else
            {
                string sscat = sous_categorie.Trim();
                Sous_Categorie SC = db.Sous_Categorie.Where(fou => fou.Libelle == sscat && fou.CentreID == C.CentreID).FirstOrDefault();
                if (SC == null)
                {
                    Sous_Categorie sc = new Sous_Categorie();
                    sc.Libelle = sous_categorie;
                    sc.CentreID = C.CentreID;
                    db.Sous_Categorie.Add(sc);
                    db.SaveChanges();

                }

            }
            string tva = TVA_Produit.Trim();

            TVA T = db.TVA.Where(fou => fou.Valeur_TVA == TVA_Produit).FirstOrDefault();
            if (T == null)
            {
                TVA tt = new TVA();
                tt.Valeur_TVA = TVA_Produit;
                db.TVA.Add(tt);
                db.SaveChanges();

            }

            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
                LigneProduit ligne = new LigneProduit();
                if (Session["ProduitsDevisFournisseur"] != null)
                {
                    ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
                    ligne.ID = ListeDesPoduits.Count() + 1;

                }
                else
                {
                    ligne.ID = 1;
                }

                ligne.LIBELLE = LIB_Produi;
                ligne.DESIGNATION = Description_Produit;
                ligne.MARQUE = marque;
                ligne.UNITE = unite;
                ligne.DEVISE = devise;
                ligne.CATEGORIE = categorie;
                ligne.Sous_CATEGORIE = sous_categorie;
            //ligne.STOCK = int.Parse(StockProduit);
                ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
                //ligne.QUANTITE = (decimal)(Quantite_Produit);
                ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
                ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
                ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
                ligne.TVA = int.Parse(TVA_Produit);
                ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
                //if (Session["ProduitsDevisFournisseur"] != null)
                //{
                //    ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];

                //}
                if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
                {
                    ListeDesPoduits.Add(ligne);
                }
               
                Session["ProduitsDevisFournisseur"] = ListeDesPoduits;
                return string.Empty;

          
           
        }
        
        public string AddLine(/*string ID_Produit,*/ string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, /*string StockProduit,*/ string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            Marque M = db.Marque.Where(fou => fou.Nom_marque == marque).FirstOrDefault();
            if (M == null)
            {
                Marque mm = new Marque();
                mm.Nom_marque = marque;
                db.Marque.Add(mm);
                db.SaveChanges();

            }
            Unite U = db.Unite.Where(fou => fou.Valeur_Unite == unite).FirstOrDefault();
            if (U == null)
            {
                Unite uu = new Unite();
                uu.Valeur_Unite = marque;
                db.Unite.Add(uu);
                db.SaveChanges();

            }
            Devise D = db.Devise.Where(fou => fou.Nom_Devise == devise).FirstOrDefault();
            if (D == null)
            {
                Devise dd = new Devise();
                dd.Nom_Devise = devise;
                db.Devise.Add(dd);
                db.SaveChanges();

            }
            Categorie C = db.Categorie.Where(fou => fou.Libelle == categorie).FirstOrDefault();
            if (C == null)
            {
                Categorie cc = new Categorie();
                cc.Libelle = categorie;
                db.Categorie.Add(cc);
                db.SaveChanges();
                Sous_Categorie sc = new Sous_Categorie();
                sc.Libelle = sous_categorie;
                sc.CentreID = cc.CentreID;
                db.Sous_Categorie.Add(sc);
                db.SaveChanges();



            }
            else
            {
                Sous_Categorie SC = db.Sous_Categorie.Where(fou => fou.Libelle == sous_categorie && fou.CentreID == C.CentreID).FirstOrDefault();
                if (SC == null)
                {
                    Sous_Categorie sc = new Sous_Categorie();
                    sc.Libelle = sous_categorie;
                    sc.CentreID = C.CentreID;
                    db.Sous_Categorie.Add(sc);
                    db.SaveChanges();

                }

            }
            TVA T = db.TVA.Where(fou => fou.Valeur_TVA == TVA_Produit).FirstOrDefault();
            if (T == null)
            {
                TVA tt = new TVA();
                tt.Valeur_TVA = TVA_Produit;
                db.TVA.Add(tt);
                db.SaveChanges();

            }

            LigneProduit ligne = new LigneProduit();
            //ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsCommandeFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }    
        public string AddLineBonReception(/*string ID_Produit,*/ string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit,string Mode)
        {
            Marque M = db.Marque.Where(fou => fou.Nom_marque == marque).FirstOrDefault();
            if (M == null)
            {
                Marque mm = new Marque();
                mm.Nom_marque = marque;
                db.Marque.Add(mm);
                db.SaveChanges();

            }
            Unite U = db.Unite.Where(fou => fou.Valeur_Unite == unite).FirstOrDefault();
            if (U == null)
            {
                Unite uu = new Unite();
                uu.Valeur_Unite = marque;
                db.Unite.Add(uu);
                db.SaveChanges();

            }
            Devise D = db.Devise.Where(fou => fou.Nom_Devise == devise).FirstOrDefault();
            if (D == null)
            {
                Devise dd = new Devise();
                dd.Nom_Devise = devise;
                db.Devise.Add(dd);
                db.SaveChanges();

            }
            Categorie C = db.Categorie.Where(fou => fou.Libelle == categorie).FirstOrDefault();
            if (C == null)
            {
                Categorie cc = new Categorie();
                cc.Libelle = categorie;
                db.Categorie.Add(cc);
                db.SaveChanges();
                Sous_Categorie sc = new Sous_Categorie();
                sc.Libelle = sous_categorie;
                sc.CentreID = cc.CentreID;
                db.Sous_Categorie.Add(sc);
                db.SaveChanges();



            }
            else
            {
                Sous_Categorie SC = db.Sous_Categorie.Where(fou => fou.Libelle == sous_categorie && fou.CentreID == C.CentreID).FirstOrDefault();
                if (SC == null)
                {
                    Sous_Categorie sc = new Sous_Categorie();
                    sc.Libelle = sous_categorie;
                    sc.CentreID = C.CentreID;
                    db.Sous_Categorie.Add(sc);
                    db.SaveChanges();

                }

            }
            TVA T = db.TVA.Where(fou => fou.Valeur_TVA == TVA_Produit).FirstOrDefault();
            if (T == null)
            {
                TVA tt = new TVA();
                tt.Valeur_TVA = TVA_Produit;
                db.TVA.Add(tt);
                db.SaveChanges();

            }

            LigneProduit ligne = new LigneProduit();
            Prix_Achat PR = db.Prix_Achat.Where(fou => fou.Libelle == LIB_Produi).FirstOrDefault();
            if (PR != null)
            {
                ligne.ID = PR.Product_ID;
                if (Mode == "Create")
                {
                    ligne.STOCK = int.Parse(StockProduit + Quantite_Produit);
                }

            }
            else
            {
                ligne.ID = db.Prix_Achat.Select(cmd => cmd.Product_ID).Count();
                if (Mode == "Create")
                {
                    ligne.STOCK += int.Parse(Quantite_Produit);
                }

                }
            if (Mode == "Edit")
            {
                ligne.STOCK += int.Parse(Quantite_Produit);
            }

            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            //ligne.STOCK = int.Parse(StockProduit);
            ligne.QUANTITE = decimal.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsBonCommandeFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineFacture(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            Marque M = db.Marque.Where(fou => fou.Nom_marque == marque).FirstOrDefault();
            if (M == null)
            {
                Marque mm = new Marque();
                mm.Nom_marque = marque;
                db.Marque.Add(mm);
                db.SaveChanges();

            }
            Unite U = db.Unite.Where(fou => fou.Valeur_Unite == unite).FirstOrDefault();
            if (U == null)
            {
                Unite uu = new Unite();
                uu.Valeur_Unite = marque;
                db.Unite.Add(uu);
                db.SaveChanges();

            }
            Devise D = db.Devise.Where(fou => fou.Nom_Devise == devise).FirstOrDefault();
            if (D == null)
            {
                Devise dd = new Devise();
                dd.Nom_Devise = devise;
                db.Devise.Add(dd);
                db.SaveChanges();

            }
            Categorie C = db.Categorie.Where(fou => fou.Libelle == categorie).FirstOrDefault();
            if (C == null)
            {
                Categorie cc = new Categorie();
                cc.Libelle = categorie;
                db.Categorie.Add(cc);
                db.SaveChanges();
                Sous_Categorie sc = new Sous_Categorie();
                sc.Libelle = sous_categorie;
                sc.CentreID = cc.CentreID;
                db.Sous_Categorie.Add(sc);
                db.SaveChanges();



            }
            else
            {
                Sous_Categorie SC = db.Sous_Categorie.Where(fou => fou.Libelle == sous_categorie && fou.CentreID == C.CentreID).FirstOrDefault();
                if (SC == null)
                {
                    Sous_Categorie sc = new Sous_Categorie();
                    sc.Libelle = sous_categorie;
                    sc.CentreID = C.CentreID;
                    db.Sous_Categorie.Add(sc);
                    db.SaveChanges();

                }

            }
            TVA T = db.TVA.Where(fou => fou.Valeur_TVA == TVA_Produit).FirstOrDefault();
            if (T == null)
            {
                TVA tt = new TVA();
                tt.Valeur_TVA = TVA_Produit;
                db.TVA.Add(tt);
                db.SaveChanges();

            }

            LigneProduit ligne = new LigneProduit();
            //if(ID_Produit!=null)
            //{
                //ligne.ID = int.Parse(ID_Produit);
            //}
            
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsFactureFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineAvoir(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            Marque M = db.Marque.Where(fou => fou.Nom_marque == marque).FirstOrDefault();
            if (M == null)
            {
                Marque mm = new Marque();
                mm.Nom_marque = marque;
                db.Marque.Add(mm);
                db.SaveChanges();

            }
            Unite U = db.Unite.Where(fou => fou.Valeur_Unite == unite).FirstOrDefault();
            if (U == null)
            {
                Unite uu = new Unite();
                uu.Valeur_Unite = marque;
                db.Unite.Add(uu);
                db.SaveChanges();

            }
            Devise D = db.Devise.Where(fou => fou.Nom_Devise == devise).FirstOrDefault();
            if (D == null)
            {
                Devise dd = new Devise();
                dd.Nom_Devise = devise;
                db.Devise.Add(dd);
                db.SaveChanges();

            }
            Categorie C = db.Categorie.Where(fou => fou.Libelle == categorie).FirstOrDefault();
            if (C == null)
            {
                Categorie cc = new Categorie();
                cc.Libelle = categorie;
                db.Categorie.Add(cc);
                db.SaveChanges();
                Sous_Categorie sc = new Sous_Categorie();
                sc.Libelle = sous_categorie;
                sc.CentreID = cc.CentreID;
                db.Sous_Categorie.Add(sc);
                db.SaveChanges();



            }
            else
            {
                Sous_Categorie SC = db.Sous_Categorie.Where(fou => fou.Libelle == sous_categorie && fou.CentreID == C.CentreID).FirstOrDefault();
                if (SC == null)
                {
                    Sous_Categorie sc = new Sous_Categorie();
                    sc.Libelle = sous_categorie;
                    sc.CentreID = C.CentreID;
                    db.Sous_Categorie.Add(sc);
                    db.SaveChanges();

                }

            }
            TVA T = db.TVA.Where(fou => fou.Valeur_TVA == TVA_Produit).FirstOrDefault();
            if (T == null)
            {
                TVA tt = new TVA();
                tt.Valeur_TVA = TVA_Produit;
                db.TVA.Add(tt);
                db.SaveChanges();

            }

            LigneProduit ligne = new LigneProduit();
            ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.STOCK = int.Parse(StockProduit);
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsAvoirFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLigneDevis(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsDevisFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLigne(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsCommandeFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLigneBonReception(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.STOCK += int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsBonCommandeFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLigneFacture(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsFactureFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLigneAvoir(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsAvoirFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public JsonResult CopieLigneDevis(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            return Json(ligne, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CopieLigneCmd(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            return Json(ligne, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CopieLigneBr(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            return Json(ligne, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CopieLigneFact(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            return Json(ligne, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CopieLigneAv(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            return Json(ligne, JsonRequestBehavior.AllowGet);
        }
        public string DeleteLineDevis(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string DeleteLigne(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string DeleteLigneBonReception(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string DeleteLigneFacture(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string DeleteLigneAvoir(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsAvoirFournisseur"] = ListeDesPoduits;
            return string.Empty;
        }
        public JsonResult GetAllLigneDevis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLigne()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLigneBonReception()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLigneFacture()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLigneAvoir()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceDevis(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            decimal IntRemise = decimal.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePrice(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            decimal IntRemise = decimal.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceBonReception(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            decimal IntRemise = decimal.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceFacture(string remise,string FODEC,string REDEVANCE)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            decimal IntRemise = decimal.Parse(remise);
            int IntFODEC = int.Parse(FODEC);
            decimal IntREDEVANCE = decimal.Parse(REDEVANCE);

            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += ((ligne.PTHT + ((ligne.PTHT * IntFODEC) / 100)) * ligne.TVA) / 100;
                //totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
           
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceFacture2(string FODEC)
      {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            int IntFODEC = int.Parse(FODEC);

            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT ;
                totalTVA += ((ligne.PTHT+((ligne.PTHT * IntFODEC) / 100)) * ligne.TVA) / 100;

            }

            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA

            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult UpdatePriceFacture2(string FODEC)
        //{
        //    List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
        //    if (Session["ProduitsFactureFournisseur"] != null)
        //    {
        //        ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
        //    }
        //    decimal totalHT = 0;
        //    decimal totalTVA = 0;
        //    foreach (LigneProduit ligne in ListeDesPoduits)
        //    {
        //        totalHT += ligne.PTHT;
        //        totalTVA += (ligne.PTHT * ligne.TVA) / 100;
        //    }
        //    dynamic Result = new
        //    {
        //        totalHT = totalHT,
        //        totalTVA = totalTVA
        //    };
        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult UpdatePriceFacture3(string REDEVANCE)
        //{
        //    List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
        //    if (Session["ProduitsFactureFournisseur"] != null)
        //    {
        //        ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
        //    }
        //    decimal totalTTC = 0;
        //    //decimal totalTVA = 0;
        //    foreach (LigneProduit ligne in ListeDesPoduits)
        //    {
        //        totalTTC += ligne.TTC;

        //    }
        //    dynamic Result = new
        //    {
        //        totalTTC = totalTTC,

        //    };
        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult UpdatePriceAvoir(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            decimal IntRemise = decimal.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
     
        public void AddFournisseur(string Code, string NOM, string Adresse, string TELEPHONE, string FAX, string EMAIL, string SITE_WEB, string ID_FISCAL, string AI, string NIS, string RC, string RIB, string CONTACT)
        {
            FOURNISSEURS NewElement = new FOURNISSEURS();
            NewElement.NOM = NOM;
            NewElement.CODE = Code;
            NewElement.ADRESSE = Adresse;
            NewElement.TELEPHONE = TELEPHONE;
            NewElement.FAX = FAX;
            NewElement.EMAIL = EMAIL;
            NewElement.SITE_WEB = SITE_WEB;
            NewElement.ID_FISCAL = ID_FISCAL;
            NewElement.AI = AI;
            NewElement.RC = RC;
            NewElement.RIB = RIB;
            NewElement.CONTACT = CONTACT;
            db.FOURNISSEURS.Add(NewElement);
            db.SaveChanges();
        }
        [HttpPost]
        public ActionResult SendDevis(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string date2 = Request["date2"] != null ? Request["date2"].ToString() : string.Empty;

            string fournisseur = Request["fournisseur"] != null ? Request["fournisseur"].ToString() : string.Empty;
            //string societe = Request["Societes"] != null ? Request["Societes"].ToString() : string.Empty;
            string Tiers = Request["Tiers"] != null ? Request["Tiers"].ToString() : string.Empty;
            string modePaiement = Request["modePaiement"] != null ? Request["modePaiement"].ToString() : string.Empty;
            string remise = Request["remise"] != null ? Request["remise"].ToString() : string.Empty;
            string totalHT = Request["totalHT"] != null ? Request["totalHT"].ToString() : "0";
            string NetHT = Request["NetHT"] != null ? Request["NetHT"].ToString() : "0";
            string totalTVA = Request["totalTVA"] != null ? Request["totalTVA"].ToString() : "0";
            string TotalTTC = Request["TotalTTC"] != null ? Request["TotalTTC"].ToString() : "0";
            string netAPaye = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            string designation = Request["designation"] != null ? Request["designation"].ToString() : string.Empty;
            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
            if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedDevis = string.Empty;
            if (Session["ProduitsDevisFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisFournisseur"];
            }
            if (Mode == "Create")
            {
                if (!db.DEVIS_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    DEVIS_FOURNISSEURS DevisClient = new DEVIS_FOURNISSEURS();
                    DevisClient.CODE = Numero;
                    DevisClient.DATE = DateTime.Parse(date);
                    DevisClient.Validite = DateTime.Parse(date2);
                    DevisClient.FOURNISSEUR = int.Parse(fournisseur);
                    //int ID_CLIENT = int.Parse(client);
                    //DevisClient.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    DevisClient.Societes = 3;
                    int ID_Soc = DevisClient.Societes;
                    DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        DevisClient.Tiers = int.Parse(Tiers);
                    }
                    else
                    {
                        DevisClient.Tiers = int.Parse(fournisseur);
                    }
                    DevisClient.MODE_PAIEMENT = modePaiement;
                    DevisClient.Designation = designation;
                    DevisClient.convert = false;
                    DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    DevisClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    DevisClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    DevisClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    db.DEVIS_FOURNISSEURS.Add(DevisClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        //Prix_Achat prixAchat = new Prix_Achat();
                        //prixAchat.Libelle = Ligne.LIBELLE;
                        //prixAchat.Designation = Ligne.DESIGNATION;
                        //prixAchat.Marque = Ligne.MARQUE;
                        //prixAchat.Devise = Ligne.DEVISE;
                        //prixAchat.Unite = Ligne.UNITE;
                        //prixAchat.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().CentreID;
                        ////Lignedfrs.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().Libelle;
                        //if (Ligne.Sous_CATEGORIE == "")
                        //{
                        //    prixAchat.Sous_Categorie = null;
                        //}
                        //else
                        //{
                        //    List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
                        //    foreach (Sous_Categorie sc in List)
                        //    {
                        //        if (sc.Libelle == Ligne.Sous_CATEGORIE)
                        //        {
                        //            prixAchat.Sous_Categorie = sc.CatID;
                        //        }
                        //    }
                        //}                        
                        ////prixAchat. = Ligne.QUANTITE;
                        //prixAchat.Stock = 0;
                        //prixAchat.Remise = Ligne.REMISE;
                        //prixAchat.PU_HT_Sans_Remise = (double)Ligne.PRIX_VENTE_HT;
                        //prixAchat.Valeur_TVA = (Ligne.TVA).ToString();
                        //prixAchat.PU_TTC = (double)Ligne.TTC;
                        //prixAchat.DEVIS_FRS = DevisClient.ID;
                        ////prixAchat.Fournisseur = db.FOURNISSEURS.Where(pr => pr.NOM == fournisseur).FirstOrDefault().ID;
                        //db.Prix_Achat.Add(prixAchat);
                        //db.SaveChanges();

                        LIGNES_DEVIS_FOURNISSEURS Lignedfrs = new LIGNES_DEVIS_FOURNISSEURS();
                        //Prix_Achat prixAchat = new Prix_Achat();
                        //Lignedfrs.Prix_achat = prixAchat.p;
                        Lignedfrs.Libelle_Prd = Ligne.LIBELLE;
                        Lignedfrs.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        Lignedfrs.Marque = Ligne.MARQUE;
                        Lignedfrs.Devise = Ligne.DEVISE;
                        Lignedfrs.Unite = Ligne.UNITE;
                        Lignedfrs.Categorie = Ligne.CATEGORIE;
                        if (Ligne.Sous_CATEGORIE == "")
                        {
                            Lignedfrs.Sous_Categorie = null;
                        }
                        else
                        {
                            Lignedfrs.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        }

                        //prixAchat. = Ligne.QUANTITE;
                        //Lignedfrs.Stock = 0;
                        Lignedfrs.REMISE = Ligne.REMISE;
                        Lignedfrs.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        Lignedfrs.TVA = Ligne.TVA;
                        Lignedfrs.TOTALE_TTC = Ligne.TTC;
                        Lignedfrs.QUANTITE = Ligne.QUANTITE;
                        Lignedfrs.TOTALE_HT = Ligne.PTHT;
                        Lignedfrs.TOTALE_TTC = Ligne.TTC;
                        Lignedfrs.DEVIS_CLIENT = DevisClient.ID;
                        db.LIGNES_DEVIS_FOURNISSEURS.Add(Lignedfrs);
                        db.SaveChanges();
                        //AddMouvementProduit("DEVIS", Produit, DevisClient.DATE, DevisClient.CODE, Ligne.QUANTITE);
                    }
                    SelectedDevis = DevisClient.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                DEVIS_FOURNISSEURS DevisClient = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                DevisClient.CODE = Numero;
                DevisClient.DATE = DateTime.Parse(date);
                DevisClient.Validite = DateTime.Parse(date2);

                DevisClient.FOURNISSEUR = int.Parse(fournisseur);
                int ID_CLIENT = int.Parse(fournisseur);
                DevisClient.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                DevisClient.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    DevisClient.Tiers = int.Parse(Tiers);
                }
                else
                {
                    DevisClient.Tiers = int.Parse(fournisseur);
                }
                DevisClient.MODE_PAIEMENT = modePaiement;
                DevisClient.Designation = designation;
                DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                DevisClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                DevisClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                DevisClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_DEVIS_FOURNISSEURS.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DEVIS_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_DEVIS_FOURNISSEURS Lignedfrs = new LIGNES_DEVIS_FOURNISSEURS();
                    
                    Lignedfrs.Libelle_Prd = Ligne.LIBELLE;
                    Lignedfrs.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    Lignedfrs.Marque = Ligne.MARQUE;
                    Lignedfrs.Devise = Ligne.DEVISE;
                    Lignedfrs.Unite = Ligne.UNITE;
                    Lignedfrs.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().Libelle;
                    
                    if (Ligne.Sous_CATEGORIE == "")
                    {
                        Lignedfrs.Sous_Categorie = null;
                    }
                    else
                    {
                        Lignedfrs.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    }
                    Lignedfrs.REMISE = Ligne.REMISE;
                    Lignedfrs.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    Lignedfrs.TVA = Ligne.TVA;
                    Lignedfrs.TOTALE_TTC = Ligne.TTC;
                    Lignedfrs.QUANTITE = Ligne.QUANTITE;
                    Lignedfrs.TOTALE_HT = Ligne.PTHT;
                    Lignedfrs.TOTALE_TTC = Ligne.TTC;
                    Lignedfrs.DEVIS_CLIENT = DevisClient.ID;
                    db.LIGNES_DEVIS_FOURNISSEURS.Add(Lignedfrs);
                    db.SaveChanges();
                   
                }

                SelectedDevis = DevisClient.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("InvoicePrint", new { CODE = SelectedDevis });
            }
            Session["ProduitsDevisFournisseur"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Devis", new { MODE = Mode });
        }
        //    if (Mode == "Create")
        //    {
        //        if (!db.DEVIS_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
        //        {
        //            DEVIS_FOURNISSEURS DevisClient = new DEVIS_FOURNISSEURS();
        //            DevisClient.CODE = Numero;
        //            DevisClient.DATE = DateTime.Parse(date);
        //            DevisClient.FOURNISSEUR = int.Parse(fournisseur);
        //            //int ID_CLIENT = int.Parse(client);
        //            //DevisClient.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
        //            DevisClient.Societes = 3;
        //            int ID_Soc = DevisClient.Societes;
        //            DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
        //            if (Tiers != "")
        //            {
        //                DevisClient.Tiers = int.Parse(Tiers);
        //            }
        //            else
        //            {
        //                DevisClient.Tiers = int.Parse(fournisseur);
        //            }
        //            DevisClient.MODE_PAIEMENT = modePaiement;
        //            DevisClient.Designation = designation;
        //            DevisClient.convert = false;
        //            DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
        //            DevisClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
        //            DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
        //            DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
        //            DevisClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
        //            DevisClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
        //            db.DEVIS_FOURNISSEURS.Add(DevisClient);
        //            db.SaveChanges();
        //            foreach (LigneProduit Ligne in ListeDesPoduits)
        //            {
        //                //Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.LIBELLE).FirstOrDefault();
        //                Prix_Achat prixAchat = new Prix_Achat();
        //                prixAchat.Libelle = Ligne.LIBELLE;
        //                prixAchat.Designation = Ligne.DESIGNATION;
        //                prixAchat.Marque = Ligne.MARQUE;
        //                prixAchat.Devise = Ligne.DEVISE;
        //                prixAchat.Unite = Ligne.UNITE;

        //                prixAchat.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().CentreID;
        //                //Lignedfrs.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().Libelle;
        //                if (Ligne.Sous_CATEGORIE == "")
        //                {
        //                    prixAchat.Sous_Categorie = null;
        //                }
        //                else
        //                {
        //                    List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
        //                    foreach (Sous_Categorie sc in List)
        //                    {
        //                        if (sc.Libelle == Ligne.Sous_CATEGORIE)
        //                        {
        //                            prixAchat.Sous_Categorie = sc.CatID;
        //                        }
        //                    }
        //                }                        //prixAchat. = Ligne.QUANTITE;
        //                prixAchat.Stock = 0;
        //                prixAchat.Remise = Ligne.REMISE;
        //                prixAchat.PU_HT_Sans_Remise = (double)Ligne.PRIX_VENTE_HT;
        //                prixAchat.Valeur_TVA = (Ligne.TVA).ToString();
        //                prixAchat.PU_TTC = (double)Ligne.TTC;
        //                prixAchat.DEVIS_FRS = DevisClient.ID;
        //                //prixAchat.Fournisseur = db.FOURNISSEURS.Where(pr => pr.NOM == fournisseur).FirstOrDefault().ID;
        //                db.Prix_Achat.Add(prixAchat);
        //                db.SaveChanges();
        //                LIGNES_DEVIS_FOURNISSEURS Lignedfrs = new LIGNES_DEVIS_FOURNISSEURS();
        //                Lignedfrs.Prix_achat = prixAchat.Product_ID;
        //                Lignedfrs.Libelle_Prd = Ligne.LIBELLE;
        //                Lignedfrs.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
        //                Lignedfrs.Marque = Ligne.MARQUE;
        //                Lignedfrs.Devise = Ligne.DEVISE;
        //                Lignedfrs.Unite = Ligne.UNITE;
        //                Lignedfrs.Categorie = Ligne.CATEGORIE;

        //                if (Ligne.Sous_CATEGORIE == "")
        //                {
        //                    Lignedfrs.Sous_Categorie = null;
        //                }
        //                else
        //                {
        //                    Lignedfrs.Sous_Categorie = Ligne.Sous_CATEGORIE;
        //                }

        //                //prixAchat. = Ligne.QUANTITE;
        //                //Lignedfrs.Stock = 0;
        //                Lignedfrs.REMISE = Ligne.REMISE;
        //                Lignedfrs.PRIX_UNITAIRE_HT =Ligne.PRIX_VENTE_HT;
        //                Lignedfrs.TVA = Ligne.TVA;
        //                Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //                Lignedfrs.QUANTITE = (double)Ligne.QUANTITE;
        //                Lignedfrs.TOTALE_HT = Ligne.PTHT;
        //                Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //                Lignedfrs.DEVIS_CLIENT = DevisClient.ID;
        //                db.LIGNES_DEVIS_FOURNISSEURS.Add(Lignedfrs);
        //                db.SaveChanges();
        //                //AddMouvementProduit("DEVIS", Produit, DevisClient.DATE, DevisClient.CODE, Ligne.QUANTITE);
        //            }
        //            SelectedDevis = DevisClient.ID.ToString();
        //        }
        //    }
        //    if (Mode == "Edit")
        //    {
        //        int ID = int.Parse(Code);
        //        DEVIS_FOURNISSEURS DevisClient = db.DEVIS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
        //        DevisClient.CODE = Numero;
        //        DevisClient.DATE = DateTime.Parse(date);
        //        DevisClient.FOURNISSEUR = int.Parse(fournisseur);
        //        int ID_CLIENT = int.Parse(fournisseur);
        //        DevisClient.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
        //        DevisClient.Societes =3;
        //        //int ID_Soc = int.Parse(societe);
        //        //DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
        //        if (Tiers != "")
        //        {
        //            DevisClient.Tiers = int.Parse(Tiers);
        //        }
        //        else
        //        {
        //            DevisClient.Tiers = int.Parse(fournisseur);
        //        }
        //        DevisClient.MODE_PAIEMENT = modePaiement;
        //        DevisClient.Designation = designation;
        //        DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
        //        DevisClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
        //        DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
        //        DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
        //        DevisClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
        //        DevisClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
        //        db.SaveChanges();
        //        db.LIGNES_DEVIS_FOURNISSEURS.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DEVIS_FOURNISSEURS.Remove(p));
        //        db.SaveChanges();
        //        foreach (LigneProduit Ligne in ListeDesPoduits)
        //        {
        //            Prix_Achat prA = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.LIBELLE && pr.DEVIS_FRS==ID).FirstOrDefault();
        //            if(prA==null)
        //            { 
        //            Prix_Achat prixAchat = new Prix_Achat();
        //            prixAchat.Libelle = Ligne.LIBELLE;
        //            prixAchat.Designation = Ligne.DESIGNATION;
        //            prixAchat.Marque = Ligne.MARQUE;
        //            prixAchat.Devise = Ligne.DEVISE;
        //            prixAchat.Unite = Ligne.UNITE;
        //            //prixAchat.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().CentreID;
        //            //prixAchat.Sous_Categorie = db.Sous_Categorie.Where(pr => pr.Libelle == Ligne.Sous_CATEGORIE).FirstOrDefault().CatID;
        //            //prixAchat. = Ligne.QUANTITE;
        //            if (Ligne.Sous_CATEGORIE == "")
        //            {
        //                prixAchat.Sous_Categorie = null;
        //            }
        //            else
        //            {
        //                List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
        //                foreach (Sous_Categorie sc in List)
        //                {
        //                    if (sc.Libelle == Ligne.Sous_CATEGORIE)
        //                    {
        //                        prixAchat.Sous_Categorie = sc.CatID;
        //                    }
        //                }
        //            }
        //            prixAchat.Stock = 0;
        //            prixAchat.Remise = Ligne.REMISE;
        //            prixAchat.PU_HT_Sans_Remise = (double)Ligne.PRIX_VENTE_HT;
        //            prixAchat.Valeur_TVA = (Ligne.TVA).ToString();
        //            prixAchat.PU_TTC = (double)Ligne.TTC;
        //            prixAchat.DEVIS_FRS = DevisClient.ID;
        //            //prixAchat.Fournisseur = db.FOURNISSEURS.Where(pr => pr.NOM == fournisseur).FirstOrDefault().ID;
        //            db.Prix_Achat.Add(prixAchat);
        //            db.SaveChanges();
        //            LIGNES_DEVIS_FOURNISSEURS Lignedfrs = new LIGNES_DEVIS_FOURNISSEURS();
        //            Lignedfrs.Prix_achat = prixAchat.Product_ID;
        //            Lignedfrs.Libelle_Prd = Ligne.LIBELLE;
        //            Lignedfrs.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
        //            Lignedfrs.Marque = Ligne.MARQUE;
        //            Lignedfrs.Devise = Ligne.DEVISE;
        //            Lignedfrs.Unite = Ligne.UNITE;
        //            Lignedfrs.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().Libelle;
        //            //Lignedfrs.Sous_Categorie = db.Sous_Categorie.Where(pr => pr.Libelle == Ligne.Sous_CATEGORIE).FirstOrDefault().Libelle;
        //            //prixAchat. = Ligne.QUANTITE;
        //            //Lignedfrs.Stock = 0;
        //            if (Ligne.Sous_CATEGORIE == "")
        //            {
        //                Lignedfrs.Sous_Categorie = null;
        //            }
        //            else
        //            {
        //                Lignedfrs.Sous_Categorie = Ligne.Sous_CATEGORIE;
        //            }
        //            Lignedfrs.REMISE = Ligne.REMISE;
        //            Lignedfrs.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
        //            Lignedfrs.TVA = Ligne.TVA;
        //            Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //            Lignedfrs.QUANTITE = (double)Ligne.QUANTITE;
        //            Lignedfrs.TOTALE_HT = Ligne.PTHT;
        //            Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //            Lignedfrs.DEVIS_CLIENT = DevisClient.ID;
        //            db.LIGNES_DEVIS_FOURNISSEURS.Add(Lignedfrs);
        //            db.SaveChanges();
        //                //UneLigne.DEVIS_FOURNISSEURS = DevisClient;
        //                //UneLigne.Prix_Achat1 = Prix_Achat;
        //            }
        //            else
        //            {
        //                LIGNES_DEVIS_FOURNISSEURS Lignedfrs = new LIGNES_DEVIS_FOURNISSEURS();
        //                Lignedfrs.Prix_achat = prA.Product_ID;
        //                Lignedfrs.Libelle_Prd = Ligne.LIBELLE;
        //                Lignedfrs.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
        //                Lignedfrs.Marque = Ligne.MARQUE;
        //                Lignedfrs.Devise = Ligne.DEVISE;
        //                Lignedfrs.Unite = Ligne.UNITE;
        //                Lignedfrs.Categorie = db.Categorie.Where(pr => pr.Libelle == Ligne.CATEGORIE).FirstOrDefault().Libelle;
        //                //Lignedfrs.Sous_Categorie = db.Sous_Categorie.Where(pr => pr.Libelle == Ligne.Sous_CATEGORIE).FirstOrDefault().Libelle;
        //                //prixAchat. = Ligne.QUANTITE;
        //                //Lignedfrs.Stock = 0;
        //                if (Ligne.Sous_CATEGORIE == "")
        //                {
        //                    Lignedfrs.Sous_Categorie = null;
        //                }
        //                else
        //                {
        //                    Lignedfrs.Sous_Categorie = Ligne.Sous_CATEGORIE;
        //                }
        //                Lignedfrs.REMISE = Ligne.REMISE;
        //                Lignedfrs.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
        //                Lignedfrs.TVA = Ligne.TVA;
        //                Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //                Lignedfrs.QUANTITE = (double)Ligne.QUANTITE;
        //                Lignedfrs.TOTALE_HT = Ligne.PTHT;
        //                Lignedfrs.TOTALE_TTC = Ligne.TTC;
        //                Lignedfrs.DEVIS_CLIENT = DevisClient.ID;
        //                db.LIGNES_DEVIS_FOURNISSEURS.Add(Lignedfrs);
        //                db.SaveChanges();
        //            }
        //        }
        //        SelectedDevis = DevisClient.ID.ToString();
        //    }
        //    if (Print)
        //    {
        //        return RedirectToAction("PrintDevisClientByID", new { CODE = SelectedDevis });
        //    }
        //    Session["ProduitsDevisFournisseur"] = null;
        //    ViewData["MODE"] = Mode;
        //    ViewBag.MODE = Mode;
        //    return RedirectToAction("Devis", new { MODE = Mode });
        //}
        [HttpPost]
        public ActionResult SendCommande(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string fournisseur = Request["fournisseur"] != null ? Request["fournisseur"].ToString() : string.Empty;
            //string societe = Request["Societes"] != null ? Request["Societes"].ToString() : string.Empty;
            string Tiers = Request["Tiers"] != null ? Request["Tiers"].ToString() : string.Empty;
            string modePaiement = Request["modePaiement"] != null ? Request["modePaiement"].ToString() : string.Empty;
            string remise = Request["remise"] != null ? Request["remise"].ToString() : string.Empty;
            string totalHT = Request["totalHT"] != null ? Request["totalHT"].ToString() : "0";
            string NetHT = Request["NetHT"] != null ? Request["NetHT"].ToString() : "0";
            string totalTVA = Request["totalTVA"] != null ? Request["totalTVA"].ToString() : "0";
            string TotalTTC = Request["TotalTTC"] != null ? Request["TotalTTC"].ToString() : "0";
            string netAPaye = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
            if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedCommande = string.Empty;
            if (Session["ProduitsCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeFournisseur"];
            }
            if (Mode == "Create")
            {
                if (!db.COMMANDES_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    COMMANDES_FOURNISSEURS CommandeFounisseur = new COMMANDES_FOURNISSEURS();
                    CommandeFounisseur.CODE = Numero;
                    CommandeFounisseur.DATE = DateTime.Parse(date);
                    CommandeFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                    int ID_FOURNISSEUR = int.Parse(fournisseur);
                    CommandeFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                    CommandeFounisseur.Societes = 3;
                    //int ID_Soc = int.Parse(societe);
                    //CommandeFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        CommandeFounisseur.Tiers = int.Parse(Tiers);
                    }
                    else
                    {
                        CommandeFounisseur.Tiers = int.Parse(fournisseur);
                    }
                    CommandeFounisseur.MODE_PAIEMENT = modePaiement;
                    CommandeFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    CommandeFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    CommandeFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    CommandeFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    CommandeFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    CommandeFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    db.COMMANDES_FOURNISSEURS.Add(CommandeFounisseur);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_COMMANDES_FOURNISSEURS UneLigne = new LIGNES_COMMANDES_FOURNISSEURS();
                        //UneLigne.Prix_achat = Ligne.ID;
                        //Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        UneLigne.Libelle_Prd = Ligne.LIBELLE;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.COMMANDE_FOURNISSEUR = CommandeFounisseur.ID;
                        UneLigne.COMMANDES_FOURNISSEURS = CommandeFounisseur;
                        //UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_COMMANDES_FOURNISSEURS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("COMMANDE", Produit, CommandeFounisseur.DATE, CommandeFounisseur.CODE, Ligne.QUANTITE);
                    }
                    SelectedCommande = CommandeFounisseur.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                COMMANDES_FOURNISSEURS CommandeFounisseur = db.COMMANDES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                CommandeFounisseur.CODE = Numero;
                CommandeFounisseur.DATE = DateTime.Parse(date);
                CommandeFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                int ID_FOURNISSEUR = int.Parse(fournisseur);
                CommandeFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                CommandeFounisseur.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //CommandeFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    CommandeFounisseur.Tiers = int.Parse(Tiers);
                }
                else
                {
                    CommandeFounisseur.Tiers = int.Parse(fournisseur);
                }
                CommandeFounisseur.MODE_PAIEMENT = modePaiement;
                CommandeFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                CommandeFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                CommandeFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                CommandeFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                CommandeFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                CommandeFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_COMMANDES_FOURNISSEURS.Where(p => p.COMMANDE_FOURNISSEUR == CommandeFounisseur.ID).ToList().ForEach(p => db.LIGNES_COMMANDES_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_COMMANDES_FOURNISSEURS UneLigne = new LIGNES_COMMANDES_FOURNISSEURS();
                    UneLigne.Prix_achat = Ligne.ID;
                    //Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    UneLigne.Libelle_Prd = Ligne.LIBELLE;
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.COMMANDE_FOURNISSEUR = CommandeFounisseur.ID;
                    UneLigne.COMMANDES_FOURNISSEURS = CommandeFounisseur;
                    //UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_COMMANDES_FOURNISSEURS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedCommande = CommandeFounisseur.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("PrintCommandeClientByID", new { CODE = SelectedCommande });
            }
            Session["ProduitsCommandeFournisseur"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Commandes", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendReception(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string fournisseur = Request["fournisseur"] != null ? Request["fournisseur"].ToString() : string.Empty;
            //string societe = Request["Societes"] != null ? Request["Societes"].ToString() : string.Empty;
            string Tiers = Request["Tiers"] != null ? Request["Tiers"].ToString() : string.Empty;
            string modePaiement = Request["modePaiement"] != null ? Request["modePaiement"].ToString() : string.Empty;
            string remise = Request["remise"] != null ? Request["remise"].ToString() : string.Empty;
            string totalHT = Request["totalHT"] != null ? Request["totalHT"].ToString() : "0";
            string NetHT = Request["NetHT"] != null ? Request["NetHT"].ToString() : "0";
            string totalTVA = Request["totalTVA"] != null ? Request["totalTVA"].ToString() : "0";
            string TotalTTC = Request["TotalTTC"] != null ? Request["TotalTTC"].ToString() : "0";
            string netAPaye = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
            if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedBonReception = string.Empty;
            if (Session["ProduitsBonCommandeFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonCommandeFournisseur"];
            }
            if (Mode == "Create")
            {
                if (!db.BONS_RECEPTIONS_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    BONS_RECEPTIONS_FOURNISSEURS BonReceptionFounisseur = new BONS_RECEPTIONS_FOURNISSEURS();
                    BonReceptionFounisseur.CODE = Numero;
                    BonReceptionFounisseur.DATE = DateTime.Parse(date);
                    BonReceptionFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                    int ID_FOURNISSEUR = int.Parse(fournisseur);
                    BonReceptionFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                    BonReceptionFounisseur.Societes = 3;
                    //int ID_Soc = int.Parse(societe);
                    //BonReceptionFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        BonReceptionFounisseur.Tiers = int.Parse(Tiers);
                    }
                    else
                    {
                        BonReceptionFounisseur.Tiers = int.Parse(fournisseur);
                    }
                    BonReceptionFounisseur.MODE_PAIEMENT = modePaiement;
                    BonReceptionFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    BonReceptionFounisseur.VALIDER = false;
                    db.BONS_RECEPTIONS_FOURNISSEURS.Add(BonReceptionFounisseur);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_BONS_RECEPTIONS_FOURNISSEURS UneLigne = new LIGNES_BONS_RECEPTIONS_FOURNISSEURS();
                        //Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.LIBELLE).FirstOrDefault();
                        //Prix_Achat prixAchat = new Prix_Achat();
                        //if (Produit == null)
                        //{
                        //    prixAchat.Libelle = Ligne.LIBELLE;
                        //    prixAchat.Designation = Ligne.DESIGNATION;
                        //    prixAchat.Marque = Ligne.MARQUE;
                        //    prixAchat.Devise = Ligne.DEVISE;
                        //    prixAchat.Unite = Ligne.UNITE;
                        //    if (Ligne.Sous_CATEGORIE == "")
                        //    {
                        //        prixAchat.Sous_Categorie = null;
                        //    }
                        //    else
                        //    {
                        //        List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
                        //        foreach (Sous_Categorie sc in List)
                        //        {
                        //            if (sc.Libelle == Ligne.Sous_CATEGORIE)
                        //            {
                        //                prixAchat.Sous_Categorie = sc.CatID;
                        //            }
                        //        }
                        //    }
                        //    prixAchat.Stock = (double)Ligne.QUANTITE;
                        //    prixAchat.Remise = Ligne.REMISE;
                        //    prixAchat.PU_HT_Sans_Remise = (double)Ligne.PRIX_VENTE_HT;
                        //    prixAchat.Valeur_TVA = (Ligne.TVA).ToString();
                        //    prixAchat.PU_TTC = (double)Ligne.TTC;
                        //    prixAchat.Fournisseur =int.Parse(fournisseur);
                        //    db.Prix_Achat.Add(prixAchat);
                        //    db.SaveChanges();
                        //    UneLigne.Prix_achat = prixAchat.Product_ID;

                        //}
                        //else
                        //{
                        //    Produit.Stock += (double)Ligne.QUANTITE;
                        //    UneLigne.Prix_achat = Produit.Product_ID;

                        //}
                        UneLigne.Libelle_Prd = Ligne.LIBELLE;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        if (Ligne.Sous_CATEGORIE == "")
                        {
                            UneLigne.Sous_Categorie = null;
                        }
                        else
                        {
                            UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        }
                        UneLigne.Stock = (double)Ligne.STOCK;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.BON_RECEPTION_FOURNISSEUR = BonReceptionFounisseur.ID;
                        UneLigne.BONS_RECEPTIONS_FOURNISSEURS = BonReceptionFounisseur;
                        //UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("BON_RECEPTION", Produit, BonReceptionFounisseur.DATE, BonReceptionFounisseur.CODE, Ligne.QUANTITE);
                    }
                    SelectedBonReception = BonReceptionFounisseur.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                BONS_RECEPTIONS_FOURNISSEURS BonReceptionFounisseur = db.BONS_RECEPTIONS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                BonReceptionFounisseur.CODE = Numero;
                BonReceptionFounisseur.DATE = DateTime.Parse(date);
                BonReceptionFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                int ID_FOURNISSEUR = int.Parse(fournisseur);
                BonReceptionFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                BonReceptionFounisseur.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //BonReceptionFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    BonReceptionFounisseur.Tiers = int.Parse(Tiers);
                }
                else
                {
                    BonReceptionFounisseur.Tiers = int.Parse(fournisseur);
                }
                BonReceptionFounisseur.MODE_PAIEMENT = modePaiement;
                BonReceptionFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                BonReceptionFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                BonReceptionFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                BonReceptionFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                BonReceptionFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                BonReceptionFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(p => p.BON_RECEPTION_FOURNISSEUR == BonReceptionFounisseur.ID).ToList().ForEach(p => db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_BONS_RECEPTIONS_FOURNISSEURS UneLigne = new LIGNES_BONS_RECEPTIONS_FOURNISSEURS();
                    //UneLigne.Prix_achat = Ligne.ID;
                    //Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.LIBELLE).FirstOrDefault();
                    //Prix_Achat prixAchat = new Prix_Achat();
                    //if (Produit == null)
                    //{
                    //    prixAchat.Designation = Ligne.DESIGNATION;
                    //    prixAchat.Marque = Ligne.MARQUE;
                    //    prixAchat.Devise = Ligne.DEVISE;
                    //    prixAchat.Unite = Ligne.UNITE;
                    //    if (Ligne.Sous_CATEGORIE == "")
                    //    {
                    //        prixAchat.Sous_Categorie = null;
                    //    }
                    //    else
                    //    {
                    //        List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
                    //        foreach (Sous_Categorie sc in List)
                    //        {
                    //            if (sc.Libelle == Ligne.Sous_CATEGORIE)
                    //            {
                    //                prixAchat.Sous_Categorie = sc.CatID;
                    //            }
                    //        }
                    //    }
                    //    prixAchat.Stock = (double)Ligne.QUANTITE;
                    //    prixAchat.Remise = (int)Ligne.REMISE;
                    //    prixAchat.PU_HT_Sans_Remise = (double)Ligne.PRIX_VENTE_HT;
                    //    prixAchat.Valeur_TVA = (Ligne.TVA).ToString();
                    //    prixAchat.PU_TTC = (double)Ligne.TTC;
                    //    //prixAchat.Fournisseur = db.FOURNISSEURS.Where(pr => pr.NOM == fournisseur).FirstOrDefault().ID;
                    //    prixAchat.Libelle = Ligne.LIBELLE;
                    //    db.Prix_Achat.Add(prixAchat);
                    //    db.SaveChanges();
                    //    UneLigne.Prix_achat = prixAchat.Product_ID;

                    //}
                    //else
                    //{
                    //    Produit.Stock = (double)Ligne.STOCK;
                    //    UneLigne.Prix_achat = Produit.Product_ID;
                    //}

                    UneLigne.Libelle_Prd = Ligne.LIBELLE;
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.Stock = (double)Ligne.STOCK;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.BON_RECEPTION_FOURNISSEUR = BonReceptionFounisseur.ID;
                    UneLigne.BONS_RECEPTIONS_FOURNISSEURS = BonReceptionFounisseur;
                    //UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedBonReception = BonReceptionFounisseur.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("PrintBonReceptionClientByID", new { CODE = SelectedBonReception });
            }
            Session["ProduitsBonCommandeFournisseur"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("BonReception", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendFacture(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string numerofact = Request["numerofact"] != null ? Request["numerofact"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string fournisseur = Request["fournisseur"] != null ? Request["fournisseur"].ToString() : string.Empty;
            //string societe = Request["Societes"] != null ? Request["Societes"].ToString() : string.Empty;
            string Tiers = Request["Tiers"] != null ? Request["Tiers"].ToString() : string.Empty;
            string modePaiement = Request["modePaiement"] != null ? Request["modePaiement"].ToString() : string.Empty;
            string remise = Request["remise"] != null ? Request["remise"].ToString() : string.Empty;
            string totalHT = Request["totalHT"] != null ? Request["totalHT"].ToString() : "0";
            string NetHT = Request["NetHT"] != null ? Request["NetHT"].ToString() : "0";
            string totalTVA = Request["totalTVA"] != null ? Request["totalTVA"].ToString() : "0";
            string TotalTTC = Request["TotalTTC"] != null ? Request["TotalTTC"].ToString() : "0";
            string netAPaye = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            string timbre = Request["timbre"] != null ? Request["timbre"].ToString() : "0";
            string REDEVANCE = Request["REDEVANCE"] != null ? Request["REDEVANCE"].ToString() : string.Empty;
            string FODEC = Request["FODEC"] != null ? Request["FODEC"].ToString() : string.Empty;
            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
            if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
            if (string.IsNullOrEmpty(REDEVANCE)) REDEVANCE = "0";
            if (string.IsNullOrEmpty(FODEC)) FODEC = "0";
            if (string.IsNullOrEmpty(numerofact)) numerofact = "0";


            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedFacture = string.Empty;
            if (Session["ProduitsFactureFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureFournisseur"];
            }
            if (Mode == "Create")
            {
                if (!db.FACTURES_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    FACTURES_FOURNISSEURS FactureFounisseur = new FACTURES_FOURNISSEURS();
                    FactureFounisseur.CODE = Numero;
                    FactureFounisseur.Num_Fact = numerofact;
                    FactureFounisseur.Redevance = decimal.Parse(REDEVANCE, CultureInfo.InvariantCulture);
                    FactureFounisseur.FODEC = decimal.Parse(FODEC, CultureInfo.InvariantCulture);
                    FactureFounisseur.DATE = DateTime.Parse(date);
                    FactureFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                    int ID_FOURNISSEUR = int.Parse(fournisseur);
                    FactureFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                    FactureFounisseur.Societes = 3;
                    //int ID_Soc = int.Parse(societe);
                    //FactureFounisseur.Societes1 = db.Societes.NewLine.ID_Bl = Idf;Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        FactureFounisseur.Tiers = int.Parse(Tiers);
                    }
                    else
                    {
                        FactureFounisseur.Tiers = int.Parse(fournisseur);
                    }
                    FactureFounisseur.MODE_PAIEMENT = modePaiement;
                    FactureFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    FactureFounisseur.FODEC = decimal.Parse(FODEC, CultureInfo.InvariantCulture);
                    FactureFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    FactureFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    FactureFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    FactureFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    FactureFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    FactureFounisseur.TIMBRE = decimal.Parse(timbre, CultureInfo.InvariantCulture);
                    FactureFounisseur.VALIDER = false;
                    FactureFounisseur.PAYEE = false;
                    db.FACTURES_FOURNISSEURS.Add(FactureFounisseur);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_FACTURES_FOURNISSEURS UneLigne = new LIGNES_FACTURES_FOURNISSEURS();
                        UneLigne.Prix_achat = Ligne.ID;
                        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        UneLigne.ID_Bl = Ligne.ID_Bl;
                        UneLigne.Libelle_Prd = Ligne.LIBELLE;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.FACTURE_FOURNISSEUR = FactureFounisseur.ID;
                        //UneLigne.FACTURES_FOURNISSEURS = FactureFounisseur;
                        //UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_FACTURES_FOURNISSEURS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("FACTURE", Produit, FactureFounisseur.DATE, FactureFounisseur.CODE, Ligne.QUANTITE);
                    }
                    SelectedFacture = FactureFounisseur.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                FACTURES_FOURNISSEURS FactureFounisseur = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                FactureFounisseur.CODE = Numero;
                FactureFounisseur.Num_Fact = numerofact;
                FactureFounisseur.Redevance = decimal.Parse(REDEVANCE, CultureInfo.InvariantCulture);
                FactureFounisseur.FODEC = decimal.Parse(FODEC, CultureInfo.InvariantCulture);
                FactureFounisseur.DATE = DateTime.Parse(date);
                FactureFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                int ID_FOURNISSEUR = int.Parse(fournisseur);
                FactureFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                FactureFounisseur.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //FactureFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    FactureFounisseur.Tiers = int.Parse(Tiers);
                }
                else
                {
                    FactureFounisseur.Tiers = int.Parse(fournisseur);
                }
                FactureFounisseur.MODE_PAIEMENT = modePaiement;
                FactureFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                FactureFounisseur.FODEC = decimal.Parse(FODEC, CultureInfo.InvariantCulture);
                FactureFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                FactureFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                FactureFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                FactureFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                FactureFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                FactureFounisseur.TIMBRE = decimal.Parse(timbre, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_FACTURES_FOURNISSEURS.Where(p => p.FACTURE_FOURNISSEUR == FactureFounisseur.ID).ToList().ForEach(p => db.LIGNES_FACTURES_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_FACTURES_FOURNISSEURS UneLigne = new LIGNES_FACTURES_FOURNISSEURS();
                    UneLigne.Prix_achat = Ligne.ID;
                    Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    UneLigne.ID_Bl = Ligne.ID_Bl;
                    UneLigne.Libelle_Prd = Ligne.LIBELLE;
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.FACTURE_FOURNISSEUR = FactureFounisseur.ID;
                    //UneLigne.FACTURES_FOURNISSEURS = FactureFounisseur;
                    //UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_FACTURES_FOURNISSEURS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedFacture = FactureFounisseur.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("PrintBonFactureByID", new { CODE = SelectedFacture });
            }
            Session["ProduitsFactureFournisseur"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Facture", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendAvoir(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string fournisseur = Request["fournisseur"] != null ? Request["fournisseur"].ToString() : string.Empty;
            //string societe = Request["Societes"] != null ? Request["Societes"].ToString() : string.Empty;
            string Tiers = Request["Tiers"] != null ? Request["Tiers"].ToString() : string.Empty;
            string modePaiement = Request["modePaiement"] != null ? Request["modePaiement"].ToString() : string.Empty;
            string remise = Request["remise"] != null ? Request["remise"].ToString() : string.Empty;
            string totalHT = Request["totalHT"] != null ? Request["totalHT"].ToString() : "0";
            string NetHT = Request["NetHT"] != null ? Request["NetHT"].ToString() : "0";
            string totalTVA = Request["totalTVA"] != null ? Request["totalTVA"].ToString() : "0";
            string TotalTTC = Request["TotalTTC"] != null ? Request["TotalTTC"].ToString() : "0";
            string netAPaye = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            string timbre = Request["timbre"] != null ? Request["timbre"].ToString() : "0";

            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
            if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
            if (string.IsNullOrEmpty(timbre)) timbre = "0";
            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedAvoir = string.Empty;
            if (Session["ProduitsAvoirFournisseur"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirFournisseur"];
            }
            if (Mode == "Create")
            {
                if (!db.AVOIRS_FOURNISSEURS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    AVOIRS_FOURNISSEURS AvoirFounisseur = new AVOIRS_FOURNISSEURS();
                    AvoirFounisseur.CODE = Numero;
                    AvoirFounisseur.DATE = DateTime.Parse(date);
                    AvoirFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                    int ID_FOURNISSEUR = int.Parse(fournisseur);
                    AvoirFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                    AvoirFounisseur.Societes = 3;
                    //int ID_Soc = int.Parse(societe);
                    //AvoirFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        AvoirFounisseur.Tiers = int.Parse(Tiers);
                    }
                    else
                    {
                        AvoirFounisseur.Tiers = int.Parse(fournisseur);
                    }
                    AvoirFounisseur.MODE_PAIEMENT = modePaiement;
                    AvoirFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    AvoirFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    AvoirFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    AvoirFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    AvoirFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    AvoirFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    AvoirFounisseur.VALIDER = false;
                    db.AVOIRS_FOURNISSEURS.Add(AvoirFounisseur);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_AVOIRS_FOURNISSEURS UneLigne = new LIGNES_AVOIRS_FOURNISSEURS();
                        UneLigne.Prix_achat = Ligne.ID;
                        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        Produit.Stock -= (double)Ligne.QUANTITE;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.AVOIR_FOURNISSEUR = AvoirFounisseur.ID;
                        UneLigne.AVOIRS_FOURNISSEURS = AvoirFounisseur;
                        UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_AVOIRS_FOURNISSEURS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("AVOIR", Produit, AvoirFounisseur.DATE, AvoirFounisseur.CODE, Ligne.QUANTITE);
                    }
                    SelectedAvoir = AvoirFounisseur.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                AVOIRS_FOURNISSEURS AvoirFounisseur = db.AVOIRS_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                AvoirFounisseur.CODE = Numero;
                AvoirFounisseur.DATE = DateTime.Parse(date);
                AvoirFounisseur.FOURNISSEUR = int.Parse(fournisseur);
                int ID_FOURNISSEUR = int.Parse(fournisseur);
                AvoirFounisseur.FOURNISSEURS = db.FOURNISSEURS.Where(fou => fou.ID == ID_FOURNISSEUR).FirstOrDefault();
                AvoirFounisseur.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //AvoirFounisseur.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    AvoirFounisseur.Tiers = int.Parse(Tiers);
                }
                else
                {
                    AvoirFounisseur.Tiers = int.Parse(fournisseur);
                }
                AvoirFounisseur.MODE_PAIEMENT = modePaiement;
                AvoirFounisseur.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                AvoirFounisseur.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                AvoirFounisseur.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                AvoirFounisseur.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                AvoirFounisseur.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                AvoirFounisseur.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_AVOIRS_FOURNISSEURS.Where(p => p.AVOIR_FOURNISSEUR == AvoirFounisseur.ID).ToList().ForEach(p => db.LIGNES_AVOIRS_FOURNISSEURS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_AVOIRS_FOURNISSEURS UneLigne = new LIGNES_AVOIRS_FOURNISSEURS();
                    UneLigne.Prix_achat = Ligne.ID;
                    Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    Produit.Stock -= (double)Ligne.QUANTITE;
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.AVOIR_FOURNISSEUR = AvoirFounisseur.ID;
                    UneLigne.AVOIRS_FOURNISSEURS = AvoirFounisseur;
                    UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_AVOIRS_FOURNISSEURS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedAvoir = AvoirFounisseur.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("PrintAvoirByID", new { CODE = SelectedAvoir });
            }
            Session["ProduitsAvoirFournisseur"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Avoir", new { MODE = Mode });
        }
        //public void AddMouvementProduit(string mode, Prix_Achat produit, DateTime date, string code, int quantite)
        //{
        //    MOUVEMENETS_PRODUITS UnMouvement = new MOUVEMENETS_PRODUITS();
        //    UnMouvement.Prix_achat = produit.Product_ID;
        //    UnMouvement.Prix_Achat1 = produit;
        //    UnMouvement.DATE_MOUVEMENT = date;
        //    UnMouvement.TYPE_MOUVEMENT = mode;
        //    UnMouvement.CODE_MOUVEMENT = code;
        //    UnMouvement.QUANTITE_MOUVEMENT = quantite;
        //    UnMouvement.QUANTITE_AVANT_MOUVEMENT = (int)produit.QUANTITE;
        //    UnMouvement.QUANTITE_APRES_MOUVEMENT = (int)produit.QUANTITE;
        //    if (mode == "BON_RECEPTION")
        //    {
        //        UnMouvement.QUANTITE_APRES_MOUVEMENT = (int)produit.QUANTITE + quantite;
        //        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == produit.Product_ID).FirstOrDefault();
        //        Produit.QUANTITE = Produit.QUANTITE + quantite;
        //        db.SaveChanges();
        //    }
        //    db.MOUVEMENETS_PRODUITS.Add(UnMouvement);
        //    db.SaveChanges();

        //}
        //convertTo a technical project
        //#region convertTo a technical project  
        //[HttpPost]
        //public ActionResult Convertprj(FormCollection formCollection)
        //{
        //    string[] ids = formCollection["affComId"].Split(new char[] { ',' });
        //    foreach (string Id in ids)
        //    {
        //        var COMMANDES_FOURNISSEURS = this.db.COMMANDES_FOURNISSEURS.Find(Convert.ToInt32(Id));
        //        Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

        //        this.db.SaveChanges();
        //        var projetTechnique = new ProjetTechnique()
        //        {

        //            ProjetTechniqueId = COMMANDES_FOURNISSEURS.ID,
        //            ReferenceTech = param.RefTech + param.CompteurTech,
        //            //DateDebut = COMMANDES_FOURNISSEURS.DateDebut,
        //            //DateFin = COMMANDES_FOURNISSEURS.DateFin,
        //            //Cout = COMMANDES_FOURNISSEURS.Cout,
        //            ClientId = COMMANDES_FOURNISSEURS.FOURNISSEUR,
        //            //PersonnelId = COMMANDES_FOURNISSEURS.PersonnelId,
        //            //Designation = COMMANDES_FOURNISSEURS.Designation,
        //            Statut = "Initié"

        //        };
        //        this.db.COMMANDES_FOURNISSEURS.Remove(COMMANDES_FOURNISSEURS);
        //        //this.db.ProjetTechniques.Add(projetTechnique);


        //        try
        //        {
        //            var pt = db.ProjetTechniques
        //                .OrderByDescending(p => p.ProjetTechniqueId)
        //                .FirstOrDefault();
        //            String ch = pt.ReferenceTech.ToString();
        //            projetTechnique.ReferenceTech = param.RefTech + param.CompteurTech;
        //            param.CompteurTech = param.CompteurTech + 1;




        //        }
        //        catch
        //        {
        //            COMMANDES_FOURNISSEURS.CODE = param.RefCom + param.CompteurCom;
        //            param.CompteurTech = param.CompteurTech + 1;
        //        }


        //        this.db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        //#endregion
    }
}