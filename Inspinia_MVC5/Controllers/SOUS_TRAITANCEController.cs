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
    public class SOUS_TRAITANCEController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: SOUS_TRAITANCE
        public ActionResult Index()
        {
            return View(db.SOUS_TRAITANCE.ToList());
        }

        // GET: SOUS_TRAITANCE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOUS_TRAITANCE sOUS_TRAITANCE = db.SOUS_TRAITANCE.Find(id);
            if (sOUS_TRAITANCE == null)
            {
                return HttpNotFound();
            }
            return View(sOUS_TRAITANCE);
        }

        // GET: SOUS_TRAITANCE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SOUS_TRAITANCE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT,Etat")] SOUS_TRAITANCE sOUS_TRAITANCE)
        {
            if (ModelState.IsValid)
            {
                db.SOUS_TRAITANCE.Add(sOUS_TRAITANCE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sOUS_TRAITANCE);
        }

        // GET: SOUS_TRAITANCE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOUS_TRAITANCE sOUS_TRAITANCE = db.SOUS_TRAITANCE.Find(id);
            if (sOUS_TRAITANCE == null)
            {
                return HttpNotFound();
            }
            return View(sOUS_TRAITANCE);
        }

        // POST: SOUS_TRAITANCE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT,Etat")] SOUS_TRAITANCE sOUS_TRAITANCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sOUS_TRAITANCE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sOUS_TRAITANCE);
        }

        // GET: SOUS_TRAITANCE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOUS_TRAITANCE sOUS_TRAITANCE = db.SOUS_TRAITANCE.Find(id);
            if (sOUS_TRAITANCE == null)
            {
                return HttpNotFound();
            }
            return View(sOUS_TRAITANCE);
        }

        // POST: SOUS_TRAITANCE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SOUS_TRAITANCE sOUS_TRAITANCE = db.SOUS_TRAITANCE.Find(id);
            db.SOUS_TRAITANCE.Remove(sOUS_TRAITANCE);
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
