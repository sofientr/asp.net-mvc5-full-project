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
    public class MarquesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Marques/
        public ActionResult Index()
        {
            return View(db.Marque.ToList());
        }

        // GET: /Marques/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marque.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // GET: /Marques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Marques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Nom_marque")] Marque marque)
        {
            if (ModelState.IsValid)
            {
                db.Marque.Add(marque);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }

            return View(marque);
        }

        // GET: /Marques/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marque.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: /Marques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Nom_marque")] Marque marque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marque);
        }

        // GET: /Marques/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marque.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: /Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Marque marque = db.Marque.Find(id);
            db.Marque.Remove(marque);
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
