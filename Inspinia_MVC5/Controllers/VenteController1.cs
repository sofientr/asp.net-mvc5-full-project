using CrystalDecisions.CrystalReports.Engine;
using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Inspinia_MVC5.Controllers
{
    public class VenteController : Controller
    {
        // GET: Vente
        // GET: /Vente/C:\Users\dell\Desktop\MVC5_Full_Version -jeudi  29-06-2017\Inspinia_MVC5\Controllers\VenteController.cs
        //GestionCommercialeEntity db = new GestionCommercialeEntity();
        private Tr db = new Tr();
        #region Views
        public ActionResult Client(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {
            int count = db.CLIENTS.Select(cmd => cmd.ID).Count();
            //int Nb = 0;
            //List<CLIENTS> frs = db.CLIENTS.ToList();
            //foreach (CLIENTS f in frs)
            //{
            //    string[] con = f.CODE.Split('C');
            //    int con1 = int.Parse(con[1]);
            //    if (con1 == count)
            //    {

            //        Nb++;

            //    }

            //}
            //if (Nb > 0)
            //{
            //    ViewBag.Numero = "C" + (count + 1);
            //}
            //else
            //{
            //    ViewBag.Numero = "C" + count;

            //}

            CLIENTS frnds = new CLIENTS();
            ViewBag.Numero = "C" + count;
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.numeroo = numero;
            ViewBag.Date = Date;
            ViewBag.designation = designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            return PartialView("Client", frnds);
        }
        public ActionResult Devis(string Mode)
        {
            Session["ProduitsDevisClient"] = null;
            Session["ProduitsDevisClient2"] = null;

            List<DEVIS_CLIENTS> Liste = db.DEVIS_CLIENTS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == com.CLIENT).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                 // SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TNET = com.TNET,
                                  //TIERS = db.Tiers.Where(fou => fou.TiersID == com.Tiers).FirstOrDefault().NOM,
                                  cc = db.COMMANDES_CLIENTS.Where(fou => fou.DEVIS_CLIENT == com.ID).FirstOrDefault(),

                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult Commandes(string Mode)
        {
            List<COMMANDES_CLIENTS> Liste = db.COMMANDES_CLIENTS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == com.CLIENT).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  TTC = com.TTC,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TNET = com.TNET,
                                  TIERS = db.Tiers.Where(fou => fou.TiersID == com.Tiers).FirstOrDefault().NOM,
                                  cc = db.BONS_LIVRAISONS_CLIENTS.Where(fou => fou.COMMANDE_CLIENT == com.ID).FirstOrDefault(),

                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult BonLivraison(string Mode)
        {
            List<BONS_LIVRAISONS_CLIENTS> Liste = db.BONS_LIVRAISONS_CLIENTS.ToList();
            dynamic Result = (from com in Liste
                              select new
                              {
                                  ID = com.ID,
                                  CODE = com.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == com.CLIENT).FirstOrDefault().NOM,
                                  DATE = com.DATE.ToShortDateString(),
                                  THT = com.NHT,
                                  TTVA = com.TTVA,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == com.Societes).FirstOrDefault().NOM,
                                  TTC = com.TTC,
                                  TNET = com.TNET,
                                  VALIDE = com.VALIDER,
                                  Type = com.Type,
                                  TIERS = db.Tiers.Where(fou => fou.TiersID == com.Tiers).FirstOrDefault().NOM,
                                  cc = db.FACTURES_CLIENTS.Where(fou => fou.BON_LIVRAISON_CLIENT == com.ID).FirstOrDefault(),

                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
       

        public ActionResult Avoir(string Mode)
        {
            List<AVOIRS_CLIENTS> Liste = db.AVOIRS_CLIENTS.ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Fact.CLIENT).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  //SOCIETE = db.Societes.Where(fou => fou.SociID == Fact.Societes).FirstOrDefault().NOM,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM,
                                  VALIDE = Fact.VALIDER,
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return View(Result);
        }
        public ActionResult Facture(string Mode)
        {
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Fact.CLIENT).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  Declar = Fact.Declaration,
                                  DateDeclar = Fact.Date_Declaration,

                                  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            

            return View(Result);
        }
        public ActionResult FactureDeclaration(string Mode)
        {
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            List<TVA> listtva = db.TVA.ToList();
            int count = listtva.Count()*2;
            dynamic Result = (from Fact in Liste
                              select new
                              {
                                  ID = Fact.ID,
                                  DateDeclar = Fact.Date_Declaration,

                                  CODE = Fact.CODE,
                                  //FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Fact.CLIENT).FirstOrDefault().NOM,
                                  //DATE = Fact.DATE.ToShortDateString(),
                                  //THT = Fact.NHT,
                                  //TTVA = Fact.TTVA,
                                  //TTC = Fact.TTC,
                                  //TNET = Fact.TNET,
                                  //VALIDE = Fact.VALIDER,
                                  //PAYEE = Fact.PAYEE,
                                  //Declar = Fact.Declaration,

                                  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM
                              }).AsEnumerable().Select(c => c.ToExpando());
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            ViewBag.count = count;

            return View(Result);
        }
        public ActionResult FacturePrdate(string date2)
        {
            string[] date22 = date2.Split(' ');
            string[] date33 = date22[0].Split('/');
            string mm = date33[1];
            string aaaa = date33[2];
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            List<FACTURES_CLIENTS> Liste2= new List<FACTURES_CLIENTS>();

            foreach (FACTURES_CLIENTS fact in Liste)
            { 
                string date=fact.DATE.ToString();
                string[] date44 = date.Split(' ');
                string[] date55 = date44[0].Split('/');
                string mm1 = date55[1];
                string aaaa1 = date55[2];
                if((mm1==mm)&&(aaaa==aaaa1))
                {
                    Liste2.Add(fact);
                }
            }
            dynamic Result = (from Fact in Liste2
                              select new
                              {
                                  ID = Fact.ID,
                                  CODE = Fact.CODE,
                                  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Fact.CLIENT).FirstOrDefault().NOM,
                                  DATE = Fact.DATE.ToShortDateString(),
                                  THT = Fact.NHT,
                                  TTVA = Fact.TTVA,
                                  TTC = Fact.TTC,
                                  TNET = Fact.TNET,
                                  VALIDE = Fact.VALIDER,
                                  PAYEE = Fact.PAYEE,
                                  Declar = Fact.Declaration,

                                  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM
                              }).AsEnumerable().Select(c => c.ToExpando()).Distinct();
            ViewBag.date = date2;

            return View(Result);
        }
        public ActionResult BonLivraisonPartiel(int? id)
        {
            ViewBag.Code = id;
            BONS_LIVRAISONS_CLIENTS bl = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == id).FirstOrDefault();

            var historique_Prix_Achat = new List<BONS_LIVRAISONS_PART_CLIENTS>();
            var hist_Prix_Achat = (from m in db.BONS_LIVRAISONS_PART_CLIENTS
                                   where m.IDBLC == id
                                   orderby m.ID
                                   select m
                                   );

            historique_Prix_Achat.AddRange(hist_Prix_Achat.Distinct());
            ViewBag.VALIDER = bl.VALIDER;
            foreach (BONS_LIVRAISONS_PART_CLIENTS blp in historique_Prix_Achat)
            {
                if (blp.Etat == true)
                {
                    ViewBag.ETAT = true;
                }
                else
                {
                    ViewBag.ETAT = false;
                }
            }
            return View(historique_Prix_Achat.ToList());
        }
        #endregion
        #region forms
        public ActionResult FormDevis(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            DEVIS_CLIENTS DevisClient = new DEVIS_CLIENTS();
            //int index;
            //int index1;

            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.Date = Date;
            ViewBag.numeroo = numero;
            ViewBag.designation =designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            string Numero = string.Empty;
            LigneProduit  ListeDesPoduits2 = (LigneProduit)Session["ProduitsDevisClient2"];
            if(ListeDesPoduits2!=null)
            {
                ViewBag.idd = ListeDesPoduits2.ID;
                ViewBag.lib = ListeDesPoduits2.LIBELLE;
                ViewBag.des = ListeDesPoduits2.DESIGNATION;
                ViewBag.NumD = ListeDesPoduits2.NumDevis;
                string ma1 = ListeDesPoduits2.MARQUE;
                
                    ViewBag.ma = ma1.Trim();
               
                ViewBag.un = ListeDesPoduits2.UNITE;
                ViewBag.dv = ListeDesPoduits2.DEVISE;
                
                string ca1 = ListeDesPoduits2.CATEGORIE;
              
                    ViewBag.ca = ca1.Trim();
               
                if(ListeDesPoduits2.Sous_CATEGORIE!=null)
                { 
                string sca1 = ListeDesPoduits2.Sous_CATEGORIE;
                ViewBag.sca = sca1.Trim();
                 
                }
                else
                {
                    ViewBag.sca = null;

                }
                ViewBag.qt = ListeDesPoduits2.QUANTITE;
                ViewBag.ttc = ListeDesPoduits2.TTC;
                ViewBag.pu = ListeDesPoduits2.PRIX_VENTE_HT;
                ViewBag.tv = ListeDesPoduits2.TVA;
                ViewBag.pth = ListeDesPoduits2.PTHT;
            }
           
            if (Mode == "Create")
            {
                Numero = "DVC";
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
                ViewBag.ListeDesPoduits = ListeDesPoduits;



            }
            if ((Mode == "Edit")||(Mode == "Aff"))
            {
                decimal somme = 0;
                int ID = int.Parse(Code);
                DevisClient = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = DevisClient.CODE;
                List<Tasks> tas = db.Tasks.Where(fou => fou.ProjetTechniquesID == ID).ToList();
                foreach(Tasks ta in tas)
                {
                    decimal c1 = (decimal)db.Personnels.Find(ta.owner_id).Cout_hor;
                    string c4 = ta.duration_h_planning.ToString();
                    int c2 = int.Parse(c4);
                    decimal c3 = c1 * c2;
                    somme = somme + c3;
                }
                List<LIGNES_DEVIS_CLIENTS> ListeLigne = db.LIGNES_DEVIS_CLIENTS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
                foreach (LIGNES_DEVIS_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Art_Devis_Frs;
                    NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.NumDevis = (ligne.LIGNES_DEVIS_FOURNISSEURS.DEVIS_CLIENT).ToString();
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (decimal)ligne.QUANTITE;
                    //NewLine.STOCK = (int)ligne.STOCK;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
                    NewLine.MARGE = (decimal)ligne.MARGE;
                    NewLine.REMISE = (decimal)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_CLIENT = DevisClient.CLIENTS.CODE;
                ViewBag.CODESOC = DevisClient.Societes;
                ViewBag.id = Code;
                ViewBag.somme = somme;

            }
            Session["ProduitsDevisClient"] = ListeDesPoduits;
            Session["ProduitsDevisClient2"] = null;


            ViewBag.Numero = Numero;
            return View(DevisClient);
        }
        public ActionResult FormCommande(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            COMMANDES_CLIENTS CommandeClient = new COMMANDES_CLIENTS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                int Max = 0;
                if (db.COMMANDES_CLIENTS.ToList().Count != 0)
                {
                    Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "CDC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            }
            if ((Mode == "Edit")|| (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = CommandeClient.CODE;
                List<LIGNES_COMMANDES_CLIENTS> ListeLigne = db.LIGNES_COMMANDES_CLIENTS.Where(lcmd => lcmd.COMMANDE_CLIENT == ID).ToList();
                foreach (LIGNES_COMMANDES_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (int)ligne.QUANTITE;
                    //NewLine.STOCK = (decimal)db.Prix_Achat.Where(fou=>fou.Libelle==NewLine.LIBELLE).FirstOrDefault().Stock;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_CLIENT = CommandeClient.CLIENTS.CODE;
                ViewBag.CODESOC = CommandeClient.Societes;
            }
            Session["ProduitsCommandeClient"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(CommandeClient);
        }
        public ActionResult FormBonLivraison(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
           
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                int Max = 0;
                if (db.BONS_LIVRAISONS_CLIENTS.ToList().Count != 0)
                {
                    Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "BLC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = BonLivraisonClient.CODE;
                List<LIGNES_BONS_LIVRAISONS_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();

                foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (int)ligne.QUANTITE;
                    Prix_Achat pr1 = db.Prix_Achat.Where(f => f.Libelle == NewLine.LIBELLE).FirstOrDefault();
                    if (pr1 != null)
                    {
                        NewLine.STOCK = (int)pr1.Stock;
                    }
                    else
                    {
                        NewLine.STOCK = 0;
                    }
                    //NewLine.STOCK = (int)ligne.STOCK;
                    NewLine.QUANTITERES = (int)ligne.QTERES;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
               
                ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
                ViewBag.CODESOC = BonLivraisonClient.Societes;
                
            }
            if(Mode == "Editcmd")
            {
                int ID = int.Parse(Code);
                BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = BonLivraisonClient.CODE;
                List<LIGNES_BONS_LIVRAISONS_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();

                foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (int)ligne.QUANTITE;
                    NewLine.QUANTITELiv = NewLine.QUANTITE;
                    Prix_Achat pr1 = db.Prix_Achat.Where(f => f.Libelle == NewLine.LIBELLE).FirstOrDefault();
                    if (pr1!=null)
                    { 
                    NewLine.STOCK = (int)pr1.Stock;
                    }
                    else
                    {
                        NewLine.STOCK = 0;
                    }
                    NewLine.QUANTITERES = (int)ligne.QTERES;
                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
              
                ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
                ViewBag.CODESOC = BonLivraisonClient.Societes;
                //ViewBag.Type = BonLivraisonClient.Type;
            }
            Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
            ViewBag.Numero = Numero;

            return View(BonLivraisonClient);
        }
        
        public ActionResult FormAvoir(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            AVOIRS_CLIENTS AvoirClient = new AVOIRS_CLIENTS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                int Max = 0;
                if (db.FACTURES_CLIENTS.ToList().Count != 0)
                {
                    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "AVC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = AvoirClient.CODE;
                List<LIGNES_AVOIRS_CLIENTS> ListeLigne = db.LIGNES_AVOIRS_CLIENTS.Where(lcmd => lcmd.AVOIR_CLIENT == ID).ToList();
                foreach (LIGNES_AVOIRS_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == NewLine.ID).FirstOrDefault().Designation;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    NewLine.QUANTITE = (int)ligne.QUANTITE;
                    NewLine.STOCK = (int)ligne.STOCK;

                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_CLIENT = AvoirClient.CLIENTS.CODE;
                ViewBag.CODESOC = AvoirClient.Societes;
            }
            Session["ProduitsAvoirClient"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(AvoirClient);
        }
        public ActionResult FormFacture(string Mode, string Code)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            FACTURES_CLIENTS FactureClient = new FACTURES_CLIENTS();
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            string Numero = string.Empty;
            if (Mode == "Create")
            {
                int Max = 0;
                if (db.FACTURES_CLIENTS.ToList().Count != 0)
                {
                    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "FC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            }
            if ((Mode == "Edit") || (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                FactureClient = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                Numero = FactureClient.CODE;
                List<LIGNES_FACTURES_CLIENTS> ListeLigne = db.LIGNES_FACTURES_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();
                foreach (LIGNES_FACTURES_CLIENTS ligne in ListeLigne)
                {
                    LigneProduit NewLine = new LigneProduit();
                    NewLine.ID = (int)ligne.Prix_achat;
                    NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
                    NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
                    NewLine.MARQUE = ligne.Marque;
                    NewLine.DEVISE = ligne.Devise;
                    NewLine.UNITE = ligne.Unite;
                    NewLine.CATEGORIE = ligne.Categorie;
                    NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
                    //NewLine.STOCK=ligne.st
                    NewLine.QUANTITE = (int)ligne.QUANTITE;
                    NewLine.STOCK = (int)ligne.STOCK;

                    NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = (int)ligne.REMISE;
                    NewLine.PTHT = (decimal)ligne.TOTALE_HT;
                    NewLine.TVA = (int)ligne.TVA;
                    NewLine.TTC = (decimal)ligne.TOTALE_TTC;
                    ListeDesPoduits.Add(NewLine);
                }
                ViewBag.CODE_CLIENT = FactureClient.CLIENTS.CODE;
                ViewBag.CODESOC = FactureClient.Societes;
                ViewBag.declar = FactureClient.Declaration;
                ViewBag.datedec = FactureClient.Date_Declaration;
                ViewBag.service = FactureClient.Bien_service;
                ViewBag.immb = FactureClient.immobilisation;


            }
            Session["ProduitsFactureClient"] = ListeDesPoduits;
            ViewBag.Numero = Numero;
            return View(FactureClient);
        }


        #endregion
        #region common functions
        public JsonResult GetAllProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LIGNES_DEVIS_FOURNISSEURS> ListeProduit = db.LIGNES_DEVIS_FOURNISSEURS.ToList();
            return Json(ListeProduit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductByID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            Prix_Achat produit = db.Prix_Achat.Where(pr => pr.Product_ID == id).FirstOrDefault();
            return Json(produit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllClient()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CLIENTS> ListeClient = db.CLIENTS.ToList();
            return Json(ListeClient, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClientByID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            CLIENTS client = db.CLIENTS.Where(pr => pr.ID == id).FirstOrDefault();
            return Json(client, JsonRequestBehavior.AllowGet);
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
                s = db.Tiers.Where(p => p.Clt == id).ToList();

            }

            var result = (from r in s
                          select new
                          {
                              id = r.TiersID,
                              name = r.NOM,
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCategorie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Categorie> Listecategorie = db.Categorie.ToList();
            return Json(Listecategorie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllDescription()
        {
            var lignes_devis_frs = new List<string>();
            var ligne_devis_frs = (from m in db.LIGNES_DEVIS_FOURNISSEURS
                                   select m.DESIGNATION_PRODUIT
                                   );
            lignes_devis_frs.AddRange(ligne_devis_frs);
            return Json(lignes_devis_frs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSousCategorie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Sous_Categorie> Listecategorie = db.Sous_Categorie.ToList();
            return Json(Listecategorie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSocByID(string ID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            Societes societe = db.Societes.Where(pr => pr.SociID == id).FirstOrDefault();
            return Json(societe, JsonRequestBehavior.AllowGet);
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
        public string Typebl(string id)
        {
            int ID = int.Parse(id);
            BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();
            BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            
                return BonLivraisonClient.Type.ToString();
           

        }
        public string verifieValiditeBl(string id)
        {
            int ID = int.Parse(id);
            BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();
            BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if ((BonLivraisonClient.VALIDER) && (BonLivraisonClient.Type==true))
            {
                return BonLivraisonClient.ID.ToString();
            }
            else
            {
                return "NO";
            }

        }
        public string verifieValiditeFac(string id)
        {
            int ID = int.Parse(id);
            FACTURES_CLIENTS facture = new FACTURES_CLIENTS();
            facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if ((facture.VALIDER) && (facture.PAYEE == true))
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
            DEVIS_CLIENTS cmdclt = new DEVIS_CLIENTS();

            COMMANDES_CLIENTS blcmd = db.COMMANDES_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).FirstOrDefault();
            if (blcmd == null)
            {
                return cmdclt.ID.ToString();
            }
            else
            {
                return "NO";
            }

        }
        public string cmdencours(string id)
        {
            int ID = int.Parse(id);
            COMMANDES_CLIENTS cmdclt = new COMMANDES_CLIENTS();
            BONS_LIVRAISONS_CLIENTS blcmd= db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.COMMANDE_CLIENT == ID).FirstOrDefault();
            if (blcmd==null)
            {
                return cmdclt.ID.ToString();
            }
            else
            {
                return "NO";
            }

        }

        #endregion
        #region Print
        public ActionResult InvoicePrint(string CODE,string Conditions,string validite,string TotalHTMI)
        {
            int ID = int.Parse(CODE);
            double somme = 0;
            DEVIS_CLIENTS DVF = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_DEVIS_CLIENTS> lIGNES_DEVIS_CLIENTS = db.LIGNES_DEVIS_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();
            List<Inspinia_MVC5.Models.Tasks> task_list = db.Tasks.Where(t => t.ProjetTechniquesID == ID).ToList();
            foreach(Tasks ta in task_list)
            {
                double c1 = (double)db.Personnels.Find(ta.owner_id).Cout_hor;
                string c4 = ta.duration_h_planning.ToString();
                int c2 = int.Parse(c4);
                double c3 = c1 * c2;
                somme = somme + c3;
            }
            decimal TotalHTMI1 = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
            decimal ttcinstal = TotalHTMI1-DVF.TTC;
            decimal totale = (decimal)somme +DVF.TTC;
            decimal ttvinstal = ttcinstal - (decimal)somme;
            ViewBag.ttcinstal = ttcinstal;
            ViewBag.somme = somme;
            ViewBag.id = DVF.ID.ToString();
            ViewBag.NOM = DVF.CLIENTS.NOM;
            ViewBag.ADRESSE = DVF.CLIENTS.ADRESSE;
            ViewBag.TEL = DVF.CLIENTS.TELEPHONE;
            ViewBag.FAX = DVF.CLIENTS.FAX;
            ViewBag.DATE = DVF.DATE;
            ViewBag.DATE2 = DateTime.Now;
            ViewBag.TTVA = DVF.TTVA+ ttvinstal;
            ViewBag.TTTC = DVF.TTC;
            ViewBag.TTNET = DVF.THT+(decimal)somme;
            ViewBag.Code = DVF.CODE;
            ViewBag.totale = totale;
            string[] cond = Conditions.Split('-');
            ViewBag.Conditions1 = cond[1];
            ViewBag.Conditions2 = cond[2];
            ViewBag.Conditions3 = cond[3];

            ViewBag.Conditions = Conditions;
            ViewBag.validite = validite;

            return View(lIGNES_DEVIS_CLIENTS);



        }
        public ActionResult PrintRetenue(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            FACTURES_CLIENTS UnDevis = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UnDevis.CODE,
                             
                             DATE = UnDevis.DATE.ToShortDateString(),
                             NOM = UnDevis.CLIENTS.NOM,
                             ADRESSE=UnDevis.CLIENTS.ADRESSE,
                             ID_FISCAL=UnDevis.CLIENTS.ID_FISCAL,
                             TNET = UnDevis.TNET ?? 0,
                             //Expr5 = UnDevis.CLIENTS.CODE,
                             //Expr2 = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().NOM,
                             //ADRESSE = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().ADRESSE,
                             //FAX = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().FAX,
                             //TEL = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().TEL,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/Retenue.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Retenue Client";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintDevisClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            DEVIS_CLIENTS UnDevis = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_DEVIS_CLIENTS> Liste = db.LIGNES_DEVIS_CLIENTS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE= UnDevis.CODE,
                             Designation = UnDevis.Designation,
                             DATE = UnDevis.DATE.ToShortDateString(),
                             NOM = UnDevis.CLIENTS.NOM,
                             Expr5 = UnDevis.CLIENTS.CODE,
                             Expr2 = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().NOM,
                             ADRESSE = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().ADRESSE,
                             FAX = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().FAX,
                             TEL = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().TEL,
                             Exttva = UnDevis.CLIENTS.Exttva,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 1,
                             Unite = cmd.Unite,
                             QUANTITE = cmd.QUANTITE ?? 1,
                             TOTALE_HT = cmd.TOTALE_HT ?? 1,
                             REMISE = cmd.REMISE ?? 1,
                             RC = db.Tiers.Where(fd => fd.Clt == UnDevis.CLIENT).FirstOrDefault().RC,
                             TVA = cmd.TVA ?? 1,
                             categorie = cmd.Categorie,

                         };
            ReportDocument rptH = new ReportDocument();

            string FileName = Server.MapPath("/Reports/DevisClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Devis Client";
            rptH.SetDataSource(dt);
            //rptH.SetParameterValue(0, dt.ToString());
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        //public ActionResult PrintDevisClientByID(string CODE)
        //{
        //    int ID = int.Parse(CODE);
        //    ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
        //    DEVIS_CLIENTS UnDevis = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
        //    List<LIGNES_DEVIS_CLIENTS> Liste = db.LIGNES_DEVIS_CLIENTS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
        //    dynamic dt = from cmd in Liste
        //                 select new
        //                 {
        //                     //CODE= UnDevis.CODE,
        //                     //Designation = UnDevis.Designation,
        //                     //DATE = UnDevis.DATE.ToShortDateString(),
        //                     //NOM = UnDevis.CLIENTS.NOM,
        //                     //Expr5 = UnDevis.CLIENTS.CODE,
        //                     //Expr2 = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().NOM,
        //                     //ADRESSE = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().ADRESSE,
        //                     //FAX = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().FAX,
        //                     //TEL = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().TEL,
        //                     //Exttva = UnDevis.CLIENTS.Exttva,
        //                     DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
        //                     PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 1,
        //                     Unite = cmd.Unite,
        //                     QUANTITE = cmd.QUANTITE ?? 1,
        //                     TOTALE_HT = cmd.TOTALE_HT ?? 1,
        //                     REMISE = cmd.REMISE ?? 1,
        //                     RC = db.Tiers.Where(fd => fd.Clt == UnDevis.CLIENT).FirstOrDefault().RC,
        //                     TVA = cmd.TVA ?? 1,
        //                     categorie = cmd.Categorie,
                             
        //                 };
        //    ReportDocument rptH = new ReportDocument();
            
        //    string FileName = Server.MapPath("/Reports/DevisClient.rpt");
        //    rptH.Load(FileName);
        //    rptH.SummaryInfo.ReportTitle = "Devis Client";
        //    rptH.SetDataSource(dt);
        //    rptH.SetParameterValue(0, dt.ToString());
        //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(stream, "application/pdf");
        //}

        public ActionResult PrintCommandeClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            COMMANDES_CLIENTS UnDevis = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_COMMANDES_CLIENTS> Liste = db.LIGNES_COMMANDES_CLIENTS.Where(lcmd => lcmd.COMMANDE_CLIENT == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                            CODE = UnDevis.CODE,
                            DATE = UnDevis.DATE.ToShortDateString(),
                            MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
                            NOM = UnDevis.CLIENTS.NOM,
                            CODE_ACCES = UnDevis.Societes1.CODE_ACCES,
                            Expr4 = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().NOM,
                            ADRESSE = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().ADRESSE,
                            FAX = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().FAX,
                            TEL = db.Tiers.Where(Fd => Fd.TiersID == UnDevis.Tiers).FirstOrDefault().TEL,
                            Expr5 = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,

                            DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                            QUANTITE = cmd.QUANTITE ?? 0,
                            Marque = cmd.Marque,
                            Categorie = cmd.Categorie,
                            sscat = cmd.Sous_Categorie,
                            Unite = cmd.Unite,
                            RC = db.Tiers.Where(fd => fd.Clt == UnDevis.CLIENT).FirstOrDefault().RC,
                            ID_FISCAL = db.Tiers.Where(fd => fd.Clt == UnDevis.CLIENT).FirstOrDefault().ID_FISCAL,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/CommandeClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commande Client";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        
        public ActionResult PrintBonLivraisonClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            BONS_LIVRAISONS_CLIENTS UnDevis = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UnDevis.CODE,
                             MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,

                             DATE = UnDevis.DATE.ToShortDateString(),
                             //CODE = UnDevis.CLIENTS.CODE,
                             NOM = UnDevis.CLIENTS.NOM,
                             ADRESSE=db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ADRESSE,
                             TEL=db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
                             Expr3=db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().NOM,
                             CODE_ACCES = db.Societes.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().CODE_ACCES,
                             Expr5 = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,

                             FAX = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().FAX,
                             //CODE_PRODUIT = cmd.CODE_PRODUIT,
                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ID_FISCAL,

                             //CHIFFRE = convert.NumberToCurrencyText(UnDevis.TNET.ToString()),
                             //RC = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().RC,
                             //CTVA = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().ID_FISCALE
                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/BonLivraison.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Bon de livraison Client N°:";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        //public ActionResult PrintBonLivraisonPARClientByID(string CODE)
        //{
        //    int ID = int.Parse(CODE);
        //    ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
        //    BONS_LIVRAISONS_CLIENTS UnDevis = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
        //    List<BONS_LIVRAISONS_PART_CLIENTS> Liste = db.BONS_LIVRAISONS_PART_CLIENTS.Where(lcmd => lcmd.IDBLC == ID).ToList();
        //    dynamic dt = from cmd in Liste
        //                 select new
        //                 {
        //                     CODE = UnDevis.CODE,
        //                     MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,

        //                     DATE = UnDevis.DATE.ToShortDateString(),
        //                     //CODE = UnDevis.CLIENTS.CODE,
        //                     Expr4 = UnDevis.Societes1.NOM,
        //                     ADRESSE = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ADRESSE,
        //                     TEL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
        //                     Expr3 = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().NOM,
        //                     CODE_ACCES = db.Societes.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().CODE_ACCES,
        //                     Nom = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,
        //                     FAX = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().FAX,
        //                     //CODE_PRODUIT = cmd.CODE_PRODUIT,
        //                     DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
        //                     QTELIV = cmd.QTELIV ?? 0,
        //                     QTERES = cmd.QTERES ?? 0,
        //                     //RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC

        //                     //CHIFFRE = convert.NumberToCurrencyText(UnDevis.TNET.ToString()),
        //                     //RC = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().RC,
        //                     //CTVA = db.SOCIETES.Where(soc => soc.ID == 1).FirstOrDefault().ID_FISCALE
        //                 };
        //    ReportDocument rptH = new ReportDocument();
        //    string FileName = Server.MapPath("/Reports/BonLivraisonPAR.rpt");
        //    rptH.Load(FileName);
        //    rptH.SummaryInfo.ReportTitle = "Bon de livraison Client N°:";
        //    rptH.SetDataSource(dt);
        //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(stream, "application/pdf");
        //}
        public ActionResult PrintFactureClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            FACTURES_CLIENTS UnDevis = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_FACTURES_CLIENTS> Liste = db.LIGNES_FACTURES_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE =UnDevis.CODE, 
                             MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
                             DATE = UnDevis.DATE.ToShortDateString(),
                             //CODE= UnDevis.CLIENTS.CODE,
                             Expr4 = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().NOM,
                             ADRESSE = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ADRESSE,
                             TEL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
                             FAX=db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().FAX,
                             NOM = UnDevis.CLIENTS.NOM,
                             Direction = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,

                             CODE_ACCES=db.Societes.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().CODE_ACCES,
                             Unite=cmd.Unite,
                             TIMBRE = UnDevis.TIMBRE ?? 0,
                             THT = UnDevis.THT ?? 0,
                             //ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
                             TTC=UnDevis.TTC,
                             TTVA = UnDevis.TTVA ?? 0,

                             //TIMBRE =UnDevis.TIMBRE,
                             // TTC = UnDevis.TTC ,
                             //TTVA=UnDevis.TTVA,
                             //TTVA=UnDevis.TTVA,

                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
                             REMISE = cmd.REMISE ?? 0,
                             TOTALE_HT = cmd.TOTALE_HT ?? 0,
                             TVA = cmd.TVA ?? 0,
                             TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
                             RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ID_FISCAL,

                             //RC = db.CLIENTS.Where(soc => soc.ID == 1).FirstOrDefault().RC

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/FactureClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Facture Client N°:";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAvoirClientByID(string CODE)
        {
            int ID = int.Parse(CODE);
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            AVOIRS_CLIENTS UnDevis = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_AVOIRS_CLIENTS> Liste = db.LIGNES_AVOIRS_CLIENTS.Where(lcmd => lcmd.AVOIR_CLIENT == ID).ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = UnDevis.CODE,
                             MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
                             DATE = UnDevis.DATE.ToShortDateString(),
                             //CODE= UnDevis.CLIENTS.CODE,
                             Expr5 = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().NOM,
                             Expr4 = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ADRESSE,
                             TEL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
                             Expr12 = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().FAX,
                             NOM = UnDevis.CLIENTS.NOM,
                            
                             Expr3 = db.CLIENTS.Where(fd => fd.ID == UnDevis.CLIENT).FirstOrDefault().CODE,
                             Unite = cmd.Unite,
                             //TIMBRE = UnDevis.TIMBRE ?? 0,
                             THT = UnDevis.THT ?? 0,
                             //ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
                             TTC = UnDevis.TTC,
                             TTVA = UnDevis.TTVA ?? 0,

                             //TIMBRE =UnDevis.TIMBRE,
                             // TTC = UnDevis.TTC ,
                             //TTVA=UnDevis.TTVA,
                             //TTVA=UnDevis.TTVA,

                             DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
                             QUANTITE = cmd.QUANTITE ?? 0,
                             PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
                             REMISE = cmd.REMISE ?? 0,
                             TOTALE_HT = cmd.TOTALE_HT ?? 0,
                             TVA = cmd.TVA ?? 0,
                             TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
                             RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
                             ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ID_FISCAL,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/AvoirClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Avoir Client";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult PrintAllDevisClient()
        {
            List<DEVIS_CLIENTS> Liste = db.DEVIS_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.CLIENTS.NOM,
                             NOM=cmd.CLIENTS.NOM,
                             Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             Designation = cmd.Designation,

                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = string.Empty,
                             //NHT = cmd.NHT,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeDevisClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Devis Client";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllCommandeClient()
        {
            List<COMMANDES_CLIENTS> Liste = db.COMMANDES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.CLIENTS.NOM,
                             NOM = cmd.CLIENTS.NOM,
                             Expr1 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             Designation = cmd.Designation,

                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDER = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = string.Empty,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeCommandeClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commandes Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllCommandeClientParmodePaiement()
        {
            List<COMMANDES_CLIENTS> Liste = db.COMMANDES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.CLIENTS.NOM,
                             NOM = cmd.CLIENTS.NOM,
                             Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             Designation = cmd.Designation,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDER = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = string.Empty,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeCommandeClientparmodepaiement.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commandes Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllCommandeClientParmodeCLT()
        {
            List<COMMANDES_CLIENTS> Liste = db.COMMANDES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             //FOURNISSEUR = cmd.CLIENTS.NOM,
                             NOM = cmd.CLIENTS.NOM,
                             Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             Designation = cmd.Designation,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDER = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = string.Empty,
                             NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeCommandeClientParclient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Commandes Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult PrintAllBonLivraisonClient()
        {
            List<BONS_LIVRAISONS_CLIENTS> Liste = db.BONS_LIVRAISONS_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = cmd.Societes1.NOM,
                             Expr1 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
                             
                             //FOURNISSEUR = cmd.CLIENTS.NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                            // VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = string.Empty,
                             //NHT = cmd.NHT ?? 0,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeBonLivraisonFournisseur.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Bon Livraisons";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureClient()
        {
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation=cmd.Designation,
                             NOM = db.CLIENTS.Where(fd => fd.ID == cmd.CLIENT).FirstOrDefault().NOM,
                             Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             
                             ////FOURNISSEUR = cmd.CLIENTS.NOM,
                             //Societes=cmd.Societes,
                             //Tiers=cmd.Tiers,
                             MODE_PAIEMENT=cmd.MODE_PAIEMENT,

                             DATE = cmd.DATE.ToShortDateString(),
                             //TTC=cmd.TTC,
                             //PAYEE = cmd.PAYEE,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //NET_HT = cmd.NHT,
                             //T_TVA = cmd.TTVA,
                             //TTC = cmd.TTC,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureClientParetat()
        {
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = db.CLIENTS.Where(fd => fd.ID == cmd.CLIENT).FirstOrDefault().NOM,
                             Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,

                             ////FOURNISSEUR = cmd.CLIENTS.NOM,
                             //Societes=cmd.Societes,
                             //Tiers=cmd.Tiers,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             DATE = cmd.DATE.ToShortDateString(),
                             //TTC=cmd.TTC,
                             PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             //PAYEE = (bool)cmd.PAYEE,
                             //NET_HT = cmd.NHT,
                             //T_TVA = cmd.TTVA,
                             //TTC = cmd.TTC,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureClientParetat.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllFactureClientParclt()
        {
            List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             //NOM = cmd.Societes1.NOM,
                             //Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             Societes = cmd.CLIENTS.NOM,

                             NOM = db.CLIENTS.Where(fd => fd.ID == cmd.CLIENT).FirstOrDefault().NOM,
                             ////FOURNISSEUR = cmd.CLIENTS.NOM,
                             //Societes=cmd.Societes,
                             //Tiers=cmd.Tiers,
                             MODE_PAIEMENT = cmd.MODE_PAIEMENT,
                             DATE = cmd.DATE.ToShortDateString(),
                             //TTC=cmd.TTC,
                             PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             //PAYEE = (bool)cmd.PAYEE,
                             //NET_HT = cmd.NHT,
                             //T_TVA = cmd.TTVA,
                             //TTC = cmd.TTC,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeFactureClientParclt.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Factures Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        public ActionResult PrintAllAvoirClient()
        {
            List<AVOIRS_CLIENTS> Liste = db.AVOIRS_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = cmd.CLIENTS.NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             Expr2 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeAvoirClient.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Avoirs Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        //public ActionResult PrintAllAvoirClientParVal()
        //{
        //    List<AVOIRS_CLIENTS> Liste = db.AVOIRS_CLIENTS.ToList();
        //    dynamic dt = from cmd in Liste
        //                 select new
        //                 {
        //                     CODE = cmd.CODE,
        //                     Designation = cmd.Designation,
        //                     NOM = cmd.CLIENTS.NOM,
        //                     DATE = cmd.DATE.ToShortDateString(),
        //                     VALIDEE = (bool)cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
        //                     //PAYEE = cmd.PAYEE ? "PAYEE" : "NON PAYEE",
        //                     Expr2 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
        //                     TTVA = cmd.TTVA ?? 0,
        //                     TTC = cmd.TTC,
        //                     TNET = cmd.TNET ?? 0,
        //                     //NET_A_PAYE = cmd.TNET

        //                 };
        //    ReportDocument rptH = new ReportDocument();
        //    string FileName = Server.MapPath("/Reports/ListeAvoirClientVal.rpt");
        //    rptH.Load(FileName);
        //    rptH.SummaryInfo.ReportTitle = "Avoirs Clients";
        //    rptH.SetDataSource(dt);
        //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(stream, "application/pdf");
        //}
        public ActionResult PrintAllAvoirClientParDate()
        {
            List<AVOIRS_CLIENTS> Liste = db.AVOIRS_CLIENTS.ToList();
            dynamic dt = from cmd in Liste
                         select new
                         {
                             CODE = cmd.CODE,
                             Designation = cmd.Designation,
                             NOM = cmd.CLIENTS.NOM,
                             DATE = cmd.DATE.ToShortDateString(),
                             //VALIDEE = (bool)cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
                             //PAYEE = cmd.PAYEE ? "PAYEE" : "NON PAYEE",
                             Expr2 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,
                             TTVA = cmd.TTVA ?? 0,
                             TTC = cmd.TTC,
                             TNET = cmd.TNET ?? 0,
                             //NET_A_PAYE = cmd.TNET

                         };
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/ListeAvoirClientDate.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Avoirs Clients";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }
        #endregion 
        public JsonResult UpdatePriceDevis(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            int IntRemise = int.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceCommande(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            int IntRemise = int.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceBonLivraison(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            int IntRemise = int.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceFacture(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            int IntRemise = int.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePriceAvoir(string remise)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            }
            decimal totalHT = 0;
            decimal totalTVA = 0;
            foreach (LigneProduit ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTVA += (ligne.PTHT * ligne.TVA) / 100;
            }
            int IntRemise = int.Parse(remise);
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTVA = totalTVA
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public string AddLineDevis(string ID_Produit, string NumDevis, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string Quantite_Produit, string PUHT_Produit, string PUHT_Produit2, string Marge, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, string des1, string sscat2, string index11, string index22)
        {
            Session["ProduitsDevisClient2"] = null;
            LigneProduit ligne = new LigneProduit();
            if(NumDevis!=null)
            {
                ligne.NumDevis = NumDevis;
            }
            ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            if (sscat2 != null)
            {
                int code1 = int.Parse(sscat2);
                char a = (char)(code1);
                
                int index2 = int.Parse(index22);
                string sous_categorie1 = sous_categorie.Insert(index2-1,a.ToString());
                ligne.Sous_CATEGORIE = sous_categorie1;
            }
            else
            {
                ligne.Sous_CATEGORIE = sous_categorie;

            }
            if (des1 != null)
            {
                int code1 = int.Parse(des1);
                char a = (char)(code1);

                int index1 = int.Parse(index11);
                string des = Description_Produit.Insert(index1, a.ToString());
                ligne.DESIGNATION = des;
            }
            else
            {
                ligne.DESIGNATION = Description_Produit;

            }
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            //ligne.Sous_CATEGORIE = sous_categorie;

            ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHT_Produit2, CultureInfo.InvariantCulture);
            ligne.MARGE = decimal.Parse(Marge, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture); 
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            }
            
                ListeDesPoduits.Add(ligne);
           
            Session["ProduitsDevisClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineCommande(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
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
            if (Session["ProduitsCommandeClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsCommandeClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineBonLivraison(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string Quantite_Liv, string Quantite_Res, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            LigneProduit ligne = new LigneProduit();
            ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.STOCK = int.Parse(StockProduit)-(int.Parse(Quantite_Liv));
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.QUANTITELiv = int.Parse(Quantite_Liv);
            ligne.QUANTITERES = int.Parse(Quantite_Res);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineAvoir(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            LigneProduit ligne = new LigneProduit();
            ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.STOCK = int.Parse(StockProduit)+ (int.Parse(Quantite_Produit));
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsAvoirClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string AddLineFacture(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            LigneProduit ligne = new LigneProduit();
            ligne.ID = int.Parse(ID_Produit);
            ligne.LIBELLE = LIB_Produi;
            ligne.DESIGNATION = Description_Produit;
            ligne.MARQUE = marque;
            ligne.UNITE = unite;
            ligne.DEVISE = devise;
            ligne.CATEGORIE = categorie;
            ligne.Sous_CATEGORIE = sous_categorie;
            ligne.STOCK = 3;
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            }
            if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
            {
                ListeDesPoduits.Add(ligne);
            }
            Session["ProduitsFactureClient"] = ListeDesPoduits;
            return string.Empty;
        }

        public string EditLineDevis(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit,string NEW_MARGE, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.MARGE=decimal.Parse(NEW_MARGE, CultureInfo.InvariantCulture);
            ligne.REMISE = decimal.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsDevisClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLineCommande(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsCommandeClient"] = ListeDesPoduits;
            return string.Empty;
        }
        
        public string EditLineBonLivraison(string ID_Produit, string Quantite_Produit, string Quantite_Liv, string Quantite_Res, string PUHT_Produit,string Mode, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {

            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(ID_Produit);
                LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
                ligne.ID = ID;
                ligne.QUANTITELiv = int.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
                ligne.QUANTITERES = ligne.QUANTITERES - int.Parse(Quantite_Liv);
                
            }
            if (Mode == "Create")
            {
                int ID = int.Parse(ID_Produit);
                LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
                ligne.ID = ID;
                ligne.QUANTITE = int.Parse(Quantite_Produit);
                ligne.QUANTITELiv = int.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
                ligne.QUANTITERES = int.Parse(Quantite_Res);
                ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
                ligne.REMISE = int.Parse(Remise_Produit);
                ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
                ligne.TVA = int.Parse(TVA_Produit);
                ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            }
            if(Mode == "Editcmd")
                {
                int ID = int.Parse(ID_Produit);
                LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
                ligne.ID = ID;
                
                ligne.QUANTITELiv = decimal.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
                ligne.QUANTITERES = ligne.QUANTITE - ligne.QUANTITELiv;

            }
            Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
            return string.Empty;

        }
        public string EditLineBonLivraisonbt(string ID_Produit, string Quantite_Produit, string Quantite_Liv, string Quantite_Res, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {

            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }

            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.ID = ID;
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.QUANTITELiv = int.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
            ligne.QUANTITERES = int.Parse(Quantite_Res);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

            Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
            return string.Empty;

        }
        public string EditLineFacture(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsFactureClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string EditLineAvoir(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            }
            int ID = int.Parse(ID_Produit);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.QUANTITE = int.Parse(Quantite_Produit);
            ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.REMISE = int.Parse(Remise_Produit);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["ProduitsAvoirClient"] = ListeDesPoduits;
            return string.Empty;
        }


        public JsonResult GetAllLineDevis()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLineCommande()
        {
           
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLineBonLivraison()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLineFacture()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLineAvoir()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public void AddClient(string Code, string NOM, string Adresse, string TELEPHONE, string FAX, string EMAIL, string SITE_WEB, string ID_FISCAL, string AI, string NIS, string RC, string RIB, string CONTACT)
        {
            CLIENTS NewElement = new CLIENTS();
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
            NewElement.Exttva = true;


            db.CLIENTS.Add(NewElement);
            db.SaveChanges();
        }
        [HttpPost]
        public ActionResult SendDevis(string Mode, string Code)
       {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
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
            string conditionsPaim = Request["conditionsPaim"] != null ? Request["conditionsPaim"].ToString() : string.Empty;
            string validite = Request["validite"] != null ? Request["validite"].ToString() : string.Empty;
            string TotalHTMI = Request["netAPaye"] != null ? Request["netAPaye"].ToString() : "0";
            string somme = Request["somme"] != null ? Request["somme"].ToString() : "0";
            string Totalht = Request["TotalHTMI"] != null ? Request["TotalHTMI"].ToString() : "0";


            List<CLIENTS> listt = db.CLIENTS.ToList();
            int nb=0;
            foreach(CLIENTS cl in listt)
            {
                int clt = cl.ID;
                if(client!=clt.ToString())
                {
                    nb++;
                }
            }
            if(nb==listt.Count)
            {
                string clt;
                foreach (CLIENTS cl in listt)
                {
                    clt = cl.NOM;
                    if (cl.NOM.Contains("\r")&&cl.NOM.Contains("\n"))
                    {
                         clt = cl.NOM.Replace("\r","");
                        clt = clt.Replace("\n", "");
                    }
                    if (cl.NOM.Contains("\r") && (!cl.NOM.Contains("\n")))
                    {
                        clt = cl.NOM.Replace("\r", "");
                    }
                    if (!(cl.NOM.Contains("\r")) && cl.NOM.Contains("\n"))
                    {
                        clt = cl.NOM.Replace("\n", "");
                    }
                    if (clt==client)
                    {
                        client = cl.ID.ToString();
                    }
                }
              
            }
            //List<Tiers> listt2 = db.Tiers.ToList();
            //int nb1 = 0;
            //foreach (Tiers tr in listt2)
            //{
            //    int trr = tr.TiersID;
            //    if (Tiers != trr.ToString())
            //    {
            //        nb1++;
            //    }
            //}
            //if (nb1 == listt2.Count)
            //{
            //    int i = db.Tiers.Where(f => f.NOM.Trim().Equals(Tiers.Trim())).FirstOrDefault().TiersID;
            //    Tiers = i.ToString();
            //}
            //
            if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
            if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
            if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
            if (string.IsNullOrEmpty(TotalHTMI)) TotalHTMI = "0";
            if (string.IsNullOrEmpty(Totalht)) Totalht = "0";
            if (string.IsNullOrEmpty(somme)) somme = "0";
            //
            string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
            if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
            Boolean Print = Boolean.Parse(WithPrint);
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            string SelectedDevis = string.Empty;
            if (Session["ProduitsDevisClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            }
            if (Mode == "Create")
            {
                if (!db.DEVIS_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    DEVIS_CLIENTS DevisClient = new DEVIS_CLIENTS();
                    DevisClient.CODE = Numero;
                    DevisClient.DATE = DateTime.Parse(date);
                    DevisClient.CLIENT = int.Parse(client);
                    int ID_CLIENT = int.Parse(client);
                    DevisClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    DevisClient.Societes = 3;
                    //int ID_Soc = int.Parse(societe);
                    //DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    //if (Tiers != "")
                    //{
                    //    DevisClient.Tiers = int.Parse(Tiers);
                    //}
                    //else
                    //{
                    //    DevisClient.Tiers = int.Parse(client);
                    //}

                    DevisClient.MODE_PAIEMENT = modePaiement;
                    DevisClient.Designation = designation;
                    DevisClient.convert = false;
                    DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    DevisClient.THT = decimal.Parse(Totalht, CultureInfo.InvariantCulture);
                    DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    //DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture) + ((decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture) - decimal.Parse(TotalTTC, CultureInfo.InvariantCulture)) - decimal.Parse(somme, CultureInfo.InvariantCulture));
                    DevisClient.TTC = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
                    DevisClient.TNET = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
                    db.DEVIS_CLIENTS.Add(DevisClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_DEVIS_CLIENTS UneLigne = new LIGNES_DEVIS_CLIENTS();
                        UneLigne.Art_Devis_Frs = Ligne.ID;
                        LIGNES_DEVIS_FOURNISSEURS Prix_Achat = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
                        //UneLigne.Libelle_Prd = Ligne.LIBELLE;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = Ligne.QUANTITE;
                        //UneLigne.STOCK = (double)Ligne.STOCK;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
                        UneLigne.MARGE = Ligne.MARGE;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.DEVIS_CLIENT = DevisClient.ID;
                        //UneLigne.DEVIS_CLIENTS = DevisClient;
                        UneLigne.LIGNES_DEVIS_FOURNISSEURS = Prix_Achat;
                        db.LIGNES_DEVIS_CLIENTS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("DEVIS", Produit, DevisClient.DATE, DevisClient.CODE, Ligne.QUANTITE);
                    }
                    SelectedDevis = DevisClient.ID.ToString();
                    Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

                    this.db.SaveChanges();
                    if (DevisClient.convert == false)
                    {
                        var projetTechnique = new ProjetTechniques()
                        {

                            Devis_clt = DevisClient.ID,
                            ReferenceTech = param.RefTech + param.CompteurTech,
                            DateDebut = DateTime.Now,
                            DateFin = DateTime.Now,
                            Cout = (float)DevisClient.TTC,
                            ClientId = DevisClient.CLIENT,
                            PersonnelId = DevisClient.Societes,
                            Designation = DevisClient.Designation,
                            Statut = "Initié",
                            CoutReel = (float)DevisClient.TTC,
                            DateDebutReel = DateTime.Now,
                            DateFinReel = DateTime.Now

                        };

                        //this.db.AffaireCommerciales.Remove(affaireCommerciale);
                        this.db.ProjetTechniques.Add(projetTechnique);

                        try
                        {
                            var pt = db.ProjetTechniques
                                .OrderByDescending(p => p.ProjetTechniqueId)
                                .FirstOrDefault();
                            String ch = pt.ReferenceTech.ToString();
                            projetTechnique.ReferenceTech = param.RefTech + param.CompteurTech;
                            param.CompteurTech = param.CompteurTech + 1;




                        }
                        catch
                        {
                            //affaireCommerciale.Reference = param.RefCom + param.CompteurCom;
                            param.CompteurTech = param.CompteurTech + 1;
                        }
                        DevisClient.convert = true;
                        db.Entry(DevisClient).State = System.Data.Entity.EntityState.Modified;
                        this.db.SaveChanges();
                        List<horaire_jour> list = db.horaire_jour.ToList();
                        foreach(horaire_jour hj in list)
                        {
                            string jour = db.Jours.Where(fou => fou.id == hj.jour).FirstOrDefault().Jour;
                            string Hdepart = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Debut;
                            string Hsortie = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Sortie;
                            string Hdepart2 = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Debut1;
                            string Hsortie2 = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Sortie2;
                            DateTime DateDeb = (DateTime)db.Tableau_Horaire.Where(fou => fou.id == hj.table_horaire).FirstOrDefault().Date_Deb;
                            DateTime DateFin = (DateTime)db.Tableau_Horaire.Where(fou => fou.id == hj.table_horaire).FirstOrDefault().Date_Fin;
                           
                            //int Hdepart1 = int.Parse(Hdepart);
                            //int Hsortie1 = int.Parse(Hsortie);
                            ParametrageSemaines param1 = new ParametrageSemaines();
                            param1.JourId = hj.jour;
                            param1.JourLibelle = jour;
                            param1.jourTravail = true;
                            param1.seance1Debut = Hdepart;
                            param1.seance1Fin = Hsortie;
                            if((Hdepart2==null)&&(Hsortie2==null))
                            {
                                param1.doubleSeance = false;
                                param1.seance2Debut = "0";
                                param1.seance2Fin = "0";
                            }
                            else {
                                param1.doubleSeance = true;
                                param1.seance2Debut = Hdepart2;
                            param1.seance2Fin = Hsortie2;
                            }
                            param1.projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt);
                            param1.Date_Deb = DateDeb;
                            param1.Date_Fin = DateFin;
                            db.ParametrageSemaines.Add(param1);
                            db.SaveChanges();

                        }

                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 0, JourLibelle = "Dimanche", jourTravail = false, doubleSeance = false, seance1Debut = 0, seance1Fin = 0, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 1, JourLibelle = "Lundi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 2, JourLibelle = "Mardi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 3, JourLibelle = "Mercredi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 4, JourLibelle = "Jeudi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 5, JourLibelle = "Vendredi", jourTravail = true, doubleSeance = false, seance1Debut = 8, seance1Fin = 14, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 6, JourLibelle = "Samedi", jourTravail = false, doubleSeance = false, seance1Debut = 0, seance1Fin = 0, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt) });
                        //db.SaveChanges();
                    }

                }
            }
            if ((Mode == "Edit")|| (Mode == "Aff"))
            {
                int ID = int.Parse(Code);
                DEVIS_CLIENTS DevisClient = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                DevisClient.CODE = Numero;
                DevisClient.DATE = DateTime.Parse(date);
                DevisClient.CLIENT = int.Parse(client);
                int ID_CLIENT = int.Parse(client);
                DevisClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                DevisClient.Societes = 3;
                //DevisClient.Societes = int.Parse(societe);
                //int ID_Soc = int.Parse(societe);
                //DevisClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    DevisClient.Tiers = int.Parse(Tiers);
                }
                else
                {
                    DevisClient.Tiers = int.Parse(client);

                }


                DevisClient.MODE_PAIEMENT = modePaiement;
                DevisClient.Designation = designation;
                DevisClient.convert = false;
                DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                DevisClient.THT = decimal.Parse(Totalht, CultureInfo.InvariantCulture);
                DevisClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                //DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture) + ((decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture) - decimal.Parse(TotalTTC, CultureInfo.InvariantCulture)) - decimal.Parse(somme, CultureInfo.InvariantCulture));
                DevisClient.TTC = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
                DevisClient.TNET = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_DEVIS_CLIENTS UneLigne = new LIGNES_DEVIS_CLIENTS();
                    UneLigne.Art_Devis_Frs = Ligne.ID;
                    LIGNES_DEVIS_FOURNISSEURS Prix_Achat = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
                    //UneLigne.Libelle_Prd = Ligne.LIBELLE;
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = Ligne.QUANTITE;
                    //UneLigne.STOCK = (double)Ligne.STOCK;
                    UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
                    UneLigne.MARGE = Ligne.MARGE;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.DEVIS_CLIENT = DevisClient.ID;
                    //UneLigne.DEVIS_CLIENTS = DevisClient;
                    UneLigne.LIGNES_DEVIS_FOURNISSEURS = Prix_Achat;
                    db.LIGNES_DEVIS_CLIENTS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedDevis = DevisClient.ID.ToString();
            }
            if (Print)
            {
                return RedirectToAction("InvoicePrint", new { CODE = SelectedDevis,Conditions= conditionsPaim, validite = validite , TotalHTMI = TotalHTMI });
            }
            Session["ProduitsDevisClient"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Devis", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendCommande(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
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
            string SelectedCommande = string.Empty;
            if (Session["ProduitsCommandeClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            }
            if (Mode == "Create")
            {
                if (!db.COMMANDES_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    COMMANDES_CLIENTS CommandeClient = new COMMANDES_CLIENTS();
                    CommandeClient.CODE = Numero;
                    CommandeClient.DATE = DateTime.Parse(date);
                    CommandeClient.CLIENT = int.Parse(client);
                    int ID_CLIENT = int.Parse(client);
                    CommandeClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    CommandeClient.Societes = 3;
                    int ID_Soc = 3;
                    CommandeClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    CommandeClient.Tiers = int.Parse(Tiers);
                    CommandeClient.MODE_PAIEMENT = modePaiement;
                    CommandeClient.Designation = designation;
                    CommandeClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    CommandeClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    CommandeClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    CommandeClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    CommandeClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    CommandeClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    db.COMMANDES_CLIENTS.Add(CommandeClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_COMMANDES_CLIENTS UneLigne = new LIGNES_COMMANDES_CLIENTS();
                        UneLigne.Prix_achat = Ligne.ID;
                        LIGNES_DEVIS_FOURNISSEURS Produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.STOCK = (double)Ligne.STOCK;

                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.COMMANDE_CLIENT = CommandeClient.ID;
                        UneLigne.COMMANDES_CLIENTS = CommandeClient;
                        UneLigne.LIGNES_DEVIS_FOURNISSEURS = Produit;
                        db.LIGNES_COMMANDES_CLIENTS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("COMMANDE", Produit, CommandeClient.DATE, CommandeClient.CODE, Ligne.QUANTITE);
                    }
                    SelectedCommande = CommandeClient.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                COMMANDES_CLIENTS CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                CommandeClient.CODE = Numero;
                CommandeClient.DATE = DateTime.Parse(date);
                CommandeClient.CLIENT = int.Parse(client);
                int ID_CLIENT = int.Parse(client);
                CommandeClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                CommandeClient.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //CommandeClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                if (Tiers != "")
                {
                    CommandeClient.Tiers = int.Parse(Tiers);
                }
                else
                {
                    CommandeClient.Tiers = int.Parse(client);

                }

                
                CommandeClient.Designation = designation;
                CommandeClient.MODE_PAIEMENT = modePaiement;
                CommandeClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                CommandeClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                CommandeClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                CommandeClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                CommandeClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                CommandeClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_COMMANDES_CLIENTS.Where(p => p.COMMANDE_CLIENT == CommandeClient.ID).ToList().ForEach(p => db.LIGNES_COMMANDES_CLIENTS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_COMMANDES_CLIENTS UneLigne = new LIGNES_COMMANDES_CLIENTS();
                    UneLigne.Prix_achat = Ligne.ID;
                    LIGNES_DEVIS_FOURNISSEURS Produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.STOCK = (double)Ligne.STOCK;

                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.COMMANDE_CLIENT = CommandeClient.ID;
                    UneLigne.COMMANDES_CLIENTS = CommandeClient;
                    UneLigne.LIGNES_DEVIS_FOURNISSEURS = Produit;
                    db.LIGNES_COMMANDES_CLIENTS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedCommande = CommandeClient.ID.ToString();
            }
            if (Mode == "Aff")
            {
                int ID = int.Parse(Code);
                COMMANDES_CLIENTS CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                SelectedCommande = CommandeClient.ID.ToString();

            }
            if (Print)
            {
                return RedirectToAction("PrintCommandeClientByID", new { CODE = SelectedCommande });
            }
            Session["ProduitsCommandeClient"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Commandes", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendBonLivraison(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string type = Request["type"] != null ? Request["type"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
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
            

            string NumeroBLP = string.Empty;
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
            string SelectedBonLivraison = string.Empty;
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }
            if (Mode == "Create")
            {
                if (!db.BONS_LIVRAISONS_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    int Max = 0;
                    if (db.BONS_LIVRAISONS_CLIENTS.ToList().Count != 0)
                    {
                        Max = db.BONS_LIVRAISONS_CLIENTS.Select(cmd => cmd.ID).Count();
                    }
                    Max++;
                    BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();

                    BonLivraisonClient.CODE = Numero;
                    BonLivraisonClient.DATE = DateTime.Parse(date);
                    BonLivraisonClient.CLIENT = int.Parse(client);
                    int ID_CLIENT = int.Parse(client);
                    BonLivraisonClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    BonLivraisonClient.Societes = 3;
                    int ID_Soc = 3;
                    BonLivraisonClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    BonLivraisonClient.Tiers = int.Parse(Tiers);
                    BonLivraisonClient.MODE_PAIEMENT = modePaiement;
                    BonLivraisonClient.Designation = designation;
                    BonLivraisonClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    BonLivraisonClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    BonLivraisonClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    BonLivraisonClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    BonLivraisonClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    BonLivraisonClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    BonLivraisonClient.Type = Boolean.Parse(type);
                  
                    db.BONS_LIVRAISONS_CLIENTS.Add(BonLivraisonClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        if (BonLivraisonClient.Type==false)
                        {
                            int Max2 = 0;
                            if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
                            {
                                Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
                            }
                            Max2++;
                            BONS_LIVRAISONS_PART_CLIENTS BonLivraisonPartClient = new BONS_LIVRAISONS_PART_CLIENTS();
                            NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");

                            BonLivraisonPartClient.CODE = NumeroBLP;
                            BonLivraisonPartClient.Code_Article = Ligne.ID;
                            BonLivraisonPartClient.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                            BonLivraisonPartClient.DATE = DateTime.Parse(date);
                            BonLivraisonPartClient.QTELIV = (double)Ligne.QUANTITELiv;
                            BonLivraisonPartClient.QTERES = (double)Ligne.QUANTITERES;
                            BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
                            BonLivraisonPartClient.Etat = false;
                            
                            db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);

                            db.SaveChanges();
                        }
                        

                        LIGNES_BONS_LIVRAISONS_CLIENTS UneLigne = new LIGNES_BONS_LIVRAISONS_CLIENTS();


                        UneLigne.Prix_achat = Ligne.ID;
                        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        //UneLigne.Libelle_Prd = Ligne.LIBELLE;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.STOCK = (double)Ligne.STOCK;
                        UneLigne.QTERES = (double)Ligne.QUANTITERES;
                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.BON_LIVRAISON_CLIENT = BonLivraisonClient.ID;
                        UneLigne.BONS_LIVRAISONS_CLIENTS = BonLivraisonClient;
                        //UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_BONS_LIVRAISONS_CLIENTS.Add(UneLigne);

                        db.SaveChanges();
                        //AddMouvementProduit("BON_LIVRAISON", Produit, BonLivraisonClient.DATE, BonLivraisonClient.CODE, Ligne.QUANTITE);
                    }



                    SelectedBonLivraison = BonLivraisonClient.ID.ToString();
                }
            }
           
            if ((Mode == "Edit")|| (Mode == "Editcmd"))
            {
                int ID = int.Parse(Code);
                BONS_LIVRAISONS_CLIENTS BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                BonLivraisonClient.CODE = Numero;
                BonLivraisonClient.DATE = DateTime.Parse(date);
                BonLivraisonClient.CLIENT = int.Parse(client);
                int ID_CLIENT = int.Parse(client);
                BonLivraisonClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                BonLivraisonClient.Societes =3;
                //int ID_Soc = int.Parse(societe);
                //BonLivraisonClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                BonLivraisonClient.Tiers = int.Parse(Tiers);
                BonLivraisonClient.MODE_PAIEMENT = modePaiement;
                BonLivraisonClient.Designation = designation;
                BonLivraisonClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                BonLivraisonClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                BonLivraisonClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                BonLivraisonClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                BonLivraisonClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                BonLivraisonClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                BonLivraisonClient.Type = Boolean.Parse(type);
                db.SaveChanges();
                db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == BonLivraisonClient.ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    if (BonLivraisonClient.Type == false)
                    {
                        
                        int Max2 = 0;
                        if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
                        {
                            Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
                        }
                        Max2++;
                        BONS_LIVRAISONS_PART_CLIENTS BonLivraisonPartClient = new BONS_LIVRAISONS_PART_CLIENTS();
                        NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");

                        BonLivraisonPartClient.CODE = NumeroBLP;
                        BonLivraisonPartClient.Code_Article = Ligne.ID;
                        BonLivraisonPartClient.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        BonLivraisonPartClient.DATE = DateTime.Parse(date);
                        BonLivraisonPartClient.QTELIV = (double)Ligne.QUANTITELiv;
                        BonLivraisonPartClient.QTERES = (double)Ligne.QUANTITERES;
                        BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
                        BonLivraisonPartClient.Etat = false;
                        
                        db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);

                        db.SaveChanges();
                    }
                    

                    LIGNES_BONS_LIVRAISONS_CLIENTS UneLigne = new LIGNES_BONS_LIVRAISONS_CLIENTS();


                    UneLigne.Prix_achat = Ligne.ID;
                    Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.STOCK = (double)Ligne.STOCK;

                    UneLigne.QTERES = (double)Ligne.QUANTITERES;
                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.BON_LIVRAISON_CLIENT = BonLivraisonClient.ID;
                    UneLigne.BONS_LIVRAISONS_CLIENTS = BonLivraisonClient;
                    //UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_BONS_LIVRAISONS_CLIENTS.Add(UneLigne);

                    db.SaveChanges();
                    //AddMouvementProduit("BON_LIVRAISON", Produit, BonLivraisonClient.DATE, BonLivraisonClient.CODE, Ligne.QUANTITE);
                }

                SelectedBonLivraison = BonLivraisonClient.ID.ToString();
            }
            if (Mode == "Aff")
            {
                int ID = int.Parse(Code);
                BONS_LIVRAISONS_CLIENTS BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                SelectedBonLivraison = BonLivraisonClient.ID.ToString();

            }
            if (Print)
            {
                return RedirectToAction("PrintBonLivraisonClientByID", new { CODE = SelectedBonLivraison });
            }
            Session["ProduitsBonLivraisonClient"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("BonLivraison", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendFacture(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string date2 = Request["date2"] != null ? Request["date2"].ToString() : string.Empty;
            string Immobilisation = Request["Immobilisation"] != null ? Request["Immobilisation"].ToString() : string.Empty;
            string service = Request["service"] != null ? Request["service"].ToString() : string.Empty;

            string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
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
            string designation = Request["designation"] != null ? Request["designation"].ToString() : string.Empty;
            string type = Request["type"] != null ? Request["type"].ToString() : string.Empty;

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
            string SelectedFacture = string.Empty;
            if (Session["ProduitsFactureClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            }
            if (Mode == "Create")
            {
                if (!db.FACTURES_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    FACTURES_CLIENTS FactureClient = new FACTURES_CLIENTS();
                    FactureClient.CODE = Numero;
                    FactureClient.DATE = DateTime.Parse(date);
                    FactureClient.CLIENT = int.Parse(client);
                    int ID_CLIENT = int.Parse(client);
                    FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    FactureClient.Societes = 3;
                    if(type=="true")
                    { 
                    FactureClient.Declaration = true;
                    }
                    else
                    {
                    FactureClient.Declaration = false;

                    }
                    if (Immobilisation == "true")
                    {
                        FactureClient.immobilisation = true;
                    }
                    else
                    {
                        FactureClient.immobilisation = false;

                    }
                    if (service == "true")
                    {
                        FactureClient.Bien_service = true;
                    }
                    else
                    {
                        FactureClient.Bien_service = false;

                    }
                    if (date2!="")
                    {
                        FactureClient.Date_Declaration = DateTime.Parse(date2);
                    }
                    int ID_Soc = 3;
                    FactureClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    if (Tiers != "")
                    {
                        FactureClient.Tiers = int.Parse(Tiers);
                    }
                    FactureClient.MODE_PAIEMENT = modePaiement;
                    FactureClient.Designation = designation;
                    FactureClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    FactureClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    FactureClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    FactureClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    FactureClient.TIMBRE = decimal.Parse(timbre, CultureInfo.InvariantCulture);
                    FactureClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    FactureClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                    FactureClient.PAYEE = false;
                    FactureClient.VALIDER = false;

                    db.FACTURES_CLIENTS.Add(FactureClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_FACTURES_CLIENTS UneLigne = new LIGNES_FACTURES_CLIENTS();
                        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        UneLigne.Prix_achat = Ligne.ID;
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.STOCK = (double)Ligne.STOCK;

                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.FACTURE_CLIENT = FactureClient.ID;
                        UneLigne.FACTURES_CLIENTS = FactureClient;
                        UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_FACTURES_CLIENTS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("FACTURE", Produit, FactureClient.DATE, FactureClient.CODE, Ligne.QUANTITE);
                    }
                    SelectedFacture = FactureClient.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                FACTURES_CLIENTS FactureClient = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                FactureClient.CODE = Numero;
                FactureClient.DATE = DateTime.Parse(date);
                FactureClient.CLIENT = int.Parse(client);
                int ID_CLIENT = int.Parse(client);
                FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                FactureClient.Societes =3;
                //int ID_Soc = int.Parse(societe);
                //FactureClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
               
                if (Tiers != "")
                {
                    FactureClient.Tiers = int.Parse(Tiers);
                }
                FactureClient.MODE_PAIEMENT = modePaiement;
                FactureClient.Designation = designation;
                if (type == "true")
                {
                    FactureClient.Declaration = true;
                }
                else
                {
                    FactureClient.Declaration = false;

                }
                if (Immobilisation == "true")
                {
                    FactureClient.immobilisation = true;
                }
                else
                {
                    FactureClient.immobilisation = false;

                }
                if (service == "true")
                {
                    FactureClient.Bien_service = true;
                }
                else
                {
                    FactureClient.Bien_service = false;

                }
                if (date2 != "")
                {
                    FactureClient.Date_Declaration = DateTime.Parse(date2);
                }
                FactureClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                FactureClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                FactureClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                FactureClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                FactureClient.TIMBRE = decimal.Parse(timbre, CultureInfo.InvariantCulture);
                FactureClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                FactureClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_FACTURES_CLIENTS.Where(p => p.FACTURE_CLIENT == FactureClient.ID).ToList().ForEach(p => db.LIGNES_FACTURES_CLIENTS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_FACTURES_CLIENTS UneLigne = new LIGNES_FACTURES_CLIENTS();
                    Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Prix_achat = Ligne.ID;

                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.STOCK = (double)Ligne.STOCK;

                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.FACTURE_CLIENT = FactureClient.ID;
                    UneLigne.FACTURES_CLIENTS = FactureClient;
                    UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_FACTURES_CLIENTS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedFacture = FactureClient.ID.ToString();
            }
            if (Mode == "Aff")
            {
                int ID = int.Parse(Code);
                FACTURES_CLIENTS facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                SelectedFacture = facture.ID.ToString();

            }
            if (Print)
            {
                return RedirectToAction("PrintFactureClientByID", new { CODE = SelectedFacture });
            }
            Session["ProduitsFactureClient"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Facture", new { MODE = Mode });
        }
        [HttpPost]
        public ActionResult SendAvoir(string Mode, string Code)
        {
            string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
            string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
            string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
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
            string SelectedAvoir = string.Empty;
            if (Session["ProduitsAvoirClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            }
            if (Mode == "Create")
            {
                if (!db.AVOIRS_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
                {
                    AVOIRS_CLIENTS AvoirClient = new AVOIRS_CLIENTS();
                    AvoirClient.CODE = Numero;
                    AvoirClient.DATE = DateTime.Parse(date);
                    AvoirClient.CLIENT = int.Parse(client);
                    int ID_CLIENT = int.Parse(client);
                    AvoirClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                    AvoirClient.Societes = 3;
                    int ID_Soc =3;
                    AvoirClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                    AvoirClient.Tiers = int.Parse(Tiers);

                    AvoirClient.MODE_PAIEMENT = modePaiement;
                    
                    AvoirClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                    AvoirClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                    AvoirClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                    AvoirClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                    AvoirClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                    AvoirClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);

                    db.AVOIRS_CLIENTS.Add(AvoirClient);
                    db.SaveChanges();
                    foreach (LigneProduit Ligne in ListeDesPoduits)
                    {
                        LIGNES_AVOIRS_CLIENTS UneLigne = new LIGNES_AVOIRS_CLIENTS();
                        UneLigne.Prix_achat = Ligne.ID;
                        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                        UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                        UneLigne.Marque = Ligne.MARQUE;
                        UneLigne.Unite = Ligne.UNITE;
                        UneLigne.Devise = Ligne.DEVISE;
                        UneLigne.Categorie = Ligne.CATEGORIE;
                        UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                        UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                        UneLigne.STOCK = (double)Ligne.STOCK;

                        UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                        UneLigne.REMISE = Ligne.REMISE;
                        UneLigne.TOTALE_HT = Ligne.PTHT;
                        UneLigne.TVA = Ligne.TVA;
                        UneLigne.TOTALE_TTC = Ligne.TTC;
                        UneLigne.AVOIR_CLIENT = AvoirClient.ID;
                        UneLigne.AVOIRS_CLIENTS = AvoirClient;
                        UneLigne.Prix_Achat1 = Produit;
                        db.LIGNES_AVOIRS_CLIENTS.Add(UneLigne);
                        db.SaveChanges();
                        //AddMouvementProduit("AVOIR", Produit, AvoirClient.DATE, AvoirClient.CODE, Ligne.QUANTITE);
                    }
                    SelectedAvoir = AvoirClient.ID.ToString();
                }
            }
            if (Mode == "Edit")
            {
                int ID = int.Parse(Code);
                AVOIRS_CLIENTS AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                AvoirClient.CODE = Numero;
                AvoirClient.DATE = DateTime.Parse(date);
                AvoirClient.CLIENT = int.Parse(client);
                int ID_CLIENT = int.Parse(client);
                AvoirClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
                AvoirClient.Societes = 3;
                //int ID_Soc = int.Parse(societe);
                //AvoirClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
                AvoirClient.Tiers = int.Parse(Tiers);
                AvoirClient.MODE_PAIEMENT = modePaiement;
                AvoirClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
                AvoirClient.THT = decimal.Parse(totalHT, CultureInfo.InvariantCulture);
                AvoirClient.NHT = decimal.Parse(NetHT, CultureInfo.InvariantCulture);
                AvoirClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture);
                AvoirClient.TTC = decimal.Parse(TotalTTC, CultureInfo.InvariantCulture);
                AvoirClient.TNET = decimal.Parse(netAPaye, CultureInfo.InvariantCulture);
                db.SaveChanges();
                db.LIGNES_AVOIRS_CLIENTS.Where(p => p.AVOIR_CLIENT == AvoirClient.ID).ToList().ForEach(p => db.LIGNES_AVOIRS_CLIENTS.Remove(p));
                db.SaveChanges();
                foreach (LigneProduit Ligne in ListeDesPoduits)
                {
                    LIGNES_AVOIRS_CLIENTS UneLigne = new LIGNES_AVOIRS_CLIENTS();
                    UneLigne.Prix_achat = Ligne.ID;
                    Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
                    UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                    UneLigne.Marque = Ligne.MARQUE;
                    UneLigne.Unite = Ligne.UNITE;
                    UneLigne.Devise = Ligne.DEVISE;
                    UneLigne.Categorie = Ligne.CATEGORIE;
                    UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
                    UneLigne.QUANTITE = (double)Ligne.QUANTITE;
                    UneLigne.STOCK = (double)Ligne.STOCK;

                    UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                    UneLigne.REMISE = Ligne.REMISE;
                    UneLigne.TOTALE_HT = Ligne.PTHT;
                    UneLigne.TVA = Ligne.TVA;
                    UneLigne.TOTALE_TTC = Ligne.TTC;
                    UneLigne.AVOIR_CLIENT = AvoirClient.ID;
                    UneLigne.AVOIRS_CLIENTS = AvoirClient;
                    UneLigne.Prix_Achat1 = Produit;
                    db.LIGNES_AVOIRS_CLIENTS.Add(UneLigne);
                    db.SaveChanges();
                }
                SelectedAvoir = AvoirClient.ID.ToString();
            }
            if (Mode == "Aff")
            {
                int ID = int.Parse(Code);
                AVOIRS_CLIENTS AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
                SelectedAvoir = AvoirClient.ID.ToString();

            }
            if (Print)
            {
                return RedirectToAction("PrintAvoirClientByID", new { CODE = SelectedAvoir });
            }
            Session["ProduitsAvoirClient"] = null;
            ViewData["MODE"] = Mode;
            ViewBag.MODE = Mode;
            return RedirectToAction("Avoir", new { MODE = Mode });
        }
     


        #region Delete
        public string DeleteDevis(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
            db.SaveChanges();
            DEVIS_CLIENTS Devis = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.DEVIS_CLIENTS.Remove(Devis);
            db.SaveChanges();
            //string[] code = Devis.CODE.Split('0');
            //string[] code1 = code[1].Split('/');
            //int code2 = int.Parse(code1[0]);
            //db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).Max(cmd => cmd.CODE);
            //int Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year ==int.Parse(code1[1])).Select(cmd => cmd.ID).Count();
            //for(int i=code2;i<Max;i++)
            //{

            //}
            return string.Empty;
        }
        public string DeleteCommande(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_COMMANDES_CLIENTS.Where(p => p.COMMANDE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_COMMANDES_CLIENTS.Remove(p));
            db.SaveChanges();
            COMMANDES_CLIENTS Devis = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.COMMANDES_CLIENTS.Remove(Devis);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteBonLaivraison(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
            db.SaveChanges();
            BONS_LIVRAISONS_CLIENTS Devis = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.BONS_LIVRAISONS_CLIENTS.Remove(Devis);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            db.LIGNES_FACTURES_CLIENTS.Where(p => p.FACTURE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_FACTURES_CLIENTS.Remove(p));
            db.SaveChanges();
            FACTURES_CLIENTS Devis = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            db.FACTURES_CLIENTS.Remove(Devis);
            db.SaveChanges();
            return string.Empty;
        }
        public string DeleteLineDevis(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsDevisClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsDevisClient"] = ListeDesPoduits;
            Session["ProduitsDevisClient2"] = null;

            return string.Empty;
        }
        public string DeleteLineCommande(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsCommandeClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsCommandeClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string DeleteLineBonLivraison(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsBonLivraisonClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string DeleteLineFacture(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsFactureClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsFactureClient"] = ListeDesPoduits;
            return string.Empty;
        }
        public string DeleteLineAvoir(string parampassed)
        {
            List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
            if (Session["ProduitsAvoirClient"] != null)
            {
                ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
            }
            int ID = int.Parse(parampassed);
            LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            Session["ProduitsAvoirClient"] = ListeDesPoduits;
            return string.Empty;
        }

        #endregion
        #region specifique fonctions  
        public ActionResult HeuresPlanification(int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttHeuresPlanification", "GanttDiag");
        }
        public ActionResult ArticlesdevisCategorie(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {
            int idd = int.Parse(id);
            Categorie cat = db.Categorie.Where(ca => ca.CentreID == idd).FirstOrDefault();
            List<LIGNES_DEVIS_FOURNISSEURS> lignes = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.Categorie == cat.Libelle).ToList();
           
            DateTime date = DateTime.Today;
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.Date = date;
            ViewBag.Art = cat.Libelle;
            ViewBag.numero = numero;
            ViewBag.designation = designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            ViewBag.Date1 = Date;
            return PartialView(lignes);
            

        }
        public ActionResult Articlesdevis(int id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {
            Session["ProduitsDevisClient2"] = null;
            LIGNES_DEVIS_FOURNISSEURS ligne = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.ID == id).FirstOrDefault();
            var lignes_devis_frs = new List<LIGNES_DEVIS_FOURNISSEURS>();
            var ligne_devis_frs = (from m in db.LIGNES_DEVIS_FOURNISSEURS
                                   where m.Libelle_Prd.Equals(ligne.Libelle_Prd)
                                   orderby m.ID
                                   select m
                                   );
            lignes_devis_frs.AddRange(ligne_devis_frs);
            DateTime date = DateTime.Today;
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.Date = date;
            ViewBag.numero = numero;
            ViewBag.designation = designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            ViewBag.Art = ligne.Libelle_Prd;
            ViewBag.Date1 = Date;
            return PartialView(lignes_devis_frs.ToList());

        }
        public ActionResult ArticlesdevisMarque(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {

            List<LIGNES_DEVIS_FOURNISSEURS> lignes = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.Marque == id).ToList();
            DateTime date = DateTime.Today;
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.Date = date;
            ViewBag.Art = id;
            ViewBag.numero = numero;
            ViewBag.designation = designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            ViewBag.Date1 = Date;
            return PartialView(lignes);



        }
        public ActionResult ArticlesdevisDes(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
        {

            List<LIGNES_DEVIS_FOURNISSEURS> lignes = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.DESIGNATION_PRODUIT == id).ToList();
            DateTime date = DateTime.Today;
            ViewBag.Mode = Mode;
            ViewBag.Code = Code;
            ViewBag.Date = date;
            ViewBag.Art = id;
            ViewBag.numero = numero;
            ViewBag.designation = designation;
            ViewBag.modePaiement = modePaiement;
            ViewBag.client = client;
            ViewBag.codeClient = codeClient;
            ViewBag.Tiers = Tiers;
            ViewBag.remise = remise;
            ViewBag.Date1 = Date;
            return PartialView(lignes);

        }
        public string Addlignedevis2(string ID)
        {
            Session["ProduitsDevisClient2"] = null;
            db.Configuration.ProxyCreationEnabled = false;
            int id = int.Parse(ID);
            LIGNES_DEVIS_FOURNISSEURS ligne2 = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.ID == id).FirstOrDefault();
            LigneProduit ligne = new LigneProduit();
            ligne.ID = id;
            ligne.LIBELLE = ligne2.Libelle_Prd;
            ligne.NumDevis = (ligne2.DEVIS_CLIENT).ToString();
            ligne.DESIGNATION = ligne2.DESIGNATION_PRODUIT;
            ligne.MARQUE = ligne2.Marque;
            ligne.UNITE = ligne2.Unite;
            ligne.DEVISE = ligne2.Devise;
            ligne.CATEGORIE = ligne2.Categorie;
            ligne.Sous_CATEGORIE = ligne2.Sous_Categorie;
            ligne.QUANTITE = (decimal)(ligne2.QUANTITE);
            ligne.REMISE = (decimal)ligne2.REMISE;
            ligne.PRIX_VENTE_HT = (decimal)ligne2.PRIX_UNITAIRE_HT-(((decimal)ligne2.PRIX_UNITAIRE_HT*ligne.REMISE)/100);
            //ligne.PTHT = (decimal)ligne2.TOTALE_HT;
            ligne.PTHT = ligne.PRIX_VENTE_HT;
            ligne.TVA = (int)ligne2.TVA;
            ligne.TTC = ligne.PTHT + (ligne.PTHT * ligne.TVA)/100;
            Session["ProduitsDevisClient2"] = ligne;
            return string.Empty;
        }
        public JsonResult GetAllLineDevis2()
        {
            db.Configuration.ProxyCreationEnabled = false;
            LigneProduit ListeDesPoduits2 = (LigneProduit)Session["ProduitsDevisClient2"];
            return Json(ListeDesPoduits2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNumeroDevis(string DATE, string Mode, string num)
        {
            DateTime d = DateTime.Parse(DATE);
            string Numero1;
            int Nb = 0;
            db.Configuration.ProxyCreationEnabled = false;
            if (Mode == "Edit")
            {
                string[] code = num.Split('/');
                int y = int.Parse(code[1]);
                string an = d.Year.ToString();
                string[] an1 = an.Split('0');
                int an2 = int.Parse(an1[1]);
                if (an2 == y)
                {
                    Numero1 = num;
                }
                else
                {
                    int Max = 0;
                    if (db.DEVIS_CLIENTS.ToList().Count != 0)
                    {
                        Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                    }
                    Max++;

                    Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
                    List<DEVIS_CLIENTS> frs = db.DEVIS_CLIENTS.ToList();
                    foreach (DEVIS_CLIENTS f in frs)
                    {
                        string[] con = f.CODE.Split('C');
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

                        Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
                    }
                    else
                    {
                        Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");

                    }

                }
            }
            else
            {
                int Max = 0;
                if (db.DEVIS_CLIENTS.ToList().Count != 0)
                {
                    Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;

                Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
                List<DEVIS_CLIENTS> frs = db.DEVIS_CLIENTS.ToList();
                foreach (DEVIS_CLIENTS f in frs)
                {
                    string[] con = f.CODE.Split('C');
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

                    Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
                }
                else
                {
                    Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");

                }

            }
            return Json(Numero1, JsonRequestBehavior.AllowGet);
        }
        public string validateBonLivraison(string parampassed,string type)
        {
            string var = "";
            int count=0;
            int ID = int.Parse(parampassed);
            BONS_LIVRAISONS_CLIENTS Bonlivraison = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_BONS_LIVRAISONS_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
            foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in liste)
            {
                Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.DESIGNATION_PRODUIT).FirstOrDefault();
                Prix_Achat prixAchat = new Prix_Achat();
                if ((Produit != null) && (Produit.Stock > ligne.QUANTITE))
                {
                    count++;
                    Produit.Stock -= ((double)ligne.QUANTITE-(double)ligne.QTERES);

                }
             
              
            }
            if(count>=liste.Count)
            {

                Bonlivraison.VALIDER = true;
                if (type != "")
                {
                    Bonlivraison.Type = Boolean.Parse(type);
                }
                else
                {
                    Bonlivraison.Type = true;
                }
                db.SaveChanges();
                var= string.Empty;
            }
            else
            {
                var="NO";
            }
            return var;

        }
        public string validateFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_CLIENTS facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            facture.VALIDER = true;
            db.SaveChanges();
            return string.Empty;
        }
        public string validateAvoir(string parampassed)
        {
            int ID = int.Parse(parampassed);
            AVOIRS_CLIENTS Avoir = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            Avoir.VALIDER = true;
            db.SaveChanges();
            return string.Empty;
        }
        public string PayeFacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_CLIENTS Facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Facture.VALIDER)
            {
                Facture.PAYEE = true;
            }
            db.SaveChanges();
            return string.Empty;
        }
        public string DevisVersCommande(string parampassed)
        {
            int ID = int.Parse(parampassed);
            DEVIS_CLIENTS Element = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_DEVIS_CLIENTS> Liste = db.LIGNES_DEVIS_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();
            COMMANDES_CLIENTS NewElement = new COMMANDES_CLIENTS();
            string Numero = string.Empty;
            int Max = 0;
            if (db.COMMANDES_CLIENTS.ToList().Count != 0)
            {
                Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "CDC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            NewElement.Designation = Element.Designation;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            NewElement.Designation = Element.Designation;
            NewElement.CLIENT = Element.CLIENT;
            NewElement.Societes = (int)Element.Societes;
            NewElement.Tiers = (int)Element.Tiers;

            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TTC = Element.TTC;
            NewElement.TNET = Element.TNET;
            NewElement.VALIDER = false;
            NewElement.REMISE = Element.REMISE;
            NewElement.DEVIS_CLIENT = Element.ID;
            NewElement.DEVIS_CLIENTS = Element;
            NewElement.CLIENTS = Element.CLIENTS;
           NewElement.Societes1 = Element.Societes1;
            db.COMMANDES_CLIENTS.Add(NewElement);
            db.SaveChanges();
            foreach (LIGNES_DEVIS_CLIENTS Ligne in Liste)
            {
                LIGNES_COMMANDES_CLIENTS NewLine = new LIGNES_COMMANDES_CLIENTS();
                NewLine.Prix_achat = (int)Ligne.Art_Devis_Frs;
                //NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                NewLine.Marque = Ligne.Marque;
                NewLine.Devise = Ligne.Devise;
                NewLine.Unite = Ligne.Unite;
                NewLine.Categorie = Ligne.Categorie;
                NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                NewLine.QUANTITE = (double)Ligne.QUANTITE;
                //NewLine.STOCK = 0;
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HTVente;
                NewLine.REMISE = Ligne.REMISE;
                NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                NewLine.COMMANDE_CLIENT = NewElement.ID;
                NewLine.COMMANDES_CLIENTS = NewElement;
                //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                db.LIGNES_COMMANDES_CLIENTS.Add(NewLine);
                db.SaveChanges();
                
                //AddMouvementProduit("COMMANDE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }
            ////////////////////////////
            //Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

            //this.db.SaveChanges();
            //if (Element.convert == false)
            //{
            //    var projetTechnique = new ProjetTechniques()
            //    {

            //        ProjetTechniqueId = NewElement.ID,
            //        ReferenceTech = param.RefTech + param.CompteurTech,
            //        DateDebut = DateTime.Now,
            //        DateFin = DateTime.Now,
            //        Cout = (float)NewElement.TTC,
            //        ClientId = NewElement.CLIENT,
            //        PersonnelId = NewElement.Societes,
            //        Designation = NewElement.Designation,
            //        Statut = "Initié",
            //        CoutReel = (float)NewElement.TTC,
            //        DateDebutReel = DateTime.Now,
            //        DateFinReel = DateTime.Now

            //    };
                
            //    //this.db.AffaireCommerciales.Remove(affaireCommerciale);
            //    this.db.ProjetTechniques.Add(projetTechnique);

            //    try
            //    {
            //        var pt = db.ProjetTechniques
            //            .OrderByDescending(p => p.ProjetTechniqueId)
            //            .FirstOrDefault();
            //        String ch = pt.ReferenceTech.ToString();
            //        projetTechnique.ReferenceTech = param.RefTech + param.CompteurTech;
            //        param.CompteurTech = param.CompteurTech + 1;




            //    }
            //    catch
            //    {
            //        //affaireCommerciale.Reference = param.RefCom + param.CompteurCom;
            //        param.CompteurTech = param.CompteurTech + 1;
            //    }
            //    Element.convert = true;
            //    db.Entry(Element).State = System.Data.Entity.EntityState.Modified;
            //    this.db.SaveChanges();
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 0, JourLibelle = "Dimanche", jourTravail = false, doubleSeance = false, seance1Debut = 0, seance1Fin = 0, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 1, JourLibelle = "Lundi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 2, JourLibelle = "Mardi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 3, JourLibelle = "Mercredi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 4, JourLibelle = "Jeudi", jourTravail = true, doubleSeance = true, seance1Debut = 8, seance1Fin = 12, seance2Debut = 13, seance2Fin = 17, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 5, JourLibelle = "Vendredi", jourTravail = true, doubleSeance = false, seance1Debut = 8, seance1Fin = 14, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.ParametrageSemaines.Add(new ParametrageSemaines() { JourId = 6, JourLibelle = "Samedi", jourTravail = false, doubleSeance = false, seance1Debut = 0, seance1Fin = 0, seance2Debut = 0, seance2Fin = 0, projetTechniqueID = db.ProjetTechniques.Max(p => p.ProjetTechniqueId) });
            //    db.SaveChanges();
                ///////////////////////////
            //}
            return NewElement.ID.ToString();
        }
        public string CommandeVersBonLivraison(string parampassed)
        {
            int ID = int.Parse(parampassed);
            COMMANDES_CLIENTS Element = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_COMMANDES_CLIENTS> Liste = db.LIGNES_COMMANDES_CLIENTS.Where(cmd => cmd.COMMANDE_CLIENT == ID).ToList();
            BONS_LIVRAISONS_CLIENTS NewElement = new BONS_LIVRAISONS_CLIENTS();
            string Numero = string.Empty;
            int Max = 0;
            if (db.BONS_LIVRAISONS_CLIENTS.ToList().Count != 0)
            {
                Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "BLC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            NewElement.Designation = Element.Designation;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            NewElement.Designation = Element.Designation;

            NewElement.CLIENT = Element.CLIENT;
            NewElement.Societes = (int)Element.Societes;
            NewElement.Tiers = (int)Element.Tiers;
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TTC = Element.TTC;
            NewElement.TNET = Element.TNET;
            NewElement.VALIDER = false;
            NewElement.REMISE = (Decimal)Element.REMISE;
            NewElement.COMMANDE_CLIENT = Element.ID;
            NewElement.COMMANDES_CLIENTS = Element;
            NewElement.CLIENTS = Element.CLIENTS;
            NewElement.Societes1 = Element.Societes1;
            db.BONS_LIVRAISONS_CLIENTS.Add(NewElement);
            db.SaveChanges();
            foreach (LIGNES_COMMANDES_CLIENTS Ligne in Liste)
            {
                LIGNES_BONS_LIVRAISONS_CLIENTS NewLine = new LIGNES_BONS_LIVRAISONS_CLIENTS();
                NewLine.Prix_achat = (int)Ligne.Prix_achat;
                //NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                NewLine.Marque = Ligne.Marque;
                NewLine.Devise = Ligne.Devise;
                NewLine.Unite = Ligne.Unite;
                NewLine.Categorie = Ligne.Categorie;
                NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                NewLine.QUANTITE = Ligne.QUANTITE;
                //QUANTITELIV=QUANTITE
                NewLine.STOCK = Ligne.STOCK;
                NewLine.QTERES = 0;
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                NewLine.REMISE = Ligne.REMISE;
                NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                NewLine.BON_LIVRAISON_CLIENT = NewElement.ID;
                NewLine.BONS_LIVRAISONS_CLIENTS = NewElement;
                //NewLine.Prix_Achat1 = Ligne.LIGNES_DEVIS_FOURNISSEURS;
                db.LIGNES_BONS_LIVRAISONS_CLIENTS.Add(NewLine);
                db.SaveChanges();
                //AddMouvementProduit("BON_LIVRAISON", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }
            return NewElement.ID.ToString();
        }
        public string BonLivraisonVersfacture(string parampassed)
        {
            int ID = int.Parse(parampassed);
            BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Element.VALIDER)
            {
                List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
                FACTURES_CLIENTS NewElement = new FACTURES_CLIENTS();
                string Numero = string.Empty;
                int Max = 0;
                if (db.FACTURES_CLIENTS.ToList().Count != 0)
                {
                    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "FC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
                NewElement.CODE = Numero;
                NewElement.DATE = Element.DATE;
                NewElement.Designation = Element.Designation;
                NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
                NewElement.Designation = Element.Designation;

                NewElement.CLIENT = Element.CLIENT;
                NewElement.Societes = Element.Societes;
                // NewElement.Tiers = Element.Tiers;
                NewElement.THT = Element.THT;
                NewElement.TTVA = Element.TTVA;
                NewElement.NHT = Element.NHT;
                NewElement.TIMBRE = decimal.Parse("0,6");
                NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
                NewElement.TNET = Element.TNET + NewElement.TIMBRE;
                NewElement.VALIDER = false;
                NewElement.PAYEE = false; 
                NewElement.REMISE = Element.REMISE;
                NewElement.BON_LIVRAISON_CLIENT = Element.ID;
                NewElement.BONS_LIVRAISONS_CLIENTS = Element;
                NewElement.CLIENTS = Element.CLIENTS;
                NewElement.Societes1 = Element.Societes1;
                db.FACTURES_CLIENTS.Add(NewElement);
                db.SaveChanges();
                foreach (LIGNES_BONS_LIVRAISONS_CLIENTS Ligne in Liste)
                {
                    LIGNES_FACTURES_CLIENTS NewLine = new LIGNES_FACTURES_CLIENTS();
                    NewLine.Prix_achat = (int)Ligne.Prix_achat;
                    // NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                    NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                    NewLine.Marque = Ligne.Marque;
                    NewLine.Devise = Ligne.Devise;
                    NewLine.Unite = Ligne.Unite;
                    NewLine.Categorie = Ligne.Categorie;
                    NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                    NewLine.QUANTITE = Ligne.QUANTITE;
                    //NewLine.QUANTITE = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == Element.ID && cmd.Code_Article = Ligne.Prix_achat).FirstOrDefault().QTELIV;
                    NewLine.STOCK = Ligne.STOCK;
                    NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = Ligne.REMISE;
                    NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                    NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                    NewLine.TVA = Ligne.TVA;
                    NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                    NewLine.FACTURE_CLIENT = NewElement.ID;
                    NewLine.FACTURES_CLIENTS = NewElement;
                    //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                    db.LIGNES_FACTURES_CLIENTS.Add(NewLine);
                    db.SaveChanges();
                    //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
                }
                return NewElement.ID.ToString();
            }
            return "NO";
        }
        [HttpPost]
        public ActionResult BonLivraisonParVersfacture(FormCollection formCollection, string Code)
        {
            string[] ids = formCollection["affComId"].Split(new char[] { ',' });
            //string Idd = ids[0];
            int ID = int.Parse(Code);
            BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            FACTURES_CLIENTS NewElement = new FACTURES_CLIENTS();

            if (!Element.VALIDER)
            {
                return RedirectToAction("BonLivraisonPartiel", "Vente", new { @id = Element.ID.ToString() });

            }

            List<LigneProduit> listepr = new List<LigneProduit>();
            List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
            //string CODE = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            
            foreach (string Id in ids)
            {
                int idd = int.Parse(Id);
               
                List<BONS_LIVRAISONS_PART_CLIENTS> blpart = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == ID).ToList();

                //int cod_art = (int)(blpart.Code_Article);
                //List<int> code_art = List<int>(blpart.Code_Article);
                foreach (LIGNES_BONS_LIVRAISONS_CLIENTS lignebl in Liste)
                {
                    foreach (BONS_LIVRAISONS_PART_CLIENTS ligneblb in blpart)
                    {
                        ligneblb.Etat = true;
                        if (lignebl.Prix_achat == ligneblb.Code_Article)
                        {
                            LigneProduit lignepr = new LigneProduit();

                            lignepr.ID = (int)ligneblb.Code_Article;
                            //lignepr.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == lignepr.ID).FirstOrDefault().Libelle;
                            lignepr.DESIGNATION = lignebl.DESIGNATION_PRODUIT;
                            lignepr.MARQUE = lignebl.Marque;
                            lignepr.DEVISE = lignebl.Devise;
                            lignepr.UNITE = lignebl.Unite;
                            lignepr.STOCK = (int)lignebl.STOCK;
                            lignepr.CATEGORIE = lignebl.Categorie;
                            lignepr.Sous_CATEGORIE = lignebl.Sous_Categorie;
                            lignepr.PRIX_VENTE_HT = (decimal)lignebl.PRIX_UNITAIRE_HT;
                            lignepr.TVA = (int)lignebl.TVA;
                            lignepr.REMISE = (int)lignebl.REMISE;
                            //lignepr.TTC = (decimal)lignebl.TOTALE_TTC;

                            lignepr.QUANTITE +=((int)ligneblb.QTELIV);
                            double qtee = (double)lignepr.QUANTITE;
                            decimal puht = lignepr.PRIX_VENTE_HT;
                            decimal ptohtssremise = (decimal)qtee * puht;
                            decimal remise = lignepr.REMISE;
                            decimal mtr = (ptohtssremise * remise) / 100;
                            decimal pthtproduit = ptohtssremise - mtr;
                            lignepr.PTHT = pthtproduit;
                            int tva = lignepr.TVA;
                            decimal mttv = (pthtproduit * tva) / 100;
                            decimal ttcproduit = pthtproduit + mttv;
                            lignepr.TTC = ttcproduit;
                            listepr.Add(lignepr);

                       }

                    }
                    
                }
           
                }
            string Numero = string.Empty;
            int Max = 0;
            if (db.FACTURES_CLIENTS.ToList().Count != 0)
            {
                Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
            }
            Max++;
            Numero = "FC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
            NewElement.CODE = Numero;
            NewElement.DATE = Element.DATE;
            NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
            NewElement.Designation = Element.Designation;

            NewElement.CLIENT = Element.CLIENT;
            NewElement.Societes = Element.Societes;
            // NewElement.Tiers = Element.Tiers;
            NewElement.THT = Element.THT;
            NewElement.TTVA = Element.TTVA;
            NewElement.NHT = Element.NHT;
            NewElement.TIMBRE = decimal.Parse("0,6");
            NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
            NewElement.TNET = Element.TNET + NewElement.TIMBRE;
            NewElement.VALIDER = false;
            NewElement.PAYEE = false;

            NewElement.REMISE = Element.REMISE;
            NewElement.BON_LIVRAISON_CLIENT = Element.ID;
            NewElement.BONS_LIVRAISONS_CLIENTS = Element;
            NewElement.CLIENTS = Element.CLIENTS;
            //NewElement.Societes1 = Element.Societes1;
            db.FACTURES_CLIENTS.Add(NewElement);
            db.SaveChanges();
            foreach (LigneProduit Ligne in listepr)
            {
                LIGNES_FACTURES_CLIENTS NewLine = new LIGNES_FACTURES_CLIENTS();
                NewLine.Prix_achat = (int)Ligne.ID;
                // NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
                NewLine.Marque = Ligne.MARQUE;
                NewLine.Devise = Ligne.DEVISE;
                NewLine.Unite = Ligne.UNITE;
                NewLine.Categorie = Ligne.CATEGORIE;
                NewLine.Sous_Categorie = Ligne.Sous_CATEGORIE;
                NewLine.QUANTITE = (double)Ligne.QUANTITE;
                NewLine.STOCK = (int)Ligne.STOCK;
                NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
                NewLine.REMISE = Ligne.REMISE;
                //NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                NewLine.TOTALE_HT = Ligne.PTHT;
                NewLine.TVA = Ligne.TVA;
                NewLine.TOTALE_TTC = Ligne.TTC;
                NewLine.FACTURE_CLIENT = NewElement.ID;
                NewLine.FACTURES_CLIENTS = NewElement;
                //NewLine.Prix_Achat1 = Ligne.;
                db.LIGNES_FACTURES_CLIENTS.Add(NewLine);
                db.SaveChanges();
                //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
            }
            return RedirectToAction("FormFacture", "Vente", new { @Mode = "Edit", @Code = NewElement.ID.ToString() });

        }
        public string FactureVersAvoir(string parampassed)
        {
            int ID = int.Parse(parampassed);
            FACTURES_CLIENTS Element = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            if (Element.VALIDER)
            {
                List<LIGNES_FACTURES_CLIENTS> Liste = db.LIGNES_FACTURES_CLIENTS.Where(cmd => cmd.FACTURE_CLIENT == ID).ToList();
                AVOIRS_CLIENTS NewElement = new AVOIRS_CLIENTS();
                string Numero = string.Empty;
                int Max = 0;
                if (db.AVOIRS_CLIENTS.ToList().Count != 0)
                {
                    Max = db.AVOIRS_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
                }
                Max++;
                Numero = "AVC" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
                NewElement.CODE = Numero;
                NewElement.DATE = Element.DATE;
                NewElement.Designation = Element.Designation;
                NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
                NewElement.Designation = Element.Designation;

                NewElement.CLIENT = Element.CLIENT;
                NewElement.Societes = (int)Element.Societes;
                NewElement.THT = Element.THT;
                NewElement.TTVA = Element.TTVA;
                NewElement.NHT = Element.NHT;
                NewElement.TTC = (decimal)(Element.TTC + Element.TIMBRE);
                NewElement.TNET = Element.TNET + Element.TIMBRE;
                NewElement.VALIDER = false;
                NewElement.REMISE = Element.REMISE;
                NewElement.FACTURE_CLIENT = Element.ID;
                NewElement.FACTURES_CLIENTS = Element;
                NewElement.CLIENTS = Element.CLIENTS;
                db.AVOIRS_CLIENTS.Add(NewElement);
                db.SaveChanges();
                foreach (LIGNES_FACTURES_CLIENTS Ligne in Liste)
                {
                    LIGNES_AVOIRS_CLIENTS NewLine = new LIGNES_AVOIRS_CLIENTS();

                    NewLine.Prix_achat = (int)Ligne.Prix_achat;
                    // NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
                    NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
                    NewLine.Marque = Ligne.Marque;
                    NewLine.Devise = Ligne.Devise;
                    NewLine.Unite = Ligne.Unite;
                    NewLine.Categorie = Ligne.Categorie;
                    NewLine.Sous_Categorie = Ligne.Sous_Categorie;
                    NewLine.QUANTITE = Ligne.QUANTITE;
                    NewLine.STOCK = Ligne.STOCK;
                    NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
                    NewLine.REMISE = Ligne.REMISE;
                    NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
                    NewLine.TOTALE_HT = Ligne.TOTALE_HT;
                    NewLine.TVA = Ligne.TVA;
                    NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
                    NewLine.AVOIR_CLIENT = NewElement.ID;
                    NewLine.AVOIRS_CLIENTS = NewElement;
                    NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
                    db.LIGNES_AVOIRS_CLIENTS.Add(NewLine);
                    db.SaveChanges();
                    //AddMouvementProduit("AVOIR", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
                }
                return NewElement.ID.ToString();
            }
            return "NO";
        }


        #endregion
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
        //    if (mode == "BON_LIVRAISON")
        //    {
        //        UnMouvement.QUANTITE_APRES_MOUVEMENT = (int)produit.QUANTITE - quantite;
        //        Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == produit.Product_ID).FirstOrDefault();
        //        Produit.QUANTITE = Produit.QUANTITE - quantite;
        //        db.SaveChanges();
        //    }
        //    db.MOUVEMENETS_PRODUITS.Add(UnMouvement);
        //    db.SaveChanges();

        //}


    }
   

}
