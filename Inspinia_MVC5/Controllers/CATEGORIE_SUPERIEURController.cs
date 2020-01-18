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
    public class CATEGORIE_SUPERIEURController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: CATEGORIE_SUPERIEUR
        public ActionResult Index()
        {
            return View(db.CATEGORIE_SUPERIEUR.ToList());
        }

        // GET: CATEGORIE_SUPERIEUR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR = db.CATEGORIE_SUPERIEUR.Find(id);
            if (cATEGORIE_SUPERIEUR == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIE_SUPERIEUR);
        }
        public ActionResult DetailsCATEGORIE(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sous_Categorie cATEGORIE_SUPERIEUR = db.Sous_Categorie.Where(f=>f.CentreID==id).FirstOrDefault();
            if (cATEGORIE_SUPERIEUR == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIE_SUPERIEUR);
        }
        // GET: CATEGORIE_SUPERIEUR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATEGORIE_SUPERIEUR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,libelle")] CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR)
        {
            if (ModelState.IsValid)
            {
                db.CATEGORIE_SUPERIEUR.Add(cATEGORIE_SUPERIEUR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATEGORIE_SUPERIEUR);
        }

        // GET: CATEGORIE_SUPERIEUR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR = db.CATEGORIE_SUPERIEUR.Find(id);
            if (cATEGORIE_SUPERIEUR == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIE_SUPERIEUR);
        }

        // POST: CATEGORIE_SUPERIEUR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,libelle")] CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORIE_SUPERIEUR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATEGORIE_SUPERIEUR);
        }

        // GET: CATEGORIE_SUPERIEUR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR = db.CATEGORIE_SUPERIEUR.Find(id);
            if (cATEGORIE_SUPERIEUR == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORIE_SUPERIEUR);
        }

        // POST: CATEGORIE_SUPERIEUR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORIE_SUPERIEUR cATEGORIE_SUPERIEUR = db.CATEGORIE_SUPERIEUR.Find(id);
            db.CATEGORIE_SUPERIEUR.Remove(cATEGORIE_SUPERIEUR);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult LISTECATEGORIE(string ID)
        {
            int idd = int.Parse(ID);
            List<Categorie> list = db.Categorie.Where(f => f.CAT_SUP == idd).ToList();
            return View(list);
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
