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
    public class DevisesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Devises/
        public ActionResult Index()
        {
            return View(db.Devise.ToList());
        }

        // GET: /Devises/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devise devise = db.Devise.Find(id);
            if (devise == null)
            {
                return HttpNotFound();
            }
            return View(devise);
        }

        // GET: /Devises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Devises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Nom_Devise")] Devise devise)
        {
            if (ModelState.IsValid)
            {
                db.Devise.Add(devise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(devise);
        }

        // GET: /Devises/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devise devise = db.Devise.Find(id);
            if (devise == null)
            {
                return HttpNotFound();
            }
            return View(devise);
        }

        // POST: /Devises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Nom_Devise")] Devise devise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(devise);
        }

        // GET: /Devises/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devise devise = db.Devise.Find(id);
            if (devise == null)
            {
                return HttpNotFound();
            }
            return View(devise);
        }

        // POST: /Devises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Devise devise = db.Devise.Find(id);
            db.Devise.Remove(devise);
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
