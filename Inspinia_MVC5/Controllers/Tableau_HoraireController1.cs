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
    public class Tableau_HoraireController : Controller
    {
        private Tr db = new Tr();

        // GET: Tableau_Horaire
        public ActionResult Index()
        {
            return View(db.Tableau_Horaire.ToList());
        }

        // GET: Tableau_Horaire/Details/5
        public ActionResult Horaire(string id, DateTime Date_Deb, DateTime Date_Fin)
        {
            ViewBag.id = id;
            ViewBag.Date_Deb = Date_Deb;
            ViewBag.Date_Fin = Date_Fin;
            return View(db.Horaire.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = db.Tableau_Horaire.Find(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tableau_Horaire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom,Date_Deb,id,Date_Fin")] Tableau_Horaire tableau_Horaire)
        {
            if (ModelState.IsValid)
            {
                db.Tableau_Horaire.Add(tableau_Horaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = db.Tableau_Horaire.Find(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // POST: Tableau_Horaire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nom,Date_Deb,id,Date_Fin")] Tableau_Horaire tableau_Horaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableau_Horaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = db.Tableau_Horaire.Find(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // POST: Tableau_Horaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tableau_Horaire tableau_Horaire = db.Tableau_Horaire.Find(id);
            db.Tableau_Horaire.Remove(tableau_Horaire);
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
