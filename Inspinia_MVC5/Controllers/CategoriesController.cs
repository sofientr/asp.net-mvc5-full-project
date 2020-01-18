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
    public class CategoriesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Categories
        public ActionResult Index()
        {
            //var categorie = db.Categorie.Include(c => c.Categorie1).Include(c => c.Categorie2);
            return View(db.Categorie.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle");
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle");
            return View();
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CentreID,Libelle,Date")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categorie.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            return View(categorie);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            return View(categorie);
        }

        // POST: Categories/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CentreID,Libelle,Date")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            //ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", categorie.CentreID);
            return View(categorie);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);
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
