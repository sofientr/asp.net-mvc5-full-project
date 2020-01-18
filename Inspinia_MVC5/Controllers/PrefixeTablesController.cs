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
    public class PrefixeTablesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: PrefixeTables
        public ActionResult Index()
        {
            return View(db.PrefixeTable.ToList());
        }

        // GET: PrefixeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefixeTable prefixeTable = db.PrefixeTable.Find(id);
            if (prefixeTable == null)
            {
                return HttpNotFound();
            }
            return View(prefixeTable);
        }

        // GET: PrefixeTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrefixeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Table,Id_Ste,Prefixe,Id")] PrefixeTable prefixeTable)
        {
            if (ModelState.IsValid)
            {
                db.PrefixeTable.Add(prefixeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prefixeTable);
        }

        // GET: PrefixeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefixeTable prefixeTable = db.PrefixeTable.Find(id);
            if (prefixeTable == null)
            {
                return HttpNotFound();
            }
            return View(prefixeTable);
        }

        // POST: PrefixeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Table,Id_Ste,Prefixe,Id")] PrefixeTable prefixeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prefixeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prefixeTable);
        }

        // GET: PrefixeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrefixeTable prefixeTable = db.PrefixeTable.Find(id);
            if (prefixeTable == null)
            {
                return HttpNotFound();
            }
            return View(prefixeTable);
        }

        // POST: PrefixeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrefixeTable prefixeTable = db.PrefixeTable.Find(id);
            db.PrefixeTable.Remove(prefixeTable);
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
