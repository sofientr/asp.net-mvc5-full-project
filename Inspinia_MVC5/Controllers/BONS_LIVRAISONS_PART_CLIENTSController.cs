using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class BONS_LIVRAISONS_PART_CLIENTSController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();
        // GET: BONS_LIVRAISONS_PART_CLIENTS
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string[] ids = formCollection["affComId"].Split(new char[] { ',' });
            FACTURES_CLIENTS NewElement = new FACTURES_CLIENTS();
            string Idd = ids[0];
            int ID = int.Parse(Idd);

            BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();

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
            NewElement.REMISE = Element.REMISE;
            NewElement.BON_LIVRAISON_CLIENT = Element.ID;
            NewElement.BONS_LIVRAISONS_CLIENTS = Element;
            NewElement.CLIENTS = Element.CLIENTS;
            //NewElement.Societes1 = Element.Societes1;
            db.FACTURES_CLIENTS.Add(NewElement);
            db.SaveChanges();
            //foreach (string Id in ids)
            //{ 
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


            return View("FormFacture");
                }
}
}