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
    public class CHEQUE_FOURNISSEURController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: CHEQUE_FOURNISSEUR
        public ActionResult Index()
        {
            var cHEQUE_FOURNISSEUR = db.CHEQUE_FOURNISSEUR.Include(c => c.FOURNISSEURS);
            return View(cHEQUE_FOURNISSEUR.ToList());
        }

        // GET: CHEQUE_FOURNISSEUR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR = db.CHEQUE_FOURNISSEUR.Find(id);
            if (cHEQUE_FOURNISSEUR == null)
            {
                return HttpNotFound();
            }

            return View(cHEQUE_FOURNISSEUR);
        }

        // GET: CHEQUE_FOURNISSEUR/Create
        public ActionResult Create()
        {
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "NOM");
            return View();
        }

        // POST: CHEQUE_FOURNISSEUR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumCheque,Fournisseur,Montant,DateCheque,DatePayement,Payee,Enattente,Annulee,ImPayee")] CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR)
        {
            string Status = Request["Status"] != null ? Request["Status"].ToString() : string.Empty;
            cHEQUE_FOURNISSEUR.Status = Status;
            if (ModelState.IsValid)
            {
                db.CHEQUE_FOURNISSEUR.Add(cHEQUE_FOURNISSEUR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "CODE", cHEQUE_FOURNISSEUR.Fournisseur);
            return View(cHEQUE_FOURNISSEUR);
        }

        // GET: CHEQUE_FOURNISSEUR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR = db.CHEQUE_FOURNISSEUR.Find(id);
            if (cHEQUE_FOURNISSEUR == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "NOM", cHEQUE_FOURNISSEUR.Fournisseur);
            return View(cHEQUE_FOURNISSEUR);
        }
        // POST: CHEQUE_FOURNISSEUR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumCheque,Fournisseur,Montant,DateCheque,DatePayement,Payee,Enattente,Annulee,ImPayee")] CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR)
        {
            string Status = Request["Status"] != null ? Request["Status"].ToString() : string.Empty;
            cHEQUE_FOURNISSEUR.Status = Status;
            if (ModelState.IsValid)
            {
                db.Entry(cHEQUE_FOURNISSEUR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fournisseur = new SelectList(db.FOURNISSEURS, "ID", "CODE", cHEQUE_FOURNISSEUR.Fournisseur);
            return View(cHEQUE_FOURNISSEUR);
        }

        // GET: CHEQUE_FOURNISSEUR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR = db.CHEQUE_FOURNISSEUR.Find(id);
            if (cHEQUE_FOURNISSEUR == null)
            {
                return HttpNotFound();
            }
            return View(cHEQUE_FOURNISSEUR);
        }

        // POST: CHEQUE_FOURNISSEUR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHEQUE_FOURNISSEUR cHEQUE_FOURNISSEUR = db.CHEQUE_FOURNISSEUR.Find(id);
            db.CHEQUE_FOURNISSEUR.Remove(cHEQUE_FOURNISSEUR);
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
