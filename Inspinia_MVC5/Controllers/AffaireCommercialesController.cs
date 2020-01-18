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
	public class AffaireCommercialesController : Controller
	{
		private MED_TRABELSI db = new MED_TRABELSI();

		// GET: /AffaireCommerciales1/
		public ActionResult Index()
		{
			//var affairecommerciales = db.AffaireCommerciales;
			List<AffaireCommerciales> list = new List<AffaireCommerciales>();
			if (ViewBag.remtab==null)
			{
				list = db.AffaireCommerciales.ToList();
			}
			else
			{
				list = ViewBag.remtab;
			}
			ViewBag.remtab = null;
			List<AffaireCommerciales> list2 = new List<AffaireCommerciales>();
			foreach (AffaireCommerciales aff in list)
			{
				ProjetTechniques proj = db.ProjetTechniques.Where(cmd => cmd.ProjetTechniqueId == aff.AffaireCommercialeId).FirstOrDefault();
				if (proj == null)
				{
					list2.Add(aff);
				}
			}

			return View(list2);
		}

		//convertTo a technical project
		[HttpPost]
		public ActionResult Index(FormCollection formCollection)
		{
			string[] ids = formCollection["affComId"].Split(new char[] { ',' });
			foreach (string Id in ids)
			{
				var affaireCommerciale = this.db.AffaireCommerciales.Find(Convert.ToInt32(Id));
				Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

				this.db.SaveChanges();
				decimal cout = 0;
				decimal ttc = db.DEVIS_CLIENTS.Where(f => f.Id_AffaireCommerciale == affaireCommerciale.AffaireCommercialeId).FirstOrDefault().TTC;
				cout += ttc;
				var projetTechnique = new PROJET_TECHNIQUE()
				{

					Id_AffaireCommerciale = affaireCommerciale.AffaireCommercialeId,
					Reference = param.RefTech + (param.CompteurTech).ToString("0000"),
					//DateDebut = affaireCommerciale.DateDebut,
					//DateFin = affaireCommerciale.DateFin,
					Cout = cout,
					ClientId = affaireCommerciale.ClientId,
					PersonnelId = affaireCommerciale.PersonnelId,
					Designation = affaireCommerciale.Designation,
					Description = affaireCommerciale.Description,
					EnCourExecution = "en Cours",
					AffaireObtenue = "Obtenue",
					ResponsableTechnique = affaireCommerciale.ResponsableTechnique,
					CoordinateurCommerciale = affaireCommerciale.CoordinateurCommerciale,
					CoordinateurRealisation = affaireCommerciale.CoordinateurRealisation,
					//CoutReel = affaireCommerciale.Cout,
					//DateDebutReel=affaireCommerciale.DateDebut,
					//DateFinReel=affaireCommerciale.DateFin

				};
				//this.db.AffaireCommerciales.Remove(affaireCommerciale);
				this.db.PROJET_TECHNIQUE.Add(projetTechnique);

				try
				{
					var pt = db.ProjetTechniques
						.OrderByDescending(p => p.ProjetTechniqueId)
						.FirstOrDefault();
					String ch = pt.ReferenceTech.ToString();
					projetTechnique.Reference = param.RefTech + param.CompteurTech;
					param.CompteurTech = param.CompteurTech + 1;




				}
				catch
				{
					affaireCommerciale.Reference = param.RefCom + param.CompteurCom;
					param.CompteurTech = param.CompteurTech + 1;
				}


				this.db.SaveChanges();
				int ID = db.DEVIS_CLIENTS.Where(f => f.Id_AffaireCommerciale == affaireCommerciale.AffaireCommercialeId).FirstOrDefault().ID;
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
					if (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
					{
						Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
					}
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
					if (db.COMMANDES_CLIENTS.Where(f => f.Societes == idste).ToList().Count != 0)
					{
						Max = db.COMMANDES_CLIENTS.Where(cmd => cmd.DATE.Year == d.Year && cmd.Societes == idste).Select(cmd => cmd.ID).Count();
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
			}
			return RedirectToAction("Index", "PROJET_TECHNIQUE");
		}

		public ActionResult Tiers(string id, string Mode, string Designation, string Description, string Delai_de_Soumission, string Date_de_Soumission, string DateConsultation, string EtatSoum, string faisabilite, string AffaireObtenue, string Importance, string NbHeurePrepOffre, string ClientId, string PersonnelId, string ResponsableTechnique, string CoordinateurCommerciale, string CoordinateurRealisation)
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
				String Numero = numPre.Replace(count, date1);
				ViewBag.Numero = Numero.Replace(count1, date2);
			}
			ViewBag.Designation = Designation;
			ViewBag.Description = Description;
			ViewBag.Delai_de_Soumission = Delai_de_Soumission;
			ViewBag.Date_de_Soumission = Date_de_Soumission;
			ViewBag.DateConsultation = DateConsultation;
			ViewBag.EtatSoum = EtatSoum;
			ViewBag.faisabilite = faisabilite;
			ViewBag.AffaireObtenue = AffaireObtenue;
			ViewBag.Importance = Importance;
			ViewBag.NbHeurePrepOffre = NbHeurePrepOffre;
			ViewBag.ClientId = ClientId;
			ViewBag.PersonnelId = PersonnelId;
			ViewBag.ResponsableTechnique = ResponsableTechnique;
			ViewBag.CoordinateurCommerciale = CoordinateurCommerciale;
			ViewBag.CoordinateurRealisation = CoordinateurRealisation;
			ViewBag.Mode = Mode;
			ViewBag.id = id;
			return PartialView("Client", frnds);
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
		public ActionResult Personnels(string id, string Mode, string Designation, string Description, string Delai_de_Soumission, string Date_de_Soumission, string DateConsultation, string EtatSoum, string faisabilite, string AffaireObtenue, string Importance, string NbHeurePrepOffre, string ClientId, string PersonnelId, string ResponsableTechnique, string CoordinateurCommerciale, string CoordinateurRealisation)
		{
			ViewBag.Designation = Designation;
			ViewBag.Description = Description;
			ViewBag.Delai_de_Soumission = Delai_de_Soumission;
			ViewBag.Date_de_Soumission = Date_de_Soumission;
			ViewBag.DateConsultation = DateConsultation;
			ViewBag.EtatSoum = EtatSoum;
			ViewBag.faisabilite = faisabilite;
			ViewBag.AffaireObtenue = AffaireObtenue;
			ViewBag.Importance = Importance;
			ViewBag.NbHeurePrepOffre = NbHeurePrepOffre;
			ViewBag.ClientId = ClientId;
			ViewBag.PersonnelId = PersonnelId;
			ViewBag.ResponsableTechnique = ResponsableTechnique;
			ViewBag.CoordinateurCommerciale = CoordinateurCommerciale;
			ViewBag.CoordinateurRealisation = CoordinateurRealisation;
			ViewBag.Mode = Mode;
			ViewBag.id = id;
			Personnels per = new Personnels();
			return PartialView("Personnel", per);
		}

		public void AddPersonnel(string Role, string Nom, string Password, string Email, string Tel)
		{
			Personnels NewElement = new Personnels();
			NewElement.Role = Role;
			NewElement.Email = Email;
			NewElement.Nom = Nom;
			NewElement.Password = Tel;
			int tel = int.Parse(Tel);
			NewElement.Tel = tel;


			db.Personnels.Add(NewElement);
			db.SaveChanges();
		}


		// GET: /AffaireCommerciales1/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AffaireCommerciales affaireCommerciale = db.AffaireCommerciales.Find(id);
			if (affaireCommerciale == null)
			{
				return HttpNotFound();
			}
			return View(affaireCommerciale);
		}

		// GET: /AffaireCommerciales1/Create
		public ActionResult Create(string Designation, string Description, string Delai_de_Soumission, string Date_de_Soumission, string DateConsultation, string EtatSoum, string faisabilite, string AffaireObtenue, string Importance, string NbHeurePrepOffre, string ClientId, string PersonnelId, string ResponsableTechnique, string CoordinateurCommerciale, string CoordinateurRealisation)

		{
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];

			ViewBag.ClientId = new SelectList(db.CLIENTS.Where(f => f.Societe == idste), "ID", "NOM");
			ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom");
			ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom");
			ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom");
			ViewBag.CoordinateurRealisation = new SelectList(db.Personnels, "PersonnelId", "Nom");

			ViewBag.Designation = Designation;
			ViewBag.Description = Description;
			ViewBag.Delai_de_Soumission = Delai_de_Soumission;
			ViewBag.Date_de_Soumission = Date_de_Soumission;
			ViewBag.DateConsultation = DateConsultation;
			ViewBag.EtatSoum = EtatSoum;
			ViewBag.faisabilite = faisabilite;
			ViewBag.AffaireObtenue = AffaireObtenue;
			ViewBag.Importance = Importance;
			ViewBag.NbHeurePrepOffre = NbHeurePrepOffre;
			if (ClientId != "" && ClientId != null)
			{
				int ClientId1 = int.Parse(ClientId);
				ViewBag.ClientId = new SelectList(db.CLIENTS.Where(f => f.Societe == idste), "ID", "NOM", ClientId1);
			}
			if (PersonnelId != "" && PersonnelId != null)
			{
				int PersonnelId1 = int.Parse(PersonnelId);
				ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", PersonnelId1);
			}
			if (ResponsableTechnique != "" && ResponsableTechnique != null)
			{
				int ResponsableTechnique1 = int.Parse(ResponsableTechnique);
				ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom", ResponsableTechnique1);
			}
			if (CoordinateurCommerciale != "" && CoordinateurCommerciale != null)
			{
				int CoordinateurCommerciale1 = int.Parse(CoordinateurCommerciale);
				ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom", CoordinateurCommerciale1);
			}
			if (CoordinateurRealisation != "" && CoordinateurRealisation != null)
			{
				int CoordinateurRealisation1 = int.Parse(CoordinateurRealisation);
				ViewBag.CoordinateurRealisation = new SelectList(db.Personnels, "PersonnelId", "Nom", CoordinateurRealisation1);
			}
			//ViewBag.PersonnelId = PersonnelId;
			//ViewBag.ResponsableTechnique = ResponsableTechnique;
			//ViewBag.CoordinateurCommerciale = CoordinateurCommerciale;
			//ViewBag.CoordinateurRealisation = CoordinateurRealisation;
			ViewBag.Mode = "Create";
			return View();
		}

		// POST: /AffaireCommerciales1/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "AffaireCommercialeId,Reference,Designation,Description,EtatSoum,ClientId,PersonnelId,Importance,NbHeurePrepOffre,faisabilite,DateConsultation,AffaireObtenue,ResponsableTechnique,CoordinateurCommerciale,CoordinateurRealisation,Delai_de_Soumission,Date_de_Soumission")] AffaireCommerciales affaireCommerciale)
		{
			if (ModelState.IsValid)
			{
				Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);
				string CompteurCom = param.CompteurCom.ToString("0000");
				affaireCommerciale.Reference = param.RefCom + CompteurCom /*param.CompteurCom*/;



				try
				{
					var pt = db.AffaireCommerciales
						.OrderByDescending(p => p.AffaireCommercialeId)
						.FirstOrDefault();
					String ch = pt.Reference.ToString();
					affaireCommerciale.Reference = param.RefCom + CompteurCom;
					param.CompteurCom = param.CompteurCom + 1;

				}
				catch
				{
					affaireCommerciale.Reference = param.RefCom + CompteurCom;
					param.CompteurCom = param.CompteurCom + 1;
				}
				db.AffaireCommerciales.Add(affaireCommerciale);
				db.SaveChanges();
				return RedirectToAction("FormDevis", "Vente", new { @Mode = "Create", @Code = "-1", @IdAffaireCommercial = affaireCommerciale.AffaireCommercialeId });
			}

			ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "NOM", affaireCommerciale.ClientId);
			ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.PersonnelId);
			return View(affaireCommerciale);
		}

		public ActionResult ADDAFFDEVIS()
		{
			string Designation = Request["Designation"] != null ? Request["Designation"].ToString() : string.Empty;
			string Description = Request["Description"] != null ? Request["Description"].ToString() : string.Empty;
			string EtatSoum = Request["EtatSoum"] != null ? Request["EtatSoum"].ToString() : string.Empty;
			string ClientId = Request["ClientId"] != null ? Request["ClientId"].ToString() : string.Empty;
			string PersonnelId = Request["PersonnelId"] != null ? Request["PersonnelId"].ToString() : string.Empty;
			string Importance = Request["Importance"] != null ? Request["Importance"].ToString() : string.Empty;
			string NbHeurePrepOffre = Request["NbHeurePrepOffre"] != null ? Request["NbHeurePrepOffre"].ToString() : string.Empty;
			string faisabilite = Request["faisabilite"] != null ? Request["faisabilite"].ToString() : string.Empty;
			string DateConsultation = Request["DateConsultation"] != null ? Request["DateConsultation"].ToString() : string.Empty;
			string AffaireObtenue = Request["AffaireObtenue"] != null ? Request["AffaireObtenue"].ToString() : string.Empty;
			string ResponsableTechnique = Request["ResponsableTechnique"] != null ? Request["ResponsableTechnique"].ToString() : string.Empty;
			string CoordinateurCommerciale = Request["CoordinateurCommerciale"] != null ? Request["CoordinateurCommerciale"].ToString() : string.Empty;
			string CoordinateurRealisation = Request["CoordinateurRealisation"] != null ? Request["CoordinateurRealisation"].ToString() : string.Empty;
			string Delai_de_Soumission = Request["Delai_de_Soumission"] != null ? Request["Delai_de_Soumission"].ToString() : string.Empty;
			string Date_de_Soumission = Request["Date_de_Soumission"] != null ? Request["Date_de_Soumission"].ToString() : string.Empty;
			AffaireCommerciales affaireCommerciale = new AffaireCommerciales();
			affaireCommerciale.Designation = Designation;
			affaireCommerciale.Description = Description;
			affaireCommerciale.EtatSoum = EtatSoum;
			affaireCommerciale.ClientId = int.Parse(ClientId);
			affaireCommerciale.PersonnelId = int.Parse(PersonnelId);
			affaireCommerciale.Importance = int.Parse(Importance);
			affaireCommerciale.NbHeurePrepOffre = int.Parse(NbHeurePrepOffre);
			affaireCommerciale.faisabilite = faisabilite;
			affaireCommerciale.DateConsultation = DateTime.Parse(DateConsultation);
			affaireCommerciale.AffaireObtenue = AffaireObtenue;
			affaireCommerciale.ResponsableTechnique = int.Parse(ResponsableTechnique);
			affaireCommerciale.CoordinateurCommerciale = int.Parse(CoordinateurCommerciale);
			affaireCommerciale.CoordinateurCommerciale = int.Parse(CoordinateurRealisation);
			affaireCommerciale.Delai_de_Soumission = DateTime.Parse(Delai_de_Soumission);
			affaireCommerciale.Date_de_Soumission = DateTime.Parse(Date_de_Soumission);


			Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);

			affaireCommerciale.Reference = param.RefCom + param.CompteurCom;



			try
			{
				var pt = db.AffaireCommerciales
					.OrderByDescending(p => p.AffaireCommercialeId)
					.FirstOrDefault();
				String ch = pt.Reference.ToString();
				affaireCommerciale.Reference = param.RefCom + param.CompteurCom;
				param.CompteurCom = param.CompteurCom + 1;

			}
			catch
			{
				affaireCommerciale.Reference = param.RefCom + param.CompteurCom;
				param.CompteurCom = param.CompteurCom + 1;
			}
			db.AffaireCommerciales.Add(affaireCommerciale);
			db.SaveChanges();
			return RedirectToAction("FormDevis", "Vente");


			//ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "NOM", affaireCommerciale.ClientId);
			//ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.PersonnelId);
			//return View(affaireCommerciale);

		}
		// GET: /AffaireCommerciales1/Edit/5
		public ActionResult Edit(int? id, string Designation, string Description, string Delai_de_Soumission, string Date_de_Soumission, string DateConsultation, string EtatSoum, string faisabilite, string AffaireObtenue, string Importance, string NbHeurePrepOffre, string ClientId, string PersonnelId, string ResponsableTechnique, string CoordinateurCommerciale, string CoordinateurRealisation)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AffaireCommerciales affaireCommerciale = db.AffaireCommerciales.Find(id);
			if (affaireCommerciale == null)
			{
				return HttpNotFound();
			}
			if (Session["SoclogoId"] == null)
			{
				return RedirectToAction("Login", "Societes");
			}
			int idste = (int)Session["SoclogoId"];
			ViewBag.ClientId = new SelectList(db.CLIENTS.Where(f => f.Societe == idste), "ID", "NOM", affaireCommerciale.ClientId);
			ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.PersonnelId);
			ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.ResponsableTechnique);
			ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.CoordinateurCommerciale);
			ViewBag.CoordinateurRealisation = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.CoordinateurRealisation);

			ViewBag.Designation = Designation;
			ViewBag.Description = Description;
			ViewBag.Delai_de_Soumission = Delai_de_Soumission;
			ViewBag.Date_de_Soumission = Date_de_Soumission;
			ViewBag.DateConsultation = DateConsultation;
			ViewBag.EtatSoum = EtatSoum;
			ViewBag.faisabilite = faisabilite;
			ViewBag.AffaireObtenue = AffaireObtenue;
			ViewBag.Importance = Importance;
			ViewBag.NbHeurePrepOffre = NbHeurePrepOffre;
			//ViewBag.ClientId = ClientId;
			//ViewBag.PersonnelId = PersonnelId;
			//ViewBag.ResponsableTechnique = ResponsableTechnique;
			//ViewBag.CoordinateurCommerciale = CoordinateurCommerciale;
			//ViewBag.CoordinateurRealisation = CoordinateurRealisation;
			ViewBag.Mode = "Edit";
			ViewBag.id = id;
			return View(affaireCommerciale);
		}

		// POST: /AffaireCommerciales1/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "AffaireCommercialeId,Reference,Designation,Description,EtatSoum,ClientId,PersonnelId,Importance,NbHeurePrepOffre,faisabilite,DateConsultation,AffaireObtenue,ResponsableTechnique,CoordinateurCommerciale,CoordinateurRealisation,Delai_de_Soumission,Date_de_Soumission")] AffaireCommerciales affaireCommerciale)
		{
			if (ModelState.IsValid)
			{
				db.Entry(affaireCommerciale).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "NOM", affaireCommerciale.ClientId);
			ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", affaireCommerciale.PersonnelId);
			return View(affaireCommerciale);
		}
		// GET: /AffaireCommerciales1/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AffaireCommerciales affaireCommerciale = db.AffaireCommerciales.Find(id);
			if (affaireCommerciale == null)
			{
				return HttpNotFound();
			}
			return View(affaireCommerciale);
		}

		// POST: /AffaireCommerciales1/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			DEVIS_CLIENTS DEVIS_CLIENTS = db.DEVIS_CLIENTS.Find(id);
			if (DEVIS_CLIENTS != null)
			{
				db.DEVIS_CLIENTS.Remove(DEVIS_CLIENTS);
				db.SaveChanges();
			}
			DEVIS_CLIENTS Devis = db.DEVIS_CLIENTS.Where(f => f.Id_AffaireCommerciale == id).FirstOrDefault();
			if (Devis != null)
			{
				List<LIGNES_DEVIS_CLIENTS> list1 = db.LIGNES_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == Devis.ID).ToList();
				List<lIGNES_SERVICES> list2 = db.lIGNES_SERVICES.Where(f => f.DEVIS_CLIENT == Devis.ID).ToList();
				List<lIGNES_SERVICESSSTRAITANCE> list3 = db.lIGNES_SERVICESSSTRAITANCE.Where(f => f.DEVIS_CLIENT == Devis.ID).ToList();
				List<Tracabilite_Devis_Client> list4 = db.Tracabilite_Devis_Client.Where(f => f.Id_Devis == Devis.ID).ToList();
				List<LIGNES_CUISINE_DEVIS_CLIENTS> list5 = db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(f => f.DEVIS_CLIENT == Devis.ID).ToList();

				if (list1 != null)
				{
					db.LIGNES_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == Devis.ID).ToList().ForEach(p => db.LIGNES_DEVIS_CLIENTS.Remove(p));
					db.SaveChanges();
				}
				if (list2 != null)
				{
					db.lIGNES_SERVICES.Where(p => p.DEVIS_CLIENT == Devis.ID).ToList().ForEach(p => db.lIGNES_SERVICES.Remove(p));
					db.SaveChanges();
				}
				if (list3 != null)
				{
					db.lIGNES_SERVICESSSTRAITANCE.Where(p => p.DEVIS_CLIENT == Devis.ID).ToList().ForEach(p => db.lIGNES_SERVICESSSTRAITANCE.Remove(p));
					db.SaveChanges();
				}
				if (list4 != null)
				{
					db.Tracabilite_Devis_Client.Where(p => p.Id_Devis == Devis.ID).ToList().ForEach(p => db.Tracabilite_Devis_Client.Remove(p));
					db.SaveChanges();
				}
				if (list5 != null)
				{
					List<LIGNES_DESCRIPTION_ACCESOIRE> list6 = db.LIGNES_DESCRIPTION_ACCESOIRE.Where(f => f.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == Devis.ID).ToList();
					if (list6 != null)
					{
						db.LIGNES_DESCRIPTION_ACCESOIRE.Where(p => p.LIGNES_CUISINE_DEVIS_CLIENTS.DEVIS_CLIENT == Devis.ID).ToList().ForEach(p => db.LIGNES_DESCRIPTION_ACCESOIRE.Remove(p));
						db.SaveChanges();
					}
					db.LIGNES_CUISINE_DEVIS_CLIENTS.Where(p => p.DEVIS_CLIENT == Devis.ID).ToList().ForEach(p => db.LIGNES_CUISINE_DEVIS_CLIENTS.Remove(p));
					db.SaveChanges();
				}
				if (Devis != null)
				{
					db.DEVIS_CLIENTS.Remove(Devis);
					db.SaveChanges();
				}
			}
			AffaireCommerciales affaireCommerciale = db.AffaireCommerciales.Find(id);
			db.AffaireCommerciales.Remove(affaireCommerciale);
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
