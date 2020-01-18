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
    public class Sous_CategorieController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Sous_Categorie/
        public ActionResult Index()
        {
            var sous_categorie = db.Sous_Categorie.Include(s => s.Categorie);
            return View(sous_categorie.ToList());
        }

        // GET: /Sous_Categorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sous_Categorie sous_Categorie = db.Sous_Categorie.Find(id);
            if (sous_Categorie == null)
            {
                return HttpNotFound();
            }
            return View(sous_Categorie);
        }

        // GET: /Sous_Categorie/Create
        public ActionResult Create()
        {
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle");
            return View();
        }

        // POST: /Sous_Categorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CatID,Libelle,CentreID")] Sous_Categorie sous_Categorie)
        {
            if (ModelState.IsValid)
            {
                db.Sous_Categorie.Add(sous_Categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", sous_Categorie.CentreID);
            return View(sous_Categorie);
        }

        // GET: /Sous_Categorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sous_Categorie sous_Categorie = db.Sous_Categorie.Find(id);
            if (sous_Categorie == null)
            {
                return HttpNotFound();
            }
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", sous_Categorie.CentreID);
            return View(sous_Categorie);
        }

        // POST: /Sous_Categorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CatID,Libelle,CentreID")] Sous_Categorie sous_Categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sous_Categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", sous_Categorie.CentreID);
            return View(sous_Categorie);
        }

        // GET: /Sous_Categorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sous_Categorie sous_Categorie = db.Sous_Categorie.Find(id);
            if (sous_Categorie == null)
            {
                return HttpNotFound();
            }
            return View(sous_Categorie);
        }

        // POST: /Sous_Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sous_Categorie sous_Categorie = db.Sous_Categorie.Find(id);
            db.Sous_Categorie.Remove(sous_Categorie);
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
