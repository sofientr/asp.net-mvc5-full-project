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
    public class SS_FACADEController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: SS_FACADE
        public ActionResult Index()
        {
            Session["LignesDESSSFACADE"] = null;
            var sS_FACADE = db.SS_FACADE.Include(s => s.FACADE).Include(s => s.FACADE1).Include(s => s.TYPE_FACADE);
            return View(sS_FACADE.ToList());
        }

        // GET: SS_FACADE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SS_FACADE sS_FACADE = db.SS_FACADE.Find(id);
            if (sS_FACADE == null)
            {
                return HttpNotFound();
            }
            List<LignesSSCategorie> ListeDesPoduits22 = new List<LignesSSCategorie>();
            List<DESCRIPTION_SSFAC> ListeLigne = db.DESCRIPTION_SSFAC.Where(lcmd => lcmd.ID_FAC == sS_FACADE.ID).ToList();
            foreach (DESCRIPTION_SSFAC ligne in ListeLigne)
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
            Session["LignesDESSSFACADE"] = ListeDesPoduits22;
            return View(sS_FACADE);
        }

        // GET: SS_FACADE/Create
        public ActionResult Create()
        {
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC");
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC");
            ViewBag.TYPE_SS_FACADE = new SelectList(db.TYPE_FACADE, "ID", "TYPE_FACADE1");
            return View();
        }

        // POST: SS_FACADE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_FAC,PTHT,PTTC,TYPE_SS_FACADE,TOTSURFACE,CATEGORIE")] SS_FACADE sS_FACADE)
        {
            string Categorie = Request["CATEGORIE"] != null ? Request["CATEGORIE"].ToString() : string.Empty;
            int idcat = int.Parse(Categorie);
            sS_FACADE.CATEGORIE = idcat;
            sS_FACADE.Categorie1 = db.Categorie.Where(f => f.CentreID == idcat).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.SS_FACADE.Add(sS_FACADE);
                db.SaveChanges();
                List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
                if (Session["LignesDESSSFACADE"] != null)
                {
                    ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
                }
                foreach (LignesSSCategorie ligne in ListeDesPoduits)
                {

                    DESCRIPTION_SSFAC des = new DESCRIPTION_SSFAC();
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
                    des.ID_FAC = sS_FACADE.ID;
                    db.DESCRIPTION_SSFAC.Add(des);
                    db.SaveChanges();

                }
                Session["LignesDESSSFACADE"] = null;
                return RedirectToAction("Index");
            }

            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.TYPE_SS_FACADE = new SelectList(db.TYPE_FACADE, "ID", "TYPE_FACADE1", sS_FACADE.TYPE_SS_FACADE);
            return View(sS_FACADE);
        }

        // GET: SS_FACADE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SS_FACADE sS_FACADE = db.SS_FACADE.Find(id);
            if (sS_FACADE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.TYPE_SS_FACADE = new SelectList(db.TYPE_FACADE, "ID", "TYPE_FACADE1", sS_FACADE.TYPE_SS_FACADE);
            ViewBag.CATEGORIE = new SelectList(db.Categorie, "CentreID", "Libelle", sS_FACADE.CATEGORIE);

            int count = 0;
            List<LignesSSCategorie> ListeDesPoduits22 = new List<LignesSSCategorie>();
            List<DESCRIPTION_SSFAC> ListeLigne = db.DESCRIPTION_SSFAC.Where(lcmd => lcmd.ID_FAC == sS_FACADE.ID).ToList();
            foreach (DESCRIPTION_SSFAC ligne in ListeLigne)
            {
                LignesSSCategorie NewLine = new LignesSSCategorie();
                count = ListeDesPoduits22.Count() + 1;
                while (ListeDesPoduits22.Select(cmd => cmd.ID).Contains(count))
                {
                    count = count + 1;
                }
                NewLine.ID = count;
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
            Session["LignesDESSSFACADE"] = ListeDesPoduits22;
            return View(sS_FACADE);
        }

        // POST: SS_FACADE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_FAC,PTHT,PTTC,TYPE_SS_FACADE,TOTSURFACE,CATEGORIE")] SS_FACADE sS_FACADE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sS_FACADE).State = EntityState.Modified;
                db.SaveChanges();
                List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
                if (Session["LignesDESSSFACADE"] != null)
                {
                    ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
                }
                db.DESCRIPTION_SSFAC.Where(p => p.ID_FAC == sS_FACADE.ID).ToList().ForEach(p => db.DESCRIPTION_SSFAC.Remove(p));
                db.SaveChanges();
                foreach (LignesSSCategorie ligne in ListeDesPoduits)
                {

                    DESCRIPTION_SSFAC des = new DESCRIPTION_SSFAC();
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
                    des.ID_FAC = sS_FACADE.ID;
                    db.DESCRIPTION_SSFAC.Add(des);
                    db.SaveChanges();

                }
                Session["LignesDESSSFACADE"] = null;
                return RedirectToAction("Index");
            }
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.ID_FAC = new SelectList(db.FACADE, "ID", "REF_FAC", sS_FACADE.ID_FAC);
            ViewBag.TYPE_SS_FACADE = new SelectList(db.TYPE_FACADE, "ID", "TYPE_FACADE1", sS_FACADE.TYPE_SS_FACADE);
            return View(sS_FACADE);
        }

        // GET: SS_FACADE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SS_FACADE sS_FACADE = db.SS_FACADE.Find(id);
            if (sS_FACADE == null)
            {
                return HttpNotFound();
            }
            return View(sS_FACADE);
        }

        // POST: SS_FACADE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SS_FACADE sS_FACADE = db.SS_FACADE.Find(id);
            db.SS_FACADE.Remove(sS_FACADE);
            db.SaveChanges();
            Session["LignesDESSSFACADE"] = null;
            return RedirectToAction("Index");
        }
        public JsonResult GetAllLineSSCat()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LignesSSCategorie> ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
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
            if (Session["LignesDESSSFACADE"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
            }
            count = ListeDesPoduits.Count() + 1;
            while (ListeDesPoduits.Select(cmd => cmd.ID).Contains(count))
            {
                count = count + 1;
            }
            ligne.ID = count;
            ListeDesPoduits.Add(ligne);
            Session["LignesDESSSFACADE"] = ListeDesPoduits;
            return string.Empty;
        }
      
        public JsonResult UpdatePriceCaisson()
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESSSFACADE"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
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
                totalSURFACE = totalSURFACE
            };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public string DeleteLineDevis(string parampassed)
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESSSFACADE"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
            }
            int ID = int.Parse(parampassed);
            LignesSSCategorie ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string EditLineFACADE(string ID_Produit, string PUHT_Produit, string PTHT_Produit, string TTC_Produit,string TVA_Produit)
        {
            List<LignesSSCategorie> ListeDesPoduits = new List<LignesSSCategorie>();
            if (Session["LignesDESSSFACADE"] != null)
            {
                ListeDesPoduits = (List<LignesSSCategorie>)Session["LignesDESSSFACADE"];
            }
            int ID = int.Parse(ID_Produit);
            LignesSSCategorie ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            Session["LignesDESSSFACADE"] = ListeDesPoduits;
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
