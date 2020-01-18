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
    public class FOURNISSEURS1Controller : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: FOURNISSEURS1
        public ActionResult Index()
        {
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login", "Societes");
            }
            int idste = (int)Session["SoclogoId"];
            List<FOURNISSEURS> frs = db.FOURNISSEURS.Where(f=>f.Id_Ste== idste).ToList();
            foreach (FOURNISSEURS f in frs)
            {
                List<DEVIS_FOURNISSEURS> df = db.DEVIS_FOURNISSEURS.Where(fo => fo.FOURNISSEUR == f.ID && fo.Societes == idste).ToList();
                List<COMMANDES_FOURNISSEURS> cf = db.COMMANDES_FOURNISSEURS.Where(fo => fo.FOURNISSEUR == f.ID && fo.Societes == idste).ToList();
                List<BONS_RECEPTIONS_FOURNISSEURS> brf = db.BONS_RECEPTIONS_FOURNISSEURS.Where(fo => fo.FOURNISSEUR == f.ID && fo.Societes == idste).ToList();
                List<FACTURES_FOURNISSEURS> ff = db.FACTURES_FOURNISSEURS.Where(fo => fo.FOURNISSEUR == f.ID && fo.Societes == idste).ToList();
                List<AVOIRS_FOURNISSEURS> avf = db.AVOIRS_FOURNISSEURS.Where(fo => fo.FOURNISSEUR == f.ID && fo.Societes == idste).ToList();
                List<Prix_Achat> Prix_Achat = db.Prix_Achat.Where(fo => fo.Fournisseur == f.ID).ToList();

                if ((df.Count != 0) || (cf.Count != 0) || (brf.Count != 0) || (ff.Count != 0) || (avf.Count != 0)|| (Prix_Achat.Count != 0))
                {
                    f.Etat = true;
                    db.SaveChanges();
                }
                else
                {
                    f.Etat = false;
                    db.SaveChanges();

                }
            }

            return View(db.FOURNISSEURS.Where(f => f.Id_Ste == idste).ToList());
        }

        // GET: FOURNISSEURS1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOURNISSEURS fOURNISSEURS = db.FOURNISSEURS.Find(id);
            if (fOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(fOURNISSEURS);
        }

        // GET: FOURNISSEURS1/Create
        public ActionResult Create()
        {
            int count = 0;
            string Numero;
            int idste = (int)Session["SoclogoId"];
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login", "Societes");
            }

            count = db.FOURNISSEURS.Where(f => f.Id_Ste == idste).Count() + 1;
            Numero = "F" + count;

            while (db.FOURNISSEURS.Where(f => f.Id_Ste == idste).Select(cmd => cmd.CODE).Contains(Numero))
            {
                count++;
                Numero = "F" + count;

            }
            ViewBag.Numero = Numero;
            return View();
            
        }

        // POST: FOURNISSEURS1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT")] FOURNISSEURS fOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
               

                int idste = (int)Session["SoclogoId"];
                fOURNISSEURS.Id_Ste = idste;
                db.FOURNISSEURS.Add(fOURNISSEURS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fOURNISSEURS);
        }

        // GET: FOURNISSEURS1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOURNISSEURS fOURNISSEURS = db.FOURNISSEURS.Find(id);
            if (fOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(fOURNISSEURS);
        }

        // POST: FOURNISSEURS1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT")] FOURNISSEURS fOURNISSEURS)
        {

            int idste = (int)Session["SoclogoId"];
            fOURNISSEURS.Id_Ste = idste;
            if (ModelState.IsValid)
            {
               
                db.Entry(fOURNISSEURS).State = EntityState.Modified;
                db.SaveChanges();



				List<lignefournisseurtiers> ListeDestiers = new List<lignefournisseurtiers>();
				if (Session["GetAllLineTIERS"] != null)
				{
					ListeDestiers = (List<lignefournisseurtiers>)Session["GetAllLineTIERS"];
				}
				if (Session["GetAllLineTIERS"] != null)
				{
					foreach (lignefournisseurtiers Ligne in ListeDestiers)
					{

						FOURNISSEURS_TIERS UneLigne = new FOURNISSEURS_TIERS();
						UneLigne.NOM = Ligne.NOM;
						UneLigne.PRENOM = Ligne.PRENOM;
						UneLigne.EMAIL = Ligne.EMAIL;
						UneLigne.NUMERO = Ligne.NUMERO;
						UneLigne.FOURNISSEUR = fOURNISSEURS.ID;
						
						db.FOURNISSEURS_TIERS.Add(UneLigne);
						db.SaveChanges();
					}

				}
				return RedirectToAction("Index");
            }

			return View(fOURNISSEURS);
        }

        // GET: FOURNISSEURS1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOURNISSEURS fOURNISSEURS = db.FOURNISSEURS.Find(id);
            if (fOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(fOURNISSEURS);
        }

        // POST: FOURNISSEURS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FOURNISSEURS fOURNISSEURS = db.FOURNISSEURS.Find(id);
            db.FOURNISSEURS.Remove(fOURNISSEURS);
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
		public JsonResult GetAllLineTIERS()
		{
			db.Configuration.ProxyCreationEnabled = false;

			List<lignefournisseurtiers> ListeDesPoduits = (List<lignefournisseurtiers>)Session["GetAllLineTIERS"];
			return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
		}
		public string AddLinetiers(int ID1, string NOM1, int NUMERO1, string PRENOM1, string EMAIL1)
		{
			lignefournisseurtiers ligne = new lignefournisseurtiers();
			ligne.ID = ID1;
			ligne.NOM = NOM1;
			ligne.PRENOM = PRENOM1;
			ligne.NUMERO = NUMERO1;
			ligne.EMAIL = EMAIL1;
			List<lignefournisseurtiers> ListeDesPoduits = new List<lignefournisseurtiers>();
			if (Session["GetAllLineTIERS"] != null)
			{
				ListeDesPoduits = (List<lignefournisseurtiers>)Session["GetAllLineTIERS"];
			}

			ListeDesPoduits.Add(ligne);
			Session["GetAllLineTIERS"] = ListeDesPoduits;
			return string.Empty;
		}
		public string DeleteLineTIERS(string parampassed)
		{
			List<lignefournisseurtiers> ListeDesSevices = new List<lignefournisseurtiers>();
			if (Session["GetAllLineTIERS"] != null)
			{
				ListeDesSevices = (List<lignefournisseurtiers>)Session["GetAllLineTIERS"];
			}
			int ID = int.Parse(parampassed);
			lignefournisseurtiers ligne = ListeDesSevices.Where(pr => pr.ID == ID).FirstOrDefault();
			ListeDesSevices.Remove(ligne);
			Session["GetAllLineTIERS"] = ListeDesSevices;

			return string.Empty;
		}

	}
}
