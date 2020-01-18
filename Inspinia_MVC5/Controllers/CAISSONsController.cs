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
    public class CAISSONsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: CAISSONs
        public ActionResult Index()
        {
            Session["LignesDESCAISSON"] = null;
            var cAISSON = db.CAISSON.Include(c => c.ACCESSOIRE).Include(c => c.TYPE_CAISSON1);
            return View(cAISSON.ToList());
        }

        // GET: CAISSONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAISSON cAISSON = db.CAISSON.Find(id);
            if (cAISSON == null)
            {
                return HttpNotFound();
            }
            List<LignesSSCategorie> ListeDesPoduits22 = new List<LignesSSCategorie>();
            List<DESCRIPTION_CAISSON> ListeLigne = db.DESCRIPTION_CAISSON.Where(lcmd => lcmd.ID_CAISS_BAS == cAISSON.ID).ToList();
            foreach (DESCRIPTION_CAISSON ligne in ListeLigne)
            {
                LignesSSCategorie NewLine = new LignesSSCategorie();
                NewLine.DESIGNATION = ligne.Designation;
                NewLine.LARGE = (decimal)ligne.LARGE;
                NewLine.LONG = (decimal)ligne.LONGE;
                NewLine.PUHT = (decimal)ligne.PUHT;
                NewLine.TVA = (int)ligne.TVA;

                NewLine.PTHT = (decimal)ligne.PTHT;
                NewLine.TTC = (decimal)ligne.PTTC;
                NewLine.CHUTTE = (decimal)ligne.CHUTTE;
                NewLine.SURFACE = (decimal)ligne.SURFACE;
                NewLine.EP = (decimal)ligne.EP;
                NewLine.QTE = (decimal)ligne.QUANTITE;
                ListeDesPoduits22.Add(NewLine);
            }
            Session["LignesDESCAISSON"] = ListeDesPoduits22;
            ViewBag.ACC = cAISSON.ID_ACC;
            return View(cAISSON);
        }

        // GET: CAISSONs/Create
        public ActionResult Create()
        {
            Session["LignesDESCAISSON"] = null;
            ViewBag.ID_ACC = new SelectList(db.ACCESSOIRE, "ID", "NOM");
            ViewBag.TYPE_CAISSON = new SelectList(db.TYPE_CAISSON, "ID", "TYPE_CAISSON1");
            return View();
        }

        // POST: CAISSONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,REF_BAS,ID_ACC,ID_FAC,PTHT,PTTC,TYPE_CAISSON,TOTSURFACE,CATEGORIE")] CAISSON cAISSON)
        {
            string Acc = Request["Acc"] != null ? Request["Acc"].ToString() : string.Empty;
            string Categorie = Request["CATEGORIE"] != null ? Request["CATEGORIE"].ToString() : string.Empty;
            int accessoire = int.Parse(Acc);
            cAISSON.ID_ACC = accessoire;
            cAISSON.ACCESSOIRE = db.ACCESSOIRE.Where(f => f.ID == accessoire).FirstOrDefault();
            int idcat = int.Parse(Categorie);
            cAISSON.CATEGORIE = idcat;
            cAISSON.Categorie1 = db.Categorie.Where(f => f.CentreID == idcat).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.CAISSON.Add(cAISSON);
                db.SaveChanges();
                List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
                if (Session["LignesDESCAISSON"] != null)
                {
                    ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
                }
                foreach (LignesSSCategorie ligne in ListeDesPoduits)
                {

                    DESCRIPTION_CAISSON des = new DESCRIPTION_CAISSON();
                    des.Designation = ligne.DESIGNATION;
                    des.LARGE = ligne.LARGE;
                    des.LONGE = ligne.LONG;
                    des.PUHT = (decimal)ligne.PUHT;
                    des.TVA = (int)ligne.TVA;
                    des.PTHT = ligne.PTHT;
                    des.PTTC = ligne.TTC;
                    des.CHUTTE = ligne.CHUTTE;
                    des.SURFACE = ligne.SURFACE;
                    des.EP = ligne.EP;
                    des.QUANTITE = ligne.QTE;
                    des.ID_CAISS_BAS = cAISSON.ID;
                    db.DESCRIPTION_CAISSON.Add(des);
                    db.SaveChanges();

                }
              
                Session["LignesDESCAISSON"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.ID_ACC = new SelectList(db.ACCESSOIRE, "ID", "NOM", cAISSON.ID_ACC);
            ViewBag.TYPE_CAISSON = new SelectList(db.TYPE_CAISSON, "ID", "TYPE_CAISSON1", cAISSON.TYPE_CAISSON);
            return View(cAISSON);
        }

        // GET: CAISSONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAISSON cAISSON = db.CAISSON.Find(id);
            if (cAISSON == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ACC = new SelectList(db.ACCESSOIRE, "ID", "NOM", cAISSON.ID_ACC);
            ViewBag.TYPE_CAISSON = new SelectList(db.TYPE_CAISSON, "ID", "TYPE_CAISSON1", cAISSON.TYPE_CAISSON);
            ViewBag.CATEGORIE = new SelectList(db.Categorie, "CentreID", "Libelle", cAISSON.CATEGORIE);

            List<LignesSSCategorie> ListeDesPoduits22 = new List<LignesSSCategorie>();
            int count = 0;
            List<DESCRIPTION_CAISSON> ListeLigne = db.DESCRIPTION_CAISSON.Where(lcmd => lcmd.ID_CAISS_BAS == cAISSON.ID).ToList();
            foreach (DESCRIPTION_CAISSON ligne in ListeLigne)
            {
                LignesSSCategorie NewLine = new LignesSSCategorie();
                count = ListeDesPoduits22.Count() + 1;
                while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
                {
                    count = count + 1;
                }
                NewLine.ID = count;
                NewLine.DESIGNATION = ligne.Designation;
                NewLine.LARGE =(decimal)ligne.LARGE;
                NewLine.LONG = (decimal)ligne.LONGE;
                NewLine.PUHT = (decimal)ligne.PUHT;
                NewLine.TVA = (int)ligne.TVA;

                NewLine.PTHT = (decimal)ligne.PTHT;
                NewLine.TTC = (decimal)ligne.PTTC;
                NewLine.CHUTTE = (decimal)ligne.CHUTTE;
                NewLine.SURFACE = (decimal)ligne.SURFACE;
                NewLine.EP = (decimal)ligne.EP;
                NewLine.QTE = (decimal)ligne.QUANTITE;
                ListeDesPoduits22.Add(NewLine);
            }
            Session["LignesDESCAISSON"] = ListeDesPoduits22;
            ViewBag.ACC = cAISSON.ID_ACC;
            return View(cAISSON);
        }

        // POST: CAISSONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,REF_BAS,ID_ACC,ID_FAC,PTHT,PTTC,TYPE_CAISSON,TOTSURFACE,CATEGORIE")] CAISSON cAISSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAISSON).State = EntityState.Modified;
                db.SaveChanges();
                List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
                if (Session["LignesDESCAISSON"] != null)
                {
                    ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
                }
                db.DESCRIPTION_CAISSON.Where(p => p.ID_CAISS_BAS == cAISSON.ID).ToList().ForEach(p => db.DESCRIPTION_CAISSON.Remove(p));
                db.SaveChanges();
                foreach (LignesSSCategorie ligne in ListeDesPoduits)
                {
                    DESCRIPTION_CAISSON des = new DESCRIPTION_CAISSON();
                    des.Designation = ligne.DESIGNATION;
                    des.LARGE = ligne.LARGE;
                    des.LONGE = ligne.LONG;
                    des.PUHT = ligne.PUHT;
                    des.TVA = ligne.TVA;
                    des.PTHT = ligne.PTHT;
                    des.PTTC = ligne.TTC;
                    des.CHUTTE = ligne.CHUTTE;
                    des.SURFACE = ligne.SURFACE;
                    des.EP = ligne.EP;
                    des.QUANTITE = ligne.QTE;
                    des.ID_CAISS_BAS = cAISSON.ID;
                    db.DESCRIPTION_CAISSON.Add(des);
                    db.SaveChanges();

                }

                Session["LignesDESCAISSON"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.ID_ACC = new SelectList(db.ACCESSOIRE, "ID", "NOM", cAISSON.ID_ACC);
            ViewBag.TYPE_CAISSON = new SelectList(db.TYPE_CAISSON, "ID", "TYPE_CAISSON1", cAISSON.TYPE_CAISSON);
            return View(cAISSON);
        }

        // GET: CAISSONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAISSON cAISSON = db.CAISSON.Find(id);
            if (cAISSON == null)
            {
                return HttpNotFound();
            }
            return View(cAISSON);
        }

        // POST: CAISSONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAISSON cAISSON = db.CAISSON.Find(id);
            db.CAISSON.Remove(cAISSON);
            db.SaveChanges();
            Session["LignesDESCAISSON"] = null;
            return RedirectToAction("Index");
        }
        public JsonResult GetAllLineSSCat()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LignesSSCategorie> ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public string AddLineSSCAT(string DESIGNATION, string LONG, string LARGE, string EP, string QTE, string CHUTTE, string SURFACE, string PUHT_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            LignesSSCategorie ligne = new LignesSSCategorie();
            int count = 0;
            ligne.DESIGNATION = DESIGNATION;
            ligne.LONG = decimal.Parse(LONG, CultureInfo.InvariantCulture);
            ligne.LARGE = decimal.Parse(LARGE, CultureInfo.InvariantCulture);
            ligne.EP = decimal.Parse(EP, CultureInfo.InvariantCulture);
            ligne.QTE = decimal.Parse(QTE, CultureInfo.InvariantCulture);
            ligne.CHUTTE = decimal.Parse(CHUTTE, CultureInfo.InvariantCulture);
            ligne.SURFACE = decimal.Parse(SURFACE, CultureInfo.InvariantCulture);
            ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESCAISSON"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
            }
            count = ListeDesPoduits.Count() + 1;
            while (ListeDesPoduits.Select(cmd => cmd.ID).Contains(count))
            { 
                count = count + 1;
            }
            ligne.ID = count;
            ListeDesPoduits.Add(ligne);
            Session["LignesDESCAISSON"] = ListeDesPoduits;
            return string.Empty;
        }
        public JsonResult UpdatePriceCaisson()
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESCAISSON"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
            }
            decimal totalHT = 0;
            decimal totalTC = 0;
            decimal totalSURFACE = 0;
            foreach (LignesSSCategorie ligne in ListeDesPoduits)
            {
                totalHT += ligne.PTHT;
                totalTC += ligne.TTC;
                totalSURFACE += ligne.SURFACE;
            }
            dynamic Result = new
            {
                totalHT = totalHT,
                totalTC = totalTC,
                totalSURFACE= totalSURFACE
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public string DeleteLineCaisson(string parampassed)
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESCAISSON"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
            }
            int ID = int.Parse(parampassed);
            LignesSSCategorie ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string EditLineCaisson(string ID_Produit, string PUHT_Produit, string PTHT_Produit, string TTC_Produit,string TVA_Produit)
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESCAISSON"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESCAISSON"];
            }
            int ID = int.Parse(ID_Produit);
            LignesSSCategorie ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["LignesDESCAISSON"] = ListeDesPoduits;
            return string.Empty;
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
