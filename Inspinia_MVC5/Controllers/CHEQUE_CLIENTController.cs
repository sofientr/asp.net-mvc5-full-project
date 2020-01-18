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
    public class CHEQUE_CLIENTController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: CHEQUE_CLIENT
        public ActionResult Index()
        {
            return View(db.CHEQUE_CLIENT.ToList());
        }

        // GET: CHEQUE_CLIENT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_CLIENT cHEQUE_CLIENT = db.CHEQUE_CLIENT.Find(id);
            if (cHEQUE_CLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cHEQUE_CLIENT);
        }

        // GET: CHEQUE_CLIENT/Create
        public ActionResult Create()
        {
            ViewBag.Client = new SelectList(db.CLIENTS, "ID", "NOM");
            return View();
        }

        // POST: CHEQUE_CLIENT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumCheque,Client,Montant,DateCheque,DatePayement,Status")] CHEQUE_CLIENT cHEQUE_CLIENT)
        {
            string Status = Request["Status"] != null ? Request["Status"].ToString() : string.Empty;
            cHEQUE_CLIENT.Status = Status;
            if (ModelState.IsValid)
            {
                db.CHEQUE_CLIENT.Add(cHEQUE_CLIENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHEQUE_CLIENT);
        }

        // GET: CHEQUE_CLIENT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_CLIENT cHEQUE_CLIENT = db.CHEQUE_CLIENT.Find(id);
            if (cHEQUE_CLIENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client = new SelectList(db.CLIENTS, "ID", "NOM");

            return View(cHEQUE_CLIENT);
        }

        // POST: CHEQUE_CLIENT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumCheque,Client,Montant,DateCheque,DatePayement,Status")] CHEQUE_CLIENT cHEQUE_CLIENT)
        {
            string Status = Request["Status"] != null ? Request["Status"].ToString() : string.Empty;
            cHEQUE_CLIENT.Status = Status;
            if (ModelState.IsValid)
            {
                db.Entry(cHEQUE_CLIENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHEQUE_CLIENT);
        }

        // GET: CHEQUE_CLIENT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHEQUE_CLIENT cHEQUE_CLIENT = db.CHEQUE_CLIENT.Find(id);
            if (cHEQUE_CLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cHEQUE_CLIENT);
        }

        // POST: CHEQUE_CLIENT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHEQUE_CLIENT cHEQUE_CLIENT = db.CHEQUE_CLIENT.Find(id);
            db.CHEQUE_CLIENT.Remove(cHEQUE_CLIENT);
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
