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
    public class PROJET_TECHNIQUEController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: PROJET_TECHNIQUE
        public ActionResult Index()
        {
            var pROJET_TECHNIQUE = db.PROJET_TECHNIQUE.Include(p => p.AffaireCommerciales).Include(p => p.CLIENTS).Include(p => p.Personnels).Include(p => p.Personnels1).Include(p => p.Personnels2).Include(p => p.Personnels3);
            return View(pROJET_TECHNIQUE.ToList());
        }

        // GET: PROJET_TECHNIQUE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJET_TECHNIQUE pROJET_TECHNIQUE = db.PROJET_TECHNIQUE.Find(id);
            if (pROJET_TECHNIQUE == null)
            {
                return HttpNotFound();
            }
            return View(pROJET_TECHNIQUE);
        }

        // GET: PROJET_TECHNIQUE/Create
        public ActionResult Create()
        {
            ViewBag.Id_AffaireCommerciale = new SelectList(db.AffaireCommerciales, "AffaireCommercialeId", "Reference");
            ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "CODE");
            ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom");
            ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom");
            ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom");
            ViewBag.CoordinateurRéalisation = new SelectList(db.Personnels, "PersonnelId", "Nom");
            return View();
        }

        // POST: PROJET_TECHNIQUE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjetTechniqueId,Reference,Designation,Description,,EtatSoum,Cout,EnCourExecution,DateLivraison")] PROJET_TECHNIQUE pROJET_TECHNIQUE)
        {
            if (ModelState.IsValid)
            {
                db.PROJET_TECHNIQUE.Add(pROJET_TECHNIQUE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_AffaireCommerciale = new SelectList(db.AffaireCommerciales, "AffaireCommercialeId", "Reference", pROJET_TECHNIQUE.Id_AffaireCommerciale);
            ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "CODE", pROJET_TECHNIQUE.ClientId);
            ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.PersonnelId);
            ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.ResponsableTechnique);
            ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurCommerciale);
            ViewBag.CoordinateurRéalisation = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurRealisation);
            return View(pROJET_TECHNIQUE);
        }

        // GET: PROJET_TECHNIQUE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJET_TECHNIQUE pROJET_TECHNIQUE = db.PROJET_TECHNIQUE.Find(id);
            if (pROJET_TECHNIQUE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_AffaireCommerciale = new SelectList(db.AffaireCommerciales, "AffaireCommercialeId", "Reference", pROJET_TECHNIQUE.Id_AffaireCommerciale);
            ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "CODE", pROJET_TECHNIQUE.ClientId);
            ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.PersonnelId);
            ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.ResponsableTechnique);
            ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurCommerciale);
            ViewBag.CoordinateurRéalisation = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurRealisation);
            return View(pROJET_TECHNIQUE);
        }

        // POST: PROJET_TECHNIQUE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjetTechniqueId,Reference,Designation,Description,Cout,EnCourExecution,DateLivraison")] PROJET_TECHNIQUE pROJET_TECHNIQUE)
        {
            PROJET_TECHNIQUE pro = db.PROJET_TECHNIQUE.Where(f => f.ProjetTechniqueId == pROJET_TECHNIQUE.ProjetTechniqueId).FirstOrDefault();
            pro.EnCourExecution = pROJET_TECHNIQUE.EnCourExecution;
            pro.Cout = pROJET_TECHNIQUE.Cout;
            pro.DateLivraison = pROJET_TECHNIQUE.DateLivraison;
            db.SaveChanges();
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    db.Entry(pROJET_TECHNIQUE).State = EntityState.Modified;
            //    db.SaveChanges();
               
            //}
            //ViewBag.Id_AffaireCommerciale = new SelectList(db.AffaireCommerciales, "AffaireCommercialeId", "Reference", pROJET_TECHNIQUE.Id_AffaireCommerciale);
            //ViewBag.ClientId = new SelectList(db.CLIENTS, "ID", "CODE", pROJET_TECHNIQUE.ClientId);
            //ViewBag.PersonnelId = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.PersonnelId);
            //ViewBag.ResponsableTechnique = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.ResponsableTechnique);
            //ViewBag.CoordinateurCommerciale = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurCommerciale);
            //ViewBag.CoordinateurRéalisation = new SelectList(db.Personnels, "PersonnelId", "Nom", pROJET_TECHNIQUE.CoordinateurRéalisation);
            //return View(pROJET_TECHNIQUE);
        }

        // GET: PROJET_TECHNIQUE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJET_TECHNIQUE pROJET_TECHNIQUE = db.PROJET_TECHNIQUE.Find(id);
            if (pROJET_TECHNIQUE == null)
            {
                return HttpNotFound();
            }
            return View(pROJET_TECHNIQUE);
        }

        // POST: PROJET_TECHNIQUE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROJET_TECHNIQUE pROJET_TECHNIQUE = db.PROJET_TECHNIQUE.Find(id);
            db.PROJET_TECHNIQUE.Remove(pROJET_TECHNIQUE);
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
