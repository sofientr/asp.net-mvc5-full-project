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
    public class ACCESSOIREsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: ACCESSOIREs
        public ActionResult Index()
        {
            Session["LignesACCParam"] = null;
            return View(db.ACCESSOIRE.ToList());
        }

        // GET: ACCESSOIREs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCESSOIRE aCCESSOIRE = db.ACCESSOIRE.Find(id);
            if (aCCESSOIRE == null)
            {
                return HttpNotFound();
            }
            List<LignesACCESSOIRE> ListeDesPoduits22 = new List<LignesACCESSOIRE>();
            List<DESCRIPTION_ACCESOIRE> ListeLigne = db.DESCRIPTION_ACCESOIRE.Where(lcmd => lcmd.ID_ACC == aCCESSOIRE.ID).ToList();
            foreach (DESCRIPTION_ACCESOIRE ligne in ListeLigne)
            {
                LignesACCESSOIRE des = new LignesACCESSOIRE();
                des.DESIGNATION = ligne.Designation;
                des.PUHT = (decimal)ligne.PUHT;
                des.TVA = (int)ligne.TVA;

                des.PTHT = (decimal)ligne.PTHT;
                des.TTC = (decimal)ligne.PTTC;
                des.QTE = (decimal)ligne.QTE;
                ListeDesPoduits22.Add(des);
            }
            Session["LignesACCParam"] = ListeDesPoduits22;
            return View(aCCESSOIRE);
        }

        // GET: ACCESSOIREs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCESSOIREs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOM,PTHT,PTTC")] ACCESSOIRE aCCESSOIRE)
        {
           
            if (ModelState.IsValid)
            {
                db.ACCESSOIRE.Add(aCCESSOIRE);
                db.SaveChanges();
                List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
                if (Session["LignesACCParam"] != null)
                {
                    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
                }
                foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
                {
                  
                    DESCRIPTION_ACCESOIRE des = new DESCRIPTION_ACCESOIRE();
                    des.Designation = ligne.DESIGNATION;
                    des.ID_SSCAT = ligne.IDDESIGNATION;
                    des.ID_ART = ligne.IDArticle;
                    des.PUHT = ligne.PUHT;
                    des.PTHT = ligne.PTHT;
                    des.TVA = ligne.TVA;
                    des.PTTC = ligne.TTC;
                    des.QTE = ligne.QTE;
                    des.ID_ACC = aCCESSOIRE.ID;
                    db.DESCRIPTION_ACCESOIRE.Add(des);
                    db.SaveChanges();

                }

                Session["LignesACCParam"] = null;
                return RedirectToAction("Index");
            }

            return View(aCCESSOIRE);
        }

        // GET: ACCESSOIREs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCESSOIRE aCCESSOIRE = db.ACCESSOIRE.Find(id);
            if (aCCESSOIRE == null)
            {
                return HttpNotFound();
            }
            List<LignesACCESSOIRE> ListeDesPoduits22 = new List<LignesACCESSOIRE>();
            int count = 0;
            List<DESCRIPTION_ACCESOIRE> ListeLigne = db.DESCRIPTION_ACCESOIRE.Where(lcmd => lcmd.ID_ACC == aCCESSOIRE.ID).ToList();
            foreach (DESCRIPTION_ACCESOIRE ligne in ListeLigne)
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
            Session["LignesACCParam"] = ListeDesPoduits22;
            return View(aCCESSOIRE);
        }

        // POST: ACCESSOIREs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOM,PTHT,PTTC")] ACCESSOIRE aCCESSOIRE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCESSOIRE).State = EntityState.Modified;
                db.SaveChanges();
                List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
                if (Session["LignesACCParam"] != null)
                {
                    ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
                }
                db.DESCRIPTION_ACCESOIRE.Where(p => p.ID_ACC == aCCESSOIRE.ID).ToList().ForEach(p => db.DESCRIPTION_ACCESOIRE.Remove(p));
                db.SaveChanges();
                foreach (LignesACCESSOIRE ligne in ListeDesPoduits)
                {

                    DESCRIPTION_ACCESOIRE des = new DESCRIPTION_ACCESOIRE();
                    des.Designation = ligne.DESIGNATION;
                    des.ID_ART = ligne.IDArticle;
                    des.ID_SSCAT = ligne.IDDESIGNATION;
                    des.PUHT = ligne.PUHT;
                    des.PTHT = ligne.PTHT;
                    des.TVA = ligne.TVA;
                    des.PTTC = ligne.TTC;
                    des.QTE = ligne.QTE;
                    des.ID_ACC = aCCESSOIRE.ID;
                    db.DESCRIPTION_ACCESOIRE.Add(des);
                    db.SaveChanges();

                }

                Session["LignesACCParam"] = null;
                return RedirectToAction("Index");
            }
            return View(aCCESSOIRE);
        }

        // GET: ACCESSOIREs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCESSOIRE aCCESSOIRE = db.ACCESSOIRE.Find(id);
            if (aCCESSOIRE == null)
            {
                return HttpNotFound();
            }
            return View(aCCESSOIRE);
        }

        // POST: ACCESSOIREs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACCESSOIRE aCCESSOIRE = db.ACCESSOIRE.Find(id);
            db.ACCESSOIRE.Remove(aCCESSOIRE);
            db.SaveChanges();
            Session["LignesACCParam"] = null;

            return RedirectToAction("Index");
        }
        public JsonResult GetAllLineACC()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<LignesACCESSOIRE> ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
            return Json(ListeDesPoduits, JsonRequestBehavior.AllowGet);
        }
        public string AddLineACC(string DESIGNATION, string Article, string IDDESIGNATION, string IDArticle, string QTE,string PUHT_Produit, string PTHT_Produit, string TVA_Produit, string TTC_Produit)
        {
            LignesACCESSOIRE ligne = new LignesACCESSOIRE();
            int count=0;
            ligne.IDDESIGNATION = int.Parse(IDDESIGNATION);
            ligne.IDArticle = int.Parse(IDArticle);
            ligne.DESIGNATION = DESIGNATION;
            ligne.Article = Article;
            ligne.QTE = decimal.Parse(QTE, CultureInfo.InvariantCulture);
            ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TVA = int.Parse(TVA_Produit);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
            if (Session["LignesACCParam"] != null)
            {
                ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
            }
            count = ListeDesPoduits.Count() + 1;
            while (ListeDesPoduits.Select(cmd => cmd.ID).Contains(count))
            {
                count = count + 1;
            }
            ligne.ID = count;
            ListeDesPoduits.Add(ligne);
            Session["LignesACCParam"] = ListeDesPoduits;
            return string.Empty;
        }
        public JsonResult UpdatePriceACCESSOIREs()
        {
            List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
            if (Session["LignesACCParam"] != null)
            {
                ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
            }
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
        
       public string DeleteLineACCESSOIREs(string parampassed)
        {
            List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
            if (Session["LignesACCParam"] != null)
            {
                ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
            }
            int ID = int.Parse(parampassed);
            LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ListeDesPoduits.Remove(ligne);
            return string.Empty;
        }
        public string EditLineACCESSOIRE(string ID_Produit,string IDARTICLE, string ARTICLE, string PUHT_Produit, string PTHT_Produit, string TTC_Produit,string NEW_TVA)
        {
            List<LignesACCESSOIRE> ListeDesPoduits = new List<LignesACCESSOIRE>();
            if (Session["LignesACCParam"] != null)
            {
                ListeDesPoduits = (List<LignesACCESSOIRE>)Session["LignesACCParam"];
            }
            int ID = int.Parse(ID_Produit);
            LignesACCESSOIRE ligne = ListeDesPoduits.Where(pr => pr.ID == ID).FirstOrDefault();
            ligne.IDArticle = int.Parse(IDARTICLE);
            ligne.Article = ARTICLE;
            ligne.PUHT = decimal.Parse(PUHT_Produit, CultureInfo.InvariantCulture);
            ligne.PTHT = decimal.Parse(PTHT_Produit, CultureInfo.InvariantCulture);
            ligne.TTC = decimal.Parse(TTC_Produit, CultureInfo.InvariantCulture);
            ligne.TVA= int.Parse(NEW_TVA);
            Session["LignesACCParam"] = ListeDesPoduits;
            return string.Empty;
        }
        public JsonResult GetAllCategorie()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Categorie> Listecategorie = db.Categorie.ToList();
            List<int> liste = new List<int>();
            foreach(Categorie s in Listecategorie)
            {
                liste.Add(s.CentreID);
            }
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategorie(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Categorie categorie = db.Categorie.Where(f=>f.CentreID==id).FirstOrDefault();
            return Json(categorie.Libelle, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetArticleByCategoryId(int id)
        {
            List<Prix_Achat> s = new List<Prix_Achat>();
            if (id > 0)
            {
                s = db.Prix_Achat.Where(p => p.Sous_Categorie == id).ToList();

            }

            var result = (from r in s
                          select new
                          {
                              id = r.Product_ID,
                              name = r.Libelle,
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNewArticleByCategoryId(string id)
        {
            string des = id.Replace("\n", "");
           
            List<Prix_Achat> s = new List<Prix_Achat>();
            if (id !="" && id!=null)
            {
                Sous_Categorie sscat = db.Sous_Categorie.Where(f => f.Libelle == des).FirstOrDefault();
                if(sscat!=null)
                { 
                s = db.Prix_Achat.Where(p => p.Sous_Categorie == sscat.CatID).ToList();
                }
            }

            var result = (from r in s
                          select new
                          {
                              id = r.Product_ID,
                              name = r.Libelle,
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPUArticle(int id)
        {
            Prix_Achat s = new Prix_Achat();
            if (id > 0)
            {
                s = db.Prix_Achat.Where(p => p.Product_ID == id).FirstOrDefault();

            }

            return Json(s.PU_HT_Sans_Remise, JsonRequestBehavior.AllowGet);
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
