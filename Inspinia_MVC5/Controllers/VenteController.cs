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
		private MED_TRABELSI db = new MED_TRABELSI();
		#region Views
		public ActionResult StockFournisseur(string Code)
		{
			int id = int.Parse(Code);
			ViewBag.id = id;
			BONS_LIVRAISONS_CLIENTS Bonlivraison = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == id).FirstOrDefault();
			List<LIGNES_BONS_LIVRAISONS_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == id).ToList();
			List<Prix_Achat> listPrixAchat1 = db.Prix_Achat.ToList();
			List<Prix_Achat> listPrixAchat = new List<Prix_Achat>();
			int iddevisbl = (int)Bonlivraison.COMMANDES_CLIENTS.DEVIS_CLIENT;
			foreach (LIGNES_BONS_LIVRAISONS_CLIENTS bl in liste)
			{
				LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == iddevisbl && f.Libelle_Prd == bl.Libelle_Prd).FirstOrDefault();
				int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
				int frs = db.DEVIS_FOURNISSEURS.Where(f => f.ID == iddevis).FirstOrDefault().FOURNISSEUR;

				Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == bl.Libelle_Prd).FirstOrDefault();
				Prix_Achat prixAchat = new Prix_Achat();

				List<Prix_Achat> listArtParFrs = db.Prix_Achat.Where(f => f.Fournisseur == frs && f.Libelle == bl.Libelle_Prd).ToList();
				List<Prix_Achat> listArtParFrs2 = db.Prix_Achat.Where(f => f.Fournisseur != frs && f.Libelle == bl.Libelle_Prd).ToList();

				double stock = 0;
				if (listArtParFrs != null)
				{
					foreach (Prix_Achat p in listArtParFrs)
					{
						stock += (double)p.Stock;
					}
				}
				if ((listArtParFrs == null) || (stock < bl.QUANTITE))
				{
					listPrixAchat = listArtParFrs2;
				}

			}
			ViewBag.listPrixAchat = listPrixAchat;
			return PartialView("StockFournisseur", listPrixAchat);
		}
		public ActionResult ACCESSOIRE(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string IdAffaireCommercial, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINEDevisClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;
			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.CAISSON = CAISSON;
			Session["CAISSON"] = CAISSON;
			Session["SSCAISSON"] = SSCAISSON;
			Session["IDTYPCAISSON"] = IDTYPCAISSON;
			Session["QuantiteCAISSON"] = QuantiteCAISSON;
			Session["IdAffaireCommercial"] = IdAffaireCommercial;
			Session["LIB_SSCAISSON"] = LIB_SSCAISSON;
			Session["TYPCAISSON"] = TYPCAISSON;

			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIRE");
		}
		public ActionResult ACCESSOIRECMD(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINECommandeClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.CAISSON = CAISSON;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			//ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIRECMD");
		}
		public ActionResult ACCESSOIREBONLIV(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINEBLClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.CAISSON = CAISSON;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIREBONLIV");
		}
		public ActionResult ACCESSOIREFACTURE(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.CAISSON = CAISSON;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIREFACTURE");
		}
		public ActionResult ACCESSOIRECAISSE(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINECAISSEClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.CAISSON = CAISSON;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIRECAISSE");
		}
		public ActionResult ACCESSOIREAvoir(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string SSCAISSON, string QuantiteCAISSON, string CAISSON, string IDTYPCAISSON, string LIB_SSCAISSON, string TYPCAISSON, string LignesCuisine)
		{
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				int idcaisson = int.Parse(SSCAISSON);
				//int idacc = (int)db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ID_ACC;
				//List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == idacc).ToList();

				//foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				//{
				//    LignesACCESSOIRE des = new LignesACCESSOIRE();
				//    count = ListeDesPoduits22.Count() + 1;
				//    while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
				//    {
				//        count = count + 1;
				//    }
				//    des.ID = count;
				//    des.IDArticle = (int)ligne.ID_ART;
				//    des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
				//    des.IDDESIGNATION = (int)ligne.ID_SSCAT;
				//    des.DESIGNATION = ligne.Designation;
				//    des.PUHT = (decimal)ligne.PUHT;
				//    des.PTHT = (decimal)ligne.PTHT;
				//    des.TVA = (int)ligne.TVA;
				//    des.TTC = (decimal)ligne.PTTC;
				//    des.QTE = (decimal)ligne.QTE;
				//    ListeDesPoduits22.Add(des);
				//}
				//Session["LignesACC"] = ListeDesPoduits22;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;
			}
			else
			{
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesCuisine>)Session["CUISINEAvoirClient"];
				LignesCuisine ligne = ListeDesPoduits2.Where(f => f.ID == idligne).FirstOrDefault();
				int idcaisson = ligne.SSCAISSON;
				ViewBag.Acc = db.CAISSON.Where(f => f.ID == idcaisson).FirstOrDefault().ACCESSOIRE.NOM;

			}
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			ViewBag.LignesCuisine = LignesCuisine;
			ViewBag.numeroo = numero;
			ViewBag.Date = Date;
			ViewBag.designation = designation;
			ViewBag.modePaiement = modePaiement;
			ViewBag.client = client;
			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.CAISSON = CAISSON;
			ViewBag.SSCAISSON = SSCAISSON;
			ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			//ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			ViewBag.TYPCAISSON = TYPCAISSON;
			return PartialView("ACCESSOIREAvoir");
		}

		public JsonResult GetAllLineACC(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				db.Configuration.ProxyCreationEnabled = false;
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			}
			else
			{
				db.Configuration.ProxyCreationEnabled = false;
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
				ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineACCCMD(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllLineACCBL(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllLineACCFACTURE(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineACCCaisse(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineACCAvoir(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREAvoir"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public string EditLineACCESSOIRE(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				db.Configuration.ProxyCreationEnabled = false;
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			}
			else
			{
				db.Configuration.ProxyCreationEnabled = false;
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
				ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			}
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);
			if (LignesCuisine == null || LignesCuisine == "")
			{
				Session["LignesACC"] = ListeDesPoduits;
			}
			if (LignesCuisine != null && LignesCuisine != "")
			{
				int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCessoire"] = ListeDesPoduits;

				listLignesCuisine = (List<LignesCuisine>)Session["CUISINEDevisClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}
		public string EditLineACCESSOIRECMD(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			//if (Session["LignesACCESSOIRECMD"] != null && LignesCuisine != null && LignesCuisine != "")
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			//}
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    Session["LignesACC"] = ListeDesPoduits;
			//}
			if (LignesCuisine != null && LignesCuisine != "")
			{
				//int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCESSOIRECMD"] = ListeDesPoduits;

				listLignesCuisine = (List<LignesCuisine>)Session["CUISINECommandeClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}
		public string EditLineACCESSOIREBL(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//if (Session["LignesACCESSOIREBonLiv"] != null && LignesCuisine != null && LignesCuisine != "")
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);

			if (LignesCuisine != null && LignesCuisine != "")
			{
				//int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCESSOIREBonLiv"] = ListeDesPoduits;

				listLignesCuisine = (List<LignesCuisine>)Session["CUISINEBLClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}

		public string EditLineACCESSOIREFacture(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();

			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//if (Session["LignesACCESSOIREFacture"] != null && LignesCuisine != null && LignesCuisine != "")
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			//}
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);

			if (LignesCuisine != null && LignesCuisine != "")
			{
				//int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCESSOIREFacture"] = ListeDesPoduits;
				listLignesCuisine = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}

		public string EditLineACCESSOIRECaisse(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIRECAISSE"] != null && LignesCuisine != null && LignesCuisine != "")
			{
				db.Configuration.ProxyCreationEnabled = false;
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
				ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			}
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);

			if (LignesCuisine != null && LignesCuisine != "")
			{
				int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCESSOIRECAISSE"] = ListeDesPoduits;
				listLignesCuisine = (List<LignesCuisine>)Session["CUISINECAISSEClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}
		public string EditLineACCESSOIREAvoir(string ID_Produit, string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit, string NEW_TVA, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			List<LignesCuisine> listLignesCuisine = new List<LignesCuisine>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIREAvoir"] != null && LignesCuisine != null && LignesCuisine != "")
			{
				db.Configuration.ProxyCreationEnabled = false;
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREAvoir"];
				ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			}
			int ID = int.Parse(ID_Produit);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.IDArticle = int.Parse(IDARTICLE);
			ligne.Article = ARTICLE;
			ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(NEW_TVA);

			if (LignesCuisine != null && LignesCuisine != "")
			{
				int idligne = int.Parse(LignesCuisine);
				decimal thtAcc = 0;
				foreach (LignesACCESSOIRE ligneAcc in ListeDesPoduits)
				{
					thtAcc += ligneAcc.PTHT;

				}
				Session["LignesACCESSOIREAvoir"] = ListeDesPoduits;
				listLignesCuisine = (List<LignesCuisine>)Session["CUISINEAvoirClient"];
				LignesCuisine ligneCuisine = listLignesCuisine.Where(f => f.ID == idligne).FirstOrDefault();
				ligneCuisine.ACC = thtAcc;
				ligneCuisine.PRIXVENTECAISSON = ligneCuisine.PRIXACHAT + thtAcc + ((ligneCuisine.PRIXACHAT + thtAcc) * ligneCuisine.POURCENTAGE / 100);
				//ligneCuisine.PTHTSSMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXACHAT + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTHTAVECMARGE = ligneCuisine.QuantiteCAISSON * (ligneCuisine.PRIXVENTECAISSON + ligneCuisine.PTHTFACADE);
				ligneCuisine.PTTCCUISINE = (ligneCuisine.PTHTAVECMARGE * ligneCuisine.TVACUISINE / 100) + ligneCuisine.PTHTAVECMARGE;

			}

			return string.Empty;
		}

		public string DeleteLineACCESSOIREs(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			}
			if (Session["LignesACCessoire"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}
		public string DeleteLineACCESSOIREsCMD(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIRECMD"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}
		public string DeleteLineACCESSOIREsBL(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIREBonLiv"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}

		public string DeleteLineACCESSOIREsFacture(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIREFacture"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}

		public string DeleteLineACCESSOIREsCaisse(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIRECAISSE"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}
		public string DeleteLineACCESSOIREsAvoir(string parampassed, string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (Session["LignesACC"] != null && (LignesCuisine == null || LignesCuisine == ""))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			if (Session["LignesACCESSOIREAvoir"] != null && (LignesCuisine != null || LignesCuisine != ""))
			{
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREAvoir"];
			}
			int ID = int.Parse(parampassed);
			LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			return string.Empty;
		}
		public ActionResult Client(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise)
		{
			CLIENTS frnds = new CLIENTS();
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 11).FirstOrDefault();
			DateTime d = DateTime.Today;
			if (PrefixeTable == null)
			{
				int count = db.CLIENTS.Max(p => p.ID) + 1;
				ViewBag.Numero = "C" + count;
			}
			else
			{
				int Max = db.CLIENTS.Where(f => f.Societe == idste).Max(p => p.ID) + 1;
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
				ViewBag.Numero = numPre.Replace(count, date1);
				ViewBag.Numero = (ViewBag.Numero).Replace(count1, date2);
			}
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			Session["ProduitsDevisClient"] = null;
			Session["ProduitsDevisClient2"] = null;
			Session["LignesServ"] = null;
			List<DEVIS_CLIENTS> Liste = db.DEVIS_CLIENTS.Where(f => f.Societes == idste).ToList();
			dynamic Result = (from com in Liste
							  select new
							  {
								  ID = com.ID,
								  CODE = com.CODE,
								  DESIGNATION = com.Designation,
								  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == com.CLIENT).FirstOrDefault().NOM,
								  DATE = com.DATE.ToShortDateString(),
								  THT = com.THT,
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			List<COMMANDES_CLIENTS> Liste = db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).ToList();
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
								  //TIERS = db.Tiers.Where(fou => fou.TiersID == com.Tiers).FirstOrDefault().NOM,
								  cc = db.BONS_LIVRAISONS_CLIENTS.Where(fou => fou.COMMANDE_CLIENT == com.ID).FirstOrDefault(),

							  }).AsEnumerable().Select(c => c.ToExpando());
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			return View(Result);
		}
		public ActionResult BonLivraison(string Mode)
		{
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			List<BONS_LIVRAISONS_CLIENTS> Liste = db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).ToList();
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
								  //TIERS = db.Tiers.Where(fou => fou.TiersID == com.Tiers).FirstOrDefault().NOM,
								  cc = db.FACTURES_CLIENTS.Where(fou => fou.BON_LIVRAISON_CLIENT == com.ID).FirstOrDefault(),

							  }).AsEnumerable().Select(c => c.ToExpando());
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			return View(Result);
		}
		public ActionResult Avoir(string Mode)
		{
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			List<AVOIRS_CLIENTS> Liste = db.AVOIRS_CLIENTS.Where(f => f.Societes == idste).ToList();
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.Where(f => f.Societes == idste).ToList();
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
		public ActionResult Caisse(string Mode)
		{
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			List<Caisse> Liste = db.Caisse.Where(f => f.Societes == idste).ToList();
			dynamic Result = (from Cais in Liste
							  select new
							  {
								  ID = Cais.ID,
								  CODE = Cais.CODE,
								  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Cais.CLIENT).FirstOrDefault().NOM,
								  DATE = Cais.DATE.ToShortDateString(),
								  THT = Cais.NHT,
								  TTVA = Cais.TTVA,
								  TTC = Cais.TTC,
								  TNET = Cais.TNET,
								  VALIDE = Cais.VALIDER,
								  PAYEE = Cais.PAYEE,
								  Declar = Cais.Declaration,
								  DateDeclar = Cais.Date_Declaration,

								  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM
							  }).AsEnumerable().Select(c => c.ToExpando());
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;


			return View(Result);
		}
		public ActionResult DeclarationTVAListe()
		{
			List<DeclarationTVA> liste = db.DeclarationTVA.ToList();
			return View(liste);
		}
		public ActionResult DeclarationTVA()
		{
			Session["RASMarche"] = null;
			Session["RASSalaire"] = null;
			Session["RASHoraire"] = null;
			Session["RASLoyer"] = null;
			Session["RASTFP"] = null;
			Session["FOPROLOS"] = null;
			return View();
		}

		public ActionResult FactureDeclaration(string Mode)
		{
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
			List<TVA> listtva = db.TVA.ToList();
			int count = listtva.Count() * 2;
			dynamic Result = (from Fact in Liste
							  select new
							  {
								  ID = Fact.ID,
								  DateDeclar = Fact.Date_Declaration,

								  CODE = Fact.CODE,
								  FOURNISSEUR = db.CLIENTS.Where(fou => fou.ID == Fact.CLIENT).FirstOrDefault().NOM,
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
			List<FACTURES_CLIENTS> Liste2 = new List<FACTURES_CLIENTS>();

			foreach (FACTURES_CLIENTS fact in Liste)
			{
				if (fact.Date_Declaration != null)
				{
					string date = fact.Date_Declaration.ToString();
					string[] date44 = date.Split(' ');
					string[] date55 = date44[0].Split('/');
					string mm1 = date55[1];
					string aaaa1 = date55[2];
					if ((mm1 == mm) && (aaaa == aaaa1))
					{
						Liste2.Add(fact);
					}
				}
			}
			List<TVA> listtva = db.TVA.ToList();
			int count = listtva.Count() * 2;
			dynamic Result = (from Fact in Liste2
							  select new
							  {
								  ID = Fact.ID,
								  DateDeclar = Fact.Date_Declaration,

								  CODE = Fact.CODE,

								  //TIERS = db.Tiers.Where(fou => fou.TiersID == Fact.Tiers).FirstOrDefault().NOM
							  }).AsEnumerable().Select(c => c.ToExpando()).Distinct();
			ViewBag.date = date2;
			ViewBag.count = count;


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
		public ActionResult FormDevis(string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string IdAffaireCommercial, string CAISSON, string SSCAISSON, string IDTYPCAISSON, string QuantiteCAISSON, string LIB_SSCAISSON, string TYPCAISSON)

		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();

			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();
			List<LignesServicesSSTraitance> ListeDesSERVICESSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();

			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			DEVIS_CLIENTS DevisClient = new DEVIS_CLIENTS();

			ViewBag.Mode = Mode;
			ViewBag.Code = Code;
			ViewBag.Date = Date;
			ViewBag.numeroo = numero;

			ViewBag.modePaiement = modePaiement;
			if (IdAffaireCommercial != null && IdAffaireCommercial != "")
			{
				int aff = int.Parse(IdAffaireCommercial);
				int idclient = (int)db.AffaireCommerciales.Where(f => f.AffaireCommercialeId == aff).FirstOrDefault().ClientId;
				string designationAFF = db.AffaireCommerciales.Where(f => f.AffaireCommercialeId == aff).FirstOrDefault().Designation;

				ViewBag.client = idclient.ToString();
				ViewBag.designation = designationAFF;

			}
			else
			{
				ViewBag.client = client;
				ViewBag.designation = designation;
			}

			ViewBag.codeClient = codeClient;
			ViewBag.Tiers = Tiers;
			ViewBag.remise = remise;
			//ViewBag.CAISSON = CAISSON;
			//ViewBag.SSCAISSON = SSCAISSON;

			//ViewBag.QuantiteCAISSON = QuantiteCAISSON;
			//ViewBag.LIB_SSCAISSON = LIB_SSCAISSON;
			//ViewBag.IDTYPCAISSON = IDTYPCAISSON;
			//ViewBag.TYPCAISSON = TYPCAISSON;
			List<LignesACCESSOIRE> LignesACC = (List<LignesACCESSOIRE>)Session["LignesACC"];
			string Numero = string.Empty;
			LigneProduit ListeDesPoduits2 = (LigneProduit)Session["ProduitsDevisClient2"];
			List<LignesServices> LignesServ = (List<LignesServices>)Session["LignesServ"];
			List<LignesServicesSSTraitance> LignesServSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
			List<LignesCuisine> LignesCuisine = (List<LignesCuisine>)Session["CUISINEDevisClient"];

			if (ListeDesPoduits2 != null)
			{
				ViewBag.idd = ListeDesPoduits2.ID;
				ViewBag.lib = ListeDesPoduits2.LIBELLE;
				string des = (ListeDesPoduits2.DESIGNATION).Replace("\n", " ");
				ViewBag.des = des;
				ViewBag.NumD = ListeDesPoduits2.NumDevis;
				string ma1 = ListeDesPoduits2.MARQUE;

				ViewBag.ma = ma1.Trim();

				ViewBag.un = ListeDesPoduits2.UNITE;
				ViewBag.dv = ListeDesPoduits2.DEVISE;

				string ca1 = ListeDesPoduits2.CATEGORIE;

				ViewBag.ca = ca1.Trim();

				if (ListeDesPoduits2.Sous_CATEGORIE != null)
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
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 1).FirstOrDefault();
				if (PrefixeTable == null)
				{
					Numero = "DVC";
				}
				else
				{
					string pref = PrefixeTable.Prefixe;
					string pref1 = pref.Replace("y", "");
					string pref2 = pref1.Replace("m", "");
					pref2 = pref2.Replace("0", "");
					Numero = pref2.Replace("-", "");
				}
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
				ViewBag.ListeDesPoduits = ListeDesPoduits;
				ViewBag.LignesServ = LignesServ;
				ViewBag.LignesServSSTraitance = LignesServSSTraitance;
				ViewBag.LignesCuisine = LignesCuisine;
				ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				decimal somme = 0;
				int ID = int.Parse(Code);
				DevisClient = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				Numero = DevisClient.CODE;


				List<Tasks> tas = db.Tasks.Where(fou => fou.ProjetTechniquesID == ID).ToList();
				foreach (Tasks ta in tas)
				{
					decimal c1 = (decimal)db.Personnels.Find(ta.owner_id).Cout_hor;
					string c4 = ta.duration_h_planning.ToString();
					int c2 = int.Parse(c4);
					decimal c3 = c1 * c2;
					somme = somme + c3;
				}
				List<LIGNES_DEVIS_CLIENTS> ListeLigne = db.LIGNES_DEVIS_CLIENTS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
				List<LIGNES_CUISINE_DEVIS_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();

				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == ID).ToList();

				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();
				foreach (LIGNES_DEVIS_CLIENTS ligne in ListeLigne)
				{
					LigneProduit NewLine = new LigneProduit();
					NewLine.ID = (int)ligne.Art_Devis_Frs;
					NewLine.Code = ID;
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
				foreach (LIGNES_CUISINE_DEVIS_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();

					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.Code = ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					//choix les articles
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					//choix les Ref des facades
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE> list = db.LIGNES_DESCRIPTION_ACCESOIRE.Where(f => f.ID_LigneDC == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}
					Session["LignesACCESSOIRE"] = listAccessoire;
				}
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == ID && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				if (ListeLigneSERVICESSSTRAITANCE != null)
				{
					foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
					{
						if (ListeDesSERVICESSTraitance != null)
						{
							ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne4 == null)
						{
							LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.Marge = (decimal)ligne.MARGE;
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == ID && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
							{
								NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
								string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
								NewLine.SOUS_TRAITANCE2.Add(nom);

							}


							ListeDesSERVICESSTraitance.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = DevisClient.CLIENTS.CODE;
				ViewBag.CODESOC = DevisClient.Societes;
				ViewBag.id = Code;
				ViewBag.somme = somme;
				ViewBag.IdAffaireCommercial = DevisClient.Id_AffaireCommerciale.ToString();
				ViewBag.EnEcours = db.COMMANDES_CLIENTS.Where(f => f.DEVIS_CLIENT == ID).FirstOrDefault();
				ViewBag.CondPaim = DevisClient.ConditionPaiement;
				if (DevisClient.AffaireCommerciales != null) { 
				ViewBag.designation = DevisClient.AffaireCommerciales.Designation;
				}
				if (DevisClient.TVAInstallation != null)
				{
					ViewBag.TVAInstallation = (int)DevisClient.TVAInstallation;
				}
			}
			if (Session["ProduitsDevisClient"] == null)
			{
				Session["ProduitsDevisClient"] = ListeDesPoduits;
			}
			if (Session["LignesServ"] == null)
			{
				Session["LignesServ"] = ListeDesSERVICE;
			}
			if (Session["LignesServSST"] == null)
			{
				Session["LignesServSST"] = ListeDesSERVICESSTraitance;
			}
			Session["ProduitsDevisClient2"] = null;

			if (Session["CUISINEDevisClient"] == null)
			{
				Session["CUISINEDevisClient"] = ListeDesCuisine;
			}

			ViewBag.Numero = Numero;
			return View(DevisClient);
		}
		public ActionResult FormCommande(string Mode, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();

			COMMANDES_CLIENTS CommandeClient = new COMMANDES_CLIENTS();
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();
			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();
			List<LignesServicesSSTraitance> ListeDesSERVICESSTraitance = new List<LignesServicesSSTraitance>();
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;
			string Numero = string.Empty;
			if (Mode == "Create")
			{
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 3).FirstOrDefault();
				if (PrefixeTable == null)
				{
					Numero = "CDC";
				}
				else
				{
					string pref = PrefixeTable.Prefixe;
					string pref1 = pref.Replace("y", "");
					string pref2 = pref1.Replace("m", "");
					pref2 = pref2.Replace("0", "");
					Numero = pref2.Replace("-", "");
				}
				//ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
				//ViewBag.ListeDesPoduits = ListeDesPoduits;
				//ViewBag.LignesServ = LignesServ;
				//ViewBag.LignesServSSTraitance = LignesServSSTraitance;
				//ViewBag.LignesCuisine = LignesCuisine;
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				int ID = int.Parse(Code);
				CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				int iddevis = (int)CommandeClient.DEVIS_CLIENT;
				Numero = CommandeClient.CODE;
				List<LIGNES_COMMANDES_CLIENTS> ListeLigne = db.LIGNES_COMMANDES_CLIENTS.Where(lcmd => lcmd.COMMANDE_CLIENT == ID).ToList();
				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<LIGNES_CUISINE_COMMANDE_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_COMMANDE_CLIENTS.Where(lcmd => lcmd.COMMANDE_CLIENT == ID).ToList();

				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();
				foreach (LIGNES_COMMANDES_CLIENTS ligne in ListeLigne)
				{
					LigneProduit NewLine = new LigneProduit();
					NewLine.ID = (int)ligne.Prix_achat;
					NewLine.Code = ID;
					NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
					NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
					NewLine.MARQUE = ligne.Marque;
					NewLine.DEVISE = ligne.Devise;
					NewLine.UNITE = ligne.Unite;
					NewLine.CATEGORIE = ligne.Categorie;
					NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
					NewLine.QUANTITE = (int)ligne.QUANTITE;
					//NewLine.STOCK = (decimal)db.Prix_Achat.Where(fou=>fou.Libelle==NewLine.LIBELLE).FirstOrDefault().Stock;
					if (ligne.PRIX_UNITAIRE_HTVente != null)
					{
						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HTVente;
					}
					else
					{
						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
					}
					NewLine.REMISE = (int)ligne.REMISE;
					NewLine.PTHT = (decimal)ligne.TOTALE_HT;
					NewLine.TVA = (int)ligne.TVA;
					NewLine.TTC = (decimal)ligne.TOTALE_TTC;
					ListeDesPoduits.Add(NewLine);
				}
				foreach (LIGNES_CUISINE_COMMANDE_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();
					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.Code = ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE_CMD> list = db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Where(f => f.ID_LigneCMD == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_CMD ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}

					Session["LignesACCESSOIRECMD"] = listAccessoire;
				}
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				if (ListeLigneSERVICESSSTRAITANCE != null)
				{
					foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
					{
						if (ListeDesSERVICESSTraitance != null)
						{
							ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne4 == null)
						{
							LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.Marge = (decimal)ligne.MARGE;
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
							{
								NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
								string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
								NewLine.SOUS_TRAITANCE2.Add(nom);

							}


							ListeDesSERVICESSTraitance.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = CommandeClient.CLIENTS.CODE;
				ViewBag.CODESOC = CommandeClient.Societes;
				ViewBag.id = CommandeClient.DEVIS_CLIENT;
				ViewBag.EnEcours = db.BONS_LIVRAISONS_CLIENTS.Where(f => f.COMMANDE_CLIENT == ID).FirstOrDefault();
				ViewBag.idd = ID;

			}
			Session["ProduitsCommandeClient"] = ListeDesPoduits;
			Session["LignesServ"] = ListeDesSERVICE;
			Session["LignesServSST"] = ListeDesSERVICESSTraitance;
			if (Session["CUISINECommandeClient"] == null)
			{
				Session["CUISINECommandeClient"] = ListeDesCuisine;
			}

			ViewBag.Numero = Numero;
			return View(CommandeClient);
		}

		public ActionResult FormBonLivraison(string Mode, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();
			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();
			List<LignesServicesSSTraitance> ListeDesSERVICESSTraitance = new List<LignesServicesSSTraitance>();
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			ViewBag.Mode = Mode;
			ViewBag.Code = Code;

			string Numero = string.Empty;
			if (Mode == "Create")
			{
				//int Max = 0;
				//if (db.BONS_LIVRAISONS_CLIENTS.ToList().Count != 0)
				//{
				//    Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
				//}
				//Max++;
				//Numero = "BL" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 5).FirstOrDefault();
				if (PrefixeTable == null)
				{
					Numero = "BL";
				}
				else
				{
					string pref = PrefixeTable.Prefixe;
					string pref1 = pref.Replace("y", "");
					string pref2 = pref1.Replace("m", "");
					pref2 = pref2.Replace("0", "");
					Numero = pref2.Replace("-", "");
				}
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				int ID = int.Parse(Code);
				BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				Numero = BonLivraisonClient.CODE;
				int iddevis = (int)BonLivraisonClient.COMMANDES_CLIENTS.DEVIS_CLIENT;
				List<LIGNES_BONS_LIVRAISONS_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(lcmd => lcmd.BONLIVRAISON_CLIENT == ID).ToList();

				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();

				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in ListeLigne)
				{
					LigneProduit NewLine = new LigneProduit();
					NewLine.ID = (int)ligne.Prix_achat;
					NewLine.Code = ID;
					NewLine.LIBELLE = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().Libelle_Prd;
					NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
					NewLine.MARQUE = ligne.Marque;
					NewLine.DEVISE = ligne.Devise;
					NewLine.UNITE = ligne.Unite;
					NewLine.CATEGORIE = ligne.Categorie;
					NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
					NewLine.QUANTITE = (decimal)ligne.QUANTITE;
					Prix_Achat pr1 = db.Prix_Achat.Where(f => f.Libelle == NewLine.LIBELLE).FirstOrDefault();
					if (pr1 != null)
					{
						NewLine.STOCK = (decimal)pr1.Stock;
					}
					else
					{
						NewLine.STOCK = 0;
					}
					//NewLine.STOCK = (int)ligne.STOCK;
					if (BonLivraisonClient.VALIDER)
					{
						NewLine.QUANTITERES = 0;

					}
					else
					{
						NewLine.QUANTITERES = (decimal)ligne.QTERES;

					}
					NewLine.QUANTITELiv = (decimal)ligne.QUANTITE;
					if (ligne.PRIX_UNITAIRE_HTVente != null)
					{
						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HTVente;
					}
					else
					{
						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
					}
					NewLine.REMISE = (int)ligne.REMISE;
					NewLine.PTHT = (decimal)ligne.TOTALE_HT;
					NewLine.TVA = (int)ligne.TVA;
					NewLine.TTC = (decimal)ligne.TOTALE_TTC;
					ListeDesPoduits.Add(NewLine);
				}
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();
					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.Code = ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> list = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.ID_LigneBL == Ligne.ID).ToList();


					foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}

					Session["LignesACCESSOIREBonLiv"] = listAccessoire;
				}
				Session["CUISINEBLClient"] = ListeDesCuisine;
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				if (ListeLigneSERVICESSSTRAITANCE != null)
				{
					foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
					{
						if (ListeDesSERVICESSTraitance != null)
						{
							ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne4 == null)
						{
							LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.Marge = (decimal)ligne.MARGE;
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
							{
								NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
								string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
								NewLine.SOUS_TRAITANCE2.Add(nom);
							}


							ListeDesSERVICESSTraitance.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
				ViewBag.CODESOC = BonLivraisonClient.Societes;

			}
			if (Mode == "Editcmd")
			{
				int ID = int.Parse(Code);
				BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				Numero = BonLivraisonClient.CODE;
				int iddevis = (int)BonLivraisonClient.COMMANDES_CLIENTS.DEVIS_CLIENT;

				List<LIGNES_BONS_LIVRAISONS_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();
				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(lcmd => lcmd.BONLIVRAISON_CLIENT == ID).ToList();

				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();
				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in ListeLigne)
				{
					LigneProduit NewLine = new LigneProduit();
					NewLine.ID = (int)ligne.Prix_achat;
					NewLine.Code = ID;
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
					if (pr1 != null)
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
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();

					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.Code = ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> list = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.ID_LigneBL == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}

					Session["LignesACCESSOIREBonLiv"] = listAccessoire;
				}

				Session["CUISINEBLClient"] = ListeDesCuisine;
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				if (ListeLigneSERVICESSSTRAITANCE != null)
				{
					foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
					{
						if (ListeDesSERVICESSTraitance != null)
						{
							ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne4 == null)
						{
							LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.Marge = (decimal)ligne.MARGE;
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
							{
								NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
								string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
								NewLine.SOUS_TRAITANCE2.Add(nom);
							}


							ListeDesSERVICESSTraitance.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
				ViewBag.CODESOC = BonLivraisonClient.Societes;
				//ViewBag.Type = BonLivraisonClient.Type;
			}
			Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
			Session["LignesServ"] = ListeDesSERVICE;
			Session["LignesServSST"] = ListeDesSERVICESSTraitance;

			int ID1 = int.Parse(Code);
			ViewBag.EnEcours = db.FACTURES_CLIENTS.Where(f => f.BON_LIVRAISON_CLIENT == ID1).FirstOrDefault();
			ViewBag.EnEcours1 = db.Caisse.Where(f => f.BON_LIVRAISON_CLIENT == ID1).FirstOrDefault();
			ViewBag.Type = BonLivraisonClient.Type;
			ViewBag.Numero = Numero;
			ViewBag.idd = BonLivraisonClient.ID;

			return View(BonLivraisonClient);
		}
		public ActionResult FormBonLivraisonPart(string Mode, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();
			BONS_LIVRAISONS_PART_CLIENTS BONS_LIVRAISONS_PART_CLIENTS = new BONS_LIVRAISONS_PART_CLIENTS();
			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();

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
				Numero = "BL" + Max.ToString("0000") + "/" + DateTime.Today.ToString("yy");
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				int ID = int.Parse(Code);
				BONS_LIVRAISONS_PART_CLIENTS = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == BONS_LIVRAISONS_PART_CLIENTS.IDBLC).FirstOrDefault();
				Numero = BonLivraisonClient.CODE;
				int iddevis = (int)BonLivraisonClient.COMMANDES_CLIENTS.DEVIS_CLIENT;

				List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_PART_CLIENT == ID).ToList();
				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				LignesServices ligne3 = new LignesServices();
				foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS ligne in ListeLigne)
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
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
				ViewBag.CODESOC = BonLivraisonClient.Societes;
				ViewBag.Valider = BonLivraisonClient.VALIDER;

			}
			if (Mode == "Editcmd")
			{
				int ID = int.Parse(Code);
				BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				Numero = BonLivraisonClient.CODE;
				int iddevis = (int)BonLivraisonClient.COMMANDES_CLIENTS.DEVIS_CLIENT;

				List<LIGNES_BONS_LIVRAISONS_CLIENTS> ListeLigne = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();
				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				LignesServices ligne3 = new LignesServices();
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
					if (pr1 != null)
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
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = BonLivraisonClient.CLIENTS.CODE;
				ViewBag.CODESOC = BonLivraisonClient.Societes;
				//ViewBag.Type = BonLivraisonClient.Type;
			}
			Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
			Session["LignesServ"] = ListeDesSERVICE;
			int ID1 = int.Parse(Code);
			ViewBag.EnEcours = db.FACTURES_CLIENTS.Where(f => f.BON_LIVRAISON_CLIENT == ID1).FirstOrDefault();
			ViewBag.EnEcours1 = db.Caisse.Where(f => f.BON_LIVRAISON_CLIENT == ID1).FirstOrDefault();

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
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesCuisine> LignesCuisine = (List<LignesCuisine>)Session["CUISINEAvoirClient"];
			if (Mode == "Create")
			{
				int Max = 0;
				if (db.AVOIRS_CLIENTS.ToList().Count != 0)
				{
					Max = db.AVOIRS_CLIENTS.Where(cmd => cmd.DATE.Year == DateTime.Today.Year).Select(cmd => cmd.ID).Count();
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
				List<LIGNES_CUISINE_AVOIR_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_AVOIR_CLIENTS.Where(lcmd => lcmd.AVOIR_CLIENT == ID).ToList();
				foreach (LIGNES_CUISINE_AVOIR_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();

					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE_AVOIR> list = db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Where(f => f.ID_LigneAVOIR == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_AVOIR ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}
					Session["LignesACCESSOIREAvoir"] = listAccessoire;
				}
				ViewBag.CODE_CLIENT = AvoirClient.CLIENTS.CODE;
				ViewBag.CODESOC = AvoirClient.Societes;
			}
			Session["ProduitsAvoirClient"] = ListeDesPoduits;
			if (Session["CUISINEAvoirClient"] == null)
			{
				Session["CUISINEAvoirClient"] = ListeDesCuisine;
			}
			ViewBag.Numero = Numero;
			return View(AvoirClient);
		}
		public JsonResult GetNumeroFacture(string DATE, string Mode, string num)
		{
			DateTime d = DateTime.Parse(DATE);
			if (Session["SoclogoId"] == null)
			{
				RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			string Numero1;
			db.Configuration.ProxyCreationEnabled = false;
			int Max = 0;
			PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 7).FirstOrDefault();
			if (PrefixeTable == null)
			{
				//if (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.CODE).Count();
				//}
				Max++;
				Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
				while (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero1))
				{
					Max++;
					Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
				}

			}
			else
			{
				Max = 0;
				//if (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
				//}
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
				string date1 = d.ToString(count);
				Numero1 = numPre.Replace(count, date1);
				foreach (char c in numPre)
				{
					if (c == 'M')
					{
						count1 += c;
					}
				}
				string date12 = d.ToString(count);
				string date2 = d.ToString(count1);
				Numero1 = numPre.Replace(count, date12);
				Numero1 = Numero1.Replace(count1, date2);
				while (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero1))
				{
					Max++;
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
						if (c == 'M')
						{
							count1 += c;
						}
					}
					date1 = d.ToString(count);
					date2 = d.ToString(count1);
					Numero1 = numPre.Replace(count, date1);
					Numero1 = Numero1.Replace(count1, date2);
				}
			}
			//DateTime d = DateTime.Parse(DATE);
			//int idste = (int)Session["SoclogoId"];
			//string Numero1;
			//int Nb = 0;
			//db.Configuration.ProxyCreationEnabled = false;
			//if (Mode == "Edit")
			//{
			//    string[] code = num.Split('/');
			//    int y = int.Parse(code[1]);
			//    string an = d.Year.ToString();
			//    string[] an1 = an.Split('0');
			//    int an2 = int.Parse(an1[1]);
			//    if (an2 == y)
			//    {
			//        Numero1 = num;
			//    }
			//    else
			//    {
			//        int Max = 0;
			//        if (db.FACTURES_CLIENTS.ToList().Count != 0)
			//        {
			//            Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
			//        }
			//        Max++;

			//        Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
			//        List<FACTURES_CLIENTS> frs = db.FACTURES_CLIENTS.ToList();
			//        foreach (FACTURES_CLIENTS f in frs)
			//        {
			//            string[] con = f.CODE.Split('C');
			//            string[] con11 = con[1].Split('/');
			//            int con1 = int.Parse(con11[0]);
			//            if (con1 == Max)
			//            {
			//                Nb++;
			//            }

			//        }
			//        if (Nb > 0)
			//        {
			//            Max++;

			//            Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
			//        }
			//        else
			//        {
			//            Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");

			//        }

			//    }
			//}
			//else
			//{
			//    int Max = 0;
			//    if (db.FACTURES_CLIENTS.ToList().Count != 0)
			//    {
			//        Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
			//    }
			//    Max++;

			//    Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
			//    List<FACTURES_CLIENTS> frs = db.FACTURES_CLIENTS.ToList();
			//    foreach (FACTURES_CLIENTS f in frs)
			//    {
			//        string[] con = f.CODE.Split('F');
			//        string[] con11 = con[1].Split('/');
			//        int con1 = int.Parse(con11[0]);
			//        if (con1 == Max)
			//        {
			//            Nb++;
			//        }

			//    }
			//    if (Nb > 0)
			//    {
			//        Max++;

			//        Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
			//    }
			//    else
			//    {
			//        Numero1 = "F" + Max.ToString("0000") + "/" + d.ToString("yy");

			//    }

			//}
			//List<FACTURES_CLIENTS> FACTURES_CLIENTS = db.FACTURES_CLIENTS.ToList();

			//string[] cod = Numero1.Split('F');
			//string[] cod11 = cod[1].Split('/');
			//int cod1 = int.Parse(cod11[0]);
			//int cod12 = cod1++;
			//foreach (FACTURES_CLIENTS FAC in FACTURES_CLIENTS)
			//{
			//    string[] cod122 = FAC.CODE.Split('F');
			//    string[] cod112 = cod122[1].Split('/');
			//    int cod13 = int.Parse(cod112[0]);
			//    if (cod13 == cod12)
			//    {
			//        cod12++;
			//        break;
			//    }
			//}
			//Numero1 = "F" + cod12.ToString("0000") + "/" + cod11[1];
			return Json(Numero1, JsonRequestBehavior.AllowGet);
		}

		public ActionResult FormFacture(string Mode, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			FACTURES_CLIENTS FactureClient = new FACTURES_CLIENTS();
			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();
			List<LignesServicesSSTraitance> ListeDesSERVICESSTraitance = new List<LignesServicesSSTraitance>();
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			ViewBag.Mode = Mode;
			ViewBag.Code = Code;
			string Numero = string.Empty;
			if (Mode == "Create")
			{
				//Numero = "F";
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 7).FirstOrDefault();
				if (PrefixeTable == null)
				{
					Numero = "F";
				}
				else
				{
					string pref = PrefixeTable.Prefixe;
					string pref1 = pref.Replace("y", "");
					string pref2 = pref1.Replace("m", "");
					pref2 = pref2.Replace("0", "");
					Numero = pref2.Replace("-", "");
				}
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				int ID = int.Parse(Code);
				FactureClient = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				List<LIGNES_FACTURES_CLIENTS> ListeLigne = db.LIGNES_FACTURES_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();
				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();
				Numero = FactureClient.CODE;
				if (FactureClient.BON_LIVRAISON_CLIENT != null)
				{
					int iddevis = (int)FactureClient.BONS_LIVRAISONS_CLIENTS.COMMANDES_CLIENTS.DEVIS_CLIENT;
					List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
					List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
					List<LIGNES_CUISINE_FACTURE_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();

					foreach (LIGNES_FACTURES_CLIENTS ligne in ListeLigne)
					{
						LigneProduit NewLine = new LigneProduit();
						NewLine.ID = (int)ligne.Prix_achat;
						NewLine.Code = ID;
						NewLine.LIBELLE = ligne.Libelle_Prd;
						NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
						NewLine.MARQUE = ligne.Marque;
						NewLine.DEVISE = ligne.Devise;
						NewLine.UNITE = ligne.Unite;
						NewLine.CATEGORIE = ligne.Categorie;
						NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
						Prix_Achat prix = db.Prix_Achat.Where(pr => pr.Product_ID == ligne.Prix_achat).FirstOrDefault();
						if (prix != null)
						{
							NewLine.STOCK = (decimal)db.Prix_Achat.Where(pr => pr.Product_ID == ligne.Prix_achat).FirstOrDefault().Stock;
						}
						else
						{
							NewLine.STOCK = 0;
						}
						NewLine.QUANTITE = (int)ligne.QUANTITE;
						//NewLine.STOCK = (int)ligne.STOCK;

						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
						if (ligne.PRIX_UNITAIRE_HTVente != null)
						{
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
						}
						else
						{
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HT;
						}
						if (ligne.MARGE != null)
						{
							NewLine.MARGE = (decimal)ligne.MARGE;
						}
						else
						{
							NewLine.MARGE = 0;
						}
						NewLine.REMISE = (int)ligne.REMISE;
						NewLine.PTHT = (decimal)ligne.TOTALE_HT;
						NewLine.TVA = (int)ligne.TVA;
						NewLine.TTC = (decimal)ligne.TOTALE_TTC;
						ListeDesPoduits.Add(NewLine);
					}
					foreach (LIGNES_CUISINE_FACTURE_CLIENTS Ligne in ListeLigneCuisine)
					{
						LignesCuisine UneLigne = new LignesCuisine();

						UneLigne.ligneCuisine = (int)Ligne.ID;
						UneLigne.Code = ID;
						UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
						UneLigne.CAISSON = (int)Ligne.CAISSON1.TYPE_CAISSON;
						UneLigne.LIB_CAISSON = Ligne.CAISSON1.TYPE_CAISSON1.TYPE_CAISSON1;
						UneLigne.SSCAISSON = Ligne.CAISSON1.ID;
						UneLigne.LIB_SSCAISSON = Ligne.CAISSON1.REF_BAS;
						UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
						UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
						UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
						if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
						{
							UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
							UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
						}
						if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
						{
							int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
							string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
							//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
							UneLigne.FACADE = FACADE;
							UneLigne.LIB_FACADE = REFFACADE;

							//choix les Types des facades

							int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
							string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
							UneLigne.SOUSFACADE = typeFACADE;
							UneLigne.LIB_SOUSFACADE = typeREFFACADE;
						}
						UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
						UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
						UneLigne.ACC = (decimal)Ligne.ACC;
						UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
						UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


						//UneLigne.STOCK = (double)Ligne.STOCK;
						UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
						UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
						UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
						UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
						UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
						UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
						UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
						int count1 = 0;
						count1 = ListeDesCuisine.Count() + 1;
						while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
						{
							count1 = count1 + 1;
						}
						UneLigne.ID = count1;
						ListeDesCuisine.Add(UneLigne);
						int count = 0;
						List<LIGNES_DESCRIPTION_ACCESOIRE_Facture> list = db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(f => f.ID_LigneFacture == Ligne.ID).ToList();
						foreach (LIGNES_DESCRIPTION_ACCESOIRE_Facture ligne in list)
						{
							LignesACCESSOIRE Acc = new LignesACCESSOIRE();
							count = listAccessoire.Count() + 1;
							while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
							{
								count = count + 1;
							}
							Acc.ID = count;
							Acc.DESIGNATION = ligne.Designation;
							Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
							Acc.IDArticle = (int)ligne.ID_ART;
							Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
							Acc.PUHT = (decimal)ligne.PUHT;
							Acc.PTHT = (decimal)ligne.PTHT;
							Acc.TVA = (int)ligne.TVA;
							Acc.TTC = (decimal)ligne.PTTC;
							Acc.QTE = (int)ligne.QTE;
							Acc.IDLIGNESDEScription = UneLigne.ID;
							listAccessoire.Add(Acc);

						}

						Session["LignesACCESSOIREFacture"] = listAccessoire;



					}
					if (ListeLigneSERVICES != null)
					{
						foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
						{
							if (ListeDesSERVICE != null)
							{
								ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

							}
							if (ligne3 == null)
							{
								LignesServices NewLine = new LignesServices();
								NewLine.ID = (int)ligne.SERVICES;
								NewLine.Code = ID;
								NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
								NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
								NewLine.UNITE = ligne.Unite;
								NewLine.QUANTITE = (decimal)ligne.QUANTITE;
								NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
								NewLine.REMISE = (decimal)ligne.REMISE;
								NewLine.PTHT = (decimal)ligne.TOTALE_HT;
								NewLine.TVA = (int)ligne.TVA;
								NewLine.TTC = (decimal)ligne.TOTALE_TTC;
								List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
								foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
								{
									NewLine.RESSOURCE.Add((int)ligne2.Personnels);
									NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

								}

								ListeDesSERVICE.Add(NewLine);

								lIGNES_SERVICES_FACTURES UneLigne = new lIGNES_SERVICES_FACTURES();
								UneLigne.FACTURE_CLIENT = FactureClient.ID;
								UneLigne.SERVICES = ligne.SERVICES;
								UneLigne.Personnels = ligne.Personnels;
								UneLigne.Unite = ligne.Unite;
								UneLigne.TVA = ligne.TVA;
								UneLigne.TOTALE_HT = ligne.TOTALE_HT;
								UneLigne.TOTALE_TTC = ligne.TOTALE_TTC;
								UneLigne.QUANTITE = ligne.QUANTITE;
								UneLigne.REMISE = ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = ligne.PRIX_UNITAIRE_HT;
								db.lIGNES_SERVICES_FACTURES.Add(UneLigne);
								db.SaveChanges();




							}
						}
					}
					if (ListeLigneSERVICESSSTRAITANCE != null)
					{
						foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
						{
							if (ListeDesSERVICESSTraitance != null)
							{
								ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

							}
							if (ligne4 == null)
							{
								LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
								NewLine.ID = (int)ligne.SERVICES;
								NewLine.Code = ID;
								NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
								NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
								NewLine.UNITE = ligne.Unite;
								NewLine.QUANTITE = (decimal)ligne.QUANTITE;
								NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
								NewLine.Marge = (decimal)ligne.MARGE;
								NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
								NewLine.REMISE = (decimal)ligne.REMISE;
								NewLine.PTHT = (decimal)ligne.TOTALE_HT;
								NewLine.TVA = (int)ligne.TVA;
								NewLine.TTC = (decimal)ligne.TOTALE_TTC;
								List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
								foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
								{
									NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
									string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
									NewLine.SOUS_TRAITANCE2.Add(nom);

								}


								ListeDesSERVICESSTraitance.Add(NewLine);
							}
						}
					}
					//Session["LignesServFact"] = ListeDesSERVICE;
					//Session["LignesServSST"] = ListeDesSERVICESSTraitance;
					//Session["CUISINEFACTUREClient"] = ListeDesCuisine;
					//Session["LignesACCESSOIREFacture"] = listAccessoire;

				}
				else
				{
					List<lIGNES_SERVICES_FACTURES> ListeLigneSERVICESFact = db.lIGNES_SERVICES_FACTURES.Where(lcmd => lcmd.FACTURE_CLIENT == FactureClient.ID).ToList();
					List<lIGNES_SERVICESSSTRAITANCE_FACTURE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE_FACTURE.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();
					List<LIGNES_CUISINE_FACTURE_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();

					foreach (LIGNES_FACTURES_CLIENTS ligne in ListeLigne)
					{
						LigneProduit NewLine = new LigneProduit();
						NewLine.ID = (int)ligne.Prix_achat;
						NewLine.Code = ID;
						NewLine.LIBELLE = ligne.Libelle_Prd;
						NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
						NewLine.MARQUE = ligne.Marque;
						NewLine.DEVISE = ligne.Devise;
						NewLine.UNITE = ligne.Unite;
						NewLine.CATEGORIE = ligne.Categorie;
						NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
						Prix_Achat prix = db.Prix_Achat.Where(pr => pr.Product_ID == ligne.Prix_achat).FirstOrDefault();
						//if (prix != null)
						//{
						NewLine.STOCK = (decimal)prix.Stock;
						//}
						//else
						//{
						//    NewLine.STOCK = 0;
						//}
						NewLine.QUANTITE = (int)ligne.QUANTITE;
						//NewLine.STOCK = (int)ligne.STOCK;

						NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
						if (ligne.PRIX_UNITAIRE_HTVente != null)
						{
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
						}
						else
						{
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HT;
						}
						if (ligne.MARGE != null)
						{
							NewLine.MARGE = (decimal)ligne.MARGE;
						}
						else
						{
							NewLine.MARGE = 0;
						}
						NewLine.REMISE = (int)ligne.REMISE;
						NewLine.PTHT = (decimal)ligne.TOTALE_HT;
						NewLine.TVA = (int)ligne.TVA;
						NewLine.TTC = (decimal)ligne.TOTALE_TTC;
						ListeDesPoduits.Add(NewLine);
					}
					foreach (LIGNES_CUISINE_FACTURE_CLIENTS Ligne in ListeLigneCuisine)
					{
						LignesCuisine UneLigne = new LignesCuisine();
						UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
						UneLigne.SSCAISSON = Ligne.CAISSON.ID;
						UneLigne.Code = ID;
						UneLigne.LIB_CAISSON = Ligne.CAISSON.REF_BAS;
						UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
						UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
						UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;

						UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
						UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
						UneLigne.ACC = (decimal)Ligne.ACC;
						UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
						UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;
						UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.LIB_FACADE = Ligne.SS_FACADE.FACADE.REF_FAC;
						UneLigne.SOUSFACADE = (int)Ligne.SS_FACADE.TYPE_FACADE.ID;
						UneLigne.LIB_SOUSFACADE = Ligne.SS_FACADE.TYPE_FACADE.TYPE_FACADE1;
						//UneLigne.STOCK = (double)Ligne.STOCK;
						UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
						UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
						UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
						UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
						UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
						UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
						UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
						int count1 = 0;
						count1 = ListeDesCuisine.Count() + 1;
						while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
						{
							count1 = count1 + 1;
						}
						UneLigne.ID = count1;
						ListeDesCuisine.Add(UneLigne);
						int count = 0;
						List<LIGNES_DESCRIPTION_ACCESOIRE_Facture> list = db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(f => f.ID_LigneFacture == Ligne.ID).ToList();
						foreach (LIGNES_DESCRIPTION_ACCESOIRE_Facture ligne in list)
						{
							LignesACCESSOIRE Acc = new LignesACCESSOIRE();
							count = ListeDesPoduits.Count() + 1;
							while (ListeDesPoduits.Select(cmd => cmd.ID).Contains(count))
							{
								count = count + 1;
							}
							Acc.ID = count;
							Acc.DESIGNATION = ligne.Designation;
							Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
							Acc.IDArticle = (int)ligne.ID_ART;
							Acc.PUHT = (decimal)ligne.PUHT;
							Acc.PTHT = (decimal)ligne.PTHT;
							Acc.TVA = (int)ligne.TVA;
							Acc.TTC = (decimal)ligne.PTTC;
							Acc.QTE = (int)ligne.QTE;
							Acc.IDLIGNESDEScription = UneLigne.ID;
							listAccessoire.Add(Acc);

						}

						Session["LignesACCESSOIREFacture"] = listAccessoire;


					}
					if (ListeLigneSERVICESFact != null)
					{
						foreach (lIGNES_SERVICES_FACTURES ligne in ListeLigneSERVICESFact)
						{
							if (ListeDesSERVICE != null)
							{
								ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

							}
							if (ligne3 == null)
							{
								LignesServices NewLine = new LignesServices();
								NewLine.ID = (int)ligne.SERVICES;
								NewLine.Code = ID;
								NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
								NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
								NewLine.UNITE = ligne.Unite;
								NewLine.QUANTITE = (decimal)ligne.QUANTITE;
								NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
								NewLine.REMISE = (decimal)ligne.REMISE;
								NewLine.PTHT = (decimal)ligne.TOTALE_HT;
								NewLine.TVA = (int)ligne.TVA;
								NewLine.TTC = (decimal)ligne.TOTALE_TTC;
								List<lIGNES_SERVICES_FACTURES> ListeLigneSERVICES2 = db.lIGNES_SERVICES_FACTURES.Where(lcmd => lcmd.FACTURE_CLIENT == FactureClient.ID && (lcmd.SERVICES == ligne.SERVICES)).ToList();
								foreach (lIGNES_SERVICES_FACTURES ligne2 in ListeLigneSERVICES2)
								{
									NewLine.RESSOURCE.Add((int)ligne2.Personnels);
									NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

								}


								ListeDesSERVICE.Add(NewLine);
							}
						}
					}

					if (ListeLigneSERVICESSSTRAITANCE != null)
					{
						foreach (lIGNES_SERVICESSSTRAITANCE_FACTURE ligne in ListeLigneSERVICESSSTRAITANCE)
						{
							if (ListeDesSERVICESSTraitance != null)
							{
								ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

							}
							if (ligne4 == null)
							{
								LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
								NewLine.ID = (int)ligne.SERVICES;
								NewLine.Code = ID;
								NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
								NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
								NewLine.UNITE = ligne.Unite;
								NewLine.QUANTITE = (decimal)ligne.QUANTITE;
								NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
								NewLine.Marge = (decimal)ligne.MARGE;
								NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
								NewLine.REMISE = (decimal)ligne.REMISE;
								NewLine.PTHT = (decimal)ligne.TOTALE_HT;
								NewLine.TVA = (int)ligne.TVA;
								NewLine.TTC = (decimal)ligne.TOTALE_TTC;
								List<lIGNES_SERVICESSSTRAITANCE_FACTURE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE_FACTURE.Where(lcmd => lcmd.FACTURE_CLIENT == ID && (lcmd.SERVICES == ligne.SERVICES)).ToList();
								foreach (lIGNES_SERVICESSSTRAITANCE_FACTURE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
								{
									NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
									string nom = db.SOUS_TRAITANCE.Where(f => f.ID == ligne5.SOUS_TRAITANCE).FirstOrDefault().NOM;
									NewLine.SOUS_TRAITANCE2.Add(nom);

								}
								ListeDesSERVICESSTraitance.Add(NewLine);
							}
						}
					}

				}
				ViewBag.CODE_CLIENT = FactureClient.CLIENTS.CODE;
				ViewBag.CODESOC = FactureClient.Societes;
				ViewBag.declar = FactureClient.Declaration;
				ViewBag.datedec = FactureClient.Date_Declaration;
				ViewBag.service = FactureClient.Bien_service;
				ViewBag.immb = FactureClient.immobilisation;
				ViewBag.idd = FactureClient.ID;

			}
			Session["ProduitsFactureClient"] = ListeDesPoduits;
			Session["LignesServFact"] = ListeDesSERVICE;
			Session["LignesServSSTFact"] = ListeDesSERVICESSTraitance;
			Session["CUISINEFACTUREClient"] = ListeDesCuisine;
			ViewBag.Numero = Numero;
			return View(FactureClient);
		}
		public ActionResult FormCaisse(string Mode, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			Caisse Caisse = new Caisse();
			List<LignesServices> ListeDesSERVICE = new List<LignesServices>();
			List<LignesServicesSSTraitance> ListeDesSERVICESSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> listAccessoire = new List<LignesACCESSOIRE>();

			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			ViewBag.Mode = Mode;
			ViewBag.Code = Code;
			string Numero = string.Empty;
			if (Mode == "Create")
			{
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 21).FirstOrDefault();
				if (PrefixeTable == null)
				{
					Numero = "C";
				}
				else
				{
					string pref = PrefixeTable.Prefixe;
					string pref1 = pref.Replace("y", "");
					string pref2 = pref1.Replace("m", "");
					pref2 = pref2.Replace("0", "");
					Numero = pref2.Replace("-", "");
				}
			}
			if ((Mode == "Edit") || (Mode == "Aff"))
			{
				int ID = int.Parse(Code);
				Caisse = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
				Numero = Caisse.CODE;
				int iddevis = (int)Caisse.BONS_LIVRAISONS_CLIENTS.COMMANDES_CLIENTS.DEVIS_CLIENT;

				List<LIGNES_Caisse> ListeLigne = db.LIGNES_Caisse.Where(lcmd => lcmd.Caisse == ID).ToList();
				List<lIGNES_SERVICES> ListeLigneSERVICES = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis).ToList();
				List<LIGNES_CUISINE_CAISSE_CLIENTS> ListeLigneCuisine = db.LIGNES_CUISINE_CAISSE_CLIENTS.Where(lcmd => lcmd.CAISSE == ID).ToList();

				LignesServices ligne3 = new LignesServices();
				LignesServicesSSTraitance ligne4 = new LignesServicesSSTraitance();
				foreach (LIGNES_Caisse ligne in ListeLigne)
				{
					LigneProduit NewLine = new LigneProduit();
					NewLine.ID = (int)ligne.Prix_achat;
					NewLine.Code = ID;
					NewLine.LIBELLE = ligne.Libelle_Prd;
					NewLine.DESIGNATION = ligne.DESIGNATION_PRODUIT;
					NewLine.MARQUE = ligne.Marque;
					NewLine.DEVISE = ligne.Devise;
					NewLine.UNITE = ligne.Unite;
					NewLine.CATEGORIE = ligne.Categorie;
					NewLine.Sous_CATEGORIE = ligne.Sous_Categorie;
					Prix_Achat prix = db.Prix_Achat.Where(pr => pr.Product_ID == ligne.Prix_achat).FirstOrDefault();
					if (prix != null)
					{
						NewLine.STOCK = (decimal)db.Prix_Achat.Where(pr => pr.Product_ID == ligne.Prix_achat).FirstOrDefault().Stock;
					}
					else
					{
						NewLine.STOCK = 0;
					}
					NewLine.QUANTITE = (int)ligne.QUANTITE;
					//NewLine.STOCK = (int)ligne.STOCK;

					NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
					if (ligne.PRIX_UNITAIRE_HTVente != null)
					{
						NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
					}
					else
					{
						NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HT;
					}
					if (ligne.MARGE != null)
					{
						NewLine.MARGE = (decimal)ligne.MARGE;
					}
					else
					{
						NewLine.MARGE = 0;
					}
					NewLine.REMISE = (int)ligne.REMISE;
					NewLine.PTHT = (decimal)ligne.TOTALE_HT;
					NewLine.TVA = (int)ligne.TVA;
					NewLine.TTC = (decimal)ligne.TOTALE_TTC;
					ListeDesPoduits.Add(NewLine);
				}
				foreach (LIGNES_CUISINE_CAISSE_CLIENTS Ligne in ListeLigneCuisine)
				{
					LignesCuisine UneLigne = new LignesCuisine();
					UneLigne.ligneCuisine = (int)Ligne.ID;
					UneLigne.Code = ID;
					UneLigne.QuantiteCAISSON = (decimal)Ligne.QuantiteCAISSON;
					UneLigne.CAISSON = (int)Ligne.CAISSON1.TYPE_CAISSON;
					UneLigne.LIB_CAISSON = Ligne.CAISSON1.TYPE_CAISSON1.TYPE_CAISSON1;
					UneLigne.SSCAISSON = Ligne.CAISSON1.ID;
					UneLigne.LIB_SSCAISSON = Ligne.CAISSON1.REF_BAS;
					UneLigne.CREVCAISSON = (decimal)Ligne.CREVCAISSON;
					UneLigne.IDTYPCAISSON = (int)Ligne.TYPECAISSON;
					UneLigne.TYPCAISSON = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPCAISSON).FirstOrDefault().Libelle;
					if (Ligne.TYPEFACADE != null && Ligne.TYPEFACADE != 0)
					{
						UneLigne.IDTYPFACADE = (int)Ligne.TYPEFACADE;
						UneLigne.TYPFACADE = db.Sous_Categorie.Where(f => f.CatID == UneLigne.IDTYPFACADE).FirstOrDefault().Libelle;
					}
					if (Ligne.SOUSFACADE != null && Ligne.SOUSFACADE != 0)
					{
						int FACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.ID;
						string REFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						//UneLigne.FACADE = (int)Ligne.SS_FACADE.FACADE.ID;
						UneLigne.FACADE = FACADE;
						UneLigne.LIB_FACADE = REFFACADE;

						//choix les Types des facades

						int typeFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.ID;
						string typeREFFACADE = db.SS_FACADE.Where(f => f.ID == Ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						UneLigne.SOUSFACADE = typeFACADE;
						UneLigne.LIB_SOUSFACADE = typeREFFACADE;
					}
					UneLigne.MARGEPRIXACHAT = (decimal)Ligne.MARGEPRIXACHAT;
					UneLigne.PRIXACHAT = (decimal)Ligne.PRIXACHAT;
					UneLigne.ACC = (decimal)Ligne.ACC;
					UneLigne.POURCENTAGE = (decimal)Ligne.POURCENTAGE;
					UneLigne.PRIXVENTECAISSON = (decimal)Ligne.PRIXVENTECAISSON;


					//UneLigne.STOCK = (double)Ligne.STOCK;
					UneLigne.QuantiteFACADE = (decimal)Ligne.QuantiteFACADE;
					UneLigne.PRIXFACADE = (decimal)Ligne.PRIXFACADE;
					UneLigne.PTHTFACADE = (decimal)Ligne.PTHTFACADE;
					UneLigne.PTHTSSMARGE = (decimal)Ligne.PTHTSSMARGE;
					UneLigne.PTHTAVECMARGE = (decimal)Ligne.PTHTAVECMARGE;
					UneLigne.TVACUISINE = (int)Ligne.TVACUISINE;
					UneLigne.PTTCCUISINE = (decimal)Ligne.PTTCCUISINE;
					int count1 = 0;
					count1 = ListeDesCuisine.Count() + 1;
					while (ListeDesCuisine.Select(cmd => cmd.ID).Contains(count1))
					{
						count1 = count1 + 1;
					}
					UneLigne.ID = count1;
					ListeDesCuisine.Add(UneLigne);
					int count = 0;
					List<LIGNES_DESCRIPTION_ACCESOIRE_CAISSE> list = db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Where(f => f.ID_LigneCAISSE == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_CAISSE ligne in list)
					{
						LignesACCESSOIRE Acc = new LignesACCESSOIRE();
						count = listAccessoire.Count() + 1;
						while (listAccessoire.Select(cmd => cmd.ID).Contains(count))
						{
							count = count + 1;
						}
						Acc.ID = count;
						Acc.DESIGNATION = ligne.Designation;
						Acc.IDDESIGNATION = (int)ligne.ID_SSCAT;
						Acc.IDArticle = (int)ligne.ID_ART;
						Acc.Article = db.Prix_Achat.Where(f => f.Product_ID == Acc.IDArticle).FirstOrDefault().Libelle;
						Acc.PUHT = (decimal)ligne.PUHT;
						Acc.PTHT = (decimal)ligne.PTHT;
						Acc.TVA = (int)ligne.TVA;
						Acc.TTC = (decimal)ligne.PTTC;
						Acc.QTE = (int)ligne.QTE;
						Acc.IDLIGNESDEScription = UneLigne.ID;
						listAccessoire.Add(Acc);

					}

					Session["LignesACCESSOIRECAISSE"] = listAccessoire;



				}
				if (ListeLigneSERVICES != null)
				{
					foreach (lIGNES_SERVICES ligne in ListeLigneSERVICES)
					{
						if (ListeDesSERVICE != null)
						{
							ligne3 = ListeDesSERVICE.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne3 == null)
						{
							LignesServices NewLine = new LignesServices();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICES> ListeLigneSERVICES2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICES ligne2 in ListeLigneSERVICES2)
							{
								NewLine.RESSOURCE.Add((int)ligne2.Personnels);
								NewLine.RESSOURCE2.Add(ligne2.Personnels1.Nom);

							}


							ListeDesSERVICE.Add(NewLine);
						}
					}
				}
				if (ListeLigneSERVICESSSTRAITANCE != null)
				{
					foreach (lIGNES_SERVICESSSTRAITANCE ligne in ListeLigneSERVICESSSTRAITANCE)
					{
						if (ListeDesSERVICESSTraitance != null)
						{
							ligne4 = ListeDesSERVICESSTraitance.Where(f => f.ID == ligne.SERVICES).FirstOrDefault();

						}
						if (ligne4 == null)
						{
							LignesServicesSSTraitance NewLine = new LignesServicesSSTraitance();
							NewLine.ID = (int)ligne.SERVICES;
							NewLine.Code = ID;
							NewLine.REFSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().REF;
							NewLine.DescriptionSERVICE = db.SERVICES.Where(pr => pr.ID == NewLine.ID).FirstOrDefault().DES_SERVICE;
							NewLine.UNITE = ligne.Unite;
							NewLine.QUANTITE = (decimal)ligne.QUANTITE;
							NewLine.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
							NewLine.Marge = (decimal)ligne.MARGE;
							NewLine.PRIX_VENTE_HT2 = (decimal)ligne.PRIX_UNITAIRE_HTVente;
							NewLine.REMISE = (decimal)ligne.REMISE;
							NewLine.PTHT = (decimal)ligne.TOTALE_HT;
							NewLine.TVA = (int)ligne.TVA;
							NewLine.TTC = (decimal)ligne.TOTALE_TTC;
							List<lIGNES_SERVICESSSTRAITANCE> ListeLigneSERVICESSSTRAITANCE2 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == iddevis && (lcmd.SERVICES == ligne.SERVICES)).ToList();
							foreach (lIGNES_SERVICESSSTRAITANCE ligne5 in ListeLigneSERVICESSSTRAITANCE2)
							{
								NewLine.SOUS_TRAITANCE.Add((int)ligne5.SOUS_TRAITANCE);
								NewLine.SOUS_TRAITANCE2.Add(ligne5.SOUS_TRAITANCE1.NOM);

							}


							ListeDesSERVICESSTraitance.Add(NewLine);
						}
					}
				}
				ViewBag.CODE_CLIENT = Caisse.CLIENTS.CODE;
				ViewBag.CODESOC = Caisse.Societes;
				ViewBag.declar = Caisse.Declaration;
				ViewBag.datedec = Caisse.Date_Declaration;
				ViewBag.service = Caisse.Bien_service;
				ViewBag.immb = Caisse.immobilisation;
				ViewBag.idd = Caisse.ID;


			}
			Session["ProduitsCaisseClient"] = ListeDesPoduits;
			Session["LignesServ"] = ListeDesSERVICE;
			Session["LignesServSST"] = ListeDesSERVICESSTraitance;
			Session["CUISINECAISSEClient"] = ListeDesCuisine;
			ViewBag.Numero = Numero;
			return View(Caisse);
		}
		#endregion
		#region common functions
		public JsonResult GetAllMois()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_CLIENTS> ListeFactClt = db.FACTURES_CLIENTS.Where(f => f.Declaration == true).ToList();
			List<FACTURES_FOURNISSEURS> ListefactFrs = db.FACTURES_FOURNISSEURS.Where(f => f.Declaration == true).ToList();
			List<string> listdate = new List<string>();
			foreach (FACTURES_CLIENTS factclt in ListeFactClt)
			{
				if (factclt.Date_Declaration != null)
				{
					string[] date1 = factclt.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].Split('/');
					string mois = date2[1] + '/' + date2[2];
					listdate.Add(mois);
				}
			}
			foreach (FACTURES_FOURNISSEURS factclt in ListefactFrs)
			{
				if (factclt.Date_Declaration != null)
				{
					string[] date1 = factclt.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].Split('/');
					string mois = date2[1] + '/' + date2[2];
					listdate.Add(mois);
				}
			}

			IEnumerable<string> listdate2 = listdate.Distinct();
			return Json(listdate2, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllSERVICE()
		{
			db.Configuration.ProxyCreationEnabled = false;
			int idste = (int)Session["SoclogoId"];

			List<SERVICES> SERVICES = db.SERVICES.ToList();
			return Json(SERVICES, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllSOUSTRAITANCE()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<SOUS_TRAITANCE> SERVICES = db.SOUS_TRAITANCE.ToList();
			return Json(SERVICES, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllProduct()
		{
			db.Configuration.ProxyCreationEnabled = false;

			int idste = (int)Session["SoclogoId"];
			List<LIGNES_DEVIS_FOURNISSEURS> ListeProduit = db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.Categorie == "ACCESSOIRE DE PRODUCTION" && f.DEVIS_FOURNISSEURS.Societes == idste).ToList();
			return Json(ListeProduit, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllArticles()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<Prix_Achat> ListeProduit = db.Prix_Achat.ToList();
			return Json(ListeProduit, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetStockbydes(string des)
		{
			double stock = 0;
			Prix_Achat prixA = db.Prix_Achat.Where(f => f.Libelle == des).FirstOrDefault();
			if (prixA != null)
			{
				stock = (double)prixA.Stock;
			}
			return Json(stock, JsonRequestBehavior.AllowGet);

		}

		public JsonResult GetProductByID(string ID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			int id = int.Parse(ID);
			LIGNES_DEVIS_FOURNISSEURS produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == id).FirstOrDefault();
			return Json(produit, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetArticleByID(string ID)
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
		public JsonResult GetAllFACADE()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<FACADE> ListeFACADE = db.FACADE.ToList();
			return Json(ListeFACADE, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetFACDEEbyIdTYPEFACADE(string FACADE, string SSFACADE, string TYPESOUSFACADE)
		{
			int facade = int.Parse(FACADE);
			int ssfacade = int.Parse(SSFACADE);
			int TYPESSfacade = int.Parse(TYPESOUSFACADE);
			SS_FACADE ssfac = db.SS_FACADE.Where(f => f.ID_FAC == facade && f.TYPE_SS_FACADE == ssfacade).FirstOrDefault();
			Prix_Achat Prix_Achat = db.Prix_Achat.Where(f => f.Sous_Categorie == TYPESSfacade).FirstOrDefault();
			decimal ptht = 0;
			ptht = (decimal)(ssfac.TOTSURFACE * (decimal)Prix_Achat.PU_HT_Sans_Remise);
			return Json(ptht, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetSSFACADEbyId(string id)
		{
			int facade = int.Parse(id);
			List<SS_FACADE> s = new List<SS_FACADE>();
			List<TYPE_FACADE> ss = new List<TYPE_FACADE>();
			List<TYPE_FACADE> ff = db.TYPE_FACADE.ToList();

			if (facade > 0)
			{
				s = db.SS_FACADE.Where(f => f.ID_FAC == facade).ToList();

			}
			foreach (TYPE_FACADE t in ff)
			{
				foreach (SS_FACADE ssf in s)
				{
					if (t.ID == ssf.TYPE_FACADE.ID)
					{
						ss.Add(t);
						break;
					}
				}
			}
			//List<SS_FACADE> ssfac = db.SS_FACADE.Where(f => f.ID_FAC == facade).ToList();

			var result = (from r in ss
						  select new
						  {
							  id = r.ID,
							  name = r.TYPE_FACADE1,
						  }).ToList();

			return Json(result, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetMPIdSSCAISSON(string id)
		{
			int facade = int.Parse(id);
			CAISSON ssfac = db.CAISSON.Where(f => f.ID == facade).FirstOrDefault();
			int idcat = (int)ssfac.CATEGORIE;
			List<Prix_Achat> s = new List<Prix_Achat>();
			List<Prix_Achat> sscat = new List<Prix_Achat>();
			List<Sous_Categorie> Listsscat = db.Sous_Categorie.ToList();

			if (idcat > 0)
			{
				s = db.Prix_Achat.Where(f => f.Categorie == idcat).ToList();

			}
			foreach (Sous_Categorie p in Listsscat)
			{
				foreach (Prix_Achat pa in s)
				{
					if (p.CatID == pa.Sous_Categorie)
					{
						sscat.Add(pa);
						break;
					}
				}
			}
			if (Session["LignesACC"] == null)
			{
				List<DESCRIPTION_ACCESOIRE> listeDescription = db.DESCRIPTION_ACCESOIRE.Where(f => f.ID_ACC == ssfac.ID_ACC).ToList();
				List<LignesACCESSOIRE> ListeDesPoduits22 = new List<LignesACCESSOIRE>();
				int count = 0;

				foreach (DESCRIPTION_ACCESOIRE ligne in listeDescription)
				{
					LignesACCESSOIRE des = new LignesACCESSOIRE();
					count = ListeDesPoduits22.Count() + 1;
					while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
					{
						count = count + 1;
					}
					des.ID = count;
					des.IDArticle = (int)ligne.ID_ART;
					des.Article = db.Prix_Achat.Where(f => f.Product_ID == des.IDArticle).FirstOrDefault().Libelle;
					des.IDDESIGNATION = (int)ligne.ID_SSCAT;
					des.DESIGNATION = ligne.Designation;
					des.PUHT = (decimal)ligne.PUHT;
					des.PTHT = (decimal)ligne.PTHT;
					des.TVA = (int)ligne.TVA;
					des.TTC = (decimal)ligne.PTTC;
					des.QTE = (decimal)ligne.QTE;
					ListeDesPoduits22.Add(des);
				}
				Session["LignesACC"] = ListeDesPoduits22;
			}
			var result = (from r in sscat
						  select new
						  {
							  id = r.Sous_Categorie,
							  name = r.Sous_Categorie1.Libelle,
						  }).ToList();

			return Json(result, JsonRequestBehavior.AllowGet);


		}
		public JsonResult GetMPIdSOUSFACADE(string id)
		{
			int facade = int.Parse(id);
			SS_FACADE ssfac = db.SS_FACADE.Where(f => f.TYPE_FACADE.ID == facade).FirstOrDefault();
			int idcat = (int)ssfac.CATEGORIE;
			List<Prix_Achat> s = new List<Prix_Achat>();
			List<Prix_Achat> sscat = new List<Prix_Achat>();
			List<Sous_Categorie> Listsscat = db.Sous_Categorie.ToList();
			if (idcat > 0)
			{
				s = db.Prix_Achat.Where(f => f.Categorie == idcat).ToList();

			}
			foreach (Sous_Categorie p in Listsscat)
			{
				foreach (Prix_Achat pa in s)
				{
					if (p.CatID == pa.Sous_Categorie)
					{
						sscat.Add(pa);
						break;
					}
				}
			}
			var result = (from r in sscat
						  select new
						  {
							  id = r.Sous_Categorie,
							  name = r.Sous_Categorie1.Libelle,
						  }).ToList();

			return Json(result, JsonRequestBehavior.AllowGet);


		}

		public JsonResult GetPHTSSCAISSONbyId(string SSCAISSON, string MP)
		{
			int sscaisson = int.Parse(SSCAISSON);
			int mp = int.Parse(MP);
			CAISSON ssfac = db.CAISSON.Where(f => f.ID == sscaisson).FirstOrDefault();

			Prix_Achat Prix_Achat = db.Prix_Achat.Where(f => f.Sous_Categorie == mp).FirstOrDefault();
			decimal ptht = 0;
			decimal Acc = 0;
			ptht = (decimal)(ssfac.TOTSURFACE * (decimal)Prix_Achat.PU_HT_Sans_Remise);
			List<LignesACCESSOIRE> list = (List<LignesACCESSOIRE>)Session["LignesACC"];
			if (list != null)
			{
				foreach (LignesACCESSOIRE ligne in list)
				{
					Acc += ligne.PTHT;
				}
			}

			dynamic result = new
			{
				ptht = ptht,
				Acc = Acc,

			};

			return Json(result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllSSFACADE()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<TYPE_FACADE> ListeTYPE_FACADE = db.TYPE_FACADE.ToList();
			return Json(ListeTYPE_FACADE, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllCAISSON()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<TYPE_CAISSON> TYPE_CAISSON = db.TYPE_CAISSON.ToList();
			return Json(TYPE_CAISSON, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllRESSOURCE()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<Personnels> Personnels = db.Personnels.ToList();
			return Json(Personnels, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetService(string ID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			int id = int.Parse(ID);
			SERVICES SERVICES = db.SERVICES.Where(pr => pr.ID == id).FirstOrDefault();
			return Json(SERVICES, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetRESSOURCEPARID(string ID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			int id = int.Parse(ID);
			Personnels Personnels = db.Personnels.Where(pr => pr.PersonnelId == id).FirstOrDefault();
			return Json(Personnels, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetClientByID(string ID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			int id = int.Parse(ID);
			CLIENTS client = db.CLIENTS.Where(pr => pr.ID == id).FirstOrDefault();
			return Json(client, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetCategoryById(string ID)
		{
			db.Configuration.ProxyCreationEnabled = false;
			int id = int.Parse(ID);
			Categorie client = db.Categorie.Where(pr => pr.CentreID == id).FirstOrDefault();
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

		public JsonResult GetCAISSONbyTypeId(string id)
		{
			List<CAISSON> s = new List<CAISSON>();
			int idd = int.Parse(id);
			if (idd > 0)
			{
				s = db.CAISSON.Where(p => p.TYPE_CAISSON == idd).ToList();

			}

			var result = (from r in s
						  select new
						  {
							  id = r.ID,
							  name = r.REF_BAS,
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
			if ((BonLivraisonClient.VALIDER) && (BonLivraisonClient.Type == true))
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
		public string verifieValiditeCai(string id)
		{
			int ID = int.Parse(id);
			Caisse facture = new Caisse();
			facture = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
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
			BONS_LIVRAISONS_CLIENTS blcmd = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.COMMANDE_CLIENT == ID).FirstOrDefault();
			if (blcmd == null)
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
		//public ActionResult InvoicePrint(string CODE, string Conditions, string validite, string TotalHTMI,string FINTION,string Tiroirs,string Charnieres,string designation,string affaireCommerciale)
		public ActionResult InvoicePrint(string CODE, string validite, string TotalHTMI)
		{
			string CodeAff = "";
			int ID = int.Parse(CODE);
			string Conditions = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().ConditionPaiement;
			string FINTION = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().FINTION;
			string Tiroirs = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().Tiroirs;
			string Charnieres = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().Charnieres;
			string designation = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().Designation;
			string affaireCommerciale = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault().Id_AffaireCommerciale.ToString();
			if (affaireCommerciale != "" && affaireCommerciale != null)
			{
				int IDAffCom = int.Parse(affaireCommerciale);
				CodeAff = db.AffaireCommerciales.Where(f => f.AffaireCommercialeId == IDAffCom).FirstOrDefault().Reference;
			}

			double somme = 0;
			DEVIS_CLIENTS DVF = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_DEVIS_CLIENTS> lIGNES_DEVIS_CLIENTS = db.LIGNES_DEVIS_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();
			//List<lig> lIGNES_Service = db.LIGNES_DEVIS_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();

			List<Inspinia_MVC5.Models.Tasks> task_list = db.Tasks.Where(t => t.ProjetTechniquesID == ID).ToList();
			List<Inspinia_MVC5.Models.LIGNES_DEVIS_CLIENTS> LIGNES_DEVIS_CLIENTS = db.LIGNES_DEVIS_CLIENTS.Where(t => t.DEVIS_CLIENT == ID).ToList();
			List<Inspinia_MVC5.Models.LIGNES_CUISINE_DEVIS_CLIENTS> LIGNES_CUISINE_DEVIS_CLIENTS = db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(t => t.DEVIS_CLIENT == ID).ToList();

			List<Inspinia_MVC5.Models.lIGNES_SERVICESSSTRAITANCE> lIGNES_SERVICESSSTRAITANCE = db.lIGNES_SERVICESSSTRAITANCE.Where(t => t.DEVIS_CLIENT == ID).ToList();
			List<Inspinia_MVC5.Models.lIGNES_SERVICES> lIGNES_SERVICES = db.lIGNES_SERVICES.Where(t => t.DEVIS_CLIENT == ID).ToList();

			foreach (Tasks ta in task_list)
			{
				double c1 = (double)db.Personnels.Find(ta.owner_id).Cout_hor;
				string c4 = ta.duration_h_planning.ToString();
				int c2 = int.Parse(c4);
				double c3 = c1 * c2;
				somme = somme + c3;
			}
			decimal TTCSoutretance = 0;
			decimal THTSoutretance = 0;
			decimal TTCService = 0;
			decimal THTService = 0;
			decimal TTVADEVIS = 0;
			decimal TTVAsERVICE = 0;
			decimal TTVASSTTRAITANCE = 0;

			foreach (LIGNES_DEVIS_CLIENTS ligne in LIGNES_DEVIS_CLIENTS)
			{

				TTVADEVIS += (decimal)(ligne.TOTALE_HT * ligne.TVA / 100);
			}
			foreach (lIGNES_SERVICESSSTRAITANCE ligne in lIGNES_SERVICESSSTRAITANCE)
			{
				TTCSoutretance += (decimal)ligne.TOTALE_TTC;
				THTSoutretance += (decimal)ligne.TOTALE_HT;
				TTVASSTTRAITANCE += (decimal)(ligne.TOTALE_HT * ligne.TVA / 100);

			}
			foreach (lIGNES_SERVICES ligne in lIGNES_SERVICES)
			{
				TTCService += (decimal)ligne.TOTALE_TTC;
				THTService += (decimal)ligne.TOTALE_HT;
				TTVAsERVICE += (decimal)(ligne.TOTALE_HT * ligne.TVA / 100);
			}
			//decimal TotalHTMI1 = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
			//decimal ttcinstal = (TotalHTMI1) - DVF.TTC;
			decimal totale = (decimal)somme + DVF.TTC;
			decimal ttvinstal = (decimal)DVF.TVAInstallation;

			ViewBag.ttcinstal = (((decimal)somme * ttvinstal) / 100) + (decimal)somme + TTCSoutretance + TTCService;
			ViewBag.somme = somme + (double)THTSoutretance + (double)THTService;
			ViewBag.id = DVF.ID.ToString();
			ViewBag.NOM = DVF.CLIENTS.NOM;
			ViewBag.ADRESSE = DVF.CLIENTS.ADRESSE;
			ViewBag.TEL = DVF.CLIENTS.TELEPHONE;
			ViewBag.FAX = DVF.CLIENTS.FAX;
			ViewBag.DATE = DVF.DATE;
			ViewBag.DATE2 = DateTime.Now;
			ViewBag.TTVA = (((decimal)somme * ttvinstal) / 100) + TTVADEVIS + TTVAsERVICE + TTVASSTTRAITANCE;
			ViewBag.TTTC = DVF.TTC;
			ViewBag.TTNET = DVF.THT + (decimal)somme;
			ViewBag.Code = DVF.CODE;
			ViewBag.totale = totale;
			//string[] cond = Conditions.Split('-');
			ViewBag.FINTION = FINTION;
			ViewBag.Tiroirs = Tiroirs;
			ViewBag.Charnieres = Charnieres;
			ViewBag.designation = designation;
			ViewBag.Conditions = Conditions;
			ViewBag.validite = validite;
			ViewBag.affaireCommerciale = affaireCommerciale;
			ViewBag.CodeAff = CodeAff;
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}

			int idste = (int)Session["SoclogoId"];
			SocieteLogo NomSociete = db.SocieteLogo.Where(f => f.id == idste).FirstOrDefault();
			ViewBag.NomSte = NomSociete.Nom_Societe;
			ViewBag.Adresse = NomSociete.Adresse;
			ViewBag.Logo = NomSociete.logo;
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
							 ADRESSE = UnDevis.CLIENTS.ADRESSE,
							 ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
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
							 CODE = UnDevis.CODE,
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
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login ", "Societes");
			}
			string soc = (string)Session["Soclogo"];

			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = UnDevis.CODE,
							 MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
							 DATE = UnDevis.DATE.ToShortDateString(),
							 //CODE= UnDevis.CLIENTS.CODE,
							 //NOM = UnDevis.CLIENTS.NOM,
							 ADRESSE = UnDevis.CLIENTS.ADRESSE,
							 //TEL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
							 FAX = UnDevis.CLIENTS.FAX,
							 NOM = UnDevis.CLIENTS.NOM,
							 //Direction = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,
							 Expr1 = societeLogo.Nom_Societe,
							 Expr7 = societeLogo.Adresse,
							 Expr5 = societeLogo.mail,
							 Expr8 = societeLogo.Agence + "-RIB:" + societeLogo.RIB + "-IBAN:" + societeLogo.IBAN,
							 MF = societeLogo.MF + "-RC:" + societeLogo.RC,
							 //CODE_ACCES=db.Societes.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().CODE_ACCES,
							 Unite = ((UnDevis.REMISE).ToString() + "%"),
							 Expr3 = UnDevis.CLIENTS.CODE,
							 THT = UnDevis.THT ?? 0,
							 //ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
							 TTC = UnDevis.TTC,
							 TTVA = UnDevis.TTVA ?? 0,

							 //TIMBRE =UnDevis.TIMBRE,
							 // TTC = UnDevis.TTC ,
							 //TTVA=UnDevis.TTVA,
							 //TTVA=UnDevis.TTVA,
							 Expr4 = cmd.Libelle_Prd,
							 DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
							 QUANTITE = cmd.QUANTITE ?? 0,
							 PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
							 //REMISE = cmd.REMISE ?? 0,
							 TOTALE_HT = cmd.TOTALE_HT ?? 0,
							 TVA = ((cmd.TVA).ToString() + "%"),
							 TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
							 //RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
							 ID_FISCAL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().ID_FISCAL,

							 //RC = db.CLIENTS.Where(soc => soc.ID == 1).FirstOrDefault().RC


						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/CommandeClient.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Bon de Commande";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);

			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}

		public ActionResult PrintBonLivraisonClientByID(string CODE)
		{
			int ID = int.Parse(CODE);
			ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
			BONS_LIVRAISONS_CLIENTS UnDevis = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_CLIENT == ID).ToList();
			List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> list3 = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(lcmd => lcmd.BONLIVRAISON_CLIENT == ID).ToList();
			if (list3 != null)
			{
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS ligne in list3)
				{
					LIGNES_BONS_LIVRAISONS_CLIENTS ligne1 = new LIGNES_BONS_LIVRAISONS_CLIENTS();
					string refCAISSON = db.CAISSON.Where(f => f.ID == ligne.SSCAISSON).FirstOrDefault().REF_BAS;

					ligne1.Libelle_Prd = refCAISSON;
					ligne1.DESIGNATION_PRODUIT = refCAISSON;
					//ligne1.PRIX_UNITAIRE_HT = ligne.PRIXVENTECAISSON;
					ligne1.QUANTITE = (double)ligne.QuantiteCAISSON;
					//ligne1.TVA = ligne.TVACUISINE;
					//ligne1.TOTALE_HT = ligne.PRIXVENTECAISSON * ligne.QuantiteCAISSON;
					//ligne1.TOTALE_TTC = ligne1.TOTALE_HT + (ligne1.TOTALE_HT * ligne.TVACUISINE) / 100;
					Liste.Add(ligne1);
				}
			}
			if (list3 != null)
			{
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS ligne in list3)
				{
					LIGNES_BONS_LIVRAISONS_CLIENTS ligne1 = new LIGNES_BONS_LIVRAISONS_CLIENTS();
					if (ligne.SOUSFACADE != 0 && ligne.SOUSFACADE != null)
					{
						string refFacade = db.SS_FACADE.Where(f => f.ID == ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						string TypeFacade = db.SS_FACADE.Where(f => f.ID == ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						ligne1.Libelle_Prd = refFacade;
						ligne1.DESIGNATION_PRODUIT = TypeFacade;
						//ligne1.PRIX_UNITAIRE_HT = ligne.PTHTFACADE;
						ligne1.QUANTITE = (double)ligne.QuantiteCAISSON;
						//ligne1.TVA = ligne.TVACUISINE;
						//ligne1.TOTALE_HT = ligne.PTHTFACADE * ligne.QuantiteCAISSON;
						//ligne1.TOTALE_TTC = ligne1.TOTALE_HT + (ligne1.TOTALE_HT * ligne.TVACUISINE) / 100;
						Liste.Add(ligne1);

					}
				}
			}


			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = UnDevis.CODE,
							 MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
							 DATE = UnDevis.DATE.ToShortDateString(),
							 //CODE = UnDevis.CLIENTS.CODE,
							 NOM = UnDevis.CLIENTS.NOM,
							 Expr3 = UnDevis.CLIENTS.CODE,
							 ADRESSE = UnDevis.CLIENTS.ADRESSE,
							 Expr1 = societeLogo.Nom_Societe,
							 Expr7 = societeLogo.Adresse,
							 Expr5 = societeLogo.mail,
							 Expr8 = societeLogo.Agence + "-RIB:" + societeLogo.RIB + "-IBAN:" + societeLogo.IBAN,
							 MF = societeLogo.MF + "-RC:" + societeLogo.RC,
							 Expr4 = cmd.Libelle_Prd,
							 DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
							 QUANTITE = cmd.QUANTITE ?? 0,
							 //RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
							 ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,


						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/BonLivraison.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Bon de livraison Client N°:";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
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
			List<lIGNES_SERVICES> list2 = db.lIGNES_SERVICES.Where(lcmd => lcmd.DEVIS_CLIENT == UnDevis.BONS_LIVRAISONS_CLIENTS.COMMANDES_CLIENTS.DEVIS_CLIENTS.ID).ToList();
			List<LIGNES_CUISINE_FACTURE_CLIENTS> list3 = db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(lcmd => lcmd.FACTURE_CLIENT == ID).ToList();

			if (list3 != null)
			{
				foreach (LIGNES_CUISINE_FACTURE_CLIENTS ligne in list3)
				{
					LIGNES_FACTURES_CLIENTS ligne1 = new LIGNES_FACTURES_CLIENTS();
					string refCAISSON = db.CAISSON.Where(f => f.ID == ligne.SSCAISSON).FirstOrDefault().REF_BAS;

					ligne1.Libelle_Prd = refCAISSON;
					ligne1.DESIGNATION_PRODUIT = refCAISSON;
					ligne1.PRIX_UNITAIRE_HT = ligne.PRIXVENTECAISSON;
					ligne1.QUANTITE = (double)ligne.QuantiteCAISSON;
					ligne1.TVA = ligne.TVACUISINE;
					ligne1.TOTALE_HT = ligne.PRIXVENTECAISSON * ligne.QuantiteCAISSON;
					ligne1.TOTALE_TTC = ligne1.TOTALE_HT + (ligne1.TOTALE_HT * ligne.TVACUISINE) / 100;
					Liste.Add(ligne1);
				}
			}
			if (list3 != null)
			{
				foreach (LIGNES_CUISINE_FACTURE_CLIENTS ligne in list3)
				{
					LIGNES_FACTURES_CLIENTS ligne1 = new LIGNES_FACTURES_CLIENTS();
					if (ligne.SOUSFACADE != 0 && ligne.SOUSFACADE != null)
					{
						string refFacade = db.SS_FACADE.Where(f => f.ID == ligne.SOUSFACADE).FirstOrDefault().FACADE.REF_FAC;
						string TypeFacade = db.SS_FACADE.Where(f => f.ID == ligne.SOUSFACADE).FirstOrDefault().TYPE_FACADE.TYPE_FACADE1;
						ligne1.Libelle_Prd = refFacade;
						ligne1.DESIGNATION_PRODUIT = TypeFacade;
						ligne1.PRIX_UNITAIRE_HT = ligne.PTHTFACADE;
						ligne1.QUANTITE = (double)ligne.QuantiteCAISSON;
						ligne1.TVA = ligne.TVACUISINE;
						ligne1.TOTALE_HT = ligne.PTHTFACADE * ligne.QuantiteCAISSON;
						ligne1.TOTALE_TTC = ligne1.TOTALE_HT + (ligne1.TOTALE_HT * ligne.TVACUISINE) / 100;
						Liste.Add(ligne1);

					}
				}
			}
			if (list2 != null)
			{
				foreach (lIGNES_SERVICES ligne in list2)
				{
					LIGNES_FACTURES_CLIENTS ligne1 = new LIGNES_FACTURES_CLIENTS();
					ligne1.Libelle_Prd = ligne.SERVICES1.REF;
					ligne1.DESIGNATION_PRODUIT = ligne.SERVICES1.DES_SERVICE;
					ligne1.PRIX_UNITAIRE_HT = ligne.PRIX_UNITAIRE_HT;
					ligne1.QUANTITE = (double)ligne.QUANTITE;
					ligne1.TVA = ligne.TVA;
					ligne1.TOTALE_HT = ligne.TOTALE_HT;
					ligne1.TOTALE_TTC = ligne.TOTALE_TTC;
					Liste.Add(ligne1);

				}
			}
			List<lIGNES_SERVICESSSTRAITANCE> list22 = db.lIGNES_SERVICESSSTRAITANCE.Where(lcmd => lcmd.DEVIS_CLIENT == UnDevis.BONS_LIVRAISONS_CLIENTS.COMMANDES_CLIENTS.DEVIS_CLIENTS.ID).ToList();
			if (list22 != null)
			{
				foreach (lIGNES_SERVICESSSTRAITANCE ligne in list22)
				{
					LIGNES_FACTURES_CLIENTS ligne1 = new LIGNES_FACTURES_CLIENTS();
					ligne1.Libelle_Prd = ligne.SERVICES1.REF;
					ligne1.DESIGNATION_PRODUIT = ligne.SERVICES1.DES_SERVICE;
					ligne1.PRIX_UNITAIRE_HT = ligne.PRIX_UNITAIRE_HT;
					ligne1.QUANTITE = (double)ligne.QUANTITE;
					ligne1.TVA = ligne.TVA;
					ligne1.TOTALE_HT = ligne.TOTALE_HT;
					ligne1.TOTALE_TTC = ligne.TOTALE_TTC;
					Liste.Add(ligne1);

				}
			}
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = UnDevis.CODE,
							 MODE_PAIEMENT = UnDevis.MODE_PAIEMENT,
							 DATE = UnDevis.DATE.ToShortDateString(),
							 //CODE= UnDevis.CLIENTS.CODE,
							 //NOM = UnDevis.CLIENTS.NOM,
							 ADRESSE = UnDevis.CLIENTS.ADRESSE,
							 //TEL = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().TEL,
							 FAX = UnDevis.CLIENTS.FAX,
							 NOM = UnDevis.CLIENTS.NOM,
							 Expr3 = UnDevis.CLIENTS.CODE,
							 //Direction = db.Direction.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().Nom,
							 Expr1 = societeLogo.Nom_Societe,
							 Expr7 = societeLogo.Adresse,
							 Expr5 = societeLogo.mail,
							 Expr8 = societeLogo.Agence + "-RIB:" + societeLogo.RIB + "-IBAN:" + societeLogo.IBAN,
							 MF = societeLogo.MF + "-RC:" + societeLogo.RC,
							 //CODE_ACCES=db.Societes.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().CODE_ACCES,
							 Unite = ((cmd.REMISE).ToString() + "%"),
							 Expr2 = UnDevis.TIMBRE ?? 0,
							 THT = UnDevis.THT ?? 0,
							 //ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,
							 TTC = UnDevis.TTC,
							 TTVA = UnDevis.TTVA ?? 0,

							 //TIMBRE =UnDevis.TIMBRE,
							 // TTC = UnDevis.TTC ,
							 //TTVA=UnDevis.TTVA,
							 //TTVA=UnDevis.TTVA,
							 Expr4 = cmd.Libelle_Prd,
							 DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
							 QUANTITE = cmd.QUANTITE ?? 0,
							 PRIX_UNITAIRE_HT = cmd.PRIX_UNITAIRE_HT ?? 0,
							 //REMISE = cmd.REMISE ?? 0,
							 TOTALE_HT = cmd.TOTALE_HT ?? 0,
							 TVA = ((cmd.TVA).ToString() + "%"),
							 TOTALE_TTC = cmd.TOTALE_TTC ?? 0,
							 //RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
							 ID_FISCAL = UnDevis.CLIENTS.ID_FISCAL,

							 //RC = db.CLIENTS.Where(soc => soc.ID == 1).FirstOrDefault().RC

						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/FactureClient1.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Facture Client N°:";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);

			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}
		public ActionResult PrintBLClientByIDPartiel(string CODE)
		{
			int ID = int.Parse(CODE);
			ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
			BONS_LIVRAISONS_PART_CLIENTS UnDevis = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			BONS_LIVRAISONS_CLIENTS UnDevis1 = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == UnDevis.IDBLC).FirstOrDefault();

			List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(lcmd => lcmd.BON_LIVRAISON_PART_CLIENT == ID).ToList();
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = UnDevis.CODE,
							 MODE_PAIEMENT = UnDevis1.MODE_PAIEMENT,
							 DATE = UnDevis.DATE.ToShortDateString(),
							 //CODE = UnDevis.CLIENTS.CODE,
							 NOM = UnDevis1.CLIENTS.NOM,
							 Expr3 = UnDevis1.CLIENTS.CODE,
							 ADRESSE = UnDevis1.CLIENTS.ADRESSE,
							 Expr1 = societeLogo.Nom_Societe,
							 Expr7 = societeLogo.Adresse,
							 Expr5 = societeLogo.mail,
							 Expr8 = societeLogo.Agence + "-RIB:" + societeLogo.RIB + "-IBAN:" + societeLogo.IBAN,
							 MF = societeLogo.MF + "-RC:" + societeLogo.RC,
							 Expr4 = cmd.Libelle_Prd,
							 DESIGNATION_PRODUIT = cmd.DESIGNATION_PRODUIT,
							 QUANTITE = cmd.QUANTITE ?? 0,
							 //RC = db.Tiers.Where(fd => fd.SociID == UnDevis.Societes).FirstOrDefault().RC,
							 ID_FISCAL = UnDevis1.CLIENTS.ID_FISCAL,

						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/BonLivraisonPartiel.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Bon de livraison Client N°:";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
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
							 NOM = cmd.CLIENTS.NOM,
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
							 NOM = cmd.SocieteLogo.Nom_Societe,
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
							 Designation = cmd.Designation,
							 NOM = db.CLIENTS.Where(fd => fd.ID == cmd.CLIENT).FirstOrDefault().NOM,
							 Expr1 = db.Tiers.Where(fd => fd.Clt == cmd.CLIENT).FirstOrDefault().NOM,

							 ////FOURNISSEUR = cmd.CLIENTS.NOM,
							 //Societes=cmd.Societes,
							 //Tiers=cmd.Tiers,
							 MODE_PAIEMENT = cmd.MODE_PAIEMENT,

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
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = cmd.CODE,
							 FOURNISSEUR = cmd.CLIENTS.NOM,
							 Societes = cmd.DATE.ToString("dd/mm/yyyy"),
							 //Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
							 DATE = cmd.DATE.ToString(),
							 //Expr4 = cmd.FOURNISSEURS.NOM,
							 //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
							 PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
							 MODE_PAIEMENT = cmd.MODE_PAIEMENT,
							 THT = cmd.THT ?? 0,
							 NHT = cmd.NHT ?? 0,
							 TTVA = cmd.TTVA ?? 0,
							 TTC = cmd.TTC,
							 TNET = cmd.TNET ?? 0,

						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/ListeFactureClientParetat.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Factures Clients";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}
		public ActionResult PrintAllFactureClientParclt()
		{
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = cmd.CODE,
							 FOURNISSEUR = cmd.CLIENTS.NOM,
							 Societes = cmd.DATE.ToShortDateString(),
							 //Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
							 DATE = cmd.DATE.ToShortDateString(),
							 //Expr4 = cmd.FOURNISSEURS.NOM,
							 //VALIDEE = cmd.VALIDER ? "VALIDEE" : "NON VALIDEE",
							 //PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
							 MODE_PAIEMENT = cmd.MODE_PAIEMENT,
							 THT = cmd.THT ?? 0,
							 NHT = cmd.NHT ?? 0,
							 TTVA = cmd.TTVA ?? 0,
							 TTC = cmd.TTC,
							 TNET = cmd.TNET ?? 0,

						 };
			ReportDocument rptH = new ReportDocument();
			string FileName = Server.MapPath("/Reports/ListeFactureClientParclt.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Factures Clients";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}
		public ActionResult PrintAllFactureClientParAn()
		{
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = cmd.CODE,
							 //FOURNISSEUR = cmd.FOURNISSEURS.NOM,
							 //Societes = cmd.Societes,
							 Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
							 Expr4 = cmd.CLIENTS.NOM,
							 DATE = cmd.DATE.ToString("yyyy"),
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
			string FileName = Server.MapPath("/Reports/ListeFactureClientParAn.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Factures Clients Par An";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}
		public ActionResult PrintAllFactureClientParAnFRS()
		{
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);
			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = cmd.CODE,
							 FOURNISSEUR = cmd.CLIENTS.NOM,
							 Societes = cmd.DATE.ToShortDateString(),
							 DATE = cmd.DATE.ToString("yyyy"),
							 PAYEE = (bool)cmd.PAYEE ? "PAYEE" : "NON PAYEE",
							 MODE_PAIEMENT = cmd.MODE_PAIEMENT,
							 THT = cmd.THT ?? 0,
							 NHT = cmd.NHT ?? 0,
							 TTVA = cmd.TTVA ?? 0,
							 TTC = cmd.TTC,
							 TNET = cmd.TNET ?? 0,
							 //SITE_WEB = "Images\a1.jpg",

						 };
			ReportDocument rptH = new ReportDocument();

			string FileName = Server.MapPath("/Reports/ListeFactureClientParAnFRS.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Factures Clients Par An/Client";
			rptH.SetDataSource(dt);
			rptH.SetParameterValue("URLimage", FileName2);
			Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			return File(stream, "application/pdf");
		}
		public ActionResult PrintAllFactureClientParMoisFRS()
		{
			List<FACTURES_CLIENTS> Liste = db.FACTURES_CLIENTS.ToList();
			if (Session["Soclogo"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			string soc = (string)Session["Soclogo"];
			string FileName2 = "";
			System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
			SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault();
			string logo = societeLogo.logo;
			FileName2 = Server.MapPath("/Images/" + logo);

			dynamic dt = from cmd in Liste
						 select new
						 {
							 CODE = cmd.CODE,
							 FOURNISSEUR = cmd.CLIENTS.NOM,
							 Societes = cmd.DATE.ToShortDateString(),
							 //Expr3 = db.Tiers.Where(fd => fd.SociID == cmd.Societes).FirstOrDefault().NOM,
							 DATE = cmd.DATE.ToString("MM/yyyy"),
							 //Expr4 = cmd.FOURNISSEURS.NOM,
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
			string FileName = Server.MapPath("/Reports/ListeFactureClientParMoisFRS.rpt");
			rptH.Load(FileName);
			rptH.SummaryInfo.ReportTitle = "Factures Clients Par Mois/Client";
			rptH.SetDataSource(dt);

			rptH.SetParameterValue("URLimage", FileName2);
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
		public JsonResult UpdatePriceACCESSOIREs(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			if (LignesCuisine == null || LignesCuisine == "")
			{
				db.Configuration.ProxyCreationEnabled = false;
				ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			}
			else
			{
				db.Configuration.ProxyCreationEnabled = false;
				int idligne = int.Parse(LignesCuisine);
				ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
				ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			}
			//if (LignesCuisine!="" && LignesCuisine!=null && (Session["LignesACCESSOIRE"] !=null))
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRE"];
			//}

			//if (Session["LignesACC"] != null && LignesCuisine == "" && LignesCuisine == null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceACCESSOIREsCMD(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (LignesCuisine != "" && LignesCuisine != null && (Session["LignesACCESSOIRECMD"] != null))
			//{
			//List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			//if (LignesCuisine == null || LignesCuisine == "")
			//{
			//    db.Configuration.ProxyCreationEnabled = false;
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			//else
			//{
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//}
			//ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
			//}

			//if (Session["LignesACC"] != null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult UpdatePriceACCESSOIREsBL(string LignesCuisine)
		{

			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			//if (LignesCuisine != "" && LignesCuisine != null && (Session["LignesACCESSOIRECMD"] != null))
			//{
			ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			//}

			//if (Session["LignesACC"] != null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceACCESSOIREsFacture(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();

			//if (LignesCuisine != "" && LignesCuisine != null && (Session["LignesACCESSOIRECMD"] != null))
			//{
			//ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//if (Session["LignesACC"] != null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult UpdatePriceACCESSOIREsCaisse(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();

			//if (LignesCuisine != "" && LignesCuisine != null && (Session["LignesACCESSOIRECMD"] != null))
			//{
			ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//if (Session["LignesACC"] != null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceACCESSOIREsAvoir(string LignesCuisine)
		{
			List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesPoduits2 = new List<LignesACCESSOIRE>();

			//if (LignesCuisine != "" && LignesCuisine != null && (Session["LignesACCESSOIRECMD"] != null))
			//{
			ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREAvoir"];
			//}
			db.Configuration.ProxyCreationEnabled = false;
			int idligne = int.Parse(LignesCuisine);
			ListeDesPoduits2 = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREAvoir"];
			ListeDesPoduits = ListeDesPoduits2.Where(f => f.IDLIGNESDEScription == idligne).ToList();
			//if (Session["LignesACC"] != null)
			//{
			//    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACC"];
			//}
			decimal totalHT = 0;
			decimal totalTC = 0;
			foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTC += ligne.TTC;
			}
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTC = totalTC
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceDevis(string remise, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			//int Id = int.Parse(Code);

			if (Session["ProduitsDevisClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
				//ListeDesPoduits = ListeDesPoduits2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServices> ListeDesServicess = new List<LignesServices>();
			List<LignesServices> ListeDesServicess2 = new List<LignesServices>();

			if (Session["LignesServ"] != null)
			{
				ListeDesServicess = (List<LignesServices>)Session["LignesServ"];
				//ListeDesServicess = ListeDesServicess2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance2 = new List<LignesServicesSSTraitance>();

			if (Session["LignesServSST"] != null)
			{
				ListeDesServicessSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
				//ListeDesServicessSSTraitance = ListeDesServicessSSTraitance2.Where(f => f.Code == Id).ToList();

			}
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesCUISINEDevisClient2 = new List<LignesCuisine>();

			if (Session["CUISINEDevisClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINEDevisClient"];
				//ListeDesCUISINEDevisClient = ListeDesCUISINEDevisClient2.Where(f => f.Code == Id).ToList();

			}
			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTSERV = 0;
			decimal totalTVASERV = 0;
			decimal totalHTSERVSST = 0;
			decimal totalTVASERVSST = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			decimal TOTPTHTACHAT = 0;
			decimal TOTPTHTVENTE = 0;
			decimal TOTPTHTACHATSST = 0;
			decimal TOTPTHTVENTESST = 0;
			decimal MargeDevis = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHAT += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTE += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesServices ligne in ListeDesServicess)
			{
				totalHTSERV += ligne.PTHT;
				totalTVASERV += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesServicesSSTraitance ligne in ListeDesServicessSSTraitance)
			{
				totalHTSERVSST += ligne.PTHT;
				totalTVASERVSST += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHATSST += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTESST += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;
				MargeDevis += (ligne.PTHTAVECMARGE - (ligne.PTHTSSMARGE + ligne.ACC));
			}
			decimal IntRemise = decimal.Parse(remise, CultureInfo.InvariantCulture);
			MargeDevis += TOTPTHTVENTE - TOTPTHTACHAT;
			MargeDevis += TOTPTHTVENTESST - TOTPTHTACHATSST;
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTSERV = totalHTSERV,
				totalTVASERV = totalTVASERV,
				totalHTSERVSST = totalHTSERVSST,
				totalTVASERVSST = totalTVASERVSST,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,
				MargeDevis = MargeDevis
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceCommande(string remise, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			//int Id = int.Parse(Code);

			if (Session["ProduitsCommandeClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
				//ListeDesPoduits = ListeDesPoduits2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServices> ListeDesServicess = new List<LignesServices>();
			List<LignesServices> ListeDesServicess2 = new List<LignesServices>();

			if (Session["LignesServ"] != null)
			{
				ListeDesServicess = (List<LignesServices>)Session["LignesServ"];
				//ListeDesServicess = ListeDesServicess2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance2 = new List<LignesServicesSSTraitance>();

			if (Session["LignesServSST"] != null)
			{
				ListeDesServicessSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
				//ListeDesServicessSSTraitance = ListeDesServicessSSTraitance2.Where(f => f.Code == Id).ToList();

			}
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesCUISINEDevisClient2 = new List<LignesCuisine>();

			if (Session["CUISINECommandeClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINECommandeClient"];
				//ListeDesCUISINEDevisClient = ListeDesCUISINEDevisClient2.Where(f => f.Code == Id).ToList();

			}
			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTSERV = 0;
			decimal totalTVASERV = 0;
			decimal totalHTSERVSST = 0;
			decimal totalTVASERVSST = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			decimal TOTPTHTACHAT = 0;
			decimal TOTPTHTVENTE = 0;
			decimal TOTPTHTACHATSST = 0;
			decimal TOTPTHTVENTESST = 0;
			decimal MargeDevis = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHAT += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTE += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesServices ligne in ListeDesServicess)
			{
				totalHTSERV += ligne.PTHT;
				totalTVASERV += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesServicesSSTraitance ligne in ListeDesServicessSSTraitance)
			{
				totalHTSERVSST += ligne.PTHT;
				totalTVASERVSST += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHATSST += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTESST += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;
				MargeDevis += (ligne.PTHTAVECMARGE - (ligne.PTHTSSMARGE + ligne.ACC));
			}
			decimal IntRemise = decimal.Parse(remise, CultureInfo.InvariantCulture);
			MargeDevis += TOTPTHTVENTE - TOTPTHTACHAT;
			MargeDevis += TOTPTHTVENTESST - TOTPTHTACHATSST;
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTSERV = totalHTSERV,
				totalTVASERV = totalTVASERV,
				totalHTSERVSST = totalHTSERVSST,
				totalTVASERVSST = totalTVASERVSST,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,
				MargeDevis = MargeDevis
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
			List<LignesServices> ListeDesServicess = new List<LignesServices>();
			if (Session["LignesServ"] != null)
			{
				ListeDesServicess = (List<LignesServices>)Session["LignesServ"];
			}
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance = new List<LignesServicesSSTraitance>();
			if (Session["LignesServSST"] != null)
			{
				ListeDesServicessSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
			}
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			if (Session["CUISINEBLClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINEBLClient"];
			}
			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTSERV = 0;
			decimal totalTVASERV = 0;
			decimal totalHTSERVSST = 0;
			decimal totalTVASERVSST = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesServices ligne in ListeDesServicess)
			{
				totalHTSERV += ligne.PTHT;
				totalTVASERV += (ligne.PTHT * ligne.TVA) / 100;
			}

			foreach (LignesServicesSSTraitance ligne in ListeDesServicessSSTraitance)
			{
				totalHTSERVSST += ligne.PTHT;
				totalTVASERVSST += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;

			}
			int IntRemise = int.Parse(remise);
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTSERV = totalHTSERV,
				totalTVASERV = totalTVASERV,
				totalHTSERVSST = totalHTSERVSST,
				totalTVASERVSST = totalTVASERVSST,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,

			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceFacture(string remise, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			//int Id = int.Parse(Code);

			if (Session["ProduitsFactureClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
				//ListeDesPoduits = ListeDesPoduits2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServices> ListeDesServicess = new List<LignesServices>();
			List<LignesServices> ListeDesServicess2 = new List<LignesServices>();

			if (Session["LignesServFact"] != null)
			{
				ListeDesServicess = (List<LignesServices>)Session["LignesServFact"];
				//ListeDesServicess = ListeDesServicess2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance2 = new List<LignesServicesSSTraitance>();

			if (Session["LignesServSST"] != null)
			{
				ListeDesServicessSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
				//ListeDesServicessSSTraitance = ListeDesServicessSSTraitance2.Where(f => f.Code == Id).ToList();

			}
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesCUISINEDevisClient2 = new List<LignesCuisine>();

			if (Session["CUISINEFACTUREClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];
				//ListeDesCUISINEDevisClient = ListeDesCUISINEDevisClient2.Where(f => f.Code == Id).ToList();

			}
			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTSERV = 0;
			decimal totalTVASERV = 0;
			decimal totalHTSERVSST = 0;
			decimal totalTVASERVSST = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			decimal TOTPTHTACHAT = 0;
			decimal TOTPTHTVENTE = 0;
			decimal TOTPTHTACHATSST = 0;
			decimal TOTPTHTVENTESST = 0;
			decimal MargeDevis = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHAT += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTE += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesServices ligne in ListeDesServicess)
			{
				totalHTSERV += ligne.PTHT;
				totalTVASERV += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesServicesSSTraitance ligne in ListeDesServicessSSTraitance)
			{
				totalHTSERVSST += ligne.PTHT;
				totalTVASERVSST += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHATSST += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTESST += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;
				MargeDevis += (ligne.PTHTAVECMARGE - (ligne.PTHTSSMARGE + ligne.ACC));
			}
			decimal IntRemise = decimal.Parse(remise, CultureInfo.InvariantCulture);
			MargeDevis += TOTPTHTVENTE - TOTPTHTACHAT;
			MargeDevis += TOTPTHTVENTESST - TOTPTHTACHATSST;
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTSERV = totalHTSERV,
				totalTVASERV = totalTVASERV,
				totalHTSERVSST = totalHTSERVSST,
				totalTVASERVSST = totalTVASERVSST,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,
				MargeDevis = MargeDevis
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult UpdatePriceCaisse(string remise, string Code)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			//int Id = int.Parse(Code);

			if (Session["ProduitsCaisseClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCaisseClient"];
				//ListeDesPoduits = ListeDesPoduits2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServices> ListeDesServicess = new List<LignesServices>();
			List<LignesServices> ListeDesServicess2 = new List<LignesServices>();

			if (Session["LignesServ"] != null)
			{
				ListeDesServicess = (List<LignesServices>)Session["LignesServ"];
				//ListeDesServicess = ListeDesServicess2.Where(f => f.Code == Id).ToList();
			}
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesServicessSSTraitance2 = new List<LignesServicesSSTraitance>();

			if (Session["LignesServSST"] != null)
			{
				ListeDesServicessSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
				//ListeDesServicessSSTraitance = ListeDesServicessSSTraitance2.Where(f => f.Code == Id).ToList();

			}
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesCUISINEDevisClient2 = new List<LignesCuisine>();

			if (Session["CUISINECAISSEClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINECAISSEClient"];
				//ListeDesCUISINEDevisClient = ListeDesCUISINEDevisClient2.Where(f => f.Code == Id).ToList();

			}
			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTSERV = 0;
			decimal totalTVASERV = 0;
			decimal totalHTSERVSST = 0;
			decimal totalTVASERVSST = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			decimal TOTPTHTACHAT = 0;
			decimal TOTPTHTVENTE = 0;
			decimal TOTPTHTACHATSST = 0;
			decimal TOTPTHTVENTESST = 0;
			decimal MargeDevis = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHAT += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTE += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesServices ligne in ListeDesServicess)
			{
				totalHTSERV += ligne.PTHT;
				totalTVASERV += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesServicesSSTraitance ligne in ListeDesServicessSSTraitance)
			{
				totalHTSERVSST += ligne.PTHT;
				totalTVASERVSST += (ligne.PTHT * ligne.TVA) / 100;
				TOTPTHTACHATSST += ligne.PRIX_VENTE_HT * ligne.QUANTITE;
				TOTPTHTVENTESST += ligne.PRIX_VENTE_HT2 * ligne.QUANTITE;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;
				MargeDevis += (ligne.PTHTAVECMARGE - (ligne.PTHTSSMARGE + ligne.ACC));
			}
			decimal IntRemise = decimal.Parse(remise, CultureInfo.InvariantCulture);
			MargeDevis += TOTPTHTVENTE - TOTPTHTACHAT;
			MargeDevis += TOTPTHTVENTESST - TOTPTHTACHATSST;
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTSERV = totalHTSERV,
				totalTVASERV = totalTVASERV,
				totalHTSERVSST = totalHTSERVSST,
				totalTVASERVSST = totalTVASERVSST,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,
				MargeDevis = MargeDevis
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
			List<LignesCuisine> ListeDesCUISINEDevisClient = new List<LignesCuisine>();
			if (Session["CUISINEAvoirClient"] != null)
			{
				ListeDesCUISINEDevisClient = (List<LignesCuisine>)Session["CUISINEAvoirClient"];
			}

			decimal totalHT = 0;
			decimal totalTVA = 0;
			decimal totalHTCuisine = 0;
			decimal totalTVACuisine = 0;
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				totalHT += ligne.PTHT;
				totalTVA += (ligne.PTHT * ligne.TVA) / 100;
			}
			foreach (LignesCuisine ligne in ListeDesCUISINEDevisClient)
			{
				totalHTCuisine += ligne.PTHTAVECMARGE;
				totalTVACuisine += (ligne.PTHTAVECMARGE * ligne.TVACUISINE) / 100;

			}
			int IntRemise = int.Parse(remise);
			dynamic Result = new
			{
				totalHT = totalHT,
				totalTVA = totalTVA,
				totalHTCuisine = totalHTCuisine,
				totalTVACuisine = totalTVACuisine,
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public string ValiderDecTVA(string RETENU, string mois, string REPORTANT)
		{
			List<RASMache> ListeDesPoduits = (List<RASMache>)Session["RASMarche"];
			List<RASSalaire1> list2 = (List<RASSalaire1>)Session["RASSalaire"];
			List<RASHoraire> list3 = (List<RASHoraire>)Session["RASHoraire"];
			List<RASLoyer1> list4 = (List<RASLoyer1>)Session["RASLoyer"];
			List<TFP1> list5 = (List<TFP1>)Session["RASTFP"];
			List<FOPROLOS1> list6 = (List<FOPROLOS1>)Session["FOPROLOS"];

			List<FACTURES_FOURNISSEURS> listfac = db.FACTURES_FOURNISSEURS.ToList();
			foreach (RASMache ras in ListeDesPoduits)
			{
				int id = int.Parse(ras.ID);
				foreach (FACTURES_FOURNISSEURS fac in listfac)
				{
					if (id == fac.ID)
					{
						fac.RAS_Marche = true;
						fac.MOIS_RAS_Marche = mois;
						db.SaveChanges();

					}
				}
			}
			DeclarationTVA dec = db.DeclarationTVA.Where(f => f.date_dec == mois).FirstOrDefault();
			if (dec == null)
			{
				DeclarationTVA dectva = new DeclarationTVA();
				dectva.date_dec = mois;
				dectva.ID = db.DeclarationTVA.Max(p => p.ID) + 1;
				dectva.Retenue = decimal.Parse(RETENU, CultureInfo.InvariantCulture);
				dectva.ReportTVA = decimal.Parse(REPORTANT, CultureInfo.InvariantCulture);
				db.DeclarationTVA.Add(dectva);
				db.SaveChanges();
			}
			else
			{
				dec.Retenue = decimal.Parse(RETENU, CultureInfo.InvariantCulture);
				dec.ReportTVA = decimal.Parse(REPORTANT, CultureInfo.InvariantCulture);

				db.SaveChanges();
			}
			if (list2 != null)
			{
				foreach (RASSalaire1 ras in list2)
				{
					if (ras.ID == 0)
					{
						RASSalaire rassal = new RASSalaire();
						rassal.salaire = ras.salaire;
						rassal.Date = ras.Date;
						db.RASSalaire.Add(rassal);
						db.SaveChanges();
					}
					else
					{
						RASSalaire rassal = db.RASSalaire.Where(f => f.id == ras.ID).FirstOrDefault();
						if (rassal != null)
						{
							rassal.salaire = ras.salaire;
							rassal.Date = ras.Date;
							db.SaveChanges();
						}
						else
						{
							RASSalaire rassal1 = new RASSalaire();
							rassal1.salaire = ras.salaire;
							rassal1.Date = ras.Date;
							db.RASSalaire.Add(rassal1);
							db.SaveChanges();

						}
					}
				}
			}
			if (list3 != null)
			{
				foreach (RASHoraire ras in list3)
				{
					if (ras.ID == 0)
					{
						RASHonoraire rassal = new RASHonoraire();
						rassal.honoraire = ras.honoraire;
						rassal.honoraire1 = ras.honoraire1;
						rassal.date = ras.Date;
						db.RASHonoraire.Add(rassal);
						db.SaveChanges();
					}
					else
					{
						RASHonoraire rassal = db.RASHonoraire.Where(f => f.id == ras.ID).FirstOrDefault();
						if (rassal != null)
						{
							rassal.honoraire = ras.honoraire;
							rassal.honoraire1 = ras.honoraire1;
							rassal.date = ras.Date;
							db.SaveChanges();

						}
						else
						{
							RASHonoraire rassal1 = new RASHonoraire();
							rassal1.honoraire = ras.honoraire;
							rassal1.honoraire1 = ras.honoraire1;
							rassal1.date = ras.Date;
							db.RASHonoraire.Add(rassal1);
							db.SaveChanges();

						}
					}
				}
			}
			if (list4 != null)
			{
				foreach (RASLoyer1 ras in list4)
				{
					if (ras.ID == 0)
					{
						RASLoyer rassal = new RASLoyer();
						rassal.loy = ras.loy;
						rassal.loy1 = ras.loy1;
						rassal.loy2 = ras.loy2;

						rassal.date = ras.Date;
						db.RASLoyer.Add(rassal);
						db.SaveChanges();
					}
					else
					{
						RASLoyer rassal = db.RASLoyer.Where(f => f.id == ras.ID).FirstOrDefault();

						if (rassal != null)
						{
							rassal.loy = ras.loy;
							rassal.loy1 = ras.loy1;
							rassal.loy2 = ras.loy2;

							rassal.date = ras.Date;
							db.SaveChanges();
						}
						else
						{
							RASLoyer rassal1 = new RASLoyer();
							rassal1.loy = ras.loy;
							rassal1.loy1 = ras.loy1;
							rassal1.loy2 = ras.loy2;
							rassal1.date = ras.Date;
							db.RASLoyer.Add(rassal1);
							db.SaveChanges();
						}
					}
				}
			}
			if (list5 != null)
			{
				foreach (TFP1 ras in list5)
				{
					if (ras.ID == 0)
					{
						TFP rassal = new TFP();
						rassal.TFP1 = ras.FTP11;
						rassal.TFP11 = ras.FTP22;

						rassal.Date = ras.Date;
						db.TFP.Add(rassal);
						db.SaveChanges();
					}
					else
					{
						TFP rassal = db.TFP.Where(f => f.id == ras.ID).FirstOrDefault();
						if (rassal != null)
						{
							rassal.TFP1 = ras.FTP11;
							rassal.TFP11 = ras.FTP22;

							rassal.Date = ras.Date;
							db.SaveChanges();
						}
						else
						{
							TFP rassal1 = new TFP();
							rassal1.TFP1 = ras.FTP11;
							rassal1.TFP11 = ras.FTP22;

							rassal1.Date = ras.Date;
							db.TFP.Add(rassal1);
							db.SaveChanges();
						}
					}
				}
			}
			if (list6 != null)
			{
				foreach (FOPROLOS1 ras in list6)
				{
					if (ras.ID == 0)
					{
						FOPROLOS rassal = new FOPROLOS();
						rassal.FOPROLOS1 = ras.FOPROLOS11;
						rassal.FOPROLOS11 = ras.FOPROLOS22;
						rassal.Date = ras.Date;
						db.FOPROLOS.Add(rassal);
						db.SaveChanges();
					}
					else
					{
						FOPROLOS rassal = db.FOPROLOS.Where(f => f.id == ras.ID).FirstOrDefault();
						if (rassal != null)
						{
							rassal.FOPROLOS1 = ras.FOPROLOS11;
							rassal.FOPROLOS11 = ras.FOPROLOS22;
							rassal.Date = ras.Date;
							db.SaveChanges();
						}
						else
						{
							FOPROLOS rassal1 = new FOPROLOS();
							rassal1.FOPROLOS1 = ras.FOPROLOS11;
							rassal1.FOPROLOS11 = ras.FOPROLOS22;
							rassal1.Date = ras.Date;
							db.FOPROLOS.Add(rassal1);
							db.SaveChanges();
						}
					}
				}
			}
			Session["RASMarche"] = null;
			Session["RASSalaire"] = null;
			Session["RASHoraire"] = null;
			Session["RASLoyer"] = null;
			Session["RASTFP"] = null;
			Session["FOPROLOS"] = null;

			return string.Empty;

		}
		public JsonResult GetDeclarationTva(string id)
		{
			DeclarationTVA declarationtva = db.DeclarationTVA.Where(f => f.date_dec == id).FirstOrDefault();
			return Json(declarationtva, JsonRequestBehavior.AllowGet);

		}
		public JsonResult getSession()
		{
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);
		}
		public JsonResult addSalaire(string salaire, string mois, string parampassed)
		{
			List<RASSalaire1> lissalaire = new List<RASSalaire1>();
			if (Session["RASSalaire"] != null)
			{
				lissalaire = (List<RASSalaire1>)Session["RASSalaire"];
			}
			RASSalaire1 rassalaire = new RASSalaire1();

			RASSalaire1 rassalaire2 = lissalaire.Where(f => f.Date == mois && f.idinput == parampassed).FirstOrDefault();
			if (rassalaire2 != null)
			{
				rassalaire2.salaire = decimal.Parse(salaire, CultureInfo.InvariantCulture);

			}
			else
			{
				rassalaire.Date = mois;
				rassalaire.salaire = decimal.Parse(salaire, CultureInfo.InvariantCulture);
				rassalaire.idinput = parampassed;
				if (lissalaire.Count == 0)
				{
					if ((db.RASSalaire.Count() != 0))
					{
						rassalaire.ID = db.RASSalaire.Max(f => f.id) + 1;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
				else

				{
					if ((db.RASSalaire.Count() != 0))
					{
						rassalaire.ID = db.RASSalaire.Max(f => f.id) + lissalaire.Count;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
			}
			lissalaire.Add(rassalaire);
			Session["RASSalaire"] = lissalaire;
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);
		}
		public JsonResult addTFP(string TFP, string TFP1, string mois)
		{
			List<TFP1> lissalaire = new List<TFP1>();
			if (Session["RASTFP"] != null)
			{
				lissalaire = (List<TFP1>)Session["RASTFP"];
			}
			TFP1 rassalaire = new TFP1();
			TFP1 rassalaire2 = lissalaire.Where(f => f.Date == mois).FirstOrDefault();
			if (rassalaire2 != null)
			{
				rassalaire.FTP11 = decimal.Parse(TFP, CultureInfo.InvariantCulture);
				rassalaire.FTP22 = decimal.Parse(TFP1, CultureInfo.InvariantCulture);
			}
			else
			{
				rassalaire.Date = mois;
				rassalaire.FTP11 = decimal.Parse(TFP, CultureInfo.InvariantCulture);
				rassalaire.FTP22 = decimal.Parse(TFP1, CultureInfo.InvariantCulture);

				if (lissalaire.Count == 0)
				{
					if ((db.TFP.Count() != 0))
					{
						rassalaire.ID = db.TFP.Max(f => f.id) + 1;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
				else

				{
					if ((db.TFP.Count() != 0))
					{
						rassalaire.ID = db.TFP.Max(f => f.id) + lissalaire.Count;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
			}
			lissalaire.Add(rassalaire);
			Session["RASTFP"] = lissalaire;
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);

		}
		public JsonResult addFOPROLOS(string FOPROLOS, string FOPROLOS1, string mois)
		{
			List<FOPROLOS1> lissalaire = new List<FOPROLOS1>();
			if (Session["FOPROLOS"] != null)
			{
				lissalaire = (List<FOPROLOS1>)Session["FOPROLOS"];
			}
			FOPROLOS1 rassalaire = new FOPROLOS1();
			FOPROLOS1 rassalaire2 = lissalaire.Where(f => f.Date == mois).FirstOrDefault();
			if (rassalaire2 != null)
			{
				rassalaire.FOPROLOS11 = decimal.Parse(FOPROLOS, CultureInfo.InvariantCulture);
				rassalaire.FOPROLOS22 = decimal.Parse(FOPROLOS1, CultureInfo.InvariantCulture);
			}
			else
			{

				rassalaire.Date = mois;
				rassalaire.FOPROLOS11 = decimal.Parse(FOPROLOS, CultureInfo.InvariantCulture);
				rassalaire.FOPROLOS22 = decimal.Parse(FOPROLOS1, CultureInfo.InvariantCulture);

				if (lissalaire.Count == 0)
				{
					if ((db.FOPROLOS.Count() != 0))
					{
						rassalaire.ID = db.FOPROLOS.Max(f => f.id) + 1;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
				else

				{
					if ((db.FOPROLOS.Count() != 0))
					{
						rassalaire.ID = db.FOPROLOS.Max(f => f.id) + lissalaire.Count;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}

			}
			lissalaire.Add(rassalaire);
			Session["FOPROLOS"] = lissalaire;
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);
		}
		public JsonResult addHoraire(string hor, string hor1, string mois, string parampassed, string param1)
		{
			List<RASHoraire> lissalaire = new List<RASHoraire>();
			if (Session["RASHoraire"] != null)
			{
				lissalaire = (List<RASHoraire>)Session["RASHoraire"];
			}
			RASHoraire rassalaire = new RASHoraire();
			RASHoraire rassalaire2 = lissalaire.Where(f => f.Date == mois && f.idhor == parampassed).FirstOrDefault();
			if (rassalaire2 != null)
			{
				rassalaire.honoraire = decimal.Parse(hor, CultureInfo.InvariantCulture);
				rassalaire.honoraire1 = decimal.Parse(hor1, CultureInfo.InvariantCulture);
			}
			else
			{

				rassalaire.Date = mois;
				rassalaire.honoraire = decimal.Parse(hor, CultureInfo.InvariantCulture);
				rassalaire.honoraire1 = decimal.Parse(hor1, CultureInfo.InvariantCulture);
				rassalaire.idhor = parampassed;
				rassalaire.idhor1 = param1;
				if (lissalaire.Count == 0)
				{
					if ((db.RASHonoraire.Count() != 0))
					{
						rassalaire.ID = db.RASHonoraire.Max(f => f.id) + 1;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
				else

				{
					if ((db.RASHonoraire.Count() != 0))
					{
						rassalaire.ID = db.RASHonoraire.Max(f => f.id) + lissalaire.Count;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
			}
			lissalaire.Add(rassalaire);
			Session["RASHoraire"] = lissalaire;
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);
		}
		public JsonResult addloyer(string loy, string loy1, string loy2, string mois, string parampassed, string param1, string param2)
		{
			List<RASLoyer1> lissalaire = new List<RASLoyer1>();
			if (Session["RASLoyer"] != null)
			{
				lissalaire = (List<RASLoyer1>)Session["RASLoyer"];
			}
			RASLoyer1 rassalaire = new RASLoyer1();
			RASLoyer1 rassalaire2 = lissalaire.Where(f => f.Date == mois && f.idloy == parampassed).FirstOrDefault();
			if (rassalaire2 != null)
			{
				rassalaire.loy = decimal.Parse(loy, CultureInfo.InvariantCulture);
				rassalaire.loy1 = decimal.Parse(loy1, CultureInfo.InvariantCulture);
				rassalaire.loy2 = decimal.Parse(loy2, CultureInfo.InvariantCulture);
			}
			else
			{

				rassalaire.Date = mois;
				rassalaire.loy = decimal.Parse(loy, CultureInfo.InvariantCulture);
				rassalaire.loy1 = decimal.Parse(loy1, CultureInfo.InvariantCulture);
				rassalaire.loy2 = decimal.Parse(loy2, CultureInfo.InvariantCulture);
				rassalaire.idloy = parampassed;
				rassalaire.idloy1 = param1;
				rassalaire.idloy2 = param2;

				if (lissalaire.Count == 0)
				{
					if ((db.RASLoyer.Count() != 0))
					{
						rassalaire.ID = db.RASSalaire.Max(f => f.id) + 1;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}
				else
				{
					if ((db.RASLoyer.Count() != 0))
					{
						rassalaire.ID = db.RASSalaire.Max(f => f.id) + lissalaire.Count;
					}
					else
					{
						rassalaire.ID = lissalaire.Count;
					}
				}

			}
			lissalaire.Add(rassalaire);
			Session["RASLoyer"] = lissalaire;
			string session = "";
			if ((Session["RASSalaire"] != null) && (Session["RASHoraire"] != null) && (Session["RASLoyer"] != null) && (Session["RASTFP"] != null) && (Session["FOPROLOS"] != null))
			{
				session = "true";
			}
			else
			{
				session = "false";
			}
			return Json(session, JsonRequestBehavior.AllowGet);
		}

		public string AddLineSERVICE(string ID_Produit, string LIB_Produi, string Description_Produit, string unite, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, List<string> RESSOURCE, List<string> RESSOURCE2)
		{
			LignesServices ligne = new LignesServices();
			ligne.ID = int.Parse(ID_Produit);
			ligne.REFSERVICE = LIB_Produi;
			ligne.DescriptionSERVICE = Description_Produit;
			ligne.UNITE = unite;
			ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(TVA_Produit);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

			foreach (string rss in RESSOURCE)
			{

				ligne.RESSOURCE.Add((int.Parse(rss)));
			}
			foreach (string rss in RESSOURCE2)
			{

				ligne.RESSOURCE2.Add(rss);
			}
			List<LignesServices> ListeDesPoduits = new List<LignesServices>();
			if (Session["LignesServ"] != null)
			{
				ListeDesPoduits = (List<LignesServices>)Session["LignesServ"];
			}

			ListeDesPoduits.Add(ligne);
			Session["LignesServ"] = ListeDesPoduits;
			return string.Empty;
		}
		public string AddLineSERVICEFacture(string ID_Produit, string LIB_Produi, string Description_Produit, string unite, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, List<string> RESSOURCE, List<string> RESSOURCE2)
		{
			LignesServices ligne = new LignesServices();
			ligne.ID = int.Parse(ID_Produit);
			ligne.REFSERVICE = LIB_Produi;
			ligne.DescriptionSERVICE = Description_Produit;
			ligne.UNITE = unite;
			ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(TVA_Produit);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

			foreach (string rss in RESSOURCE)
			{

				ligne.RESSOURCE.Add((int.Parse(rss)));
			}
			foreach (string rss in RESSOURCE2)
			{

				ligne.RESSOURCE2.Add(rss);
			}
			List<LignesServices> ListeDesPoduits = new List<LignesServices>();
			if (Session["LignesServFact"] != null)
			{
				ListeDesPoduits = (List<LignesServices>)Session["LignesServFact"];
			}

			ListeDesPoduits.Add(ligne);
			Session["LignesServFact"] = ListeDesPoduits;
			return string.Empty;
		}

		public string AddLineSERVICESOUSTRAITANCE(string ID_Produit, string LIB_Produi, string Description_Produit, string unite, string Quantite_Produit, string PUHT_Produit, string margeSOUSTRAITANCE, string PUHTSOUSTRAITANCE2, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, List<string> SOUS_TRAITANCE, List<string> SOUS_TRAITANCE2)
		{
			LignesServicesSSTraitance ligne = new LignesServicesSSTraitance();
			ligne.ID = int.Parse(ID_Produit);
			ligne.REFSERVICE = LIB_Produi;
			ligne.DescriptionSERVICE = Description_Produit;
			ligne.UNITE = unite;
			ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.Marge = decimal.Parse(margeSOUSTRAITANCE, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHTSOUSTRAITANCE2, CultureInfo.InvariantCulture);
			ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(TVA_Produit);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

			foreach (string rss in SOUS_TRAITANCE)
			{

				ligne.SOUS_TRAITANCE.Add((int.Parse(rss)));
			}
			foreach (string rss in SOUS_TRAITANCE2)
			{

				ligne.SOUS_TRAITANCE2.Add(rss);
			}
			List<LignesServicesSSTraitance> ListeDesPoduits = new List<LignesServicesSSTraitance>();
			if (Session["LignesServSST"] != null)
			{
				ListeDesPoduits = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
			}

			ListeDesPoduits.Add(ligne);
			Session["LignesServSST"] = ListeDesPoduits;
			return string.Empty;
		}

		public string AddLineSERVICESOUSTRAITANCEFact(string ID_Produit, string LIB_Produi, string Description_Produit, string unite, string Quantite_Produit, string PUHT_Produit, string margeSOUSTRAITANCE, string PUHTSOUSTRAITANCE2, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, List<string> SOUS_TRAITANCE, List<string> SOUS_TRAITANCE2)
		{
			LignesServicesSSTraitance ligne = new LignesServicesSSTraitance();
			ligne.ID = int.Parse(ID_Produit);
			ligne.REFSERVICE = LIB_Produi;
			ligne.DescriptionSERVICE = Description_Produit;
			ligne.UNITE = unite;
			ligne.QUANTITE = decimal.Parse(Quantite_Produit, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.Marge = decimal.Parse(margeSOUSTRAITANCE, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHTSOUSTRAITANCE2, CultureInfo.InvariantCulture);
			ligne.REMISE = decimal.Parse(Remise_Produit, CultureInfo.InvariantCulture);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(TVA_Produit);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

			foreach (string rss in SOUS_TRAITANCE)
			{
				ligne.SOUS_TRAITANCE.Add((int.Parse(rss)));
			}
			foreach (string rss in SOUS_TRAITANCE2)
			{
				ligne.SOUS_TRAITANCE2.Add(rss);
			}
			List<LignesServicesSSTraitance> ListeDesPoduits = new List<LignesServicesSSTraitance>();
			if (Session["LignesServSSTFact"] != null)
			{
				ListeDesPoduits = (List<LignesServicesSSTraitance>)Session["LignesServSSTFact"];
			}

			ListeDesPoduits.Add(ligne);
			Session["LignesServSSTFact"] = ListeDesPoduits;
			return string.Empty;
		}
		public string AddLineDevisCuisine(string QuantiteCAISSON, string CAISSON, string LIB_CAISSON, string SSCAISSON, string LIB_SSCAISSON, string IDTYPCAISSON, string TYPCAISSON, string IDTYPFACADE, string TYPFACADE, string CREVCAISSON, string MARGEPRIXACHAT, string PRIXACHAT, string ACC, string POURCENTAGE, string PRIXVENTECAISSON, string FACADE, string LIB_FACADE, string SOUSFACADE, string LIB_SOUSFACADE, string QuantiteFACADE, string PRIXFACADE, string PTHTFACADE, string PTHTSSMARGE, string PTHTAVECMARGE, string TVACUISINE, string PTTCCUISINE)
		{
			LignesCuisine ligne = new LignesCuisine();
			int count = 0;
			Session["CAISSON"] = null;
			Session["SSCAISSON"] = null;
			Session["IDTYPCAISSON"] = null;
			Session["QuantiteCAISSON"] = null;
			Session["IdAffaireCommercial"] = null;
			Session["LIB_SSCAISSON"] = null;
			Session["TYPCAISSON"] = null;
			ligne.QuantiteCAISSON = decimal.Parse(QuantiteCAISSON, CultureInfo.InvariantCulture);
			ligne.CAISSON = int.Parse(CAISSON);
			ligne.LIB_CAISSON = LIB_CAISSON;
			ligne.SSCAISSON = int.Parse(SSCAISSON);
			ligne.LIB_SSCAISSON = LIB_SSCAISSON;
			ligne.IDTYPCAISSON = int.Parse(IDTYPCAISSON);
			ligne.TYPCAISSON = TYPCAISSON;
			ligne.TYPFACADE = TYPFACADE;
			ligne.CREVCAISSON = decimal.Parse(CREVCAISSON, CultureInfo.InvariantCulture);
			ligne.MARGEPRIXACHAT = decimal.Parse(MARGEPRIXACHAT, CultureInfo.InvariantCulture);
			ligne.PRIXACHAT = decimal.Parse(PRIXACHAT, CultureInfo.InvariantCulture);
			ligne.ACC = decimal.Parse(ACC, CultureInfo.InvariantCulture);
			ligne.POURCENTAGE = decimal.Parse(POURCENTAGE, CultureInfo.InvariantCulture);
			ligne.PRIXVENTECAISSON = decimal.Parse(PRIXVENTECAISSON, CultureInfo.InvariantCulture);
			//if (string.IsNullOrEmpty(FACADE)) FACADE = null;
			//if (string.IsNullOrEmpty(SOUSFACADE)) SOUSFACADE = null;
			//if (string.IsNullOrEmpty(IDTYPFACADE)) IDTYPFACADE = null;
			if (IDTYPFACADE != "" && IDTYPFACADE != null)
			{
				ligne.IDTYPFACADE = int.Parse(IDTYPFACADE);
			}
			if (FACADE != "" && FACADE != null)
			{
				ligne.FACADE = int.Parse(FACADE);
			}
			if (SOUSFACADE != "" && SOUSFACADE != null)
			{
				ligne.SOUSFACADE = int.Parse(SOUSFACADE);
			}

			ligne.LIB_FACADE = LIB_FACADE;

			ligne.LIB_SOUSFACADE = LIB_SOUSFACADE;
			ligne.QuantiteFACADE = decimal.Parse(QuantiteFACADE, CultureInfo.InvariantCulture);
			ligne.PRIXFACADE = decimal.Parse(PRIXFACADE, CultureInfo.InvariantCulture);
			ligne.PTHTFACADE = decimal.Parse(PTHTFACADE, CultureInfo.InvariantCulture);

			ligne.PTHTSSMARGE = decimal.Parse(PTHTSSMARGE, CultureInfo.InvariantCulture);
			ligne.PTHTAVECMARGE = decimal.Parse(PTHTAVECMARGE, CultureInfo.InvariantCulture);
			ligne.TVACUISINE = int.Parse(TVACUISINE);
			ligne.PTTCCUISINE = decimal.Parse(PTTCCUISINE, CultureInfo.InvariantCulture);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			if (Session["CUISINEDevisClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEDevisClient"];
			}
			count = ListeDesPoduits.Count() + 1;
			while (ListeDesPoduits.Select(cmd => cmd.ID).Contains(count))
			{
				count = count + 1;
			}
			ligne.ID = count;
			ListeDesPoduits.Add(ligne);
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesAccessoire2 = new List<LignesACCESSOIRE>();
			List<LignesACCESSOIRE> ListeDesAccessoire3 = new List<LignesACCESSOIRE>();

			if (Session["LignesACC"] != null)
			{
				ListeDesAccessoire3 = (List<LignesACCESSOIRE>)Session["LignesACC"];
			}
			if (Session["LignesACCessoire"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
			}
			int count1 = 0;
			foreach (LignesACCESSOIRE ligneAcc in ListeDesAccessoire)
			{
				LignesACCESSOIRE Acc = new LignesACCESSOIRE();
				count1 = ListeDesAccessoire2.Count() + 1;
				while (ListeDesAccessoire2.Select(cmd => cmd.ID).Contains(count1))
				{
					count1 = count1 + 1;
				}
				Acc.ID = count1;
				Acc.IDArticle = ligneAcc.IDArticle;
				Acc.Article = ligneAcc.Article;
				Acc.IDDESIGNATION = ligneAcc.IDDESIGNATION;
				Acc.DESIGNATION = ligneAcc.DESIGNATION;
				Acc.PUHT = ligneAcc.PUHT;
				Acc.PTHT = ligneAcc.PTHT;
				Acc.TVA = ligneAcc.TVA;
				Acc.TTC = ligneAcc.TTC;
				Acc.QTE = ligneAcc.QTE;
				Acc.IDLIGNESDEScription = ligneAcc.IDLIGNESDEScription;
				ListeDesAccessoire2.Add(Acc);
			}
			foreach (LignesACCESSOIRE ligneAcc in ListeDesAccessoire3)
			{
				LignesACCESSOIRE Acc = new LignesACCESSOIRE();
				count1 = ListeDesAccessoire2.Count() + 1;
				while (ListeDesAccessoire2.Select(cmd => cmd.ID).Contains(count1))
				{
					count1 = count1 + 1;
				}
				Acc.ID = count1;
				Acc.IDArticle = ligneAcc.IDArticle;
				Acc.Article = ligneAcc.Article;
				Acc.IDDESIGNATION = ligneAcc.IDDESIGNATION;
				Acc.DESIGNATION = ligneAcc.DESIGNATION;
				Acc.PUHT = ligneAcc.PUHT;
				Acc.PTHT = ligneAcc.PTHT;
				Acc.TVA = ligneAcc.TVA;
				Acc.TTC = ligneAcc.TTC;
				Acc.QTE = ligneAcc.QTE;
				Acc.IDLIGNESDEScription = ligne.ID;
				ListeDesAccessoire2.Add(Acc);
			}
			Session["LignesACCessoire"] = ListeDesAccessoire2;

			Session["CUISINEDevisClient"] = ListeDesPoduits;
			Session["LignesACC"] = null;
			return string.Empty;
		}

		public string AddLineDevis(string ID_Produit, string NumDevis, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string Quantite_Produit, string PUHT_Produit, string PUHT_Produit2, string Marge, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit, string des1, string sscat2, string index11, string index22)
		{
			Session["ProduitsDevisClient2"] = null;
			LigneProduit ligne = new LigneProduit();
			if (NumDevis != null)
			{
				ligne.NumDevis = NumDevis;
			}
			ligne.ID = int.Parse(ID_Produit);
			ligne.LIBELLE = LIB_Produi;
			//if (sscat2 != null)
			//{
			//    int code1 = int.Parse(sscat2);
			//    char a = (char)(code1);

			//    int index2 = int.Parse(index22);
			//    string sous_categorie1 = sous_categorie.Insert(index2-1,a.ToString());
			//    ligne.Sous_CATEGORIE = sous_categorie1;
			//}
			//else
			//{
			ligne.Sous_CATEGORIE = sous_categorie;

			//}
			//if (des1 != null)
			//{
			//    int code1 = int.Parse(des1);
			//    char a = (char)(code1);

			//    int index1 = int.Parse(index11);
			//    string des = Description_Produit.Insert(index1, a.ToString());
			//    ligne.DESIGNATION = des;
			//}
			//else
			//{
			ligne.DESIGNATION = Description_Produit;

			// }
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
			ligne.STOCK = int.Parse(StockProduit) - (int.Parse(Quantite_Liv));
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
			ligne.STOCK = int.Parse(StockProduit) + (int.Parse(Quantite_Produit));
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
		public string AddLineFacture(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string PUHT_Produit2, string Marge, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
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
			ligne.STOCK = decimal.Parse(StockProduit, CultureInfo.InvariantCulture);
			ligne.QUANTITE = int.Parse(Quantite_Produit);
			ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHT_Produit2, CultureInfo.InvariantCulture);
			ligne.MARGE = decimal.Parse(Marge, CultureInfo.InvariantCulture);
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

		//public string AddLineFacture(string ID_Produit, string LIB_Produi, string Description_Produit, string marque, string unite, string devise, string categorie, string sous_categorie, string StockProduit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
		//{
		//    LigneProduit ligne = new LigneProduit();
		//    ligne.ID = int.Parse(ID_Produit);
		//    ligne.LIBELLE = LIB_Produi;
		//    ligne.DESIGNATION = Description_Produit;
		//    ligne.MARQUE = marque;
		//    ligne.UNITE = unite;
		//    ligne.DEVISE = devise;
		//    ligne.CATEGORIE = categorie;
		//    ligne.Sous_CATEGORIE = sous_categorie;
		//    ligne.STOCK = int.Parse(StockProduit);
		//    ligne.QUANTITE = int.Parse(Quantite_Produit);
		//    ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
		//    ligne.REMISE = int.Parse(Remise_Produit);
		//    ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
		//    ligne.TVA = int.Parse(TVA_Produit);
		//    ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
		//    List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
		//    if (Session["ProduitsFactureClient"] != null)
		//    {
		//        ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
		//    }
		//    if (!ListeDesPoduits.Select(prd => prd.ID).Contains(ligne.ID))
		//    {
		//        ListeDesPoduits.Add(ligne);
		//    }
		//    Session["ProduitsFactureClient"] = ListeDesPoduits;
		//    return string.Empty;
		//}
		public string EditLineFacture(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string NEW_MARGE, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
		{
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			if (Session["ProduitsFactureClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
			}
			int ID = int.Parse(ID_Produit);
			LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ligne.QUANTITE = int.Parse(Quantite_Produit);
			ligne.PRIX_VENTE_HT2 = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
			ligne.MARGE = decimal.Parse(NEW_MARGE, CultureInfo.InvariantCulture);
			ligne.REMISE = int.Parse(Remise_Produit);
			ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
			ligne.TVA = int.Parse(TVA_Produit);
			ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
			Session["ProduitsFactureClient"] = ListeDesPoduits;
			return string.Empty;
		}

		public string EditSessionCuisine(string SSCAISSON, string ptht)
		{
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			if (Session["CUISINEDevisClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEDevisClient"];
				int ID = int.Parse(SSCAISSON);
				LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.SSCAISSON == ID).FirstOrDefault();
				if (ligne != null)
				{
					ligne.ACC = decimal.Parse(ptht, CultureInfo.InvariantCulture);
				}
				Session["CUISINEDevisClient"] = ListeDesPoduits;
				return string.Empty;
			}
			else
			{
				return ptht;
			}

		}
		public string EditLineDevis(string ID_Produit, string Quantite_Produit, string PUHT_Produit, string Remise_Produit, string NEW_MARGE, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
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
			ligne.MARGE = decimal.Parse(NEW_MARGE, CultureInfo.InvariantCulture);
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

		public string EditLineBonLivraison(string ID_Produit, string Quantite_Produit, string Quantite_Liv, string Quantite_Res, string PUHT_Produit, string Mode, string Remise_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
		{
			decimal qteliv = decimal.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
			decimal qteRes = decimal.Parse(Quantite_Res, CultureInfo.InvariantCulture);
			if (qteliv > qteRes)
			{
				return "NO";
			}
			else
			{


				List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
				if (Session["ProduitsBonLivraisonClient"] != null)
				{
					ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
				}
				//if (Mode == "Edit")
				//{
				int ID = int.Parse(ID_Produit);
				LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
				ligne.ID = ID;
				ligne.QUANTITE = decimal.Parse(Quantite_Produit);
				ligne.QUANTITELiv = decimal.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
				ligne.QUANTITERES = decimal.Parse(Quantite_Res, CultureInfo.InvariantCulture) - decimal.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
				ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
				ligne.REMISE = decimal.Parse(Remise_Produit);
				ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
				ligne.TVA = int.Parse(TVA_Produit);
				ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);

				//}
				//if (Mode == "Create")
				//{
				//    int ID = int.Parse(ID_Produit);
				//    LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
				//    ligne.ID = ID;
				//    ligne.QUANTITE = int.Parse(Quantite_Produit);
				//    ligne.QUANTITELiv = int.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
				//    ligne.QUANTITERES = int.Parse(Quantite_Res);
				//    ligne.PRIX_VENTE_HT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
				//    ligne.REMISE = int.Parse(Remise_Produit);
				//    ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
				//    ligne.TVA = int.Parse(TVA_Produit);
				//    ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
				//}
				//if(Mode == "Editcmd")
				//    {
				//    int ID = int.Parse(ID_Produit);
				//    LigneProduit ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
				//    ligne.ID = ID;

				//    ligne.QUANTITELiv = decimal.Parse(Quantite_Liv, CultureInfo.InvariantCulture);
				//    ligne.QUANTITERES = ligne.QUANTITE - ligne.QUANTITELiv;

				//}
				Session["ProduitsBonLivraisonClient"] = ListeDesPoduits;
				return string.Empty;
			}

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
		public JsonResult GetTVACollecttotalclt(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_CLIENTS> listfactclt = db.FACTURES_CLIENTS.ToList();
			List<LIGNES_FACTURES_CLIENTS> listlignefactclt = db.LIGNES_FACTURES_CLIENTS.ToList();
			List<LIGNES_FACTURES_CLIENTS> listfacttfrs2 = new List<LIGNES_FACTURES_CLIENTS>();

			List<FACTURES_CLIENTS> listfacttfrs = new List<FACTURES_CLIENTS>();
			foreach (FACTURES_CLIENTS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs.Add(fact);
					}
				}
			}
			decimal tottcc = 0;
			decimal tottva = 0;
			foreach (FACTURES_CLIENTS fact in listfacttfrs)
			{
				foreach (LIGNES_FACTURES_CLIENTS ligfact in listlignefactclt)
				{
					if (ligfact.FACTURE_CLIENT == fact.ID)
					{

						tottcc = tottcc + (decimal)(ligfact.TOTALE_HT);
						tottva = tottva + (decimal)((ligfact.TOTALE_TTC) - (ligfact.TOTALE_HT));
					}
				}

			}
			dynamic Result = new
			{
				tottcc = tottcc,
				tottva = tottva
			};
			return Json(Result, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetTotalRS(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();

			List<FACTURES_FOURNISSEURS> listfactclt22 = db.FACTURES_FOURNISSEURS.Where(f => f.MOIS_RAS_Marche == id).ToList();
			if (listfactclt22.Count() == 0)
			{
				List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.Where(f => f.TTC > 1000).ToList();
				foreach (FACTURES_FOURNISSEURS fact in listfactclt)
				{
					if (fact.Date_Declaration != null)
					{
						string[] date1 = fact.Date_Declaration.ToString().Split(' ');
						string[] date2 = date1[0].ToString().Split('/');
						string mm1 = date2[1];
						string yyyy = date2[2];
						if ((mm == mm1) && (yy == yyyy))
						{
							listfacttfrs.Add(fact);
						}
					}
				}
			}
			else
			{
				listfacttfrs = listfactclt22;
			}
			List<RASMache> ListeDesPoduits = new List<RASMache>();
			foreach (FACTURES_FOURNISSEURS fc in listfacttfrs)
			{
				RASMache NewLine = new RASMache();
				NewLine.ID = fc.ID.ToString();
				NewLine.CodeFact = fc.Num_Fact;
				NewLine.Date = fc.DATE.ToString();
				NewLine.FRS = fc.FOURNISSEUR.ToString();
				NewLine.Base = fc.TTC;
				ListeDesPoduits.Add(NewLine);
			}
			Session["RASMarche"] = ListeDesPoduits;
			List<RASMache> ListeDesPoduits1 = (List<RASMache>)Session["RASMache"];
			decimal rs = 0;
			if (ListeDesPoduits != null)
			{
				foreach (RASMache ras in ListeDesPoduits)
				{
					rs = rs + (ras.Base * (decimal)(1.5 / 100));
				}

			}
			return Json(rs, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetTotalFodec(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.ToList();


			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();
			foreach (FACTURES_FOURNISSEURS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs.Add(fact);
					}
				}
			}
			decimal totfodec = 0;
			decimal tvafodec = 0;

			foreach (FACTURES_FOURNISSEURS fact in listfacttfrs)
			{
				if (fact.FODEC != null && fact.FODEC != 0)
				{
					totfodec = totfodec + (decimal)(fact.THT * 1 / 100);
					tvafodec = (totfodec * 18) / 100;
				}
			}
			dynamic Result = new
			{
				totfodec = totfodec,
				tvafodec = tvafodec,

			};
			return Json(Result, JsonRequestBehavior.AllowGet);


		}
		public JsonResult GetTVACollecttotal(string id)
		{
			decimal totHT = 0;
			decimal totMTVA = 0;
			List<TVACollecte> liste = (List<TVACollecte>)Session["ProduitsdecTVA"];
			if (liste.Count != 0)
			{
				foreach (TVACollecte tva1 in liste)
				{

					totHT = totHT + tva1.HT;
					totMTVA = totMTVA + (tva1.TTC - tva1.HT);

				}

			}
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.ToList();


			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();
			foreach (FACTURES_FOURNISSEURS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs.Add(fact);
					}
				}
			}
			decimal totfodec = 0;
			decimal tvafodec = 0;

			foreach (FACTURES_FOURNISSEURS fact in listfacttfrs)
			{
				LIGNES_FACTURES_FOURNISSEURS lignefact = db.LIGNES_FACTURES_FOURNISSEURS.Where(f => f.FACTURE_FOURNISSEUR == fact.ID).FirstOrDefault();
				int tva1 = (int)lignefact.TVA;
				if (fact.FODEC != null && fact.FODEC != 0)
				{
					totfodec = totfodec + (decimal)(fact.THT * 1 / 100);
					tvafodec = (totfodec * tva1) / 100;
				}
			}
			totHT = totfodec + totHT;
			totMTVA = totMTVA + tvafodec;
			dynamic Result = new
			{
				totHT = totHT,
				totMTVA = totMTVA
			};
			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		//public JsonResult GetTVACollecttotal(string id)
		//{
		//    string[] date = id.Split('/');
		//    string mm = date[0];
		//    string yy = date[1];


		//    db.Configuration.ProxyCreationEnabled = false;
		//    List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.ToList();
		//    List<LIGNES_FACTURES_FOURNISSEURS> listlignefactclt = db.LIGNES_FACTURES_FOURNISSEURS.ToList();
		//    List<LIGNES_FACTURES_FOURNISSEURS> listfacttfrs2 = new List<LIGNES_FACTURES_FOURNISSEURS>();

		//    List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();
		//    foreach (FACTURES_FOURNISSEURS fact in listfactclt)
		//    {
		//        if (fact.Date_Declaration != null)
		//        {
		//            string[] date1 = fact.Date_Declaration.ToString().Split(' ');
		//            string[] date2 = date1[0].ToString().Split('/');
		//            string mm1 = date2[1];
		//            string yyyy = date2[2];
		//            if ((mm == mm1) && (yy == yyyy))
		//            {
		//                listfacttfrs.Add(fact);
		//            }
		//        }
		//    }
		//    decimal tottcc = 0;
		//    decimal tottva = 0;
		//    foreach (FACTURES_FOURNISSEURS fact in listfacttfrs)
		//    {
		//        foreach (LIGNES_FACTURES_FOURNISSEURS ligfact in listlignefactclt)
		//        {
		//            if (ligfact.FACTURE_FOURNISSEUR == fact.ID)
		//            {
		//                tottcc = tottcc + (decimal)(ligfact.TOTALE_HT);
		//                tottva = tottva + (decimal)((ligfact.TOTALE_TTC) - (ligfact.TOTALE_HT));
		//            }
		//        }

		//    }
		//    dynamic Result = new
		//    {
		//        tottcc = tottcc,
		//        tottva = tottva
		//    };
		//    return Json(Result, JsonRequestBehavior.AllowGet);

		//}
		public JsonResult GetTVACollect(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];
			int yy1 = int.Parse(yy);

			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.ToList();
			List<LIGNES_FACTURES_FOURNISSEURS> listlignefactclt = db.LIGNES_FACTURES_FOURNISSEURS.ToList();
			List<LIGNES_FACTURES_FOURNISSEURS> listfacttfrs2 = new List<LIGNES_FACTURES_FOURNISSEURS>();

			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();
			foreach (FACTURES_FOURNISSEURS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs.Add(fact);
					}
				}
			}
			foreach (FACTURES_FOURNISSEURS fact in listfacttfrs)
			{
				foreach (LIGNES_FACTURES_FOURNISSEURS ligfact in listlignefactclt)
				{
					if (ligfact.FACTURE_FOURNISSEUR == fact.ID)
					{
						listfacttfrs2.Add(ligfact);
					}
				}
			}
			decimal TTC = 0;
			decimal HT = 0;
			decimal HTFODEC = 0;

			List<TVACollecte> ListeDesPoduits = new List<TVACollecte>();
			List<TVA> listtva = db.TVA.Where(f => f.Annee == yy1).ToList();
			foreach (TVA tva in listtva)
			{
				TVACollecte NewLine = new TVACollecte();
				NewLine.TTC = TTC;
				NewLine.TVA = int.Parse(tva.Valeur_TVA);
				NewLine.HT = HT;
				NewLine.HTFODEC = HTFODEC;
				ListeDesPoduits.Add(NewLine);
			}
			foreach (TVACollecte tva1 in ListeDesPoduits)
			{
				foreach (LIGNES_FACTURES_FOURNISSEURS fc in listfacttfrs2)
				{
					if (tva1.TVA == fc.TVA)
					{
						FACTURES_FOURNISSEURS fact1 = db.FACTURES_FOURNISSEURS.Where(f => f.ID == fc.FACTURE_FOURNISSEUR).FirstOrDefault();
						if ((fact1.FODEC != null) && (fact1.FODEC != 0))
						{
							tva1.TTC = tva1.TTC + (decimal)fc.TOTALE_TTC;
							tva1.HT = tva1.HT + (decimal)fc.TOTALE_HT;
							tva1.HTFODEC = 1;
						}
						else
						{
							tva1.TTC = tva1.TTC + (decimal)fc.TOTALE_TTC;
							tva1.HT = tva1.HT + (decimal)fc.TOTALE_HT;
						}
					}
				}

			}
			Session["ProduitsdecTVA"] = ListeDesPoduits;



			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetTVADeductible(string id)
		{

			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];
			int yy1 = int.Parse(yy);

			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_CLIENTS> listfactclt = db.FACTURES_CLIENTS.ToList();
			List<LIGNES_FACTURES_CLIENTS> listlignefactclt = db.LIGNES_FACTURES_CLIENTS.ToList();
			List<LIGNES_FACTURES_CLIENTS> listfacttfrs2 = new List<LIGNES_FACTURES_CLIENTS>();

			List<FACTURES_CLIENTS> listfacttfrs = new List<FACTURES_CLIENTS>();
			foreach (FACTURES_CLIENTS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs.Add(fact);
					}
				}
			}
			foreach (FACTURES_CLIENTS fact in listfacttfrs)
			{
				foreach (LIGNES_FACTURES_CLIENTS ligfact in listlignefactclt)
				{
					if (ligfact.FACTURE_CLIENT == fact.ID)
					{
						listfacttfrs2.Add(ligfact);
					}
				}
			}
			decimal TTC = 0;
			decimal HT = 0;
			List<TVACollecte> ListeDesPoduits = new List<TVACollecte>();
			List<TVA> listtva = db.TVA.Where(f => f.Annee == yy1).ToList();
			foreach (TVA tva in listtva)
			{
				TVACollecte NewLine = new TVACollecte();
				NewLine.TTC = TTC;
				NewLine.TVA = int.Parse(tva.Valeur_TVA);
				NewLine.HT = HT;
				ListeDesPoduits.Add(NewLine);
			}
			foreach (TVACollecte tva1 in ListeDesPoduits)
			{
				foreach (LIGNES_FACTURES_CLIENTS fc in listfacttfrs2)
				{
					if (tva1.TVA == fc.TVA)
					{
						tva1.TTC = tva1.TTC + (decimal)fc.TOTALE_TTC;
						tva1.HT = tva1.HT + (decimal)fc.TOTALE_HT;
					}
				}

			}
			Session["ProduitsdecTVA"] = ListeDesPoduits;



			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);

		}
		public JsonResult GetTVAMarchettotal(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];
			decimal tottcc = 0;
			decimal totrs = 0;

			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.Where(f => f.TTC > 1000).ToList();
			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();
			foreach (FACTURES_FOURNISSEURS fact in listfactclt)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						tottcc = tottcc + fact.TTC;
						totrs = totrs + (fact.TTC * 15 / 100);

					}
				}
			}
			dynamic Result = new
			{
				tottcc = tottcc,
				totrs = totrs
			};

			return Json(Result, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetfactClt(string id)
		{
			Session["RASMarche"] = null;
			Session["RASSalaire"] = null;
			Session["RASHoraire"] = null;
			Session["RASLoyer"] = null;
			Session["RASTFP"] = null;
			Session["FOPROLOS"] = null;
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;
			List<FACTURES_FOURNISSEURS> listfacttfrs = new List<FACTURES_FOURNISSEURS>();

			List<FACTURES_FOURNISSEURS> listfactclt22 = db.FACTURES_FOURNISSEURS.Where(f => f.MOIS_RAS_Marche == id).ToList();
			if (listfactclt22.Count() == 0)
			{
				List<FACTURES_FOURNISSEURS> listfactclt = db.FACTURES_FOURNISSEURS.Where(f => f.TTC > 1000).ToList();
				foreach (FACTURES_FOURNISSEURS fact in listfactclt)
				{
					if (fact.Date_Declaration != null)
					{
						string[] date1 = fact.Date_Declaration.ToString().Split(' ');
						string[] date2 = date1[0].ToString().Split('/');
						string mm1 = date2[1];
						string yyyy = date2[2];
						if ((mm == mm1) && (yy == yyyy))
						{
							listfacttfrs.Add(fact);
						}
					}
				}
			}
			else
			{
				listfacttfrs = listfactclt22;
			}
			List<RASMache> ListeDesPoduits = new List<RASMache>();
			foreach (FACTURES_FOURNISSEURS fc in listfacttfrs)
			{
				RASMache NewLine = new RASMache();
				NewLine.ID = fc.ID.ToString();
				NewLine.CodeFact = fc.Num_Fact;
				NewLine.Date = fc.DATE.ToString();
				NewLine.FRS = fc.FOURNISSEUR.ToString();
				NewLine.Base = fc.TTC;
				ListeDesPoduits.Add(NewLine);
			}
			Session["RASMarche"] = ListeDesPoduits;
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult calculTimbre(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];


			db.Configuration.ProxyCreationEnabled = false;

			List<FACTURES_CLIENTS> listfactclt1 = db.FACTURES_CLIENTS.ToList();
			List<FACTURES_CLIENTS> listfacttfrs1 = new List<FACTURES_CLIENTS>();
			foreach (FACTURES_CLIENTS fact in listfactclt1)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs1.Add(fact);
					}
				}
			}
			decimal fact1 = 0;
			int count1 = listfacttfrs1.Count;
			if (count1 != 0)
			{
				fact1 = fact1 + (decimal)listfacttfrs1[0].TIMBRE;
			}
			decimal sommmeTimbre = fact1 * count1;
			return Json(sommmeTimbre, JsonRequestBehavior.AllowGet);
		}
		public JsonResult getRSbase(string id)
		{
			decimal basers = 0;
			List<RASMache> ListeDesPoduits = (List<RASMache>)Session["RASMarche"];
			foreach (RASMache ras in ListeDesPoduits)
			{
				if (ras.ID == id)
				{
					basers = ras.Base * (decimal)(1.5 / 100);
				}
			}
			return Json(basers, JsonRequestBehavior.AllowGet);
		}
		public JsonResult getTVAReport(string id)
		{
			string date2;
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];
			int mm1 = int.Parse(mm);
			int mm2 = mm1 - 1;
			if (mm2 < 10)
			{
				date2 = "0" + mm2.ToString() + "/" + yy;
			}
			else
			{
				date2 = mm2.ToString() + "/" + yy;

			}
			decimal tvareport = 0;
			DeclarationTVA dectva = db.DeclarationTVA.Where(f => f.date_dec == date2).FirstOrDefault();
			if (dectva != null)
			{
				tvareport = (decimal)dectva.ReportTVA;

			}
			else
			{
				tvareport = 0;
			}
			return Json(tvareport, JsonRequestBehavior.AllowGet);


		}
		public JsonResult calculTCL(string id)
		{
			string[] date = id.Split('/');
			string mm = date[0];
			string yy = date[1];
			decimal TTC = 0;
			List<FACTURES_CLIENTS> listfactclt1 = db.FACTURES_CLIENTS.ToList();
			List<FACTURES_CLIENTS> listfacttfrs1 = new List<FACTURES_CLIENTS>();
			foreach (FACTURES_CLIENTS fact in listfactclt1)
			{
				if (fact.Date_Declaration != null)
				{
					string[] date1 = fact.Date_Declaration.ToString().Split(' ');
					string[] date2 = date1[0].ToString().Split('/');
					string mm1 = date2[1];
					string yyyy = date2[2];
					if ((mm == mm1) && (yy == yyyy))
					{
						listfacttfrs1.Add(fact);
					}
				}
			}
			foreach (FACTURES_CLIENTS f1 in listfacttfrs1)
			{
				TTC = TTC + f1.TTC;
			}
			return Json(TTC, JsonRequestBehavior.AllowGet);
		}
		public JsonResult getRS()
		{
			decimal rs = 0;
			List<RASMache> ListeDesPoduits = (List<RASMache>)Session["RASMarche"];
			if (ListeDesPoduits != null)
			{
				foreach (RASMache ras in ListeDesPoduits)
				{
					rs = rs + (ras.Base * (decimal)(1.5 / 100));
				}
			}
			return Json(rs, JsonRequestBehavior.AllowGet);

		}

		public JsonResult GetAllsalaire2(string id)
		{
			List<RASSalaire> ras = db.RASSalaire.Where(f => f.Date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASSalaire1> ListeDesPoduits = new List<RASSalaire1>();
			foreach (RASSalaire r in ras)
			{
				RASSalaire1 ras1 = new RASSalaire1();
				ras1.Date = r.Date;
				ras1.ID = r.id;
				ras1.salaire = (decimal)r.salaire;
				ListeDesPoduits.Add(ras1);
			}
			Session["RASSalaire"] = ListeDesPoduits;
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetTOTsalaire2(string id)
		{
			List<RASSalaire> ras = db.RASSalaire.Where(f => f.Date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASSalaire1> ListeDesPoduits = new List<RASSalaire1>();
			foreach (RASSalaire r in ras)
			{
				RASSalaire1 ras1 = new RASSalaire1();
				ras1.Date = r.Date;
				ras1.ID = r.id;
				ras1.salaire = (decimal)r.salaire;
				ListeDesPoduits.Add(ras1);
			}
			//Session["RASSalaire"] = ListeDesPoduits;
			decimal totsal = 0;
			if (ListeDesPoduits != null)
			{
				foreach (RASSalaire1 ras1 in ListeDesPoduits)
				{
					totsal = totsal + ras1.salaire;
				}
			}
			return Json(totsal, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllTFP2(string id)
		{
			List<TFP> ras = db.TFP.Where(f => f.Date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<TFP1> ListeDesPoduits = new List<TFP1>();
			foreach (TFP r in ras)
			{
				TFP1 ras1 = new TFP1();
				ras1.Date = r.Date;
				ras1.ID = r.id;

				ras1.FTP11 = (decimal)r.TFP1;
				ras1.FTP22 = (decimal)r.TFP11;

				ListeDesPoduits.Add(ras1);
			}
			Session["RASTFP"] = ListeDesPoduits;

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllFOPROLOS2(string id)
		{
			List<FOPROLOS> ras = db.FOPROLOS.Where(f => f.Date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<FOPROLOS1> ListeDesPoduits = new List<FOPROLOS1>();
			foreach (FOPROLOS r in ras)
			{
				FOPROLOS1 ras1 = new FOPROLOS1();
				ras1.Date = r.Date;
				ras1.ID = r.id;

				ras1.FOPROLOS11 = (decimal)r.FOPROLOS1;
				ras1.FOPROLOS22 = (decimal)r.FOPROLOS11;

				ListeDesPoduits.Add(ras1);
			}
			Session["FOPROLOS"] = ListeDesPoduits;

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllHonoraire2(string id)
		{
			List<RASHonoraire> ras = db.RASHonoraire.Where(f => f.date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASHoraire> ListeDesPoduits = new List<RASHoraire>();
			foreach (RASHonoraire r in ras)
			{
				RASHoraire ras1 = new RASHoraire();
				ras1.Date = r.date;
				ras1.ID = r.id;

				ras1.honoraire = (decimal)r.honoraire;
				ras1.honoraire1 = (decimal)r.honoraire1;
				ListeDesPoduits.Add(ras1);
			}
			Session["RASHoraire"] = ListeDesPoduits;

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetTOThor2(string id)
		{
			List<RASHonoraire> ras = db.RASHonoraire.Where(f => f.date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASHoraire> ListeDesPoduits = new List<RASHoraire>();
			foreach (RASHonoraire r in ras)
			{
				RASHoraire ras1 = new RASHoraire();
				ras1.Date = r.date;
				ras1.ID = r.id;

				ras1.honoraire = (decimal)r.honoraire;
				ras1.honoraire1 = (decimal)r.honoraire1;
				ListeDesPoduits.Add(ras1);
			}
			//Session["RASHoraire"] = ListeDesPoduits;
			decimal rashor = 0;
			if (ListeDesPoduits != null)
			{
				foreach (RASHoraire rash in ListeDesPoduits)
				{
					rashor = rashor + rash.honoraire1;

				}
			}
			return Json(rashor, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLoyer2(string id)
		{
			List<RASLoyer> ras = db.RASLoyer.Where(f => f.date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASLoyer1> ListeDesPoduits = new List<RASLoyer1>();
			foreach (RASLoyer r in ras)
			{
				RASLoyer1 ras1 = new RASLoyer1();
				ras1.Date = r.date;
				ras1.ID = r.id;

				ras1.loy = (decimal)r.loy;
				ras1.loy1 = (decimal)r.loy1;
				ras1.loy2 = (decimal)r.loy2;
				ListeDesPoduits.Add(ras1);
			}
			Session["RASLoyer"] = ListeDesPoduits;

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetTOTLoy2(string id)
		{
			List<RASLoyer> ras = db.RASLoyer.Where(f => f.date == id).ToList();
			db.Configuration.ProxyCreationEnabled = false;
			List<RASLoyer1> ListeDesPoduits = new List<RASLoyer1>();
			foreach (RASLoyer r in ras)
			{
				RASLoyer1 ras1 = new RASLoyer1();
				ras1.Date = r.date;
				ras1.ID = r.id;

				ras1.loy = (decimal)r.loy;
				ras1.loy1 = (decimal)r.loy1;
				ras1.loy2 = (decimal)r.loy2;
				ListeDesPoduits.Add(ras1);
			}
			//Session["RASLoyer"] = ListeDesPoduits;
			decimal totloy = 0;
			if (ListeDesPoduits != null)
			{
				foreach (RASLoyer1 rasloy in ListeDesPoduits)
				{
					totloy = totloy + rasloy.loy2;
				}
			}
			return Json(totloy, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllRASSalaire()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<RASSalaire1> ListeDesPoduits = (List<RASSalaire1>)Session["RASSalaire"];

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllTFP()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<TFP1> ListeDesPoduits = (List<TFP1>)Session["RASTFP"];

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllFOPROLOS()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<FOPROLOS1> ListeDesPoduits = (List<FOPROLOS1>)Session["FOPROLOS"];

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllRASHonoraire()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<RASHoraire> ListeDesPoduits = (List<RASHoraire>)Session["RASHoraire"];
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllRASLoy()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<RASLoyer1> ListeDesPoduits = (List<RASLoyer1>)Session["RASLoyer"];

			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLigneTVA()
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<RASMache> ListeDesPoduits = (List<RASMache>)Session["RASMarche"];
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllLineDevis(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisine(string Code)
		{

			db.Configuration.ProxyCreationEnabled = false;
			//int Id = int.Parse(Code);
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEDevisClient"];
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisinecmd(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINECommandeClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisineAvoir(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEAvoirClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisinebl(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEBLClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisineFacture(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCuisineCaisse(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesCuisine> ListeDesPoduits2 = new List<LignesCuisine>();
			List<LignesCuisine> ListeDesPoduits = (List<LignesCuisine>)Session["CUISINECAISSEClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLinSERVICE(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			//List<LignesServices> Lignes = (List<LignesServices>)Session["LignesServices"];
			//return Json(Lignes, JsonRequestBehavior.AllowGet);

			List<LignesServices> ListeDesPoduits2 = new List<LignesServices>();
			List<LignesServices> ListeDesPoduits = (List<LignesServices>)Session["LignesServ"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLinSERVICEFacture(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesServices> ListeDesPoduits2 = new List<LignesServices>();
			List<LignesServices> ListeDesPoduits = (List<LignesServices>)Session["LignesServFact"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLinSERVICESOUSTRAITANCE(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;

			List<LignesServicesSSTraitance> ListeDesPoduits2 = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesPoduits = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllLinSERVICESOUSTRAITANCEFact(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LignesServicesSSTraitance> ListeDesPoduits2 = new List<LignesServicesSSTraitance>();
			List<LignesServicesSSTraitance> ListeDesPoduits = (List<LignesServicesSSTraitance>)Session["LignesServSSTFact"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCommande(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineBonLivraison(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineBonLivraisonTotal(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			foreach (LigneProduit ligne in ListeDesPoduits)
			{
				ligne.QUANTITELiv = ligne.QUANTITE;
				ligne.QUANTITERES = 0;
				ligne.PTHT = (ligne.PRIX_VENTE_HT * ligne.QUANTITE) - (((ligne.PRIX_VENTE_HT * ligne.QUANTITE) * ligne.REMISE) / 100);
				ligne.TTC = ligne.PTHT + (((ligne.PTHT) * ligne.TVA) / 100);

			}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineFacture(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineCaisse(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCaisseClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetAllLineAvoir(string Code)
		{
			db.Configuration.ProxyCreationEnabled = false;
			List<LigneProduit> ListeDesPoduits2 = new List<LigneProduit>();
			List<LigneProduit> ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
			//int Id = int.Parse(Code);
			//if (ListeDesPoduits != null)
			//{
			//	ListeDesPoduits2 = ListeDesPoduits.Where(f => f.Code == Id).ToList();
			//}
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
		public ActionResult SendDevis(string Mode, string Code, string IdAffaireCommercial)
		{
			string Numero = Request["numero"] != null ? Request["numero"].ToString() : string.Empty;
			string date = Request["date"] != null ? Request["date"].ToString() : string.Empty;
			string client = Request["client"] != null ? Request["client"].ToString() : string.Empty;
			string TVAProduit1 = Request["TVAProduit1"] != null ? Request["TVAProduit1"].ToString() : "0";
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
			string MargeDevis = Request["MargeDevis"] != null ? Request["MargeDevis"].ToString() : "0";
			string FINTION = Request["FINTION"] != null ? Request["FINTION"].ToString() : string.Empty;
			string Tiroirs = Request["Tiroirs"] != null ? Request["Tiroirs"].ToString() : string.Empty;
			string Charnieres = Request["Charnieres"] != null ? Request["Charnieres"].ToString() : string.Empty;
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];


			//
			if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
			if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
			if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
			if (string.IsNullOrEmpty(TotalHTMI)) TotalHTMI = "0";
			if (string.IsNullOrEmpty(Totalht)) Totalht = "0";
			if (string.IsNullOrEmpty(somme)) somme = "0";
			if (string.IsNullOrEmpty(TVAProduit1)) TVAProduit1 = "0";
			//
			string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
			if (string.IsNullOrEmpty(WithPrint)) WithPrint = "false";
			Boolean Print = Boolean.Parse(WithPrint);
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();

			List<LignesServices> ListeDesServices = new List<LignesServices>();
			List<LignesServicesSSTraitance> ListeDesServicesSSTraitance = new List<LignesServicesSSTraitance>();
			List<DEVIS_CLIENTS> DEVIS_CLIENTS = db.DEVIS_CLIENTS.ToList();
			string SelectedDevis = string.Empty;
			if (Session["ProduitsDevisClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsDevisClient"];
			}
			if (Session["LignesServ"] != null)
			{
				ListeDesServices = (List<LignesServices>)Session["LignesServ"];

			}

			if (Session["LignesServSST"] != null)
			{
				ListeDesServicesSSTraitance = (List<LignesServicesSSTraitance>)Session["LignesServSST"];

			}
			if (Session["CUISINEDevisClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINEDevisClient"];

			}
			if (Session["LignesACCessoire"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];

			}
			if (Mode == "Create")
			{
				db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == null).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
				if (!db.DEVIS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					DEVIS_CLIENTS DevisClient = new DEVIS_CLIENTS();

					DevisClient.CODE = Numero;
					DevisClient.DATE = DateTime.Parse(date);
					DevisClient.FINTION = FINTION;
					DevisClient.Tiroirs = Tiroirs;
					DevisClient.Charnieres = Charnieres;
					string an = DevisClient.DATE.ToString("yyyy");
					int annee = int.Parse(an);
					while (!db.Exercice.Select(cmd => cmd.Annee).Contains(annee))
					{
						Exercice ex = new Exercice();
						ex.Annee = annee;
						db.Exercice.Add(ex);
						db.SaveChanges();
					}
					DevisClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					DevisClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					DevisClient.Societes = idste;
					DevisClient.MODE_PAIEMENT = modePaiement;
					DevisClient.Designation = designation;
					DevisClient.convert = false;
					DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
					DevisClient.NHT = ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) - ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100)));
					DevisClient.TVAInstallation = int.Parse(TVAProduit1);
					DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture) + ((decimal.Parse(netAPaye, CultureInfo.InvariantCulture) - decimal.Parse(TotalTTC, CultureInfo.InvariantCulture)) - (decimal.Parse(somme, CultureInfo.InvariantCulture) - (decimal.Parse(somme, CultureInfo.InvariantCulture) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100))));
					DevisClient.THT = decimal.Parse(Totalht, CultureInfo.InvariantCulture);
					DevisClient.TTC = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
					DevisClient.TNET = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
					DevisClient.Marge = decimal.Parse(MargeDevis, CultureInfo.InvariantCulture);
					if (IdAffaireCommercial != null && IdAffaireCommercial != "")
					{
						DevisClient.Id_AffaireCommerciale = int.Parse(IdAffaireCommercial);
					}
					db.DEVIS_CLIENTS.Add(DevisClient);
					db.SaveChanges();
					Tracabilite_Devis_Client trac_Devis_Clt = new Tracabilite_Devis_Client();
					trac_Devis_Clt.Date = DateTime.Today;
					if (Session["ID"] != null)
					{
						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						trac_Devis_Clt.Personnel = personnel1;
						trac_Devis_Clt.Ajoute_Par = true;
					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					trac_Devis_Clt.Societe = IdSoc;
					trac_Devis_Clt.Id_Devis = DevisClient.ID;
					db.Tracabilite_Devis_Client.Add(trac_Devis_Clt);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_DEVIS_CLIENTS UneLigne = new LIGNES_DEVIS_CLIENTS();
						UneLigne.Art_Devis_Frs = Ligne.ID;
						LIGNES_DEVIS_FOURNISSEURS Prix_Achat = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;
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
					if (Session["LignesServ"] != null)
					{
						foreach (LignesServices Ligne in ListeDesServices)
						{
							foreach (int ress in Ligne.RESSOURCE)
							{
								lIGNES_SERVICES UneLigne = new lIGNES_SERVICES();
								UneLigne.DEVIS_CLIENT = DevisClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.Personnels = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								db.lIGNES_SERVICES.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					if (Session["LignesServSST"] != null)
					{
						foreach (LignesServicesSSTraitance Ligne in ListeDesServicesSSTraitance)
						{
							foreach (int ress in Ligne.SOUS_TRAITANCE)
							{
								lIGNES_SERVICESSSTRAITANCE UneLigne = new lIGNES_SERVICESSSTRAITANCE();
								UneLigne.DEVIS_CLIENT = DevisClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.SOUS_TRAITANCE = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								UneLigne.MARGE = Ligne.Marge;
								UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
								db.lIGNES_SERVICESSSTRAITANCE.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					if (Session["CUISINEDevisClient"] != null)
					{
						foreach (LignesCuisine Ligne in ListeDesCuisine)
						{
							LIGNES_CUISINE_DEVIS_CLIENTS lignecuisine = new LIGNES_CUISINE_DEVIS_CLIENTS();
							lignecuisine.SSCAISSON = Ligne.SSCAISSON;
							lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
							lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
							lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
							lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
							lignecuisine.ACC = Ligne.ACC;
							lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
							lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
							if (Ligne.SOUSFACADE != 0 && Ligne.IDTYPFACADE != 0 && Ligne.FACADE != 0)
							{
								int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
								lignecuisine.SOUSFACADE = ssfacade;
								lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
							}
							lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
							lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
							lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
							lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
							lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
							lignecuisine.TVACUISINE = Ligne.TVACUISINE;
							lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
							lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
							lignecuisine.DEVIS_CLIENT = DevisClient.ID;
							db.LIGNES_CUISINE_DEVIS_CLIENTS.Add(lignecuisine);
							db.SaveChanges();
							List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
							foreach (LignesACCESSOIRE lig in listAcc)
							{
								LIGNES_DESCRIPTION_ACCESOIRE des = new LIGNES_DESCRIPTION_ACCESOIRE();
								des.Designation = lig.DESIGNATION;
								des.ID_SSCAT = lig.IDDESIGNATION;
								des.ID_ART = lig.IDArticle;
								des.PUHT = lig.PUHT;
								des.PTHT = lig.PTHT;
								des.TVA = lig.TVA;
								des.PTTC = lig.TTC;
								des.QTE = lig.QTE;
								des.ID_LigneDC = lignecuisine.ID;
								db.LIGNES_DESCRIPTION_ACCESOIRE.Add(des);
								db.SaveChanges();
							}
						}


					}

				}
				else
				{
					int Max = 0;
					DateTime d = DateTime.Parse(date);
					PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 1).FirstOrDefault();
					if (PrefixeTable == null)
					{
						if (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
						{
							Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
							//Max2 = Max2.Substring(3, 4);
							//Max = int.Parse(Max2);
						}
						Max++;
						Numero = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
						while (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
						{
							Max++;
							Numero = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
						}
					}
					else
					{

						if (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
						{
							Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
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
							if (c == 'M')
							{
								count1 += c;
							}
						}
						string date1 = d.ToString(count);
						string date2 = d.ToString(count1);
						Numero = numPre.Replace(count, date1);
						Numero = Numero.Replace(count1, date2);
						while (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
						{
							Max++;
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
								if (c == 'M')
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
					DEVIS_CLIENTS DevisClient = new DEVIS_CLIENTS();
					DevisClient.CODE = Numero;
					DevisClient.DATE = DateTime.Parse(date);
					DevisClient.FINTION = FINTION;
					DevisClient.Tiroirs = Tiroirs;
					DevisClient.Charnieres = Charnieres;
					string an = DevisClient.DATE.ToString("yyyy");
					int annee = int.Parse(an);
					while (!db.Exercice.Select(cmd => cmd.Annee).Contains(annee))
					{
						Exercice ex = new Exercice();
						ex.Annee = annee;
						db.Exercice.Add(ex);
						db.SaveChanges();
					}
					DevisClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					DevisClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					DevisClient.Societes = idste;
					DevisClient.MODE_PAIEMENT = modePaiement;
					DevisClient.Designation = designation;
					DevisClient.convert = false;
					DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
					DevisClient.NHT = ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) - ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100)));
					DevisClient.TVAInstallation = int.Parse(TVAProduit1);

					DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture) + ((decimal.Parse(netAPaye, CultureInfo.InvariantCulture) - decimal.Parse(TotalTTC, CultureInfo.InvariantCulture)) - (decimal.Parse(somme, CultureInfo.InvariantCulture) - (decimal.Parse(somme, CultureInfo.InvariantCulture) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100))));
					DevisClient.THT = decimal.Parse(Totalht, CultureInfo.InvariantCulture);

					DevisClient.TTC = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
					DevisClient.TNET = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
					DevisClient.Marge = decimal.Parse(MargeDevis, CultureInfo.InvariantCulture);
					if (IdAffaireCommercial != null && IdAffaireCommercial != "")
					{
						DevisClient.Id_AffaireCommerciale = int.Parse(IdAffaireCommercial);
					}
					db.DEVIS_CLIENTS.Add(DevisClient);
					db.SaveChanges();
					Tracabilite_Devis_Client trac_Devis_Clt = new Tracabilite_Devis_Client();
					trac_Devis_Clt.Date = DateTime.Today;
					if (Session["ID"] != null)
					{

						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						trac_Devis_Clt.Personnel = personnel1;
						trac_Devis_Clt.Ajoute_Par = true;
						trac_Devis_Clt.Modifie_Par = false;

					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					trac_Devis_Clt.Societe = IdSoc;
					trac_Devis_Clt.Id_Devis = DevisClient.ID;
					db.Tracabilite_Devis_Client.Add(trac_Devis_Clt);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_DEVIS_CLIENTS UneLigne = new LIGNES_DEVIS_CLIENTS();
						UneLigne.Art_Devis_Frs = int.Parse(Ligne.NumDevis);
						LIGNES_DEVIS_FOURNISSEURS Prix_Achat = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;
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
					if (Session["LignesServ"] != null)
					{
						foreach (LignesServices Ligne in ListeDesServices)
						{
							foreach (int ress in Ligne.RESSOURCE)
							{
								lIGNES_SERVICES UneLigne = new lIGNES_SERVICES();
								UneLigne.DEVIS_CLIENT = DevisClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.Personnels = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								db.lIGNES_SERVICES.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					if (Session["LignesServSST"] != null)
					{
						foreach (LignesServicesSSTraitance Ligne in ListeDesServicesSSTraitance)
						{
							foreach (int ress in Ligne.SOUS_TRAITANCE)
							{
								lIGNES_SERVICESSSTRAITANCE UneLigne = new lIGNES_SERVICESSSTRAITANCE();
								UneLigne.DEVIS_CLIENT = DevisClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.SOUS_TRAITANCE = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								UneLigne.MARGE = Ligne.Marge;
								UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
								db.lIGNES_SERVICESSSTRAITANCE.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					if (Session["CUISINEDevisClient"] != null)
					{
						foreach (LignesCuisine Ligne in ListeDesCuisine)
						{
							LIGNES_CUISINE_DEVIS_CLIENTS lignecuisine = new LIGNES_CUISINE_DEVIS_CLIENTS();
							lignecuisine.SSCAISSON = Ligne.CAISSON;
							lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
							lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
							lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
							lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
							lignecuisine.ACC = Ligne.ACC;
							lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
							lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
							int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
							lignecuisine.SOUSFACADE = ssfacade;
							lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
							lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
							lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
							lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
							lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
							lignecuisine.TVACUISINE = Ligne.TVACUISINE;
							lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
							lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
							lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
							lignecuisine.DEVIS_CLIENT = DevisClient.ID;
							db.LIGNES_CUISINE_DEVIS_CLIENTS.Add(lignecuisine);
							db.SaveChanges();
							List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
							foreach (LignesACCESSOIRE lig in listAcc)
							{
								LIGNES_DESCRIPTION_ACCESOIRE des = new LIGNES_DESCRIPTION_ACCESOIRE();
								des.Designation = lig.DESIGNATION;
								des.ID_SSCAT = lig.IDDESIGNATION;
								des.ID_ART = lig.IDArticle;
								des.PUHT = lig.PUHT;
								des.PTHT = lig.PTHT;
								des.TVA = lig.TVA;
								des.PTTC = lig.TTC;
								des.QTE = lig.QTE;
								des.ID_LigneDC = lignecuisine.ID;
								db.LIGNES_DESCRIPTION_ACCESOIRE.Add(des);
								db.SaveChanges();
							}
						}


					}
					//Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

					//this.db.SaveChanges();
					//if (DevisClient.convert == false)
					//{
					//    var projetTechnique = new ProjetTechniques()
					//    {

					//        Devis_clt = DevisClient.ID,
					//        ReferenceTech = param.RefTech + param.CompteurTech,
					//        DateDebut = DateTime.Now,
					//        DateFin = DateTime.Now,
					//        Cout = (float)DevisClient.TTC,
					//        ClientId = DevisClient.CLIENT,
					//        PersonnelId = DevisClient.Societes,
					//        Designation = DevisClient.Designation,
					//        Statut = "Initié",
					//        CoutReel = (float)DevisClient.TTC,
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
					//    DevisClient.convert = true;
					//    db.Entry(DevisClient).State = System.Data.Entity.EntityState.Modified;
					//    this.db.SaveChanges();
					//    List<horaire_jour> list = db.horaire_jour.ToList();
					//    foreach (horaire_jour hj in list)
					//    {
					//        string jour = db.Jours.Where(fou => fou.id == hj.jour).FirstOrDefault().Jour;
					//        string Hdepart = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Debut;
					//        string Hsortie = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Sortie;
					//        string Hdepart2 = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Debut1;
					//        string Hsortie2 = db.Horaire.Where(fou => fou.id == hj.horaire).FirstOrDefault().Sortie2;
					//        DateTime DateDeb = (DateTime)db.Tableau_Horaire.Where(fou => fou.id == hj.table_horaire).FirstOrDefault().Date_Deb;
					//        DateTime DateFin = (DateTime)db.Tableau_Horaire.Where(fou => fou.id == hj.table_horaire).FirstOrDefault().Date_Fin;

					//        //int Hdepart1 = int.Parse(Hdepart);
					//        //int Hsortie1 = int.Parse(Hsortie);
					//        ParametrageSemaines param1 = new ParametrageSemaines();
					//        param1.JourId = hj.jour;
					//        param1.JourLibelle = jour;
					//        param1.jourTravail = true;
					//        param1.seance1Debut = Hdepart;
					//        param1.seance1Fin = Hsortie;
					//        if ((Hdepart2 == null) && (Hsortie2 == null))
					//        {
					//            param1.doubleSeance = false;
					//            param1.seance2Debut = "0";
					//            param1.seance2Fin = "0";
					//        }
					//        else
					//        {
					//            param1.doubleSeance = true;
					//            param1.seance2Debut = Hdepart2;
					//            param1.seance2Fin = Hsortie2;
					//        }
					//        param1.projetTechniqueID = db.ProjetTechniques.Max(p => p.Devis_clt);
					//        param1.Date_Deb = DateDeb;
					//        param1.Date_Fin = DateFin;
					//        db.ParametrageSemaines.Add(param1);
					//        db.SaveChanges();

					//    }

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
			if (((Mode == "Edit") || (Mode == "Aff")) && (!Print))
			{
				db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == null).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
				int ID = int.Parse(Code);
				DEVIS_CLIENTS DevisClient = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID && cmd.Societes == idste).FirstOrDefault();
				int Max = 0;
				DateTime d = DateTime.Parse(date);
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 1).FirstOrDefault();
				if (PrefixeTable == null)
				{
					//if (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();

					//}
					Max++;
					//Numero = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.DEVIS_CLIENTS.Where(f => f.Societes == idste && f.ID != ID).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						Numero = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
				}
				else
				{

					//if (db.DEVIS_CLIENTS.Where(cmd=>cmd.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}
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
						if (c == 'M')
						{
							count1 += c;
						}
					}
					string date1 = d.ToString(count);
					string date2 = d.ToString(count1);
					Numero = numPre.Replace(count, date1);
					Numero = Numero.Replace(count1, date2);
					while (db.DEVIS_CLIENTS.Where(f => f.Societes == idste && f.ID != ID).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
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
							if (c == 'M')
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
				DevisClient.CODE = Numero;
				DevisClient.DATE = DateTime.Parse(date);
				DevisClient.FINTION = FINTION;
				DevisClient.Tiroirs = Tiroirs;
				DevisClient.Charnieres = Charnieres;
				string an = DevisClient.DATE.ToString("yyyy");
				int annee = int.Parse(an);
				while (!db.Exercice.Select(cmd => cmd.Annee).Contains(annee))
				{
					Exercice ex = new Exercice();
					ex.Annee = annee;
					db.Exercice.Add(ex);
					db.SaveChanges();
				}
				DevisClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				DevisClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				DevisClient.Societes = idste;

				DevisClient.MODE_PAIEMENT = modePaiement;
				DevisClient.Designation = designation;
				DevisClient.convert = false;
				DevisClient.REMISE = decimal.Parse(remise, CultureInfo.InvariantCulture);
				DevisClient.NHT = ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) - ((decimal.Parse(totalHT, CultureInfo.InvariantCulture) + decimal.Parse(somme, CultureInfo.InvariantCulture)) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100)));
				DevisClient.TVAInstallation = int.Parse(TVAProduit1, CultureInfo.InvariantCulture);
				DevisClient.TTVA = decimal.Parse(totalTVA, CultureInfo.InvariantCulture) + ((decimal.Parse(netAPaye, CultureInfo.InvariantCulture) - decimal.Parse(TotalTTC, CultureInfo.InvariantCulture)) - (decimal.Parse(somme, CultureInfo.InvariantCulture) - (decimal.Parse(somme, CultureInfo.InvariantCulture) * (decimal.Parse(remise, CultureInfo.InvariantCulture) / 100))));
				DevisClient.TTC = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
				DevisClient.THT = decimal.Parse(Totalht, CultureInfo.InvariantCulture);

				DevisClient.TNET = decimal.Parse(TotalHTMI, CultureInfo.InvariantCulture);
				DevisClient.Marge = decimal.Parse(MargeDevis, CultureInfo.InvariantCulture);
				if (IdAffaireCommercial != null && IdAffaireCommercial != "")
				{
					DevisClient.Id_AffaireCommerciale = int.Parse(IdAffaireCommercial);
				}
				db.SaveChanges();
				db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
				Tracabilite_Devis_Client trac_Devis_Clt = new Tracabilite_Devis_Client();
				trac_Devis_Clt.Date = DateTime.Today;
				if (Session["ID"] != null)
				{

					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					trac_Devis_Clt.Personnel = personnel1;
					trac_Devis_Clt.Modifie_Par = true;
					trac_Devis_Clt.Ajoute_Par = false;

				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				trac_Devis_Clt.Societe = IdSoc;
				trac_Devis_Clt.Id_Devis = DevisClient.ID;
				db.Tracabilite_Devis_Client.Add(trac_Devis_Clt);
				db.SaveChanges();

				foreach (LigneProduit Ligne in ListeDesPoduits)
				{
					LIGNES_DEVIS_CLIENTS UneLigne = new LIGNES_DEVIS_CLIENTS();
					UneLigne.Art_Devis_Frs = int.Parse(Ligne.NumDevis);
					LIGNES_DEVIS_FOURNISSEURS Prix_Achat = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
					UneLigne.Libelle_Prd = Ligne.LIBELLE;
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
				db.lIGNES_SERVICES.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.lIGNES_SERVICES.Remove(p));
				db.SaveChanges();
				if (Session["LignesServ"] != null)
				{
					foreach (LignesServices Ligne in ListeDesServices)
					{
						foreach (int ress in Ligne.RESSOURCE)
						{
							lIGNES_SERVICES UneLigne = new lIGNES_SERVICES();
							UneLigne.DEVIS_CLIENT = DevisClient.ID;
							UneLigne.SERVICES = Ligne.ID;
							UneLigne.Personnels = ress;
							UneLigne.Unite = Ligne.UNITE;
							UneLigne.TVA = Ligne.TVA;
							UneLigne.TOTALE_HT = Ligne.PTHT;
							UneLigne.TOTALE_TTC = Ligne.TTC;
							UneLigne.QUANTITE = Ligne.QUANTITE;
							UneLigne.REMISE = Ligne.REMISE;
							UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
							db.lIGNES_SERVICES.Add(UneLigne);
							db.SaveChanges();
						}
					}
				}
				db.lIGNES_SERVICESSSTRAITANCE.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.lIGNES_SERVICESSSTRAITANCE.Remove(p));
				db.SaveChanges();
				if (Session["LignesServSST"] != null)
				{
					foreach (LignesServicesSSTraitance Ligne in ListeDesServicesSSTraitance)
					{
						foreach (int ress in Ligne.SOUS_TRAITANCE)
						{
							lIGNES_SERVICESSSTRAITANCE UneLigne = new lIGNES_SERVICESSSTRAITANCE();
							UneLigne.DEVIS_CLIENT = DevisClient.ID;
							UneLigne.SERVICES = Ligne.ID;
							UneLigne.SOUS_TRAITANCE = ress;
							UneLigne.Unite = Ligne.UNITE;
							UneLigne.TVA = Ligne.TVA;
							UneLigne.TOTALE_HT = Ligne.PTHT;
							UneLigne.TOTALE_TTC = Ligne.TTC;
							UneLigne.QUANTITE = Ligne.QUANTITE;
							UneLigne.REMISE = Ligne.REMISE;
							UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
							UneLigne.MARGE = Ligne.Marge;
							UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
							db.lIGNES_SERVICESSSTRAITANCE.Add(UneLigne);
							db.SaveChanges();
						}
					}
				}
				db.LIGNES_DESCRIPTION_ACCESOIRE.Where(p => p.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
				if (Session["CUISINEDevisClient"] != null)
				{
					db.LIGNES_DESCRIPTION_ACCESOIRE.Where(p => p.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == DevisClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE.Remove(p));
					db.SaveChanges();
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_DEVIS_CLIENTS lignecuisine = new LIGNES_CUISINE_DEVIS_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.DEVIS_CLIENT = DevisClient.ID;
						db.LIGNES_CUISINE_DEVIS_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE des = new LIGNES_DESCRIPTION_ACCESOIRE();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneDC = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE.Add(des);
							db.SaveChanges();
						}
					}


				}
			}
			if (Print)
			{
				int ID2 = int.Parse(Code);
				DEVIS_CLIENTS devisClt = db.DEVIS_CLIENTS.Where(f => f.ID == ID2).FirstOrDefault();

				return RedirectToAction("InvoicePrint", new { CODE = ID2, validite = validite, TotalHTMI = TotalHTMI });
			}
			Session["ProduitsDevisClient"] = null;
			Session["LignesServ"] = null;
			Session["LignesServSST"] = null;
			Session["CUISINEDevisClient"] = null;
			Session["LignesACCESSOIRE"] = null;
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			if (IdAffaireCommercial != "" && IdAffaireCommercial != null)
			{
				return RedirectToAction("Index", "AffaireCommerciales");

			}
			else
			{
				return RedirectToAction("Devis", new { MODE = Mode });
			}
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
			int idste = (int)Session["SoclogoId"];
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
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();
			string SelectedCommande = string.Empty;
			if (Session["ProduitsCommandeClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsCommandeClient"];
			}
			if (Session["CUISINECommandeClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINECommandeClient"];

			}
			if (Session["LignesACCESSOIRECMD"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];

			}
			if (Mode == "Create")
			{
				if (!db.COMMANDES_CLIENTS.Where(cmd => cmd.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					COMMANDES_CLIENTS CommandeClient = new COMMANDES_CLIENTS();
					CommandeClient.CODE = Numero;
					CommandeClient.DATE = DateTime.Parse(date);
					CommandeClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					CommandeClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					CommandeClient.Societes = idste;
					int ID_Soc = idste;
					CommandeClient.SocieteLogo = db.SocieteLogo.Where(fou => fou.id == ID_Soc).FirstOrDefault();
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

					Tracabilite_Commande_Client Tracabilite_Commande_Client = new Tracabilite_Commande_Client();
					Tracabilite_Commande_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{

						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_Commande_Client.Personnel = personnel1;
						Tracabilite_Commande_Client.Ajoute_Par = true;
						Tracabilite_Commande_Client.Modifie_Par = false;

					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_Commande_Client.Societe = IdSoc;
					Tracabilite_Commande_Client.Id_Commande = CommandeClient.ID;
					db.Tracabilite_Commande_Client.Add(Tracabilite_Commande_Client);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_COMMANDES_CLIENTS UneLigne = new LIGNES_COMMANDES_CLIENTS();
						UneLigne.Prix_achat = Ligne.ID;
						LIGNES_DEVIS_FOURNISSEURS Produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;

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
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_COMMANDE_CLIENTS lignecuisine = new LIGNES_CUISINE_COMMANDE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.CAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.COMMANDE_CLIENT = CommandeClient.ID;
						db.LIGNES_CUISINE_COMMANDE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_CMD des = new LIGNES_DESCRIPTION_ACCESOIRE_CMD();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneCMD = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Add(des);
							db.SaveChanges();
						}
					}
					SelectedCommande = CommandeClient.ID.ToString();
				}
				else
				{
					DateTime d = DateTime.Parse(date);
					int Max = 0;
					while (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						Numero = "CDC" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
					COMMANDES_CLIENTS CommandeClient = new COMMANDES_CLIENTS();
					CommandeClient.CODE = Numero;
					CommandeClient.DATE = DateTime.Parse(date);
					CommandeClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					CommandeClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					CommandeClient.Societes = idste;
					int ID_Soc = idste;
					CommandeClient.SocieteLogo = db.SocieteLogo.Where(fou => fou.id == ID_Soc).FirstOrDefault();
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
					Tracabilite_Commande_Client Tracabilite_Commande_Client = new Tracabilite_Commande_Client();
					Tracabilite_Commande_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{

						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_Commande_Client.Personnel = personnel1;
						Tracabilite_Commande_Client.Ajoute_Par = true;
						Tracabilite_Commande_Client.Modifie_Par = false;

					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_Commande_Client.Societe = IdSoc;
					Tracabilite_Commande_Client.Id_Commande = CommandeClient.ID;
					db.Tracabilite_Commande_Client.Add(Tracabilite_Commande_Client);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_COMMANDES_CLIENTS UneLigne = new LIGNES_COMMANDES_CLIENTS();
						UneLigne.Prix_achat = Ligne.ID;
						LIGNES_DEVIS_FOURNISSEURS Produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;

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
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_COMMANDE_CLIENTS lignecuisine = new LIGNES_CUISINE_COMMANDE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.COMMANDE_CLIENT = CommandeClient.ID;
						db.LIGNES_CUISINE_COMMANDE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_CMD des = new LIGNES_DESCRIPTION_ACCESOIRE_CMD();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneCMD = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Add(des);
							db.SaveChanges();
						}
					}
					SelectedCommande = CommandeClient.ID.ToString();


				}
			}
			if ((Mode == "Edit") && (!Print))
			{
				int ID = int.Parse(Code);
				COMMANDES_CLIENTS CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID && cmd.Societes == idste).FirstOrDefault();
				int Max = 0;
				DateTime d = DateTime.Parse(date);
				while (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "CDC" + Max.ToString("0000") + "/" + d.ToString("yy");
				}

				CommandeClient.CODE = Numero;
				CommandeClient.DATE = DateTime.Parse(date);
				CommandeClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				CommandeClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				CommandeClient.Societes = idste;
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
				Tracabilite_Commande_Client Tracabilite_Commande_Client = new Tracabilite_Commande_Client();
				Tracabilite_Commande_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{

					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_Commande_Client.Personnel = personnel1;
					Tracabilite_Commande_Client.Ajoute_Par = false;
					Tracabilite_Commande_Client.Modifie_Par = true;
				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_Commande_Client.Societe = IdSoc;
				Tracabilite_Commande_Client.Id_Commande = CommandeClient.ID;
				db.Tracabilite_Commande_Client.Add(Tracabilite_Commande_Client);
				db.SaveChanges();

				foreach (LigneProduit Ligne in ListeDesPoduits)
				{
					LIGNES_COMMANDES_CLIENTS UneLigne = new LIGNES_COMMANDES_CLIENTS();
					UneLigne.Prix_achat = Ligne.ID;
					LIGNES_DEVIS_FOURNISSEURS Produit = db.LIGNES_DEVIS_FOURNISSEURS.Where(pr => pr.ID == Ligne.ID).FirstOrDefault();
					UneLigne.Libelle_Prd = Ligne.LIBELLE;

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
				db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Where(p => p.LIGNES_CUISINE_COMMANDE_CLIENTS.COMMANDE_CLIENT == CommandeClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_COMMANDE_CLIENTS.Where(p => p.COMMANDE_CLIENT == CommandeClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_COMMANDE_CLIENTS.Remove(p));
				db.SaveChanges();
				foreach (LignesCuisine Ligne in ListeDesCuisine)
				{
					LIGNES_CUISINE_COMMANDE_CLIENTS lignecuisine = new LIGNES_CUISINE_COMMANDE_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
					lignecuisine.SOUSFACADE = ssfacade;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
					lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
					lignecuisine.COMMANDE_CLIENT = CommandeClient.ID;
					db.LIGNES_CUISINE_COMMANDE_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
					foreach (LignesACCESSOIRE lig in listAcc)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_CMD des = new LIGNES_DESCRIPTION_ACCESOIRE_CMD();
						des.Designation = lig.DESIGNATION;
						des.ID_SSCAT = lig.IDDESIGNATION;
						des.ID_ART = lig.IDArticle;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.TTC;
						des.QTE = lig.QTE;
						des.ID_LigneCMD = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Add(des);
						db.SaveChanges();
					}
				}
				SelectedCommande = CommandeClient.ID.ToString();
			}
			if ((Mode == "Aff") && (!Print))
			{
				int ID = int.Parse(Code);
				COMMANDES_CLIENTS CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedCommande = CommandeClient.ID.ToString();

			}
			if (Print)
			{
				int ID = int.Parse(Code);
				COMMANDES_CLIENTS CommandeClient = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedCommande = CommandeClient.ID.ToString();
				return RedirectToAction("PrintCommandeClientByID", new { CODE = SelectedCommande });
			}
			Session["ProduitsCommandeClient"] = null;
			Session["CUISINECommandeClient"] = null;
			Session["LignesACCESSOIRECMD"] = null;
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];


			string NumeroBLP = string.Empty;
			//
			if (string.IsNullOrEmpty(totalHT)) totalHT = "0";
			if (string.IsNullOrEmpty(NetHT)) NetHT = "0";
			if (string.IsNullOrEmpty(totalTVA)) totalTVA = "0";
			if (string.IsNullOrEmpty(TotalTTC)) TotalTTC = "0";
			if (string.IsNullOrEmpty(netAPaye)) netAPaye = "0";
			//
			string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
			if (string.IsNullOrEmpty(WithPrint))
				WithPrint = "false";
			Boolean Print = Boolean.Parse(WithPrint);
			List<LigneProduit> ListeDesPoduits = new List<LigneProduit>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();
			string SelectedBonLivraison = string.Empty;
			if (Session["ProduitsBonLivraisonClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsBonLivraisonClient"];
			}
			if (Session["CUISINEBLClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINEBLClient"];

			}
			if (Session["LignesACCESSOIREBonLiv"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];

			}
			BONS_LIVRAISONS_PART_CLIENTS BonLivraisonPartClient = new BONS_LIVRAISONS_PART_CLIENTS();

			if (Mode == "Create")
			{
				if (!db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					//int Max = 0;
					//if (db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}
					//Max++;
					BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();

					BonLivraisonClient.CODE = Numero;
					BonLivraisonClient.DATE = DateTime.Parse(date);
					BonLivraisonClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					BonLivraisonClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					BonLivraisonClient.Societes = idste;
					int ID_Soc = idste;
					BonLivraisonClient.SocieteLogo = db.SocieteLogo.Where(fou => fou.id == ID_Soc).FirstOrDefault();
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
					Tracabilite_bl_Client Tracabilite_bl_Client = new Tracabilite_bl_Client();
					Tracabilite_bl_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{
						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_bl_Client.Personnel = personnel1;
						Tracabilite_bl_Client.Ajoute_Par = true;
						Tracabilite_bl_Client.Modifie_Par = false;

					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_bl_Client.Societe = IdSoc;
					Tracabilite_bl_Client.Id_BL = BonLivraisonClient.ID;
					db.Tracabilite_bl_Client.Add(Tracabilite_bl_Client);
					db.SaveChanges();

					if (BonLivraisonClient.Type == false)
					{
						int Max2 = 0;
						if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
						{
							Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
						}
						Max2++;
						NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");
						BonLivraisonPartClient.CODE = NumeroBLP;
						BonLivraisonPartClient.DATE = DateTime.Parse(date);
						BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
						BonLivraisonPartClient.Etat = false;
						BonLivraisonPartClient.VALIDER = false;
						db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);

						db.SaveChanges();
					}

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						if (BonLivraisonClient.Type == false)
						{

							if (Ligne.QUANTITELiv != 0)
							{
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS LIGNES_BONS_LIVRAISONS_PART_CLIENTS = new LIGNES_BONS_LIVRAISONS_PART_CLIENTS();
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Prix_achat = Ligne.ID;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Libelle_Prd = Ligne.LIBELLE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QUANTITE = (double)Ligne.QUANTITELiv;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QTERES = (double)Ligne.QUANTITERES;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Unite = Ligne.UNITE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Categorie = Ligne.CATEGORIE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Marque = Ligne.MARQUE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Devise = Ligne.DEVISE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.REMISE = Ligne.REMISE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.STOCK = (double)Ligne.STOCK;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_HT = Ligne.PTHT;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_TTC = Ligne.TTC;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TVA = Ligne.TVA;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.BON_LIVRAISON_PART_CLIENT = BonLivraisonPartClient.ID;
								//BonLivraisonPartClient.Etat = false;

								db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Add(LIGNES_BONS_LIVRAISONS_PART_CLIENTS);

								db.SaveChanges();
							}
						}


						LIGNES_BONS_LIVRAISONS_CLIENTS UneLigne = new LIGNES_BONS_LIVRAISONS_CLIENTS();


						UneLigne.Prix_achat = Ligne.ID;
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
						UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
						UneLigne.Libelle_Prd = Ligne.LIBELLE;
						UneLigne.Marque = Ligne.MARQUE;
						UneLigne.Unite = Ligne.UNITE;
						UneLigne.Devise = Ligne.DEVISE;
						UneLigne.Categorie = Ligne.CATEGORIE;
						UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
						UneLigne.QUANTITE = (double)Ligne.QUANTITE;
						UneLigne.STOCK = (double)Ligne.STOCK;
						double qteRes = 0;
						List<BONS_LIVRAISONS_PART_CLIENTS> listBLP = db.BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.IDBLC == UneLigne.BON_LIVRAISON_CLIENT).ToList();
						if (listBLP.Count != 0)
						{
							foreach (BONS_LIVRAISONS_PART_CLIENTS blp in listBLP)
							{
								List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> listLignesBLP = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.BON_LIVRAISON_PART_CLIENT == blp.ID && f.Libelle_Prd == Ligne.LIBELLE).ToList();
								if (listLignesBLP.Count != 0)
								{
									foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS lignesblp in listLignesBLP)
									{
										qteRes += (double)lignesblp.QUANTITE;
									}
								}
							}
						}
						UneLigne.QTERES = (double)Ligne.QUANTITE - qteRes;
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

					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_BONLIVRAISON_CLIENTS lignecuisine = new LIGNES_CUISINE_BONLIVRAISON_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.BONLIVRAISON_CLIENT = BonLivraisonClient.ID;
						db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_BL des = new LIGNES_DESCRIPTION_ACCESOIRE_BL();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneBL = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Add(des);
							db.SaveChanges();
						}

					}


					SelectedBonLivraison = BonLivraisonClient.ID.ToString();
				}
				else
				{
					DateTime d = DateTime.Parse(date);
					int Max = 0;
					Max++;
					Numero = "BL" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						Numero = "BL" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
					BONS_LIVRAISONS_CLIENTS BonLivraisonClient = new BONS_LIVRAISONS_CLIENTS();

					BonLivraisonClient.CODE = Numero;
					BonLivraisonClient.DATE = DateTime.Parse(date);
					BonLivraisonClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					BonLivraisonClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					BonLivraisonClient.Societes = idste;
					int ID_Soc = idste;
					BonLivraisonClient.SocieteLogo = db.SocieteLogo.Where(fou => fou.id == ID_Soc).FirstOrDefault();
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
					Tracabilite_bl_Client Tracabilite_bl_Client = new Tracabilite_bl_Client();
					Tracabilite_bl_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{
						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_bl_Client.Personnel = personnel1;
						Tracabilite_bl_Client.Ajoute_Par = true;
						Tracabilite_bl_Client.Modifie_Par = false;
					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_bl_Client.Societe = IdSoc;
					Tracabilite_bl_Client.Id_BL = BonLivraisonClient.ID;
					db.Tracabilite_bl_Client.Add(Tracabilite_bl_Client);
					db.SaveChanges();

					if (BonLivraisonClient.Type == false)
					{
						int Max2 = 0;
						if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
						{
							Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
						}
						Max2++;
						NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");
						BonLivraisonPartClient.CODE = NumeroBLP;
						BonLivraisonPartClient.DATE = DateTime.Parse(date);
						BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
						BonLivraisonPartClient.Etat = false;
						BonLivraisonPartClient.VALIDER = false;
						db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);

						db.SaveChanges();
					}
					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						if (BonLivraisonClient.Type == false)
						{

							if (Ligne.QUANTITE != Ligne.QUANTITELiv)
							{
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS LIGNES_BONS_LIVRAISONS_PART_CLIENTS = new LIGNES_BONS_LIVRAISONS_PART_CLIENTS();
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Prix_achat = Ligne.ID;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Libelle_Prd = Ligne.LIBELLE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QUANTITE = (double)Ligne.QUANTITELiv;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QTERES = (double)Ligne.QUANTITERES;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Unite = Ligne.UNITE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Categorie = Ligne.CATEGORIE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Marque = Ligne.MARQUE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Devise = Ligne.DEVISE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.REMISE = Ligne.REMISE;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.STOCK = (double)Ligne.STOCK;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_HT = Ligne.PTHT;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_TTC = Ligne.TTC;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TVA = Ligne.TVA;
								LIGNES_BONS_LIVRAISONS_PART_CLIENTS.BON_LIVRAISON_PART_CLIENT = BonLivraisonPartClient.ID;
								//BonLivraisonPartClient.Etat = false;

								db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Add(LIGNES_BONS_LIVRAISONS_PART_CLIENTS);

								db.SaveChanges();
							}
						}



						LIGNES_BONS_LIVRAISONS_CLIENTS UneLigne = new LIGNES_BONS_LIVRAISONS_CLIENTS();


						UneLigne.Prix_achat = Ligne.ID;
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
						UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
						UneLigne.Libelle_Prd = Ligne.LIBELLE;
						UneLigne.Marque = Ligne.MARQUE;
						UneLigne.Unite = Ligne.UNITE;
						UneLigne.Devise = Ligne.DEVISE;
						UneLigne.Categorie = Ligne.CATEGORIE;
						UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
						UneLigne.QUANTITE = (double)Ligne.QUANTITE;
						UneLigne.STOCK = (double)Ligne.STOCK;
						double qteRes = 0;
						List<BONS_LIVRAISONS_PART_CLIENTS> listBLP = db.BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.IDBLC == UneLigne.BON_LIVRAISON_CLIENT).ToList();
						if (listBLP.Count != 0)
						{
							foreach (BONS_LIVRAISONS_PART_CLIENTS blp in listBLP)
							{
								List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> listLignesBLP = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.BON_LIVRAISON_PART_CLIENT == blp.ID && f.Libelle_Prd == Ligne.LIBELLE).ToList();
								if (listLignesBLP.Count != 0)
								{
									foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS lignesblp in listLignesBLP)
									{
										qteRes += (double)lignesblp.QUANTITE;
									}
								}
							}
						}
						UneLigne.QTERES = (double)Ligne.QUANTITE - qteRes;
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

					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_BONLIVRAISON_CLIENTS lignecuisine = new LIGNES_CUISINE_BONLIVRAISON_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.BONLIVRAISON_CLIENT = BonLivraisonClient.ID;
						db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_BL des = new LIGNES_DESCRIPTION_ACCESOIRE_BL();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneBL = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Add(des);
							db.SaveChanges();
						}
					}

					SelectedBonLivraison = BonLivraisonClient.ID.ToString();
				}
			}

			if (((Mode == "Edit") || (Mode == "Editcmd")) && (!Print))
			{
				int ID = int.Parse(Code);
				BONS_LIVRAISONS_CLIENTS BonLivraisonClient = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID && cmd.Societes == idste).FirstOrDefault();
				int Max = 0;
				DateTime d = DateTime.Parse(date);

				while (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "BL" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
				BonLivraisonClient.CODE = Numero;
				BonLivraisonClient.DATE = DateTime.Parse(date);
				BonLivraisonClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				BonLivraisonClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				BonLivraisonClient.Societes = idste;
				//int ID_Soc = int.Parse(societe);
				//BonLivraisonClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
				//BonLivraisonClient.Tiers = int.Parse(Tiers);
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
				Tracabilite_bl_Client Tracabilite_bl_Client = new Tracabilite_bl_Client();
				Tracabilite_bl_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_bl_Client.Personnel = personnel1;
					Tracabilite_bl_Client.Ajoute_Par = true;
					Tracabilite_bl_Client.Modifie_Par = false;

				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_bl_Client.Societe = IdSoc;
				Tracabilite_bl_Client.Id_BL = BonLivraisonClient.ID;
				db.Tracabilite_bl_Client.Add(Tracabilite_bl_Client);
				db.SaveChanges();
				db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == BonLivraisonClient.ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
				db.SaveChanges();

				if (BonLivraisonClient.Type == false)
				{
					int Max2 = 0;
					if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
					{
						Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
					}
					Max2++;
					NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");
					BonLivraisonPartClient.CODE = NumeroBLP;
					BonLivraisonPartClient.DATE = DateTime.Parse(date);
					BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
					BonLivraisonPartClient.Etat = false;
					BonLivraisonPartClient.VALIDER = false;
					db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);
					db.SaveChanges();
				}
				foreach (LigneProduit Ligne in ListeDesPoduits)
				{
					//if (BonLivraisonClient.Type == false)
					//{

					//    int Max2 = 0;
					//    if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
					//    {
					//        Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
					//    }
					//    Max2++;
					//    BONS_LIVRAISONS_PART_CLIENTS BonLivraisonPartClient = new BONS_LIVRAISONS_PART_CLIENTS();
					//    NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");

					//    BonLivraisonPartClient.CODE = NumeroBLP;
					//    BonLivraisonPartClient.Code_Article = Ligne.ID;
					//    BonLivraisonPartClient.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
					//    BonLivraisonPartClient.DATE = DateTime.Parse(date);
					//    BonLivraisonPartClient.QTELIV = (double)Ligne.QUANTITELiv;
					//    BonLivraisonPartClient.QTERES = (double)Ligne.QUANTITERES;
					//    BonLivraisonPartClient.IDBLC = BonLivraisonClient.ID;
					//    BonLivraisonPartClient.Etat = false;

					//    db.BONS_LIVRAISONS_PART_CLIENTS.Add(BonLivraisonPartClient);

					//    db.SaveChanges();
					//}
					if (BonLivraisonClient.Type == false)
					{
						//    int Max2 = 0;
						//    if (db.BONS_LIVRAISONS_PART_CLIENTS.ToList().Count != 0)
						//    {
						//        Max2 = db.BONS_LIVRAISONS_PART_CLIENTS.Select(cmd => cmd.ID).Count();
						//    }
						//    Max2++;
						//    BONS_LIVRAISONS_PART_CLIENTS BonLivraisonPartClient = new BONS_LIVRAISONS_PART_CLIENTS();
						//    NumeroBLP = Numero + "/" + "BLP" + Max2.ToString("0000");

						//    BonLivraisonPartClient.CODE = NumeroBLP;
						if (Ligne.QUANTITE != Ligne.QUANTITELiv)
						{
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS LIGNES_BONS_LIVRAISONS_PART_CLIENTS = new LIGNES_BONS_LIVRAISONS_PART_CLIENTS();
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Prix_achat = Ligne.ID;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Libelle_Prd = Ligne.LIBELLE;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QUANTITE = (double)Ligne.QUANTITELiv;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.QTERES = (double)Ligne.QUANTITERES;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Unite = Ligne.UNITE;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Categorie = Ligne.CATEGORIE;
							//LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Sous_Categorie = Ligne.Sous_Categorie;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TVA = Ligne.TVA;

							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Marque = Ligne.MARQUE;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Devise = Ligne.DEVISE;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.REMISE = Ligne.REMISE;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.STOCK = (double)Ligne.STOCK;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_HT = Ligne.PTHT;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.TOTALE_TTC = Ligne.TTC;
							LIGNES_BONS_LIVRAISONS_PART_CLIENTS.BON_LIVRAISON_PART_CLIENT = BonLivraisonPartClient.ID;
							//BonLivraisonPartClient.Etat = false;

							db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Add(LIGNES_BONS_LIVRAISONS_PART_CLIENTS);

							db.SaveChanges();
						}
					}


					LIGNES_BONS_LIVRAISONS_CLIENTS UneLigne = new LIGNES_BONS_LIVRAISONS_CLIENTS();

					UneLigne.Prix_achat = Ligne.ID;
					Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
					UneLigne.Libelle_Prd = Ligne.LIBELLE;
					UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
					UneLigne.Marque = Ligne.MARQUE;
					UneLigne.Unite = Ligne.UNITE;
					UneLigne.Devise = Ligne.DEVISE;
					UneLigne.Categorie = Ligne.CATEGORIE;
					UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
					UneLigne.QUANTITE = (double)Ligne.QUANTITE;
					UneLigne.STOCK = (double)Ligne.STOCK;
					double qteRes = 0;
					List<BONS_LIVRAISONS_PART_CLIENTS> listBLP = db.BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.IDBLC == UneLigne.BON_LIVRAISON_CLIENT).ToList();
					foreach (BONS_LIVRAISONS_PART_CLIENTS blp in listBLP)
					{
						List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> listLignesBLP = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.BON_LIVRAISON_PART_CLIENT == blp.ID && f.Libelle_Prd == Ligne.LIBELLE).ToList();
						foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS lignesblp in listLignesBLP)
						{
							qteRes += (double)lignesblp.QUANTITE;
						}

					}
					UneLigne.QTERES = (double)Ligne.QUANTITE - qteRes;
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
				db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(p => p.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == BonLivraisonClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(p => p.BONLIVRAISON_CLIENT == BonLivraisonClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Remove(p));
				db.SaveChanges();
				foreach (LignesCuisine Ligne in ListeDesCuisine)
				{
					LIGNES_CUISINE_BONLIVRAISON_CLIENTS lignecuisine = new LIGNES_CUISINE_BONLIVRAISON_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
					lignecuisine.SOUSFACADE = ssfacade;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
					lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
					lignecuisine.BONLIVRAISON_CLIENT = BonLivraisonClient.ID;
					db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
					foreach (LignesACCESSOIRE lig in listAcc)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_BL des = new LIGNES_DESCRIPTION_ACCESOIRE_BL();
						des.Designation = lig.DESIGNATION;
						des.ID_SSCAT = lig.IDDESIGNATION;
						des.ID_ART = lig.IDArticle;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.TTC;
						des.QTE = lig.QTE;
						des.ID_LigneBL = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Add(des);
						db.SaveChanges();
					}
				}
				SelectedBonLivraison = BonLivraisonClient.ID.ToString();
			}
			if ((Mode == "Aff") && (!Print))
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
			Session["CUISINEBLClient"] = null;
			Session["LignesACCESSOIREBonLiv"] = null;
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			return RedirectToAction("BonLivraison", new { MODE = Mode });
		}
		[HttpPost]
		public ActionResult SendBonLivraisonPart(string Mode, string Code)
		{
			string WithPrint = Request["WithPrint"] != null ? Request["WithPrint"].ToString() : "false";
			if (string.IsNullOrEmpty(WithPrint))
				WithPrint = "false";
			Boolean Print = Boolean.Parse(WithPrint);
			if (Print)
			{
				return RedirectToAction("PrintBLClientByIDPartiel", new { CODE = Code });
			}
			return RedirectToAction("PrintBLClientByIDPartiel", new { CODE = Code });


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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
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
			List<LignesServices> ListeDesServices = new List<LignesServices>();
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();
			string SelectedFacture = string.Empty;
			if (Session["ProduitsFactureClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
			}
			if (Session["LignesServFact"] != null)
			{
				ListeDesServices = (List<LignesServices>)Session["LignesServFact"];

			}
			if (Session["CUISINEFACTUREClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];

			}
			if (Session["LignesACCESSOIREFacture"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];

			}
			if (Mode == "Create")
			{
				if (!db.FACTURES_CLIENTS.Where(cmd => cmd.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					FACTURES_CLIENTS FactureClient = new FACTURES_CLIENTS();
					FactureClient.CODE = Numero;
					FactureClient.DATE = DateTime.Parse(date);
					FactureClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					FactureClient.Societes = idste;
					if (type == "true")
					{
						FactureClient.Declaration = true;
						Tracabilite_Facture_Client Tracabilite_Facture_Client1 = new Tracabilite_Facture_Client();
						Tracabilite_Facture_Client1.Date = DateTime.Today;
						if (Session["ID"] != null)
						{
							string personnel = (string)Session["ID"];
							int personnel1 = int.Parse(personnel);
							Tracabilite_Facture_Client1.Personnel = personnel1;
							Tracabilite_Facture_Client1.Déclaré_Par = true;


						}
						string soc1 = (string)Session["Soclogo"];
						int IdSoc1 = db.SocieteLogo.Where(f => f.Nom_Societe == soc1).FirstOrDefault().id;
						Tracabilite_Facture_Client1.Societe = IdSoc1;
						Tracabilite_Facture_Client1.Id_Facture = FactureClient.ID;
						db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client1);
						db.SaveChanges();
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
					//int ID_Soc = 3;
					//FactureClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
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

					Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
					Tracabilite_Facture_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{
						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_Facture_Client.Personnel = personnel1;
						Tracabilite_Facture_Client.Ajoute_Par = true;
						Tracabilite_Facture_Client.Modifie_Par = false;


					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_Facture_Client.Societe = IdSoc;
					Tracabilite_Facture_Client.Id_Facture = FactureClient.ID;
					db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_FACTURES_CLIENTS UneLigne = new LIGNES_FACTURES_CLIENTS();
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;
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
						UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
						UneLigne.MARGE = Ligne.MARGE;
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

					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_FACTURE_CLIENTS lignecuisine = new LIGNES_CUISINE_FACTURE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.FACTURE_CLIENT = FactureClient.ID;
						db.LIGNES_CUISINE_FACTURE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_Facture des = new LIGNES_DESCRIPTION_ACCESOIRE_Facture();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneFacture = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Add(des);
							db.SaveChanges();
						}
					}
					if (Session["LignesServFact"] != null)
					{
						foreach (LignesServices Ligne in ListeDesServices)
						{
							foreach (int ress in Ligne.RESSOURCE)
							{
								lIGNES_SERVICES_FACTURES UneLigne = new lIGNES_SERVICES_FACTURES();
								UneLigne.FACTURE_CLIENT = FactureClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.Personnels = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								db.lIGNES_SERVICES_FACTURES.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					SelectedFacture = FactureClient.ID.ToString();
				}
				else
				{
					DateTime d = DateTime.Parse(date);
					int Max = 0;
					Max++;
					Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;

						Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
					FACTURES_CLIENTS FactureClient = new FACTURES_CLIENTS();
					FactureClient.CODE = Numero;
					FactureClient.DATE = DateTime.Parse(date);
					FactureClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					FactureClient.Societes = idste;
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
					int ID_Soc = idste;
					//FactureClient.Societes1 = db.Societes.Where(fou => fou.SociID == ID_Soc).FirstOrDefault();
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
					Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
					Tracabilite_Facture_Client.Date = DateTime.Today;
					if (Session["ID"] != null)
					{
						string personnel = (string)Session["ID"];
						int personnel1 = int.Parse(personnel);
						Tracabilite_Facture_Client.Personnel = personnel1;
						Tracabilite_Facture_Client.Ajoute_Par = true;
						Tracabilite_Facture_Client.Modifie_Par = false;
					}
					string soc = (string)Session["Soclogo"];
					int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
					Tracabilite_Facture_Client.Societe = IdSoc;
					Tracabilite_Facture_Client.Id_Facture = FactureClient.ID;
					db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
					db.SaveChanges();

					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_FACTURES_CLIENTS UneLigne = new LIGNES_FACTURES_CLIENTS();
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == Ligne.LIBELLE).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;

						UneLigne.Prix_achat = Produit.Product_ID;
						UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
						UneLigne.Marque = Ligne.MARQUE;
						UneLigne.Unite = Ligne.UNITE;
						UneLigne.Devise = Ligne.DEVISE;
						UneLigne.Categorie = Ligne.CATEGORIE;
						UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
						UneLigne.QUANTITE = (double)Ligne.QUANTITE;
						UneLigne.STOCK = (double)Ligne.STOCK;

						UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
						UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
						UneLigne.MARGE = Ligne.MARGE;
						UneLigne.REMISE = Ligne.REMISE;
						UneLigne.TOTALE_HT = Ligne.PTHT;
						UneLigne.TVA = Ligne.TVA;
						UneLigne.TOTALE_TTC = Ligne.TTC;
						UneLigne.FACTURE_CLIENT = FactureClient.ID;
						UneLigne.FACTURES_CLIENTS = FactureClient;
						//UneLigne.Prix_Achat1 = Produit;
						db.LIGNES_FACTURES_CLIENTS.Add(UneLigne);
						db.SaveChanges();
						//AddMouvementProduit("FACTURE", Produit, FactureClient.DATE, FactureClient.CODE, Ligne.QUANTITE);
					}
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_FACTURE_CLIENTS lignecuisine = new LIGNES_CUISINE_FACTURE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.FACTURE_CLIENT = FactureClient.ID;
						db.LIGNES_CUISINE_FACTURE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_Facture des = new LIGNES_DESCRIPTION_ACCESOIRE_Facture();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneFacture = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Add(des);
							db.SaveChanges();
						}
					}
					if (Session["LignesServFact"] != null)
					{
						foreach (LignesServices Ligne in ListeDesServices)
						{
							foreach (int ress in Ligne.RESSOURCE)
							{
								lIGNES_SERVICES_FACTURES UneLigne = new lIGNES_SERVICES_FACTURES();
								UneLigne.FACTURE_CLIENT = FactureClient.ID;
								UneLigne.SERVICES = Ligne.ID;
								UneLigne.Personnels = ress;
								UneLigne.Unite = Ligne.UNITE;
								UneLigne.TVA = Ligne.TVA;
								UneLigne.TOTALE_HT = Ligne.PTHT;
								UneLigne.TOTALE_TTC = Ligne.TTC;
								UneLigne.QUANTITE = Ligne.QUANTITE;
								UneLigne.REMISE = Ligne.REMISE;
								UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
								db.lIGNES_SERVICES_FACTURES.Add(UneLigne);
								db.SaveChanges();
							}
						}
					}
					SelectedFacture = FactureClient.ID.ToString();

				}
			}

			if ((Mode == "Edit") && (!Print))
			{
				int ID = int.Parse(Code);
				int Max = 0;
				DateTime d = DateTime.Parse(date);
				FACTURES_CLIENTS FactureClient = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID && cmd.Societes == idste).FirstOrDefault();
				while (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;

					Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
				FactureClient.CODE = Numero;
				FactureClient.DATE = DateTime.Parse(date);
				FactureClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				FactureClient.Societes = idste;

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
				Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
				Tracabilite_Facture_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_Facture_Client.Personnel = personnel1;
					Tracabilite_Facture_Client.Modifie_Par = true;
					Tracabilite_Facture_Client.Ajoute_Par = false;

				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_Facture_Client.Societe = IdSoc;
				Tracabilite_Facture_Client.Id_Facture = FactureClient.ID;
				db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
				db.SaveChanges();

				db.LIGNES_FACTURES_CLIENTS.Where(p => p.FACTURE_CLIENT == FactureClient.ID).ToList().ForEach(p => db.LIGNES_FACTURES_CLIENTS.Remove(p));
				db.SaveChanges();
				foreach (LigneProduit Ligne in ListeDesPoduits)
				{
					LIGNES_FACTURES_CLIENTS UneLigne = new LIGNES_FACTURES_CLIENTS();
					Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
					UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
					UneLigne.Prix_achat = Ligne.ID;
					UneLigne.Libelle_Prd = Ligne.LIBELLE;

					UneLigne.Marque = Ligne.MARQUE;
					UneLigne.Unite = Ligne.UNITE;
					UneLigne.Devise = Ligne.DEVISE;
					UneLigne.Categorie = Ligne.CATEGORIE;
					UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
					UneLigne.QUANTITE = (double)Ligne.QUANTITE;
					UneLigne.STOCK = (double)Ligne.STOCK;

					UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
					UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
					UneLigne.MARGE = Ligne.MARGE;
					UneLigne.REMISE = Ligne.REMISE;
					UneLigne.TOTALE_HT = Ligne.PTHT;
					UneLigne.TVA = Ligne.TVA;
					UneLigne.TOTALE_TTC = Ligne.TTC;
					UneLigne.FACTURE_CLIENT = FactureClient.ID;
					UneLigne.FACTURES_CLIENTS = FactureClient;
					db.LIGNES_FACTURES_CLIENTS.Add(UneLigne);
					db.SaveChanges();
				}
				db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(p => p.LIGNES_CUISINE_FACTURE_CLIENTS.FACTURE_CLIENT == FactureClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(p => p.FACTURE_CLIENT == FactureClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_FACTURE_CLIENTS.Remove(p));
				db.SaveChanges();
				foreach (LignesCuisine Ligne in ListeDesCuisine)
				{
					LIGNES_CUISINE_FACTURE_CLIENTS lignecuisine = new LIGNES_CUISINE_FACTURE_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
					lignecuisine.SOUSFACADE = ssfacade;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
					lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
					lignecuisine.FACTURE_CLIENT = FactureClient.ID;
					db.LIGNES_CUISINE_FACTURE_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
					foreach (LignesACCESSOIRE lig in listAcc)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_Facture des = new LIGNES_DESCRIPTION_ACCESOIRE_Facture();
						des.Designation = lig.DESIGNATION;
						des.ID_SSCAT = lig.IDDESIGNATION;
						des.ID_ART = lig.IDArticle;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.TTC;
						des.QTE = lig.QTE;
						des.ID_LigneFacture = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Add(des);
						db.SaveChanges();
					}
				}
				SelectedFacture = FactureClient.ID.ToString();
			}
			if ((Mode == "Aff") && (!Print))
			{
				int ID = int.Parse(Code);
				FACTURES_CLIENTS facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedFacture = facture.ID.ToString();

			}
			if (Print)
			{
				int ID = int.Parse(Code);
				FACTURES_CLIENTS facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedFacture = facture.ID.ToString();
				return RedirectToAction("PrintFactureClientByID", new { CODE = SelectedFacture });
			}
			Session["ProduitsFactureClient"] = null;
			Session["LignesServFact"] = null;
			Session["CUISINEFACTUREClient"] = null;
			Session["LignesACCESSOIREFacture"] = null;
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			return RedirectToAction("Facture", new { MODE = Mode });
		}
		[HttpPost]
		public ActionResult SendCaisse(string Mode, string Code)
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
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
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();
			string SelectedFacture = string.Empty;
			if (Session["ProduitsCaisseClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsFactureClient"];
			}
			if (Session["CUISINECAISSEClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINECAISSEClient"];

			}
			if (Session["LignesACCESSOIRECAISSE"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];

			}
			if (Mode == "Create")
			{
				if (!db.Caisse.Where(cmd => cmd.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Caisse FactureClient = new Caisse();
					FactureClient.CODE = Numero;
					FactureClient.DATE = DateTime.Parse(date);
					FactureClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					FactureClient.Societes = idste;
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
					int ID_Soc = idste;
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

					db.Caisse.Add(FactureClient);
					db.SaveChanges();
					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_Caisse UneLigne = new LIGNES_Caisse();
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
						UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
						UneLigne.Prix_achat = Ligne.ID;
						UneLigne.Libelle_Prd = Ligne.LIBELLE;

						UneLigne.Marque = Ligne.MARQUE;
						UneLigne.Unite = Ligne.UNITE;
						UneLigne.Devise = Ligne.DEVISE;
						UneLigne.Categorie = Ligne.CATEGORIE;
						UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
						UneLigne.QUANTITE = (double)Ligne.QUANTITE;
						UneLigne.STOCK = (double)Ligne.STOCK;

						UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
						UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
						UneLigne.MARGE = Ligne.MARGE;
						UneLigne.REMISE = Ligne.REMISE;
						UneLigne.TOTALE_HT = Ligne.PTHT;
						UneLigne.TVA = Ligne.TVA;
						UneLigne.TOTALE_TTC = Ligne.TTC;
						UneLigne.Caisse = FactureClient.ID;
						UneLigne.Caisse1 = FactureClient;
						//UneLigne.Prix_Achat1 = Produit;
						db.LIGNES_Caisse.Add(UneLigne);
						db.SaveChanges();
						//AddMouvementProduit("FACTURE", Produit, FactureClient.DATE, FactureClient.CODE, Ligne.QUANTITE);
					}
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_CAISSE_CLIENTS lignecuisine = new LIGNES_CUISINE_CAISSE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.CAISSE = FactureClient.ID;
						db.LIGNES_CUISINE_CAISSE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_CAISSE des = new LIGNES_DESCRIPTION_ACCESOIRE_CAISSE();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneCAISSE = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Add(des);
							db.SaveChanges();
						}
					}
					SelectedFacture = FactureClient.ID.ToString();
				}
				else
				{
					List<Caisse> FACTURES_CLIENTS = db.Caisse.Where(cmd => cmd.Societes == idste).ToList();

					string[] cod = Numero.Split('C');
					string[] cod11 = cod[1].Split('/');
					int cod1 = int.Parse(cod11[0]);
					int cod12 = cod1++;
					foreach (Caisse FAC in FACTURES_CLIENTS)
					{
						string[] cod122 = FAC.CODE.Split('C');
						string[] cod112 = cod122[1].Split('/');
						int cod13 = int.Parse(cod112[0]);
						if (cod13 == cod12)
						{
							cod12++;
							break;
						}
					}
					Numero = "C" + cod12.ToString("0000") + "/" + cod11[1];
					Caisse FactureClient = new Caisse();
					FactureClient.CODE = Numero;
					FactureClient.DATE = DateTime.Parse(date);
					FactureClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					FactureClient.Societes = idste;
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
					int ID_Soc = idste;
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
					db.Caisse.Add(FactureClient);
					db.SaveChanges();
					foreach (LigneProduit Ligne in ListeDesPoduits)
					{
						LIGNES_Caisse UneLigne = new LIGNES_Caisse();
						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
						UneLigne.Libelle_Prd = Ligne.LIBELLE;

						UneLigne.Prix_achat = Produit.Product_ID;
						UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
						UneLigne.Marque = Ligne.MARQUE;
						UneLigne.Unite = Ligne.UNITE;
						UneLigne.Devise = Ligne.DEVISE;
						UneLigne.Categorie = Ligne.CATEGORIE;
						UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
						UneLigne.QUANTITE = (double)Ligne.QUANTITE;
						UneLigne.STOCK = (double)Ligne.STOCK;

						UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
						UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
						UneLigne.MARGE = Ligne.MARGE;
						UneLigne.REMISE = Ligne.REMISE;
						UneLigne.TOTALE_HT = Ligne.PTHT;
						UneLigne.TVA = Ligne.TVA;
						UneLigne.TOTALE_TTC = Ligne.TTC;
						UneLigne.Caisse = FactureClient.ID;
						//UneLigne.FACTURES_CLIENTS = FactureClient;
						//UneLigne.Prix_Achat1 = Produit;
						db.LIGNES_Caisse.Add(UneLigne);
						db.SaveChanges();
						//AddMouvementProduit("FACTURE", Produit, FactureClient.DATE, FactureClient.CODE, Ligne.QUANTITE);
					}
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_CAISSE_CLIENTS lignecuisine = new LIGNES_CUISINE_CAISSE_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.SSCAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.CAISSE = FactureClient.ID;
						db.LIGNES_CUISINE_CAISSE_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_CAISSE des = new LIGNES_DESCRIPTION_ACCESOIRE_CAISSE();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneCAISSE = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Add(des);
							db.SaveChanges();
						}
					}
					SelectedFacture = FactureClient.ID.ToString();

				}
			}

			if ((Mode == "Edit") && (!Print))
			{
				int ID = int.Parse(Code);
				Caisse FactureClient = db.Caisse.Where(cmd => cmd.ID == ID && cmd.Societes == idste).FirstOrDefault();
				FactureClient.CODE = Numero;
				FactureClient.DATE = DateTime.Parse(date);
				FactureClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				FactureClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				FactureClient.Societes = idste;

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
				db.LIGNES_Caisse.Where(p => p.Caisse == FactureClient.ID).ToList().ForEach(p => db.LIGNES_Caisse.Remove(p));
				db.SaveChanges();
				foreach (LigneProduit Ligne in ListeDesPoduits)
				{
					LIGNES_Caisse UneLigne = new LIGNES_Caisse();
					Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Product_ID == Ligne.ID).FirstOrDefault();
					UneLigne.DESIGNATION_PRODUIT = Ligne.DESIGNATION;
					UneLigne.Prix_achat = Ligne.ID;
					UneLigne.Libelle_Prd = Ligne.LIBELLE;

					UneLigne.Marque = Ligne.MARQUE;
					UneLigne.Unite = Ligne.UNITE;
					UneLigne.Devise = Ligne.DEVISE;
					UneLigne.Categorie = Ligne.CATEGORIE;
					UneLigne.Sous_Categorie = Ligne.Sous_CATEGORIE;
					UneLigne.QUANTITE = (double)Ligne.QUANTITE;
					UneLigne.STOCK = (double)Ligne.STOCK;

					UneLigne.PRIX_UNITAIRE_HT = Ligne.PRIX_VENTE_HT;
					UneLigne.PRIX_UNITAIRE_HTVente = Ligne.PRIX_VENTE_HT2;
					UneLigne.MARGE = Ligne.MARGE;
					UneLigne.REMISE = Ligne.REMISE;
					UneLigne.TOTALE_HT = Ligne.PTHT;
					UneLigne.TVA = Ligne.TVA;
					UneLigne.TOTALE_TTC = Ligne.TTC;
					UneLigne.Caisse = FactureClient.ID;
					//UneLigne.FACTURES_CLIENTS = FactureClient;
					db.LIGNES_Caisse.Add(UneLigne);
					db.SaveChanges();
				}
				db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Where(p => p.LIGNES_CUISINE_CAISSE_CLIENTS.CAISSE == FactureClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_CAISSE_CLIENTS.Where(p => p.CAISSE == FactureClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_CAISSE_CLIENTS.Remove(p));
				db.SaveChanges();
				foreach (LignesCuisine Ligne in ListeDesCuisine)
				{
					LIGNES_CUISINE_CAISSE_CLIENTS lignecuisine = new LIGNES_CUISINE_CAISSE_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
					lignecuisine.SOUSFACADE = ssfacade;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
					lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
					lignecuisine.CAISSE = FactureClient.ID;
					db.LIGNES_CUISINE_CAISSE_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
					foreach (LignesACCESSOIRE lig in listAcc)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_CAISSE des = new LIGNES_DESCRIPTION_ACCESOIRE_CAISSE();
						des.Designation = lig.DESIGNATION;
						des.ID_SSCAT = lig.IDDESIGNATION;
						des.ID_ART = lig.IDArticle;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.TTC;
						des.QTE = lig.QTE;
						des.ID_LigneCAISSE = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Add(des);
						db.SaveChanges();
					}
				}
				SelectedFacture = FactureClient.ID.ToString();
			}
			if ((Mode == "Aff") && (!Print))
			{
				int ID = int.Parse(Code);
				Caisse facture = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedFacture = facture.ID.ToString();

			}
			if (Print)
			{
				int ID = int.Parse(Code);
				Caisse facture = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedFacture = facture.ID.ToString();
				return RedirectToAction("PrintCaisseClientByID", new { CODE = SelectedFacture });
			}
			Session["ProduitsCaisseClient"] = null;
			Session["CUISINECAISSEClient"] = null;
			Session["LignesACCESSOIRECAISSE"] = null;
			ViewData["MODE"] = Mode;
			ViewBag.MODE = Mode;
			return RedirectToAction("Caisse", new { MODE = Mode });
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
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
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
			List<LignesCuisine> ListeDesCuisine = new List<LignesCuisine>();
			List<LignesACCESSOIRE> LignesACCESSOIRE = new List<LignesACCESSOIRE>();

			string SelectedAvoir = string.Empty;
			if (Session["ProduitsAvoirClient"] != null)
			{
				ListeDesPoduits = (List<LigneProduit>)Session["ProduitsAvoirClient"];
			}
			if (Session["CUISINEAvoirClient"] != null)
			{
				ListeDesCuisine = (List<LignesCuisine>)Session["CUISINEAvoirClient"];

			}
			if (Session["LignesACCessoireAvoir"] != null)
			{
				LignesACCESSOIRE = (List<LignesACCESSOIRE>)Session["LignesACCessoireAvoir"];

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
					AvoirClient.Societes = idste;
					int ID_Soc = idste;
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
					if (Session["CUISINEAvoirClient"] != null)
					{
						foreach (LignesCuisine Ligne in ListeDesCuisine)
						{
							LIGNES_CUISINE_AVOIR_CLIENTS lignecuisine = new LIGNES_CUISINE_AVOIR_CLIENTS();
							lignecuisine.SSCAISSON = Ligne.CAISSON;
							lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
							lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
							lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
							lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
							lignecuisine.ACC = Ligne.ACC;
							lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
							lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
							int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
							lignecuisine.SOUSFACADE = ssfacade;
							lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
							lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
							lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
							lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
							lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
							lignecuisine.TVACUISINE = Ligne.TVACUISINE;
							lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
							lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
							lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
							lignecuisine.AVOIR_CLIENT = AvoirClient.ID;
							db.LIGNES_CUISINE_AVOIR_CLIENTS.Add(lignecuisine);
							db.SaveChanges();
							List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
							foreach (LignesACCESSOIRE lig in listAcc)
							{
								LIGNES_DESCRIPTION_ACCESOIRE_AVOIR des = new LIGNES_DESCRIPTION_ACCESOIRE_AVOIR();
								des.Designation = lig.DESIGNATION;
								des.ID_SSCAT = lig.IDDESIGNATION;
								des.ID_ART = lig.IDArticle;
								des.PUHT = lig.PUHT;
								des.PTHT = lig.PTHT;
								des.TVA = lig.TVA;
								des.PTTC = lig.TTC;
								des.QTE = lig.QTE;
								des.ID_LigneAVOIR = lignecuisine.ID;
								db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Add(des);
								db.SaveChanges();
							}
						}


					}
					SelectedAvoir = AvoirClient.ID.ToString();
				}
				else
				{

					AVOIRS_CLIENTS AvoirClient = new AVOIRS_CLIENTS();
					int Max = 0;
					Max++;
					DateTime d = DateTime.Parse(date);
					Numero = "AV" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.AVOIRS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						Numero = "AV" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
					AvoirClient.CODE = Numero;
					AvoirClient.DATE = DateTime.Parse(date);
					AvoirClient.CLIENT = int.Parse(client);
					int ID_CLIENT = int.Parse(client);
					AvoirClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
					AvoirClient.Societes = idste;
					int ID_Soc = idste;
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
					if (Session["CUISINEAvoirClient"] != null)
					{
						foreach (LignesCuisine Ligne in ListeDesCuisine)
						{
							LIGNES_CUISINE_AVOIR_CLIENTS lignecuisine = new LIGNES_CUISINE_AVOIR_CLIENTS();
							lignecuisine.SSCAISSON = Ligne.CAISSON;
							lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
							lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
							lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
							lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
							lignecuisine.ACC = Ligne.ACC;
							lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
							lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
							int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
							lignecuisine.SOUSFACADE = ssfacade;
							lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
							lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
							lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
							lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
							lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
							lignecuisine.TVACUISINE = Ligne.TVACUISINE;
							lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
							lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
							lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
							lignecuisine.AVOIR_CLIENT = AvoirClient.ID;
							db.LIGNES_CUISINE_AVOIR_CLIENTS.Add(lignecuisine);
							db.SaveChanges();
							List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
							foreach (LignesACCESSOIRE lig in listAcc)
							{
								LIGNES_DESCRIPTION_ACCESOIRE_AVOIR des = new LIGNES_DESCRIPTION_ACCESOIRE_AVOIR();
								des.Designation = lig.DESIGNATION;
								des.ID_SSCAT = lig.IDDESIGNATION;
								des.ID_ART = lig.IDArticle;
								des.PUHT = lig.PUHT;
								des.PTHT = lig.PTHT;
								des.TVA = lig.TVA;
								des.PTTC = lig.TTC;
								des.QTE = lig.QTE;
								des.ID_LigneAVOIR = lignecuisine.ID;
								db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Add(des);
								db.SaveChanges();
							}
						}


					}
					SelectedAvoir = AvoirClient.ID.ToString();
				}
			}
			if ((Mode == "Edit") && (!Print))
			{
				int ID = int.Parse(Code);
				AVOIRS_CLIENTS AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				int Max = 0;
				DateTime d = DateTime.Parse(date);
				while (db.AVOIRS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "AV" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
				AvoirClient.CODE = Numero;
				AvoirClient.DATE = DateTime.Parse(date);
				AvoirClient.CLIENT = int.Parse(client);
				int ID_CLIENT = int.Parse(client);
				AvoirClient.CLIENTS = db.CLIENTS.Where(fou => fou.ID == ID_CLIENT).FirstOrDefault();
				AvoirClient.Societes = idste;
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
				db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Where(p => p.LIGNES_CUISINE_AVOIR_CLIENTS.AVOIR_CLIENT == AvoirClient.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Remove(p));
				db.SaveChanges();
				db.LIGNES_CUISINE_AVOIR_CLIENTS.Where(p => p.AVOIR_CLIENT == AvoirClient.ID).ToList().ForEach(p => db.LIGNES_CUISINE_AVOIR_CLIENTS.Remove(p));
				db.SaveChanges();
				if (Session["CUISINEAvoirClient"] != null)
				{
					foreach (LignesCuisine Ligne in ListeDesCuisine)
					{
						LIGNES_CUISINE_AVOIR_CLIENTS lignecuisine = new LIGNES_CUISINE_AVOIR_CLIENTS();
						lignecuisine.SSCAISSON = Ligne.CAISSON;
						lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
						lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
						lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
						lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
						lignecuisine.ACC = Ligne.ACC;
						lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
						lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
						int ssfacade = db.SS_FACADE.Where(f => f.TYPE_SS_FACADE == Ligne.SOUSFACADE && f.ID_FAC == Ligne.FACADE).FirstOrDefault().ID;
						lignecuisine.SOUSFACADE = ssfacade;
						lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
						lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
						lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
						lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
						lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
						lignecuisine.TVACUISINE = Ligne.TVACUISINE;
						lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
						lignecuisine.TYPECAISSON = Ligne.IDTYPCAISSON;
						lignecuisine.TYPEFACADE = Ligne.IDTYPFACADE;
						lignecuisine.AVOIR_CLIENT = AvoirClient.ID;
						db.LIGNES_CUISINE_AVOIR_CLIENTS.Add(lignecuisine);
						db.SaveChanges();
						List<LignesACCESSOIRE> listAcc = LignesACCESSOIRE.Where(f => f.IDLIGNESDEScription == Ligne.ID).ToList();
						foreach (LignesACCESSOIRE lig in listAcc)
						{
							LIGNES_DESCRIPTION_ACCESOIRE_AVOIR des = new LIGNES_DESCRIPTION_ACCESOIRE_AVOIR();
							des.Designation = lig.DESIGNATION;
							des.ID_SSCAT = lig.IDDESIGNATION;
							des.ID_ART = lig.IDArticle;
							des.PUHT = lig.PUHT;
							des.PTHT = lig.PTHT;
							des.TVA = lig.TVA;
							des.PTTC = lig.TTC;
							des.QTE = lig.QTE;
							des.ID_LigneAVOIR = lignecuisine.ID;
							db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Add(des);
							db.SaveChanges();
						}
					}


				}
				SelectedAvoir = AvoirClient.ID.ToString();
			}
			if ((Mode == "Aff") && (!Print))
			{
				int ID = int.Parse(Code);
				AVOIRS_CLIENTS AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedAvoir = AvoirClient.ID.ToString();

			}
			if (Print)
			{
				int ID = int.Parse(Code);
				AVOIRS_CLIENTS AvoirClient = db.AVOIRS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
				SelectedAvoir = AvoirClient.ID.ToString();
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
			List<LIGNES_DEVIS_CLIENTS> list1 = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == ID).ToList();
			List<lIGNES_SERVICES> list2 = db.lIGNES_SERVICES.Where(f => f.DEVIS_CLIENT == ID).ToList();
			List<lIGNES_SERVICESSSTRAITANCE> list3 = db.lIGNES_SERVICESSSTRAITANCE.Where(f => f.DEVIS_CLIENT == ID).ToList();
			List<Tracabilite_Devis_Client> list4 = db.Tracabilite_Devis_Client.Where(f => f.Id_Devis == ID).ToList();
			List<LIGNES_CUISINE_DEVIS_CLIENTS> list5 = db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == ID).ToList();

			if (list1 != null)
			{
				db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			if (list2 != null)
			{
				db.lIGNES_SERVICES.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.lIGNES_SERVICES.Remove(p));
				db.SaveChanges();
			}
			if (list3 != null)
			{
				db.lIGNES_SERVICESSSTRAITANCE.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.lIGNES_SERVICESSSTRAITANCE.Remove(p));
				db.SaveChanges();
			}
			if (list4 != null)
			{
				db.Tracabilite_Devis_Client.Where(p => p.Id_Devis == ID).ToList().ForEach(p => db.Tracabilite_Devis_Client.Remove(p));
				db.SaveChanges();
			}

			if (list5 != null)
			{
				List<LIGNES_DESCRIPTION_ACCESOIRE> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE.Where(f => f.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == ID).ToList();
				if (list6 != null)
				{
					db.LIGNES_DESCRIPTION_ACCESOIRE.Where(p => p.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == ID).ToList().ForEach(p => db.LIGNES_CUISINE_DEVIS_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			DEVIS_CLIENTS Devis = db.DEVIS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			if (Devis != null)
			{
				db.DEVIS_CLIENTS.Remove(Devis);
				db.SaveChanges();
			}

			return string.Empty;
		}
		public string DeleteCommande(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LIGNES_COMMANDES_CLIENTS> list1 = db.LIGNES_COMMANDES_CLIENTS.Where(f => f.COMMANDE_CLIENT == ID).ToList();
			List<Tracabilite_Commande_Client> list4 = db.Tracabilite_Commande_Client.Where(f => f.Id_Commande == ID).ToList();
			List<LIGNES_CUISINE_COMMANDE_CLIENTS> list5 = db.LIGNES_CUISINE_COMMANDE_CLIENTS.Where(f => f.COMMANDE_CLIENT == ID).ToList();
			if (list5 != null)
			{
				List<LIGNES_DESCRIPTION_ACCESOIRE_CMD> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Where(f => f.LIGNES_CUISINE_COMMANDE_CLIENTS.COMMANDE_CLIENT == ID).ToList();
				if (list6 != null)
				{
					db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Where(p => p.LIGNES_CUISINE_COMMANDE_CLIENTS.COMMANDE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_CUISINE_COMMANDE_CLIENTS.Where(p => p.COMMANDE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_CUISINE_COMMANDE_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			if (list1 != null)
			{
				db.LIGNES_COMMANDES_CLIENTS.Where(p => p.COMMANDE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_COMMANDES_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			if (list4 != null)
			{
				db.Tracabilite_Commande_Client.Where(p => p.Id_Commande == ID).ToList().ForEach(p => db.Tracabilite_Commande_Client.Remove(p));
				db.SaveChanges();
			}

			COMMANDES_CLIENTS Devis = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			if (Devis != null)
			{
				db.COMMANDES_CLIENTS.Remove(Devis);
				db.SaveChanges();
			}

			return string.Empty;
		}
		public string DeleteBonLaivraison(string parampassed)
		{
			int ID = int.Parse(parampassed);
			int idBlFrs = 0;
			BONS_LIVRAISONS_CLIENTS bl = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_BONS_LIVRAISONS_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
			int iddevisbl = (int)bl.COMMANDES_CLIENTS.DEVIS_CLIENT;
			if (bl.VALIDER == false)
			{
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> list5 = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(f => f.BONLIVRAISON_CLIENT == ID).ToList();
				if (list5 != null)
				{
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList();
					if (list6 != null)
					{
						db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(p => p.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Remove(p));
						db.SaveChanges();
					}
					db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(p => p.BONLIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
				db.SaveChanges();
				db.Tracabilite_bl_Client.Where(p => p.Id_BL == ID).ToList().ForEach(p => db.Tracabilite_bl_Client.Remove(p));
				db.SaveChanges();

			}
			if (bl.VALIDER == true)
			{
				//foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in lignes)
				//{
				//    Prix_Achat pr = db.Prix_Achat.Where(f => f.Libelle == ligne.Libelle_Prd).FirstOrDefault();
				//    if (pr != null)
				//    {
				//        pr.Stock += ligne.QUANTITE;
				//        db.SaveChanges();

				//    }

				//    List<Détails_Articles> details = db.Détails_Articles.Where(f => f.IdPrixAchat == pr.Product_ID).ToList();
				//    if(details.Count()!=0)
				//    {
				//        int IDDevisClt = (int)bl.COMMANDES_CLIENTS.DEVIS_CLIENT;
				//        LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == IDDevisClt && f.Libelle_Prd == ligne.Libelle_Prd).FirstOrDefault();
				//        int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
				//        BONS_RECEPTIONS_FOURNISSEURS blFRS = db.BONS_RECEPTIONS_FOURNISSEURS.Where(f => f.COMMANDES_FOURNISSEURS.Devis_Frs == iddevis).FirstOrDefault();
				//        if(blFRS!=null)
				//        {
				//            Détails_Articles detailsArt = details.Where(f => f.NumBl == blFRS.ID).FirstOrDefault();

				//            if (detailsArt.Quantite>0)
				//            {
				//                detailsArt.Quantite +=(decimal)ligne.QUANTITE;
				//                db.SaveChanges();
				//            }
				//            else
				//            {
				//                if(detailsArt.Quantite == 0)
				//                {
				//                    double QTEFRS = (double)db.LIGNES_BONS_RECEPTIONS_FOURNISSEURS.Where(f => f.BON_RECEPTION_FOURNISSEUR == blFRS.ID).FirstOrDefault().QUANTITE;
				//                    if(QTEFRS==ligne.QUANTITE)
				//                    {
				//                        detailsArt.Quantite = (decimal)ligne.QUANTITE;
				//                        db.SaveChanges();

				//                    }
				//                    else
				//                    {
				//                        if (QTEFRS < ligne.QUANTITE)
				//                        {
				//                            double QTEAjouter = (double)ligne.QUANTITE - QTEFRS;
				//                            double QTEAjouter2 = (double)ligne.QUANTITE - QTEAjouter;
				//                            detailsArt.Quantite = (decimal)QTEAjouter2;
				//                            db.SaveChanges();
				//                            Détails_Articles detailsArt2 = details.Where(f => f.NumBl != blFRS.ID).FirstOrDefault();
				//                            detailsArt2.Quantite = (decimal)QTEAjouter;
				//                            db.SaveChanges();


				//                        }

				//                    }
				//                }

				//            }
				//        }


				//    }

				//}
				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in liste)
				{
					LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == iddevisbl && f.Libelle_Prd == ligne.Libelle_Prd).FirstOrDefault();
					int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
					int frs = db.DEVIS_FOURNISSEURS.Where(f => f.ID == iddevis).FirstOrDefault().FOURNISSEUR;

					COMMANDES_FOURNISSEURS commandefRS = db.COMMANDES_FOURNISSEURS.Where(f => f.Devis_Frs == iddevis).FirstOrDefault();
					if (commandefRS != null)
					{
						idBlFrs = db.BONS_RECEPTIONS_FOURNISSEURS.Where(f => f.COMMANDE_FOURNISSEUR == commandefRS.ID).FirstOrDefault().ID;
					}
					Prix_Achat pr = db.Prix_Achat.Where(f => f.Libelle == ligne.Libelle_Prd).FirstOrDefault();
					if (pr != null)
					{
						pr.Stock += ligne.QUANTITE;
						db.SaveChanges();

					}
					if (idBlFrs != 0)
					{
						Détails_Articles listArtParFrs = db.Détails_Articles.Where(f => f.Fournisseur == frs && f.Reference == ligne.Libelle_Prd && f.NumBl == idBlFrs).FirstOrDefault();

						if (listArtParFrs != null)
						{
							listArtParFrs.Quantite += (decimal)ligne.QUANTITE;
							db.SaveChanges();
						}
					}
				}
				List<LIGNES_DESCRIPTION_ACCESOIRE_BL> listeAcc = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(cmd => cmd.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList();
				foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL ligne in listeAcc)
				{
					Prix_Achat pr = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART).FirstOrDefault();
					if (pr != null)
					{
						pr.Stock += (double)(ligne.QTE * ligne.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.QuantiteCAISSON);
						db.SaveChanges();

					}

					Détails_Articles details = db.Détails_Articles.Where(f => f.IdPrixAchat == pr.Product_ID).FirstOrDefault();
					if (details != null)
					{
						details.Quantite += (decimal)(ligne.QTE * ligne.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.QuantiteCAISSON);
						db.SaveChanges();

					}

				}
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> list5 = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(f => f.BONLIVRAISON_CLIENT == ID).ToList();
				if (list5 != null)
				{
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList();
					if (list6 != null)
					{
						db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(p => p.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Remove(p));
						db.SaveChanges();
					}
					db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(p => p.BONLIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
				db.SaveChanges();
				db.Tracabilite_bl_Client.Where(p => p.Id_BL == ID).ToList().ForEach(p => db.Tracabilite_bl_Client.Remove(p));
				db.SaveChanges();

			}
			List<BONS_LIVRAISONS_PART_CLIENTS> lignes2 = db.BONS_LIVRAISONS_PART_CLIENTS.Where(p => p.IDBLC == ID).ToList();
			foreach (BONS_LIVRAISONS_PART_CLIENTS ligne in lignes2)
			{
				List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> lignes22 = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(p => p.BON_LIVRAISON_PART_CLIENT == ligne.ID).ToList();
				if (lignes22 != null)
				{
					db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(p => p.BON_LIVRAISON_PART_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Remove(p));
					db.SaveChanges();
				}
				db.BONS_LIVRAISONS_PART_CLIENTS.Remove(ligne);
				db.SaveChanges();

			}
			if (bl != null)
			{
				db.BONS_LIVRAISONS_CLIENTS.Remove(bl);
				db.SaveChanges();
			}

			return string.Empty;
		}
		public string DeleteBonLaivraisonPart(string parampassed)
		{
			int ID = int.Parse(parampassed);
			BONS_LIVRAISONS_PART_CLIENTS blp = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			BONS_LIVRAISONS_CLIENTS bl = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == blp.IDBLC).FirstOrDefault();
			if (blp.VALIDER == false)
			{
				db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(p => p.BON_LIVRAISON_PART_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Remove(p));
				db.SaveChanges();
				db.BONS_LIVRAISONS_PART_CLIENTS.Remove(blp);
				db.SaveChanges();
			}
			else
			{
				List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_PART_CLIENT == blp.ID).ToList();

				foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS ligne in liste)
				{
					Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.Libelle_Prd).FirstOrDefault();

					//if (Produit == null)
					//{
					//    Prix_Achat prixAchat = new Prix_Achat();
					//    prixAchat.Libelle = ligne.Libelle_Prd;
					//    prixAchat.Designation = ligne.DESIGNATION_PRODUIT;
					//    prixAchat.Marque = ligne.Marque;
					//    prixAchat.Devise = ligne.Devise;
					//    prixAchat.Unite = ligne.Unite;
					//    if (ligne.Sous_Categorie == "")
					//    {
					//        prixAchat.Sous_Categorie = null;
					//    }
					//    else
					//    {
					//        List<Sous_Categorie> List = db.Sous_Categorie.Where(fr => fr.CentreID == (prixAchat.Categorie)).ToList();
					//        foreach (Sous_Categorie sc in List)
					//        {
					//            if (sc.Libelle == ligne.Sous_Categorie)
					//            {
					//                prixAchat.Sous_Categorie = sc.CatID;
					//            }
					//        }
					//    }
					//    prixAchat.Stock = (double)ligne.QUANTITE;
					//    prixAchat.Remise = (int)ligne.REMISE;
					//    prixAchat.PU_HT_Sans_Remise = (double)ligne.PRIX_UNITAIRE_HT;
					//    prixAchat.Valeur_TVA = (ligne.TVA).ToString();
					//    prixAchat.PU_TTC = (double)ligne.TOTALE_TTC;
					//    db.Prix_Achat.Add(prixAchat);
					//    db.SaveChanges();
					//    //prixAchat.Fournisseur = BonReception.FOURNISSEUR;
					//    //prixAchat.BLfRS = BonReception.ID;

					//    ligne.Prix_achat = prixAchat.Product_ID;
					//    db.SaveChanges();
					//    Détails_Articles DétailsArticles = new Détails_Articles();
					//    DétailsArticles.Reference = ligne.Libelle_Prd;
					//    DétailsArticles.Description = ligne.DESIGNATION_PRODUIT;
					//    DétailsArticles.NumBl = BonReception.ID;
					//    DétailsArticles.Quantite = (decimal)ligne.QUANTITE;
					//    DétailsArticles.IdPrixAchat = prixAchat.Product_ID;
					//    DétailsArticles.Fournisseur = BonReception.FOURNISSEUR;
					//    db.Détails_Articles.Add(DétailsArticles);
					//    db.SaveChanges();
					//}
					//else
					if (Produit != null)
					{
						Produit.Stock += (double)ligne.QUANTITE;
						//ligne.Prix_achat = Produit.Product_ID;
						//Détails_Articles DétailsArticles = new Détails_Articles();
						//DétailsArticles.Reference = ligne.Libelle_Prd;
						//DétailsArticles.Description = ligne.DESIGNATION_PRODUIT;
						////DétailsArticles.NumBl = BonReception.ID;
						//DétailsArticles.Quantite = (decimal)ligne.QUANTITE;
						//DétailsArticles.IdPrixAchat = Produit.Product_ID;
						////DétailsArticles.Fournisseur = BonReception.FOURNISSEUR;
						//db.Détails_Articles.Add(DétailsArticles);
						db.SaveChanges();

					}
				}

			}

			//if (bl.VALIDER == false)
			//{
			//    db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
			//    db.SaveChanges();
			//    db.BONS_LIVRAISONS_CLIENTS.Remove(bl);
			//    db.SaveChanges();
			//}
			//if (bl.VALIDER == true)
			//{
			//    List<LIGNES_BONS_LIVRAISONS_CLIENTS> lignes = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList();
			//    foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in lignes)
			//    {
			//        Prix_Achat pr = db.Prix_Achat.Where(f => f.Libelle == ligne.Libelle_Prd).FirstOrDefault();
			//        if (pr != null)
			//        {
			//            pr.Stock += ligne.QUANTITE;
			//            db.SaveChanges();

			//        }
			//    }
			//    db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(p => p.BON_LIVRAISON_CLIENT == ID).ToList().ForEach(p => db.LIGNES_BONS_LIVRAISONS_CLIENTS.Remove(p));
			//    db.SaveChanges();
			//    db.BONS_LIVRAISONS_CLIENTS.Remove(bl);
			//    db.SaveChanges();
			//}


			return string.Empty;
		}
		public string DeleteFacture(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LIGNES_FACTURES_CLIENTS> liste = db.LIGNES_FACTURES_CLIENTS.Where(f => f.Num_BLP == ID).ToList();
			foreach (LIGNES_FACTURES_CLIENTS ligne in liste)
			{
				if (ligne.Num_BLP != null)
				{
					BONS_LIVRAISONS_PART_CLIENTS BLP = db.BONS_LIVRAISONS_PART_CLIENTS.Where(f => f.ID == ligne.Num_BLP).FirstOrDefault();
					BLP.Etat = false;
					db.SaveChanges();
				}
			}
			List<LIGNES_FACTURES_CLIENTS> list1 = db.LIGNES_FACTURES_CLIENTS.Where(f => f.FACTURE_CLIENT == ID).ToList();
			List<lIGNES_SERVICES_FACTURES> list2 = db.lIGNES_SERVICES_FACTURES.Where(f => f.FACTURE_CLIENT == ID).ToList();
			List<lIGNES_SERVICESSSTRAITANCE_FACTURE> list3 = db.lIGNES_SERVICESSSTRAITANCE_FACTURE.Where(f => f.FACTURE_CLIENT == ID).ToList();
			List<Tracabilite_Facture_Client> list4 = db.Tracabilite_Facture_Client.Where(f => f.Id_Facture == ID).ToList();
			List<LIGNES_CUISINE_FACTURE_CLIENTS> list5 = db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(f => f.FACTURE_CLIENT == ID).ToList();
			if (list5 != null)
			{
				List<LIGNES_DESCRIPTION_ACCESOIRE_Facture> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(f => f.LIGNES_CUISINE_FACTURE_CLIENTS.FACTURE_CLIENT == ID).ToList();
				if (list6 != null)
				{
					db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(p => p.LIGNES_CUISINE_FACTURE_CLIENTS.FACTURE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(p => p.FACTURE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_CUISINE_FACTURE_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			if (list1 != null)
			{
				db.LIGNES_FACTURES_CLIENTS.Where(p => p.FACTURE_CLIENT == ID).ToList().ForEach(p => db.LIGNES_FACTURES_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			if (list2 != null)
			{
				db.lIGNES_SERVICES_FACTURES.Where(p => p.FACTURE_CLIENT == ID).ToList().ForEach(p => db.lIGNES_SERVICES_FACTURES.Remove(p));
				db.SaveChanges();
			}
			if (list3 != null)
			{
				db.lIGNES_SERVICESSSTRAITANCE_FACTURE.Where(p => p.FACTURE_CLIENT == ID).ToList().ForEach(p => db.lIGNES_SERVICESSSTRAITANCE_FACTURE.Remove(p));
				db.SaveChanges();
			}
			if (list4 != null)
			{
				db.Tracabilite_Facture_Client.Where(p => p.Id_Facture == ID).ToList().ForEach(p => db.Tracabilite_Facture_Client.Remove(p));
				db.SaveChanges();
			}


			FACTURES_CLIENTS Devis = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			if (Devis != null)
			{
				db.FACTURES_CLIENTS.Remove(Devis);
				db.SaveChanges();
			}
			return string.Empty;
		}
		public string DeleteCaisse(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<Tracabilite_Caisse_Client> list4 = db.Tracabilite_Caisse_Client.Where(f => f.Id_Caisse == ID).ToList();
			List<LIGNES_CUISINE_CAISSE_CLIENTS> list5 = db.LIGNES_CUISINE_CAISSE_CLIENTS.Where(f => f.CAISSE == ID).ToList();
			if (list5 != null)
			{
				List<LIGNES_DESCRIPTION_ACCESOIRE_CAISSE> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Where(f => f.LIGNES_CUISINE_CAISSE_CLIENTS.CAISSE == ID).ToList();
				if (list6 != null)
				{
					db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Where(p => p.LIGNES_CUISINE_CAISSE_CLIENTS.CAISSE == ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Remove(p));
					db.SaveChanges();
				}
				db.LIGNES_CUISINE_CAISSE_CLIENTS.Where(p => p.CAISSE == ID).ToList().ForEach(p => db.LIGNES_CUISINE_CAISSE_CLIENTS.Remove(p));
				db.SaveChanges();
			}
			db.LIGNES_Caisse.Where(p => p.Caisse == ID).ToList().ForEach(p => db.LIGNES_Caisse.Remove(p));
			db.SaveChanges();
			Caisse Devis = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
			db.Caisse.Remove(Devis);
			db.SaveChanges();
			return string.Empty;
		}
		public JsonResult deleteligneSession(string id)
		{
			int ID1 = int.Parse(id);
			List<RASSalaire1> ListeDesPoduits = (List<RASSalaire1>)Session["RASSalaire"];
			RASSalaire1 rassal = ListeDesPoduits.Where(f => f.idinput == id).FirstOrDefault();
			RASSalaire1 rassal2 = ListeDesPoduits.Where(f => f.ID == ID1).FirstOrDefault();

			decimal resp = 0;
			if (rassal != null)
			{
				resp = rassal.salaire;

				ListeDesPoduits.Remove(rassal);
				Session["RASSalaire"] = ListeDesPoduits;
			}
			else
			{
				if (rassal2 != null)
				{
					resp = rassal2.salaire;

					ListeDesPoduits.Remove(rassal2);
					Session["RASSalaire"] = ListeDesPoduits;
				}

			}
			return Json(resp, JsonRequestBehavior.AllowGet);

		}
		public JsonResult deleteligneSessionhor(string id)
		{
			int ID1 = int.Parse(id);
			List<RASHoraire> ListeDesPoduits = (List<RASHoraire>)Session["RASHoraire"];
			RASHoraire rassal = ListeDesPoduits.Where(f => f.idhor == id).FirstOrDefault();
			RASHoraire rassal2 = ListeDesPoduits.Where(f => f.ID == ID1).FirstOrDefault();

			decimal resp = 0;
			if (rassal != null)
			{
				resp = rassal.honoraire1;

				ListeDesPoduits.Remove(rassal);
				Session["RASHoraire"] = ListeDesPoduits;
			}
			else
			{
				if (rassal2 != null)
				{
					resp = rassal2.honoraire1;

					ListeDesPoduits.Remove(rassal2);
					Session["RASHoraire"] = ListeDesPoduits;
				}

			}
			return Json(resp, JsonRequestBehavior.AllowGet);

		}
		public JsonResult deleteligneSessionloy(string id)
		{
			int ID1 = int.Parse(id);
			List<RASLoyer1> ListeDesPoduits = (List<RASLoyer1>)Session["RASLoyer"];
			RASLoyer1 rassal = ListeDesPoduits.Where(f => f.idloy == id).FirstOrDefault();
			RASLoyer1 rassal2 = ListeDesPoduits.Where(f => f.ID == ID1).FirstOrDefault();

			decimal resp = 0;
			if (rassal != null)
			{
				resp = rassal.loy2;

				ListeDesPoduits.Remove(rassal);
				Session["RASLoyer"] = ListeDesPoduits;
			}
			else
			{
				if (rassal2 != null)
				{
					resp = rassal2.loy2;

					ListeDesPoduits.Remove(rassal2);
					Session["RASLoyer"] = ListeDesPoduits;
				}

			}
			return Json(resp, JsonRequestBehavior.AllowGet);

		}
		public JsonResult DeleteLigneTVA(string parampassed)
		{
			List<RASMache> ListeDesPoduits = new List<RASMache>();
			if (Session["RASMarche"] != null)
			{
				ListeDesPoduits = (List<RASMache>)Session["RASMarche"];
			}
			RASMache ligne = ListeDesPoduits.Where(pr => pr.ID == parampassed).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["RASMarche"] = ListeDesPoduits;
			decimal rs = ligne.Base * (decimal)(1.5 / 100);
			return Json(rs, JsonRequestBehavior.AllowGet);
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
		public string DeleteLineDevisCuisine(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();

			if (Session["CUISINEDevisClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEDevisClient"];
			}
			if (Session["LignesACCessoire"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCessoire"];
				List<LignesACCESSOIRE> ListeDesAccessoire2 = ListeDesAccessoire.Where(pr => pr.IDLIGNESDEScription == ID).ToList();
				if (ListeDesAccessoire2 != null)
				{
					ListeDesAccessoire.Where(p => p.IDLIGNESDEScription == ID).ToList().ForEach(p => ListeDesAccessoire.Remove(p));
				}
			}

			LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["CUISINEDevisClient"] = ListeDesPoduits;
			return string.Empty;
		}
		public string DeleteLineCmdCuisine(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();

			if (Session["CUISINECommandeClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINECommandeClient"];
			}
			if (Session["LignesACCESSOIRECMD"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECMD"];
				List<LignesACCESSOIRE> ListeDesAccessoire2 = ListeDesAccessoire.Where(pr => pr.IDLIGNESDEScription == ID).ToList();
				if (ListeDesAccessoire2 != null)
				{
					ListeDesAccessoire.Where(p => p.IDLIGNESDEScription == ID).ToList().ForEach(p => ListeDesAccessoire.Remove(p));
				}
			}

			LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["CUISINECommandeClient"] = ListeDesPoduits;
			return string.Empty;
		}
		public string DeleteLineblCuisine(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();

			if (Session["CUISINEBLClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEBLClient"];
			}
			if (Session["LignesACCESSOIREBonLiv"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREBonLiv"];
				List<LignesACCESSOIRE> ListeDesAccessoire2 = ListeDesAccessoire.Where(pr => pr.IDLIGNESDEScription == ID).ToList();
				if (ListeDesAccessoire2 != null)
				{
					ListeDesAccessoire.Where(p => p.IDLIGNESDEScription == ID).ToList().ForEach(p => ListeDesAccessoire.Remove(p));
				}
			}

			LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["CUISINEBLClient"] = ListeDesPoduits;
			return string.Empty;
		}
		public string DeleteLineFactureCuisine(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();

			if (Session["CUISINEFACTUREClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINEFACTUREClient"];
			}
			if (Session["LignesACCESSOIREFacture"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCESSOIREFacture"];
				List<LignesACCESSOIRE> ListeDesAccessoire2 = ListeDesAccessoire.Where(pr => pr.IDLIGNESDEScription == ID).ToList();
				if (ListeDesAccessoire2 != null)
				{
					ListeDesAccessoire.Where(p => p.IDLIGNESDEScription == ID).ToList().ForEach(p => ListeDesAccessoire.Remove(p));
				}
			}

			LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["CUISINEFACTUREClient"] = ListeDesPoduits;
			return string.Empty;
		}

		public string DeleteLineCaisseCuisine(string parampassed)
		{
			int ID = int.Parse(parampassed);
			List<LignesCuisine> ListeDesPoduits = new List<LignesCuisine>();
			List<LignesACCESSOIRE> ListeDesAccessoire = new List<LignesACCESSOIRE>();

			if (Session["CUISINECaisseClient"] != null)
			{
				ListeDesPoduits = (List<LignesCuisine>)Session["CUISINECaisseClient"];
			}
			if (Session["LignesACCESSOIRECAISSE"] != null)
			{
				ListeDesAccessoire = (List<LignesACCESSOIRE>)Session["LignesACCESSOIRECAISSE"];
				List<LignesACCESSOIRE> ListeDesAccessoire2 = ListeDesAccessoire.Where(pr => pr.IDLIGNESDEScription == ID).ToList();
				if (ListeDesAccessoire2 != null)
				{
					ListeDesAccessoire.Where(p => p.IDLIGNESDEScription == ID).ToList().ForEach(p => ListeDesAccessoire.Remove(p));
				}
			}

			LignesCuisine ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["CUISINECaisseClient"] = ListeDesPoduits;
			return string.Empty;
		}
		public string DeleteLineService(string parampassed)
		{
			List<LignesServices> ListeDesPoduits = new List<LignesServices>();
			if (Session["LignesServ"] != null)
			{
				ListeDesPoduits = (List<LignesServices>)Session["LignesServ"];
			}
			int ID = int.Parse(parampassed);
			LignesServices ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["LignesServ"] = ListeDesPoduits;

			return string.Empty;
		}
		public string DeleteLineServiceSST(string parampassed)
		{
			List<LignesServicesSSTraitance> ListeDesPoduits = new List<LignesServicesSSTraitance>();
			if (Session["LignesServSST"] != null)
			{
				ListeDesPoduits = (List<LignesServicesSSTraitance>)Session["LignesServSST"];
			}
			int ID = int.Parse(parampassed);
			LignesServicesSSTraitance ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesPoduits.Remove(ligne);
			Session["LignesServSST"] = ListeDesPoduits;

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
		public ActionResult ArticlesdevisCategorie(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string IdAffaireCommercial)
		{

			List<LIGNES_DEVIS_FOURNISSEURS> lignes = db.LIGNES_DEVIS_FOURNISSEURS.Where(fou => fou.Categorie == id).ToList();

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
			ViewBag.IdAffaireCommercial = IdAffaireCommercial;

			return PartialView(lignes);



		}
		public ActionResult Articlesdevis(int id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string IdAffaireCommercial)
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
			ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			return PartialView(lignes_devis_frs.ToList());

		}
		public ActionResult ArticlesdevisMarque(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string IdAffaireCommercial)
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
			ViewBag.IdAffaireCommercial = IdAffaireCommercial;
			return PartialView(lignes);




		}
		public ActionResult ArticlesdevisDes(string id, string Mode, string Code, string Date, string numero, string designation, string modePaiement, string client, string codeClient, string Tiers, string remise, string IdAffaireCommercial)
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
			ViewBag.IdAffaireCommercial = IdAffaireCommercial;
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
			ligne.PRIX_VENTE_HT = (decimal)ligne2.PRIX_UNITAIRE_HT - (((decimal)ligne2.PRIX_UNITAIRE_HT * ligne.REMISE) / 100);
			//ligne.PTHT = (decimal)ligne2.TOTALE_HT;
			ligne.PTHT = ligne.PRIX_VENTE_HT;
			ligne.TVA = (int)ligne2.TVA;
			ligne.TTC = ligne.PTHT + (ligne.PTHT * ligne.TVA) / 100;
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
			if (Session["SoclogoId"] == null)
			{
				RedirectToAction("Login", "Societes");
			}
			int idste1 = (int)Session["SoclogoId"];
			string Numero1;

			db.Configuration.ProxyCreationEnabled = false;
			//if (Mode == "Edit")
			//{
			int idste = (int)Session["SoclogoId"];
			PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 1).FirstOrDefault();
			if (PrefixeTable == null)
			{
				//string[] code = num.Split('/');
				//int y = int.Parse(code[1]);
				//string an = d.Year.ToString();
				//string[] an1 = an.Split('0');
				//int an2 = int.Parse(an1[1]);
				//if (an2 == y)
				//{
				//    Numero1 = num;
				//}
				//else
				//{
				int Max = 0;
				//if (db.DEVIS_CLIENTS.ToList().Count != 0)
				//{
				//    Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
				//}
				Max++;

				Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
				while (db.DEVIS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero1))
				{
					Max++;
					Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
				//List<DEVIS_CLIENTS> frs = db.DEVIS_CLIENTS.ToList();
				//foreach (DEVIS_CLIENTS f in frs)
				//{
				//    string[] con = f.CODE.Split('C');
				//    string[] con11 = con[1].Split('/');
				//    int con1 = int.Parse(con11[0]);
				//    if (con1 == Max)
				//    {
				//        Nb++;
				//    }

				//}
				//if (Nb > 0)
				//{
				//    Max++;

				//    Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");
				//}
				//else
				//{
				//    Numero1 = "DVC" + Max.ToString("0000") + "/" + d.ToString("yy");

				//}


			}
			else
			{
				int Max = 0;
				//if (db.DEVIS_CLIENTS.ToList().Count != 0)
				//{
				//    Max = db.DEVIS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
				//}
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
					if (c == 'M')
					{
						count1 += c;
					}
				}
				string date1 = d.ToString(count);
				string date2 = d.ToString(count1);
				Numero1 = numPre.Replace(count, date1);
				Numero1 = Numero1.Replace(count1, date2);
			}
			return Json(Numero1, JsonRequestBehavior.AllowGet);
		}
		public string validateBonLivraison(string parampassed, string type)
		{
			string var = "";
			int ID = int.Parse(parampassed);
			//List<string> ListArtStockInf = new List<string>();
			BONS_LIVRAISONS_CLIENTS Bonlivraison = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_BONS_LIVRAISONS_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
			List<LIGNES_DESCRIPTION_ACCESOIRE_BL> listeAcc = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(cmd => cmd.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.BONLIVRAISON_CLIENT == ID).ToList();
			int varTest = 0;
			int varTest2 = 0;
			int iddevisbl = (int)Bonlivraison.COMMANDES_CLIENTS.DEVIS_CLIENT;
			if (liste.Count != 0)
			{
				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in liste)
				{
					LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == iddevisbl && f.Libelle_Prd == ligne.Libelle_Prd).FirstOrDefault();
					int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
					int frs = db.DEVIS_FOURNISSEURS.Where(f => f.ID == iddevis).FirstOrDefault().FOURNISSEUR;

					Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.Libelle_Prd && pr.Stock != 0).FirstOrDefault();

					List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.Fournisseur == frs && f.Reference == ligne.Libelle_Prd && f.Quantite > 0).ToList();
					List<Détails_Articles> listArtParFrs1 = db.Détails_Articles.Where(f => f.Fournisseur != frs && f.Reference == ligne.Libelle_Prd && f.Quantite > 0).ToList();
					double QteLiv = (double)ligne.QUANTITE/* - (double)ligne.QTERES*/;
					if (Produit == null)
					{
						varTest2++;

					}

				}
			}

			if (listeAcc.Count != 0)
			{

				foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL ligne in listeAcc)
				{
					Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART).FirstOrDefault();
					if (p != null)
					{
						double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.QuantiteCAISSON;
						if (qteAcc > p.Stock)
						{
							varTest++;
						}

					}
				}

			}
			if (varTest != 0 || varTest2 != 0)
			{
				var = "NO";
			}
			else
			{
				var = "Yes";
			}

			if (var == "Yes")
			{
				if (liste.Count != 0)
				{
					foreach (LIGNES_BONS_LIVRAISONS_CLIENTS ligne in liste)
					{
						LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == iddevisbl && f.Libelle_Prd == ligne.Libelle_Prd).FirstOrDefault();
						int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
						int frs = db.DEVIS_FOURNISSEURS.Where(f => f.ID == iddevis).FirstOrDefault().FOURNISSEUR;

						Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.Libelle_Prd && pr.Stock != 0).FirstOrDefault();

						List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.Fournisseur == frs && f.Reference == ligne.Libelle_Prd && f.Quantite > 0).ToList();
						List<Détails_Articles> listArtParFrs1 = db.Détails_Articles.Where(f => f.Fournisseur != frs && f.Reference == ligne.Libelle_Prd && f.Quantite > 0).ToList();
						double stock = 0;
						double QteLiv = (double)ligne.QUANTITE/* - (double)ligne.QTERES*/;
						if (Produit != null)
						{
							stock = (double)Produit.Stock;
						}

						double stockReserve = 0;
						double stockTot = 0;
						double stockDétails = 0;
						foreach (Détails_Articles détails in listArtParFrs)
						{
							stockDétails += (double)détails.Quantite;
						}
						if (stock >= QteLiv)
						{
							Produit.Stock -= QteLiv;
							db.SaveChanges();
							if (listArtParFrs.Count != 0)
							{
								foreach (Détails_Articles p in listArtParFrs)
								{
									stockTot = stockTot + (double)p.Quantite;
									p.Quantite = 0;
									if (QteLiv < stockTot)
									{
										double Qteres = (double)(stockTot - QteLiv);
										stockTot -= Qteres;
										//stock += stockTot;
										p.Quantite += (decimal)Qteres;
										db.SaveChanges();
										break;
									}
									if (stockTot == QteLiv)
									{
										break;
									}
								}
								if (stockDétails < QteLiv)
								{
									if (listArtParFrs1.Count != 0)
									{
										double Qtedéf = (double)QteLiv - stockDétails;
										stockDétails = 0;
										foreach (Détails_Articles détails in listArtParFrs1)
										{
											stockDétails += (double)détails.Quantite;
										}

										if (stockDétails >= Qtedéf)
										{
											foreach (Détails_Articles p in listArtParFrs1)
											{
												stockReserve = stockReserve + (double)p.Quantite;
												p.Quantite = 0;
												if (Qtedéf < stockReserve)
												{
													double Qteres = stockReserve - Qtedéf;
													stockReserve -= Qteres;
													stock += stockReserve;
													p.Quantite += (decimal)Qteres;
													db.SaveChanges();


												}
												if (stockReserve == Qtedéf)
												{
													break;

												}

											}
										}

									}
								}
								else
								{
									if (listArtParFrs1.Count != 0)
									{

										foreach (Détails_Articles détails in listArtParFrs)
										{
											stockDétails += (double)détails.Quantite;
										}
										if (stockDétails >= QteLiv)
										{
											double Qtedéf = (double)QteLiv - stock;

											foreach (Détails_Articles p in listArtParFrs1)
											{
												stockReserve = stockReserve + (double)p.Quantite;
												p.Quantite = 0;
												if (Qtedéf < stockReserve)
												{
													double Qteres = stockReserve - Qtedéf;
													stockReserve -= Qteres;

													p.Quantite += (decimal)Qteres;
													db.SaveChanges();

												}
												if (stockReserve == Qtedéf)
												{

													break;

												}

											}
										}

									}

								}

							}



						}
					}
				}

				if (listeAcc.Count != 0)
				{
					double stockTot = 0;
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL ligne in listeAcc)
					{
						Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART).FirstOrDefault();
						if (p != null)
						{
							double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.QuantiteCAISSON;
							if (qteAcc < p.Stock)
							{
								p.Stock -= qteAcc;
								db.SaveChanges();

								List<Détails_Articles> listArtParFrsArticle = db.Détails_Articles.Where(f => f.IdPrixAchat == p.Product_ID).ToList();
								if (listArtParFrsArticle.Count() != 0)
								{
									foreach (Détails_Articles desp in listArtParFrsArticle)
									{
										stockTot = stockTot + (double)desp.Quantite;
										desp.Quantite = 0;
										if (qteAcc < stockTot)
										{
											double Qteres = (double)(stockTot - qteAcc);
											stockTot -= Qteres;
											//stock += stockTot;
											desp.Quantite += (decimal)Qteres;
											db.SaveChanges();
											break;
										}
										if (stockTot == qteAcc)
										{
											break;
										}

									}

								}

							}
						}
					}

				}
				Bonlivraison.VALIDER = true;
				Bonlivraison.Type = Boolean.Parse(type);
				db.SaveChanges();
				Tracabilite_bl_Client Tracabilite_bl_Client = new Tracabilite_bl_Client();
				Tracabilite_bl_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_bl_Client.Personnel = personnel1;
					Tracabilite_bl_Client.Valide_Par = true;
					Tracabilite_bl_Client.Ajoute_Par = false;
					Tracabilite_bl_Client.Modifie_Par = false;


				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_bl_Client.Societe = IdSoc;
				Tracabilite_bl_Client.Id_BL = Bonlivraison.ID;
				db.Tracabilite_bl_Client.Add(Tracabilite_bl_Client);
				db.SaveChanges();

				var = string.Empty;
			}


			return var;

		}
		public string validateBonLivraisonPar(string parampassed)
		{
			string var = "";
			int count = 0;
			int ID = int.Parse(parampassed);
			//List<string> ListArtStockInf = new List<string>();
			BONS_LIVRAISONS_PART_CLIENTS Bonlivraison = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			BONS_LIVRAISONS_CLIENTS BL = db.BONS_LIVRAISONS_CLIENTS.Where(f => f.ID == Bonlivraison.IDBLC).FirstOrDefault();
			List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> liste = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_PART_CLIENT == Bonlivraison.IDBLC).ToList();
			int iddevisbl = (int)BL.COMMANDES_CLIENTS.DEVIS_CLIENT;
			foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS ligne in liste)
			{
				LIGNES_DEVIS_CLIENTS ligneDevis = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == iddevisbl && f.Libelle_Prd == ligne.Libelle_Prd).FirstOrDefault();
				int iddevis = (int)db.LIGNES_DEVIS_FOURNISSEURS.Where(f => f.ID == ligneDevis.Art_Devis_Frs).FirstOrDefault().DEVIS_CLIENT;
				int frs = db.DEVIS_FOURNISSEURS.Where(f => f.ID == iddevis).FirstOrDefault().FOURNISSEUR;

				Prix_Achat Produit = db.Prix_Achat.Where(pr => pr.Libelle == ligne.Libelle_Prd && pr.Stock != 0).FirstOrDefault();
				//Prix_Achat prixAchat = new Prix_Achat();

				List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.Fournisseur == frs && f.Reference == ligne.Libelle_Prd).ToList();
				List<Détails_Articles> listArtParFrs1 = db.Détails_Articles.Where(f => f.Fournisseur != frs && f.Reference == ligne.Libelle_Prd && f.Quantite != 0).ToList();
				double stock = 0;
				double QteLiv = (double)ligne.QUANTITE;
				if (Produit != null)
				{
					stock = (double)Produit.Stock;
				}
				else
				{
					var = "NO";
				}
				double stockReserve = 0;
				double stockTot = 0;
				double stockDétails = 0;
				foreach (Détails_Articles détails in listArtParFrs)
				{
					stockDétails += (double)détails.Quantite;
				}
				if (stock >= QteLiv)
				{
					Produit.Stock -= QteLiv;
					if (listArtParFrs.Count != 0)
					{
						var = "Yes";
						count++;

						db.SaveChanges();
						foreach (Détails_Articles p in listArtParFrs)
						{
							stockTot = stockTot + (double)p.Quantite;
							p.Quantite = 0;
							if (QteLiv < stockTot)
							{
								double Qteres = (double)(stockTot - QteLiv);
								stockTot -= Qteres;
								//stock += stockTot;
								p.Quantite += (decimal)Qteres;
								db.SaveChanges();
								break;
							}
							if (stockTot == QteLiv)
							{
								break;
							}
							//Produit.Stock -= ((double)QteLiv - (double)ligne.QTERES);
						}
						if (stockDétails < QteLiv)
						{
							if (listArtParFrs1.Count != 0)
							{
								double Qtedéf = (double)QteLiv - stockDétails;
								stockDétails = 0;
								foreach (Détails_Articles détails in listArtParFrs1)
								{
									stockDétails += (double)détails.Quantite;
								}

								if (stockDétails >= Qtedéf)
								{
									foreach (Détails_Articles p in listArtParFrs1)
									{
										stockReserve = stockReserve + (double)p.Quantite;
										p.Quantite = 0;
										if (Qtedéf < stockReserve)
										{
											double Qteres = stockReserve - Qtedéf;
											stockReserve -= Qteres;
											stock += stockReserve;
											p.Quantite += (decimal)Qteres;
											db.SaveChanges();


										}
										if (stockReserve == Qtedéf)
										{
											var = "Yes";
											break;

										}

									}
								}
								else
								{
									var = "NO";
								}

							}
							else
							{
								var = "NO";
							}
						}
					}
					else
					{
						if (listArtParFrs1.Count != 0)
						{
							var = "Yes";
							foreach (Détails_Articles détails in listArtParFrs)
							{
								stockDétails += (double)détails.Quantite;
							}
							if (stockDétails >= QteLiv)
							{
								double Qtedéf = (double)QteLiv - stock;

								foreach (Détails_Articles p in listArtParFrs1)
								{
									stockReserve = stockReserve + (double)p.Quantite;
									p.Quantite = 0;
									if (Qtedéf < stockReserve)
									{
										double Qteres = stockReserve - Qtedéf;
										stockReserve -= Qteres;
										//stock += stockReserve;
										p.Quantite += (decimal)Qteres;
										db.SaveChanges();

									}
									if (stockReserve == Qtedéf)
									{
										var = "Yes";
										break;

									}

								}
							}
							else
							{
								var = "NO";
							}
						}
						else
						{
							var = "NO";
						}
					}

				}

				else
				{
					var = "NO";

				}


			}

			if (var == "Yes")
			{
				var = string.Empty;
			}
			else
			{
				var = "NO";
			}

			return var;

		}

		public string validateFacture(string parampassed)
		{
			int ID = int.Parse(parampassed);
			string var = "";
			int varTest = 0;
			int varTest2 = 0;


			FACTURES_CLIENTS facture = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_DESCRIPTION_ACCESOIRE_Facture> listeAcc = db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(cmd => cmd.LIGNES_CUISINE_FACTURE_CLIENTS.FACTURE_CLIENT == ID).ToList();
			List<LIGNES_FACTURES_CLIENTS> lignes = db.LIGNES_FACTURES_CLIENTS.Where(f => f.FACTURE_CLIENT == ID).ToList();

			if (facture.BON_LIVRAISON_CLIENT == null)
			{


				if (lignes.Count != 0)
				{
					foreach (LIGNES_FACTURES_CLIENTS ligne in lignes)
					{
						Prix_Achat Produit = db.Prix_Achat.Where(f => f.Product_ID == ligne.Prix_achat && f.Stock > 0).FirstOrDefault();
						if (Produit == null)
						{
							varTest2++;
						}
						//List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.IdPrixAchat == ligne.Prix_achat).ToList();
						if (Produit.Stock < ligne.QUANTITE)
						{
							varTest2++;
						}
					}
				}
				if (listeAcc.Count != 0)
				{

					foreach (LIGNES_DESCRIPTION_ACCESOIRE_Facture ligne in listeAcc)
					{
						Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART && f.Stock > 0).FirstOrDefault();
						if (p != null)
						{
							double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_FACTURE_CLIENTS.QuantiteCAISSON;
							if (qteAcc > p.Stock)
							{
								varTest++;
							}

						}
					}

				}

			}
			if (varTest != 0 || varTest2 != 0)
			{
				var = "NO";
			}
			else
			{
				var = "Yes";
			}
			if (var == "Yes")
			{
				if (lignes.Count != 0)
				{
					foreach (LIGNES_FACTURES_CLIENTS ligne in lignes)
					{
						Prix_Achat Produit = db.Prix_Achat.Where(f => f.Product_ID == ligne.Prix_achat && f.Stock != 0).FirstOrDefault();
						//if (Produit == null)
						//{
						//    varTest2++;
						//}
						List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.IdPrixAchat == ligne.Prix_achat && f.Quantite > 0).ToList();
						if (Produit.Stock >= ligne.QUANTITE)
						{
							//varTest2++;
							//var = "Yes";
							Produit.Stock -= ligne.QUANTITE;
							db.SaveChanges();
							double stockTot = 0;
							if (listArtParFrs.Count != 0)
							{
								foreach (Détails_Articles p in listArtParFrs)
								{
									if (p.Quantite > (decimal)ligne.QUANTITE)
									{
										p.Quantite -= (decimal)ligne.QUANTITE;
										db.SaveChanges();
										break;
									}
									else
									{
										stockTot = stockTot + (double)p.Quantite;
										p.Quantite = 0;
										db.SaveChanges();
										if (ligne.QUANTITE < stockTot)
										{
											double Qteres = (double)(stockTot - ligne.QUANTITE);
											stockTot -= Qteres;
											//stock += stockTot;
											p.Quantite += (decimal)Qteres;
											db.SaveChanges();
											break;
										}
										if (stockTot == ligne.QUANTITE)
										{
											break;
										}
									}
								}
							}
						}
						//else
						//{
						//    var = "NO";
						//}
						db.SaveChanges();
					}
				}
				if (listeAcc.Count != 0)
				{
					double stockTot = 0;
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_Facture ligne in listeAcc)
					{
						Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART && f.Stock > 0).FirstOrDefault();
						if (p != null)
						{
							double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_FACTURE_CLIENTS.QuantiteCAISSON;
							if (qteAcc < p.Stock)
							{
								p.Stock -= qteAcc;
								db.SaveChanges();

								List<Détails_Articles> listArtParFrsArticle = db.Détails_Articles.Where(f => f.IdPrixAchat == p.Product_ID && f.Quantite > 0).ToList();
								if (listArtParFrsArticle.Count() != 0)
								{
									foreach (Détails_Articles desp in listArtParFrsArticle)
									{
										stockTot = stockTot + (double)desp.Quantite;
										desp.Quantite = 0;
										if (qteAcc < stockTot)
										{
											double Qteres = (double)(stockTot - qteAcc);
											stockTot -= Qteres;
											//stock += stockTot;
											desp.Quantite += (decimal)Qteres;
											db.SaveChanges();
											break;
										}
										if (stockTot == qteAcc)
										{
											break;
										}

									}

								}

							}
						}
					}

				}
				facture.VALIDER = true;
				db.SaveChanges();
			}

			Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
			Tracabilite_Facture_Client.Date = DateTime.Today;
			if (Session["ID"] != null)
			{
				string personnel = (string)Session["ID"];
				int personnel1 = int.Parse(personnel);
				Tracabilite_Facture_Client.Personnel = personnel1;
				Tracabilite_Facture_Client.Valider_Par = true;


			}
			string soc = (string)Session["Soclogo"];
			int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
			Tracabilite_Facture_Client.Societe = IdSoc;
			Tracabilite_Facture_Client.Id_Facture = facture.ID;
			db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
			db.SaveChanges();
			return string.Empty;
		}
		public string validateCaisse(string parampassed)
		{
			int ID = int.Parse(parampassed);
			string var = "";
			int varTest = 0;
			int varTest2 = 0;

			Caisse facture = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_DESCRIPTION_ACCESOIRE_CAISSE> listeAcc = db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Where(cmd => cmd.LIGNES_CUISINE_CAISSE_CLIENTS.CAISSE == ID).ToList();
			List<LIGNES_Caisse> lignes = db.LIGNES_Caisse.Where(f => f.Caisse == ID).ToList();

			if (facture.BON_LIVRAISON_CLIENT == null)
			{
				if (lignes.Count != 0)
				{
					foreach (LIGNES_Caisse ligne in lignes)
					{
						Prix_Achat Produit = db.Prix_Achat.Where(f => f.Product_ID == ligne.Prix_achat && f.Stock > 0).FirstOrDefault();
						if (Produit == null)
						{
							varTest2++;
						}
						//List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.IdPrixAchat == ligne.Prix_achat).ToList();
						if (Produit.Stock < ligne.QUANTITE)
						{
							varTest2++;
						}
					}
				}
				if (listeAcc.Count != 0)
				{

					foreach (LIGNES_DESCRIPTION_ACCESOIRE_CAISSE ligne in listeAcc)
					{
						Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART && f.Stock > 0).FirstOrDefault();
						if (p != null)
						{
							double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_CAISSE_CLIENTS.QuantiteCAISSON;
							if (qteAcc > p.Stock)
							{
								varTest++;
							}

						}
					}

				}

			}
			if (varTest != 0 || varTest2 != 0)
			{
				var = "NO";
			}
			else
			{
				var = "Yes";
			}
			if (var == "Yes")
			{
				if (lignes.Count != 0)
				{
					foreach (LIGNES_Caisse ligne in lignes)
					{
						Prix_Achat Produit = db.Prix_Achat.Where(f => f.Product_ID == ligne.Prix_achat && f.Stock != 0).FirstOrDefault();
						//if (Produit == null)
						//{
						//    varTest2++;
						//}
						List<Détails_Articles> listArtParFrs = db.Détails_Articles.Where(f => f.IdPrixAchat == ligne.Prix_achat && f.Quantite > 0).ToList();
						if (Produit.Stock >= ligne.QUANTITE)
						{
							//varTest2++;
							//var = "Yes";
							Produit.Stock -= ligne.QUANTITE;
							db.SaveChanges();
							double stockTot = 0;
							if (listArtParFrs.Count != 0)
							{
								foreach (Détails_Articles p in listArtParFrs)
								{
									if (p.Quantite > (decimal)ligne.QUANTITE)
									{
										p.Quantite -= (decimal)ligne.QUANTITE;
										db.SaveChanges();
										break;
									}
									else
									{
										stockTot = stockTot + (double)p.Quantite;
										p.Quantite = 0;
										db.SaveChanges();
										if (ligne.QUANTITE < stockTot)
										{
											double Qteres = (double)(stockTot - ligne.QUANTITE);
											stockTot -= Qteres;
											//stock += stockTot;
											p.Quantite += (decimal)Qteres;
											db.SaveChanges();
											break;
										}
										if (stockTot == ligne.QUANTITE)
										{
											break;
										}
									}
								}
							}
						}
						//else
						//{
						//    var = "NO";
						//}
						db.SaveChanges();
					}
				}
				if (listeAcc.Count != 0)
				{
					double stockTot = 0;
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_CAISSE ligne in listeAcc)
					{
						Prix_Achat p = db.Prix_Achat.Where(f => f.Product_ID == ligne.ID_ART && f.Stock > 0).FirstOrDefault();
						if (p != null)
						{
							double qteAcc = (double)ligne.QTE * (double)ligne.LIGNES_CUISINE_CAISSE_CLIENTS.QuantiteCAISSON;
							if (qteAcc < p.Stock)
							{
								p.Stock -= qteAcc;
								db.SaveChanges();

								List<Détails_Articles> listArtParFrsArticle = db.Détails_Articles.Where(f => f.IdPrixAchat == p.Product_ID && f.Quantite > 0).ToList();
								if (listArtParFrsArticle.Count() != 0)
								{
									foreach (Détails_Articles desp in listArtParFrsArticle)
									{
										stockTot = stockTot + (double)desp.Quantite;
										desp.Quantite = 0;
										if (qteAcc < stockTot)
										{
											double Qteres = (double)(stockTot - qteAcc);
											stockTot -= Qteres;
											//stock += stockTot;
											desp.Quantite += (decimal)Qteres;
											db.SaveChanges();
											break;
										}
										if (stockTot == qteAcc)
										{
											break;
										}

									}

								}

							}
						}
					}

				}
				facture.VALIDER = true;
				db.SaveChanges();
			}


			Tracabilite_Caisse_Client Tracabilite_Facture_Client = new Tracabilite_Caisse_Client();
			Tracabilite_Facture_Client.Date = DateTime.Today;
			if (Session["ID"] != null)
			{
				string personnel = (string)Session["ID"];
				int personnel1 = int.Parse(personnel);
				Tracabilite_Facture_Client.Personnel = personnel1;
				Tracabilite_Facture_Client.Valider_Par = true;


			}
			string soc = (string)Session["Soclogo"];
			int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
			Tracabilite_Facture_Client.Societe = IdSoc;
			Tracabilite_Facture_Client.Id_Caisse = facture.ID;
			db.Tracabilite_Caisse_Client.Add(Tracabilite_Facture_Client);
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
				Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
				Tracabilite_Facture_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_Facture_Client.Personnel = personnel1;
					Tracabilite_Facture_Client.Paiement = true;


				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_Facture_Client.Societe = IdSoc;
				Tracabilite_Facture_Client.Id_Facture = Facture.ID;
				db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
				db.SaveChanges();
			}
			db.SaveChanges();
			return string.Empty;
		}
		public string PayeCaisse(string parampassed)
		{
			int ID = int.Parse(parampassed);
			Caisse Facture = db.Caisse.Where(cmd => cmd.ID == ID).FirstOrDefault();
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
			List<LIGNES_CUISINE_DEVIS_CLIENTS> Listecuisine = db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(cmd => cmd.DEVIS_CLIENT == ID).ToList();

			COMMANDES_CLIENTS NewElement = new COMMANDES_CLIENTS();
			string Numero = string.Empty;
			DateTime d = Element.DATE;
			int Max = 0;
			if (Session["SoclogoId"] == null)
			{
				RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 3).FirstOrDefault();
			if (PrefixeTable == null)
			{
				//if (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
				//}
				Max++;

				Numero = "CDC" + Max.ToString("0000") + "/" + d.ToString("yy");
				while (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "CDC" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
			}
			else
			{
				//if (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
				//}
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
				while (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
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
			NewElement.CODE = Numero;
			NewElement.DATE = Element.DATE;
			NewElement.Designation = Element.Designation;
			NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
			NewElement.Designation = Element.Designation;
			NewElement.CLIENT = Element.CLIENT;
			NewElement.Societes = (int)Element.Societes;
			//NewElement.Tiers = (int)Element.Tiers;

			NewElement.TTVA = Element.TTVA;
			NewElement.NHT = Element.NHT;
			NewElement.TTC = Element.TTC;
			NewElement.TNET = Element.TNET;
			NewElement.THT = Element.THT;
			NewElement.VALIDER = false;
			NewElement.REMISE = Element.REMISE;
			NewElement.DEVIS_CLIENT = Element.ID;
			NewElement.DEVIS_CLIENTS = Element;
			NewElement.CLIENTS = Element.CLIENTS;
			NewElement.SocieteLogo = Element.SocieteLogo;
			db.COMMANDES_CLIENTS.Add(NewElement);
			db.SaveChanges();
			Tracabilite_Commande_Client Tracabilite_Commande_Client = new Tracabilite_Commande_Client();
			Tracabilite_Commande_Client.Date = DateTime.Today;
			if (Session["ID"] != null)
			{
				string personnel = (string)Session["ID"];
				int personnel1 = int.Parse(personnel);
				Tracabilite_Commande_Client.Personnel = personnel1;
				Tracabilite_Commande_Client.Ajoute_Par = true;
				Tracabilite_Commande_Client.Modifie_Par = false;

			}
			string soc = (string)Session["Soclogo"];
			int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
			Tracabilite_Commande_Client.Societe = IdSoc;
			Tracabilite_Commande_Client.Id_Commande = NewElement.ID;
			db.Tracabilite_Commande_Client.Add(Tracabilite_Commande_Client);
			db.SaveChanges();

			foreach (LIGNES_DEVIS_CLIENTS Ligne in Liste)
			{
				LIGNES_COMMANDES_CLIENTS NewLine = new LIGNES_COMMANDES_CLIENTS();
				NewLine.Prix_achat = (int)Ligne.Art_Devis_Frs;
				//NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
				NewLine.Libelle_Prd = Ligne.Libelle_Prd;

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
			foreach (LIGNES_CUISINE_DEVIS_CLIENTS Ligne in Listecuisine)
			{
				LIGNES_CUISINE_COMMANDE_CLIENTS lignecuisine = new LIGNES_CUISINE_COMMANDE_CLIENTS();
				lignecuisine.SSCAISSON = Ligne.SSCAISSON;
				lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
				lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
				lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
				lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
				lignecuisine.ACC = Ligne.ACC;
				lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
				lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
				lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
				lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
				lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
				lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
				lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
				lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
				lignecuisine.TVACUISINE = Ligne.TVACUISINE;
				lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
				lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
				lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
				lignecuisine.COMMANDE_CLIENT = NewElement.ID;
				db.LIGNES_CUISINE_COMMANDE_CLIENTS.Add(lignecuisine);
				db.SaveChanges();
				List<LIGNES_DESCRIPTION_ACCESOIRE> LignesACCESSOIRE = db.LIGNES_DESCRIPTION_ACCESOIRE.Where(f => f.ID_LigneDC == Ligne.ID).ToList();
				foreach (LIGNES_DESCRIPTION_ACCESOIRE lig in LignesACCESSOIRE)
				{
					LIGNES_DESCRIPTION_ACCESOIRE_CMD des = new LIGNES_DESCRIPTION_ACCESOIRE_CMD();
					des.Designation = lig.Designation;
					des.ID_SSCAT = lig.ID_SSCAT;
					des.ID_ART = lig.ID_ART;
					des.PUHT = lig.PUHT;
					des.PTHT = lig.PTHT;
					des.TVA = lig.TVA;
					des.PTTC = lig.PTTC;
					des.QTE = lig.QTE;
					des.ID_LigneCMD = lignecuisine.ID;
					db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Add(des);
					db.SaveChanges();
				}
			}

			return NewElement.ID.ToString();
		}
		public string CommandeVersBonLivraison(string parampassed)
		{
			int ID = int.Parse(parampassed);
			COMMANDES_CLIENTS Element = db.COMMANDES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			List<LIGNES_COMMANDES_CLIENTS> Liste = db.LIGNES_COMMANDES_CLIENTS.Where(cmd => cmd.COMMANDE_CLIENT == ID).ToList();
			List<LIGNES_CUISINE_COMMANDE_CLIENTS> Listecuisine = db.LIGNES_CUISINE_COMMANDE_CLIENTS.Where(cmd => cmd.COMMANDE_CLIENT == ID).ToList();

			BONS_LIVRAISONS_CLIENTS NewElement = new BONS_LIVRAISONS_CLIENTS();
			string Numero = string.Empty;
			DateTime d = Element.DATE;

			int Max = 0;
			if (Session["SoclogoId"] == null)
			{
				RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 5).FirstOrDefault();
			if (PrefixeTable == null)
			{

				//if (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
				//}
				Max++;
				Numero = "BL" + Max.ToString("0000") + "/" + d.ToString("yy");
				while (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "BL" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
			}
			else
			{
				//if (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
				//{
				//    Max = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
				//}
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
				while (db.BONS_LIVRAISONS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
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
			NewElement.CODE = Numero;
			NewElement.DATE = Element.DATE;
			NewElement.Designation = Element.Designation;
			NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
			NewElement.Designation = Element.Designation;
			NewElement.Type = true;
			NewElement.CLIENT = Element.CLIENT;
			NewElement.Societes = (int)Element.Societes;
			//NewElement.Tiers = (int)Element.Tiers;
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
			NewElement.SocieteLogo = Element.SocieteLogo;
			db.BONS_LIVRAISONS_CLIENTS.Add(NewElement);
			db.SaveChanges();
			Tracabilite_bl_Client Tracabilite_bl_Client = new Tracabilite_bl_Client();
			Tracabilite_bl_Client.Date = DateTime.Today;
			if (Session["ID"] != null)
			{
				string personnel = (string)Session["ID"];
				int personnel1 = int.Parse(personnel);
				Tracabilite_bl_Client.Personnel = personnel1;
				Tracabilite_bl_Client.Ajoute_Par = true;
				Tracabilite_bl_Client.Modifie_Par = false;

			}
			string soc = (string)Session["Soclogo"];
			int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
			Tracabilite_bl_Client.Societe = IdSoc;
			Tracabilite_bl_Client.Id_BL = NewElement.ID;
			db.Tracabilite_bl_Client.Add(Tracabilite_bl_Client);
			db.SaveChanges();
			foreach (LIGNES_COMMANDES_CLIENTS Ligne in Liste)
			{
				if (Liste != null)
				{
					LIGNES_BONS_LIVRAISONS_CLIENTS NewLine = new LIGNES_BONS_LIVRAISONS_CLIENTS();
					NewLine.Prix_achat = (int)Ligne.Prix_achat;
					//NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
					NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
					NewLine.Libelle_Prd = Ligne.Libelle_Prd;

					NewLine.Marque = Ligne.Marque;
					NewLine.Devise = Ligne.Devise;
					NewLine.Unite = Ligne.Unite;
					NewLine.Categorie = Ligne.Categorie;
					NewLine.Sous_Categorie = Ligne.Sous_Categorie;
					NewLine.QUANTITE = Ligne.QUANTITE;
					//QUANTITELIV=QUANTITE
					NewLine.STOCK = Ligne.STOCK;
					NewLine.QTERES = Ligne.QUANTITE;
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
			}
			foreach (LIGNES_CUISINE_COMMANDE_CLIENTS Ligne in Listecuisine)
			{
				LIGNES_CUISINE_BONLIVRAISON_CLIENTS lignecuisine = new LIGNES_CUISINE_BONLIVRAISON_CLIENTS();
				lignecuisine.SSCAISSON = Ligne.SSCAISSON;
				lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
				lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
				lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
				lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
				lignecuisine.ACC = Ligne.ACC;
				lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
				lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
				lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
				lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
				lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
				lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
				lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
				lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
				lignecuisine.TVACUISINE = Ligne.TVACUISINE;
				lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
				lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
				lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
				lignecuisine.BONLIVRAISON_CLIENT = NewElement.ID;
				db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Add(lignecuisine);
				db.SaveChanges();
				List<LIGNES_DESCRIPTION_ACCESOIRE_CMD> LignesACCESSOIRE = db.LIGNES_DESCRIPTION_ACCESOIRE_CMD.Where(f => f.ID_LigneCMD == Ligne.ID).ToList();
				foreach (LIGNES_DESCRIPTION_ACCESOIRE_CMD lig in LignesACCESSOIRE)
				{
					LIGNES_DESCRIPTION_ACCESOIRE_BL des = new LIGNES_DESCRIPTION_ACCESOIRE_BL();
					des.Designation = lig.Designation;
					des.ID_SSCAT = lig.ID_SSCAT;
					des.ID_ART = lig.ID_ART;
					des.PUHT = lig.PUHT;
					des.PTHT = lig.PTHT;
					des.TVA = lig.TVA;
					des.PTTC = lig.PTTC;
					des.QTE = lig.QTE;
					des.ID_LigneBL = lignecuisine.ID;
					db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Add(des);
					db.SaveChanges();
				}
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
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> Listecuisine = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(cmd => cmd.BONLIVRAISON_CLIENT == ID).ToList();

				FACTURES_CLIENTS NewElement = new FACTURES_CLIENTS();
				string Numero = string.Empty;
				DateTime d = Element.DATE;
				if (Session["SoclogoId"] == null)
				{
					RedirectToAction("Login", "Societes");
				}
				int idste = (int)Session["SoclogoId"];
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 7).FirstOrDefault();
				int Max = 0;
				if (PrefixeTable == null)
				{
					//if (db.FACTURES_CLIENTS.Where(f=>f.Societes==idste).ToList().Count != 0)
					//{
					//    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}
					Max++;
					Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;

						Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
				}
				else
				{
					//if (db.FACTURES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}

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
					while (db.FACTURES_CLIENTS.Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						string PRF1 = PrefixeTable.Prefixe;
						string numPre1 = PRF.Replace("0000", Max.ToString("0000"));
						string count2 = "";
						string count3 = "";
						foreach (char c in numPre)
						{
							if (c == 'y')
							{
								count2 += c;
							}
						}
						foreach (char c in numPre)
						{
							if (c == 'm')
							{
								count3 += c;
							}
						}
						string date11 = d.ToString(count2);
						string date22 = d.ToString(count3);
						Numero = numPre.Replace(count, date1);
						Numero = Numero.Replace(count1, date2);
					}
				}
				NewElement.CODE = Numero;
				NewElement.DATE = Element.DATE;
				NewElement.Designation = Element.Designation;
				NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
				NewElement.Designation = Element.Designation;
				NewElement.CLIENT = Element.CLIENT;
				NewElement.Societes = Element.Societes;
				//NewElement.Tiers = Element.Tiers;
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
				Tracabilite_Facture_Client Tracabilite_Facture_Client = new Tracabilite_Facture_Client();
				Tracabilite_Facture_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_Facture_Client.Personnel = personnel1;
					Tracabilite_Facture_Client.Ajoute_Par = true;
					Tracabilite_Facture_Client.Modifie_Par = false;

				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_Facture_Client.Societe = IdSoc;
				Tracabilite_Facture_Client.Id_Facture = NewElement.ID;
				db.Tracabilite_Facture_Client.Add(Tracabilite_Facture_Client);
				db.SaveChanges();
				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS Ligne in Liste)
				{
					LIGNES_FACTURES_CLIENTS NewLine = new LIGNES_FACTURES_CLIENTS();

					NewLine.Prix_achat = db.Prix_Achat.Where(f => f.Libelle == Ligne.Libelle_Prd).FirstOrDefault().Product_ID;
					// NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
					NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
					NewLine.Libelle_Prd = Ligne.Libelle_Prd;

					NewLine.Marque = Ligne.Marque;
					NewLine.Devise = Ligne.Devise;
					NewLine.Unite = Ligne.Unite;
					NewLine.Categorie = Ligne.Categorie;
					NewLine.Sous_Categorie = Ligne.Sous_Categorie;
					NewLine.QUANTITE = Ligne.QUANTITE;
					//NewLine.QUANTITE = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == Element.ID && cmd.Code_Article = Ligne.Prix_achat).FirstOrDefault().QTELIV;
					NewLine.STOCK = Ligne.STOCK;
					NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
					NewLine.PRIX_UNITAIRE_HTVente = NewLine.PRIX_UNITAIRE_HT;
					NewLine.MARGE = 0;
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
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS Ligne in Listecuisine)
				{
					LIGNES_CUISINE_FACTURE_CLIENTS lignecuisine = new LIGNES_CUISINE_FACTURE_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
					lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
					lignecuisine.FACTURE_CLIENT = NewElement.ID;
					db.LIGNES_CUISINE_FACTURE_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> LignesACCESSOIRE = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.ID_LigneBL == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL lig in LignesACCESSOIRE)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_Facture des = new LIGNES_DESCRIPTION_ACCESOIRE_Facture();
						des.Designation = lig.Designation;
						des.ID_SSCAT = lig.ID_SSCAT;
						des.ID_ART = lig.ID_ART;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.PTTC;
						des.QTE = lig.QTE;
						des.ID_LigneFacture = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Add(des);
						db.SaveChanges();
					}
				}
				return NewElement.ID.ToString();
			}
			return "NO";
		}
		public string BonLivraisonPartVersfacture(string parampassed)
		{
			int ID = int.Parse(parampassed);
			decimal tottva = 0;
			decimal totht = 0;
			decimal totttc = 0;
			decimal totNet = 0;

			BONS_LIVRAISONS_PART_CLIENTS Element = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			BONS_LIVRAISONS_CLIENTS Element1 = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == Element.IDBLC).FirstOrDefault();
			if (Element.VALIDER == true)
			{
				List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_PART_CLIENT == ID).ToList();
				FACTURES_CLIENTS NewElement = new FACTURES_CLIENTS();
				foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS lig in Liste)
				{
					totht += (decimal)lig.TOTALE_HT;
					totttc += (decimal)lig.TOTALE_TTC;
					tottva += (decimal)(lig.TOTALE_HT * lig.TVA) / 100;
					totNet += (decimal)(lig.TOTALE_HT * lig.REMISE) / 100;
				}
				string Numero = string.Empty;
				DateTime d = Element.DATE;

				int Max = 0;
				if (db.FACTURES_CLIENTS.ToList().Count != 0)
				{
					Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
				}
				Max++;
				Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
				NewElement.CODE = Numero;
				NewElement.DATE = Element.DATE;
				NewElement.Designation = Element1.Designation;
				NewElement.MODE_PAIEMENT = Element1.MODE_PAIEMENT;
				NewElement.Designation = Element1.Designation;

				NewElement.CLIENT = Element1.CLIENT;
				NewElement.Societes = Element1.Societes;
				// NewElement.Tiers = Element.Tiers;
				NewElement.THT = totht;
				NewElement.TTVA = tottva;
				NewElement.NHT = totNet;
				NewElement.TIMBRE = decimal.Parse("0,6");
				NewElement.TTC = (Decimal)(totttc + NewElement.TIMBRE);
				NewElement.TNET = totNet + NewElement.TIMBRE;
				NewElement.VALIDER = false;
				NewElement.PAYEE = false;
				NewElement.REMISE = Element1.REMISE;
				NewElement.BON_LIVRAISON_CLIENT = Element.ID;
				//NewElement.BONS_LIVRAISONS_CLIENTS = Element;
				NewElement.CLIENTS = Element1.CLIENTS;
				//NewElement.Societes1 = Element1.Societes1;
				db.FACTURES_CLIENTS.Add(NewElement);
				db.SaveChanges();
				foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS Ligne in Liste)
				{
					LIGNES_FACTURES_CLIENTS NewLine = new LIGNES_FACTURES_CLIENTS();

					NewLine.Prix_achat = db.Prix_Achat.Where(f => f.Libelle == Ligne.Libelle_Prd).FirstOrDefault().Product_ID;
					// NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
					NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
					NewLine.Libelle_Prd = Ligne.Libelle_Prd;

					NewLine.Marque = Ligne.Marque;
					NewLine.Devise = Ligne.Devise;
					NewLine.Unite = Ligne.Unite;
					NewLine.Categorie = Ligne.Categorie;
					NewLine.Sous_Categorie = Ligne.Sous_Categorie;
					NewLine.QUANTITE = Ligne.QUANTITE;
					//NewLine.QUANTITE = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == Element.ID && cmd.Code_Article = Ligne.Prix_achat).FirstOrDefault().QTELIV;
					NewLine.STOCK = Ligne.STOCK;
					NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
					NewLine.PRIX_UNITAIRE_HTVente = NewLine.PRIX_UNITAIRE_HT;
					NewLine.MARGE = 0;
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
		public string BonLivraisonVersCaisse(string parampassed)
		{
			int ID = int.Parse(parampassed);
			BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			if (Element.VALIDER)
			{
				List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
				List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> Listecuisine = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(cmd => cmd.BONLIVRAISON_CLIENT == ID).ToList();

				Caisse NewElement = new Caisse();
				string Numero = string.Empty;
				DateTime d = Element.DATE;
				if (Session["SoclogoId"] == null)
				{
					RedirectToAction("Login", "Societes");
				}
				int idste = (int)Session["SoclogoId"];
				PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 21).FirstOrDefault();
				int Max = 0;
				if (PrefixeTable == null)
				{
					//if (db.Caisse.Where(f => f.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.Caisse.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}
					Max++;
					Numero = "C" + Max.ToString("0000") + "/" + d.ToString("yy");
					while (db.Caisse.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;

						Numero = "C" + Max.ToString("0000") + "/" + d.ToString("yy");
					}
				}
				else
				{
					//if (db.Caisse.Where(f => f.Societes == idste).ToList().Count != 0)
					//{
					//    Max = db.Caisse.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					//}

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
					while (db.Caisse.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
					{
						Max++;
						string PRF1 = PrefixeTable.Prefixe;
						string numPre1 = PRF.Replace("0000", Max.ToString("0000"));
						string count2 = "";
						string count3 = "";
						foreach (char c in numPre)
						{
							if (c == 'y')
							{
								count2 += c;
							}
						}
						foreach (char c in numPre)
						{
							if (c == 'm')
							{
								count3 += c;
							}
						}
						string date11 = d.ToString(count2);
						string date22 = d.ToString(count3);
						Numero = numPre.Replace(count, date1);
						Numero = Numero.Replace(count1, date2);
					}
				}
				NewElement.CODE = Numero;
				NewElement.DATE = Element.DATE;
				NewElement.Designation = Element.Designation;
				NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
				NewElement.Designation = Element.Designation;
				NewElement.CLIENT = Element.CLIENT;
				NewElement.Societes = Element.Societes;
				//NewElement.Tiers = Element.Tiers;
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
				db.Caisse.Add(NewElement);
				db.SaveChanges();
				Tracabilite_Caisse_Client Tracabilite_Facture_Client = new Tracabilite_Caisse_Client();
				Tracabilite_Facture_Client.Date = DateTime.Today;
				if (Session["ID"] != null)
				{
					string personnel = (string)Session["ID"];
					int personnel1 = int.Parse(personnel);
					Tracabilite_Facture_Client.Personnel = personnel1;
					Tracabilite_Facture_Client.Ajoute_Par = true;
					Tracabilite_Facture_Client.Modifie_Par = false;

				}
				string soc = (string)Session["Soclogo"];
				int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
				Tracabilite_Facture_Client.Societe = IdSoc;
				Tracabilite_Facture_Client.Id_Caisse = NewElement.ID;
				db.Tracabilite_Caisse_Client.Add(Tracabilite_Facture_Client);
				db.SaveChanges();
				foreach (LIGNES_BONS_LIVRAISONS_CLIENTS Ligne in Liste)
				{
					LIGNES_Caisse NewLine = new LIGNES_Caisse();

					NewLine.Prix_achat = db.Prix_Achat.Where(f => f.Libelle == Ligne.Libelle_Prd).FirstOrDefault().Product_ID;
					// NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
					NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
					NewLine.Libelle_Prd = Ligne.Libelle_Prd;

					NewLine.Marque = Ligne.Marque;
					NewLine.Devise = Ligne.Devise;
					NewLine.Unite = Ligne.Unite;
					NewLine.Categorie = Ligne.Categorie;
					NewLine.Sous_Categorie = Ligne.Sous_Categorie;
					NewLine.QUANTITE = Ligne.QUANTITE;
					//NewLine.QUANTITE = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == Element.ID && cmd.Code_Article = Ligne.Prix_achat).FirstOrDefault().QTELIV;
					NewLine.STOCK = Ligne.STOCK;
					NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
					NewLine.PRIX_UNITAIRE_HTVente = NewLine.PRIX_UNITAIRE_HT;
					NewLine.MARGE = 0;
					NewLine.REMISE = Ligne.REMISE;
					NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
					NewLine.TOTALE_HT = Ligne.TOTALE_HT;
					NewLine.TVA = Ligne.TVA;
					NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
					NewLine.Caisse = NewElement.ID;
					NewLine.Caisse1 = NewElement;
					//NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
					db.LIGNES_Caisse.Add(NewLine);
					db.SaveChanges();
					//AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
				}
				foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS Ligne in Listecuisine)
				{
					LIGNES_CUISINE_CAISSE_CLIENTS lignecuisine = new LIGNES_CUISINE_CAISSE_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
					lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
					lignecuisine.CAISSE = NewElement.ID;
					db.LIGNES_CUISINE_CAISSE_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LIGNES_DESCRIPTION_ACCESOIRE_BL> LignesACCESSOIRE = db.LIGNES_DESCRIPTION_ACCESOIRE_BL.Where(f => f.ID_LigneBL == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_BL lig in LignesACCESSOIRE)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_CAISSE des = new LIGNES_DESCRIPTION_ACCESOIRE_CAISSE();
						des.Designation = lig.Designation;
						des.ID_SSCAT = lig.ID_SSCAT;
						des.ID_ART = lig.ID_ART;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.PTTC;
						des.QTE = lig.QTE;
						des.ID_LigneCAISSE = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_CAISSE.Add(des);
						db.SaveChanges();
					}
				}
				return NewElement.ID.ToString();
			}
			return "NO";
		}
		//public string BonLivraisonVersCaisse(string parampassed)
		//{
		//    int ID = int.Parse(parampassed);
		//    BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
		//    if (Element.VALIDER)
		//    {
		//        List<LIGNES_BONS_LIVRAISONS_CLIENTS> Liste = db.LIGNES_BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_CLIENT == ID).ToList();
		//        List<LIGNES_CUISINE_BONLIVRAISON_CLIENTS> Listecuisine = db.LIGNES_CUISINE_BONLIVRAISON_CLIENTS.Where(cmd => cmd.BONLIVRAISON_CLIENT == ID).ToList();

		//        Caisse NewElement = new Caisse();
		//        string Numero = string.Empty;
		//        DateTime d = Element.DATE;

		//        int Max = 0;
		//        if (db.Caisse.ToList().Count != 0)
		//        {
		//            Max = db.Caisse.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
		//        }
		//        Max++;
		//        Numero = "C" + Max.ToString("0000") + "/" + d.ToString("yy");
		//        NewElement.CODE = Numero;
		//        NewElement.DATE = Element.DATE;
		//        NewElement.Designation = Element.Designation;
		//        NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
		//        NewElement.Designation = Element.Designation;

		//        NewElement.CLIENT = Element.CLIENT;
		//        NewElement.Societes = Element.Societes;
		//        // NewElement.Tiers = Element.Tiers;
		//        NewElement.THT = Element.THT;
		//        NewElement.TTVA = Element.TTVA;
		//        NewElement.NHT = Element.NHT;
		//        NewElement.TIMBRE = decimal.Parse("0,6");
		//        NewElement.TTC = (Decimal)(Element.TTC + NewElement.TIMBRE);
		//        NewElement.TNET = Element.TNET + NewElement.TIMBRE;
		//        NewElement.VALIDER = false;
		//        NewElement.PAYEE = false;
		//        NewElement.REMISE = Element.REMISE;
		//        NewElement.BON_LIVRAISON_CLIENT = Element.ID;
		//        NewElement.BONS_LIVRAISONS_CLIENTS = Element;
		//        NewElement.CLIENTS = Element.CLIENTS;
		//        //NewElement.SocieteLogo = Element.SocieteLogo;
		//        db.Caisse.Add(NewElement);
		//        db.SaveChanges();
		//        Tracabilite_Caisse_Client Tracabilite_Caisse_Client = new Tracabilite_Caisse_Client();
		//        Tracabilite_Caisse_Client.Date = DateTime.Today;
		//        if (Session["ID"] != null)
		//        {
		//            string personnel = (string)Session["ID"];
		//            int personnel1 = int.Parse(personnel);
		//            Tracabilite_Caisse_Client.Personnel = personnel1;
		//            Tracabilite_Caisse_Client.Ajoute_Par = true;

		//        }
		//        string soc = (string)Session["Soclogo"];
		//        int IdSoc = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
		//        Tracabilite_Caisse_Client.Societe = IdSoc;
		//        Tracabilite_Caisse_Client.Id_Caisse = NewElement.ID;
		//        db.Tracabilite_Caisse_Client.Add(Tracabilite_Caisse_Client);
		//        db.SaveChanges();
		//        foreach (LIGNES_BONS_LIVRAISONS_CLIENTS Ligne in Liste)
		//        {
		//            LIGNES_Caisse NewLine = new LIGNES_Caisse();

		//            NewLine.Prix_achat = db.Prix_Achat.Where(f => f.Libelle == Ligne.Libelle_Prd).FirstOrDefault().Product_ID;
		//            // NewLine.CODE_PRODUIT = Ligne.CODE_PRODUIT;
		//            NewLine.DESIGNATION_PRODUIT = Ligne.DESIGNATION_PRODUIT;
		//            NewLine.Libelle_Prd = Ligne.Libelle_Prd;

		//            NewLine.Marque = Ligne.Marque;
		//            NewLine.Devise = Ligne.Devise;
		//            NewLine.Unite = Ligne.Unite;
		//            NewLine.Categorie = Ligne.Categorie;
		//            NewLine.Sous_Categorie = Ligne.Sous_Categorie;
		//            NewLine.QUANTITE = Ligne.QUANTITE;
		//            //NewLine.QUANTITE = db.BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.IDBLC == Element.ID && cmd.Code_Article = Ligne.Prix_achat).FirstOrDefault().QTELIV;
		//            NewLine.STOCK = Ligne.STOCK;
		//            NewLine.PRIX_UNITAIRE_HT = Ligne.PRIX_UNITAIRE_HT;
		//            NewLine.PRIX_UNITAIRE_HTVente = NewLine.PRIX_UNITAIRE_HT;
		//            NewLine.MARGE = 0;
		//            NewLine.REMISE = Ligne.REMISE;
		//            NewLine.TOTALE_REMISE_HT = Ligne.TOTALE_REMISE_HT;
		//            NewLine.TOTALE_HT = Ligne.TOTALE_HT;
		//            NewLine.TVA = Ligne.TVA;
		//            NewLine.TOTALE_TTC = Ligne.TOTALE_TTC;
		//            NewLine.Caisse = NewElement.ID;
		//            //NewLine.Caisse = NewElement;
		//            //NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
		//            db.LIGNES_Caisse.Add(NewLine);
		//            db.SaveChanges();
		//            //AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
		//        }
		//        foreach (LIGNES_CUISINE_BONLIVRAISON_CLIENTS Ligne in Listecuisine)
		//        {
		//            LIGNES_CUISINE_CAISSE_CLIENTS lignecuisine = new LIGNES_CUISINE_CAISSE_CLIENTS();
		//            lignecuisine.SSCAISSON = Ligne.SSCAISSON;
		//            lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
		//            lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
		//            lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
		//            lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
		//            lignecuisine.ACC = Ligne.ACC;
		//            lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
		//            lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
		//            lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
		//            lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
		//            lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
		//            lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
		//            lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
		//            lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
		//            lignecuisine.TVACUISINE = Ligne.TVACUISINE;
		//            lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
		//            lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
		//            lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
		//            lignecuisine.CAISSE = NewElement.ID;
		//            db.LIGNES_CUISINE_CAISSE_CLIENTS.Add(lignecuisine);
		//            db.SaveChanges();
		//        }
		//        return NewElement.ID.ToString();
		//    }
		//    return "NO";
		//}
		[HttpPost]
		public ActionResult BonLivraisonParVersfacture(FormCollection formCollection, string Code)
		{
			string[] ids = formCollection["affComId"].Split(new char[] { ',' });
			//string Idd = ids[0];
			int ID = int.Parse(Code);
			decimal tottva = 0;
			decimal totht = 0;
			decimal totttc = 0;
			decimal totNet = 0;
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
				//foreach (LIGNES_BONS_LIVRAISONS_CLIENTS lignebl in Liste)
				//{
				foreach (BONS_LIVRAISONS_PART_CLIENTS ligneblb in blpart)
				{
					List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> Lignesblpart = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_PART_CLIENT == ligneblb.ID).ToList();

					foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS ligne in Lignesblpart)
					{
						totht += (decimal)ligne.TOTALE_HT;
						totttc += (decimal)ligne.TOTALE_TTC;
						tottva += (decimal)(ligne.TOTALE_HT * ligne.TVA) / 100;
						totNet += (decimal)(ligne.TOTALE_HT * ligne.REMISE) / 100;

						ligneblb.Etat = true;
						//if (lignebl.Prix_achat == ligneblb.Code_Article)
						//{
						LigneProduit lignepr = new LigneProduit();
						lignepr.ID = (int)db.Prix_Achat.Where(f => f.Libelle == ligne.Libelle_Prd).FirstOrDefault().Product_ID;
						//lignepr.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == lignepr.ID).FirstOrDefault().Libelle;
						lignepr.DESIGNATION = ligne.DESIGNATION_PRODUIT;
						lignepr.MARQUE = ligne.Marque;
						lignepr.DEVISE = ligne.Devise;
						lignepr.UNITE = ligne.Unite;
						lignepr.STOCK = (int)ligne.STOCK;
						lignepr.CATEGORIE = ligne.Categorie;
						lignepr.Sous_CATEGORIE = ligne.Sous_Categorie;
						lignepr.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
						lignepr.TVA = (int)ligne.TVA;
						lignepr.REMISE = (int)ligne.REMISE;
						lignepr.ID_Bl = (int)ligne.BON_LIVRAISON_PART_CLIENT;
						//lignepr.TTC = (decimal)lignebl.TOTALE_TTC;
						lignepr.QUANTITE += ((int)ligne.QUANTITE);
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
						//}
						//}
					}

				}

			}
			DateTime d = Element.DATE;

			string Numero = string.Empty;
			int Max = 0;
			if (db.FACTURES_CLIENTS.ToList().Count != 0)
			{
				Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
			}
			Max++;
			Numero = "F" + Max.ToString("0000") + "/" + d.ToString("yy");
			NewElement.CODE = Numero;
			NewElement.DATE = Element.DATE;
			NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
			NewElement.Designation = Element.Designation;
			NewElement.CLIENT = Element.CLIENT;
			NewElement.Societes = Element.Societes;
			//NewElement.Tiers = Element.Tiers;
			NewElement.THT = totht;
			NewElement.TTVA = tottva;
			NewElement.NHT = totNet;
			NewElement.TIMBRE = decimal.Parse("0,6");
			NewElement.TTC = (Decimal)(totNet + NewElement.TIMBRE);
			NewElement.TNET = totNet + NewElement.TIMBRE;
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
				NewLine.Num_BLP = Ligne.ID_Bl;
				NewLine.FACTURE_CLIENT = NewElement.ID;
				NewLine.FACTURES_CLIENTS = NewElement;
				//NewLine.Prix_Achat1 = Ligne.;
				db.LIGNES_FACTURES_CLIENTS.Add(NewLine);
				db.SaveChanges();
				//AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
			}
			return RedirectToAction("FormFacture", "Vente", new { @Mode = "Edit", @Code = NewElement.ID.ToString() });

		}
		public ActionResult BonLivraisonParVersCaisse(FormCollection formCollection, string Code)
		{
			string[] ids = formCollection["affComId"].Split(new char[] { ',' });
			//string Idd = ids[0];
			int ID = int.Parse(Code);
			decimal tottva = 0;
			decimal totht = 0;
			decimal totttc = 0;
			decimal totNet = 0;
			BONS_LIVRAISONS_CLIENTS Element = db.BONS_LIVRAISONS_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			Caisse NewElement = new Caisse();

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
				//foreach (LIGNES_BONS_LIVRAISONS_CLIENTS lignebl in Liste)
				//{
				foreach (BONS_LIVRAISONS_PART_CLIENTS ligneblb in blpart)
				{
					List<LIGNES_BONS_LIVRAISONS_PART_CLIENTS> Lignesblpart = db.LIGNES_BONS_LIVRAISONS_PART_CLIENTS.Where(cmd => cmd.BON_LIVRAISON_PART_CLIENT == ligneblb.ID).ToList();
					foreach (LIGNES_BONS_LIVRAISONS_PART_CLIENTS ligne in Lignesblpart)
					{
						totht += (decimal)ligne.TOTALE_HT;
						totttc += (decimal)ligne.TOTALE_TTC;
						tottva += (decimal)(ligne.TOTALE_HT * ligne.TVA) / 100;
						totNet += (decimal)(ligne.TOTALE_HT * ligne.REMISE) / 100;
						ligneblb.Etat = true;
						//if (lignebl.Prix_achat == ligneblb.Code_Article)
						//{
						LigneProduit lignepr = new LigneProduit();
						lignepr.ID = (int)db.Prix_Achat.Where(f => f.Libelle == ligne.Libelle_Prd).FirstOrDefault().Product_ID;
						//lignepr.LIBELLE = db.Prix_Achat.Where(pr => pr.Product_ID == lignepr.ID).FirstOrDefault().Libelle;
						lignepr.DESIGNATION = ligne.DESIGNATION_PRODUIT;
						lignepr.MARQUE = ligne.Marque;
						lignepr.DEVISE = ligne.Devise;
						lignepr.UNITE = ligne.Unite;
						lignepr.STOCK = (int)ligne.STOCK;
						lignepr.CATEGORIE = ligne.Categorie;
						lignepr.Sous_CATEGORIE = ligne.Sous_Categorie;
						lignepr.PRIX_VENTE_HT = (decimal)ligne.PRIX_UNITAIRE_HT;
						lignepr.TVA = (int)ligne.TVA;
						lignepr.REMISE = (int)ligne.REMISE;
						lignepr.ID_Bl = (int)ligne.BON_LIVRAISON_PART_CLIENT;
						//lignepr.TTC = (decimal)lignebl.TOTALE_TTC;
						lignepr.QUANTITE += ((int)ligne.QUANTITE);
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
						//}
						//}
					}

				}

			}
			DateTime d = Element.DATE;

			string Numero = string.Empty;
			int Max = 0;
			if (db.FACTURES_CLIENTS.ToList().Count != 0)
			{
				Max = db.FACTURES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
			}
			Max++;
			Numero = "C" + Max.ToString("0000") + "/" + d.ToString("yy");
			NewElement.CODE = Numero;
			NewElement.DATE = Element.DATE;
			NewElement.MODE_PAIEMENT = Element.MODE_PAIEMENT;
			NewElement.Designation = Element.Designation;
			NewElement.CLIENT = Element.CLIENT;
			NewElement.Societes = Element.Societes;
			//NewElement.Tiers = Element.Tiers;
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
			db.Caisse.Add(NewElement);
			db.SaveChanges();
			foreach (LigneProduit Ligne in listepr)
			{
				LIGNES_Caisse NewLine = new LIGNES_Caisse();
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
				NewLine.Num_BLP = Ligne.ID_Bl;
				NewLine.Caisse = NewElement.ID;
				NewLine.Caisse1 = NewElement;
				//NewLine.Prix_Achat1 = Ligne.;
				db.LIGNES_Caisse.Add(NewLine);
				db.SaveChanges();
				//AddMouvementProduit("FACTURE", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
			}
			return RedirectToAction("FormCaisse", "Vente", new { @Mode = "Edit", @Code = NewElement.ID.ToString() });

		}
		public string FactureVersAvoir(string parampassed)
		{
			int ID = int.Parse(parampassed);
			FACTURES_CLIENTS Element = db.FACTURES_CLIENTS.Where(cmd => cmd.ID == ID).FirstOrDefault();
			if (Session["SoclogoId"] == null)
			{
				RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			if (Element.VALIDER)
			{
				List<LIGNES_FACTURES_CLIENTS> Liste = db.LIGNES_FACTURES_CLIENTS.Where(cmd => cmd.FACTURE_CLIENT == ID).ToList();
				List<LIGNES_CUISINE_FACTURE_CLIENTS> Listecuisine = db.LIGNES_CUISINE_FACTURE_CLIENTS.Where(cmd => cmd.FACTURE_CLIENT == ID).ToList();

				AVOIRS_CLIENTS NewElement = new AVOIRS_CLIENTS();
				string Numero = string.Empty;
				DateTime d = Element.DATE;

				int Max = 0;
				//if (db.AVOIRS_CLIENTS.ToList().Count != 0)
				//{
				//    Max = db.AVOIRS_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year).Select(cmd => cmd.ID).Count();
				//}
				Max++;
				Numero = "AVC" + Max.ToString("0000") + "/" + d.ToString("yy");

				while (db.AVOIRS_CLIENTS.Where(f => f.Societes == idste).Select(cmd => cmd.CODE).Contains(Numero))
				{
					Max++;
					Numero = "AVC" + Max.ToString("0000") + "/" + d.ToString("yy");
				}
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
					//NewLine.Prix_Achat1 = Ligne.Prix_Achat1;
					db.LIGNES_AVOIRS_CLIENTS.Add(NewLine);
					db.SaveChanges();
					//AddMouvementProduit("AVOIR", NewLine.Prix_Achat1, NewElement.DATE, NewElement.CODE, (int)NewLine.QUANTITE);
				}
				foreach (LIGNES_CUISINE_FACTURE_CLIENTS Ligne in Listecuisine)
				{
					LIGNES_CUISINE_AVOIR_CLIENTS lignecuisine = new LIGNES_CUISINE_AVOIR_CLIENTS();
					lignecuisine.SSCAISSON = Ligne.SSCAISSON;
					lignecuisine.QuantiteCAISSON = Ligne.QuantiteCAISSON;
					lignecuisine.CREVCAISSON = Ligne.CREVCAISSON;
					lignecuisine.MARGEPRIXACHAT = Ligne.MARGEPRIXACHAT;
					lignecuisine.PRIXACHAT = Ligne.PRIXACHAT;
					lignecuisine.ACC = Ligne.ACC;
					lignecuisine.POURCENTAGE = Ligne.POURCENTAGE;
					lignecuisine.PRIXVENTECAISSON = Ligne.PRIXVENTECAISSON;
					lignecuisine.SOUSFACADE = Ligne.SOUSFACADE;
					lignecuisine.QuantiteFACADE = Ligne.QuantiteFACADE;
					lignecuisine.PRIXFACADE = Ligne.PRIXFACADE;
					lignecuisine.PTHTFACADE = Ligne.PTHTFACADE;
					lignecuisine.PTHTSSMARGE = Ligne.PTHTSSMARGE;
					lignecuisine.PTHTAVECMARGE = Ligne.PTHTAVECMARGE;
					lignecuisine.TVACUISINE = Ligne.TVACUISINE;
					lignecuisine.PTTCCUISINE = Ligne.PTTCCUISINE;
					lignecuisine.TYPECAISSON = Ligne.TYPECAISSON;
					lignecuisine.TYPEFACADE = Ligne.TYPEFACADE;
					lignecuisine.AVOIR_CLIENT = NewElement.ID;
					db.LIGNES_CUISINE_AVOIR_CLIENTS.Add(lignecuisine);
					db.SaveChanges();
					List<LIGNES_DESCRIPTION_ACCESOIRE_Facture> LignesACCESSOIRE = db.LIGNES_DESCRIPTION_ACCESOIRE_Facture.Where(f => f.ID_LigneFacture == Ligne.ID).ToList();
					foreach (LIGNES_DESCRIPTION_ACCESOIRE_Facture lig in LignesACCESSOIRE)
					{
						LIGNES_DESCRIPTION_ACCESOIRE_AVOIR des = new LIGNES_DESCRIPTION_ACCESOIRE_AVOIR();
						des.Designation = lig.Designation;
						des.ID_SSCAT = lig.ID_SSCAT;
						des.ID_ART = lig.ID_ART;
						des.PUHT = lig.PUHT;
						des.PTHT = lig.PTHT;
						des.TVA = lig.TVA;
						des.PTTC = lig.PTTC;
						des.QTE = lig.QTE;
						des.ID_LigneAVOIR = lignecuisine.ID;
						db.LIGNES_DESCRIPTION_ACCESOIRE_AVOIR.Add(des);
						db.SaveChanges();
					}
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
