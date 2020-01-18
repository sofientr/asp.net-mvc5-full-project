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
    public class DecaissementsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Decaissements/
        public ActionResult Index()
        { 
            var viewModel = new MonViewModel();
            viewModel.Modele1s = db.Decaissement;
            viewModel.Modele3s = db.Categorie;
            viewModel.Modele2s = db.Sous_Categorie;
            viewModel.Modele4s = db.Projets;
            viewModel.Modele5s = db.Facturation;
            viewModel.Modele7s = db.Societes;
            viewModel.Modele8s = db.Direction;
            return View(viewModel);
        }


        public ActionResult MesDemandesA()
        {
            var decaissement = db.Decaissement.Include(d => d.Categorie).Include(d => d.Direction).Include(d => d.Categorie);
            return View(decaissement.ToList());
        }
        public ActionResult DAttentes()
        {
            var decaissement = db.Decaissement.Include(d => d.Categorie).Include(d => d.Direction).Include(d => d.Categorie);
            return View(decaissement.ToList());
        }
        // GET: /Decaissements/Details/5
        public ActionResult Recap()
        {
            var viewModel = new MonViewModel();
            viewModel.Modele1s = db.Decaissement;
            viewModel.Modele3s = db.Categorie;
            viewModel.Modele2s = db.Sous_Categorie;
            viewModel.Modele4s = db.Projets;
            viewModel.Modele5s = db.Facturation;

            return View(viewModel);
        }

        public ActionResult Acceuil()
        {
            var viewModel = new MonViewModel();
            viewModel.Modele1s = db.Decaissement;
            viewModel.Modele2s = db.Sous_Categorie;
            viewModel.Modele3s = db.Categorie;
            viewModel.Modele4s = db.Projets;
            viewModel.Modele5s = db.Facturation;
            viewModel.Modele7s = db.Societes;
            viewModel.Modele8s = db.Direction;
            return View(viewModel);
        }
        public ActionResult Acceuil_previsionnel()
        {
            var viewModel = new MonViewModel();
            viewModel.Modele1s = db.Decaissement;
            viewModel.Modele3s = db.Categorie;
            viewModel.Modele2s = db.Sous_Categorie;
            viewModel.Modele4s = db.Projets;
            viewModel.Modele5s = db.Facturation;
            viewModel.Modele7s = db.Societes;
            return View(viewModel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decaissement decaissement = db.Decaissement.Find(id);
            if (decaissement == null)
            {
                return HttpNotFound();
            }
            return View(decaissement);
        }

        // GET: /Decaissements/Create
        public ActionResult Couts()
        {
            Categorie frnds = new Categorie();

            return PartialView("CC", frnds);
        }
        public void AddCouts(string Libelle)
        {
            Categorie NewElement = new Categorie();
            NewElement.Libelle = Libelle;
           


            db.Categorie.Add(NewElement);
            db.SaveChanges();
        }
        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.Sous_Categorie, "CatID", "Libelle");
            ViewBag.DiretionID = new SelectList(db.Direction, "DiretionID", "Nom");
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle");
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "NOM_PROJET");

            return View();
        }

        // POST: /Decaissements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "DecaissID,CentreID,DiretionID,Date,Prix,CatID,Mo_pay,Demandeur,qte,etat,PrID")] Decaissement decaissement)
        {
            if (ModelState.IsValid)
            {
                db.Decaissement.Add(decaissement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Sous_Categorie, "CatID", "Libelle", decaissement.CatID);
            ViewBag.DiretionID = new SelectList(db.Direction, "DiretionID", "Nom", decaissement.DiretionID);
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", decaissement.CentreID);
            ViewBag.PrID = new SelectList(db.Projets, "PrID", "NOM_PROJET");

            return View(decaissement);
        }
       

        // GET: /Decaissements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decaissement decaissement = db.Decaissement.Find(id);
            if (decaissement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Sous_Categorie, "CatID", "Libelle", decaissement.CatID);
            ViewBag.DiretionID = new SelectList(db.Direction, "DiretionID", "Nom", decaissement.DiretionID);
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", decaissement.CentreID);
            return View(decaissement);
        }

        // POST: /Decaissements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "DecaissID,CentreID,DiretionID,Date,Prix,CatID,Mo_pay,etat,Demandeur,qte")] Decaissement decaissement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decaissement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Sous_Categorie, "CatID", "Libelle", decaissement.CatID);
            ViewBag.DiretionID = new SelectList(db.Direction, "DiretionID", "Nom", decaissement.DiretionID);
            ViewBag.CentreID = new SelectList(db.Categorie, "CentreID", "Libelle", decaissement.CentreID);
            return View(decaissement);
        }

        // GET: /Decaissements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decaissement decaissement = db.Decaissement.Find(id);
            if (decaissement == null)
            {
                return HttpNotFound();
            }
            return View(decaissement);
        }

        // POST: /Decaissements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decaissement decaissement = db.Decaissement.Find(id);
            db.Decaissement.Remove(decaissement);
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
