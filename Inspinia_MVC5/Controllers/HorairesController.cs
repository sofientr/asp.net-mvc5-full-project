using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class HorairesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Horaires
        public async Task<ActionResult> Index()
        {
            return View(await db.Horaire.ToListAsync());
        }

        // GET: Horaires/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = await db.Horaire.FindAsync(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // GET: Horaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Horaire1,Debut,Sortie,Debut1,Sortie2,id")] Horaire horaire)
        {
            if (ModelState.IsValid)
            {
                horaire.id = db.Horaire.Count()+2;
                db.Horaire.Add(horaire);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(horaire);
        }

        // GET: Horaires/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = await db.Horaire.FindAsync(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // POST: Horaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Horaire1,Debut,Sortie,id")] Horaire horaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(horaire).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(horaire);
        }

        // GET: Horaires/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horaire horaire = await db.Horaire.FindAsync(id);
            if (horaire == null)
            {
                return HttpNotFound();
            }
            return View(horaire);
        }

        // POST: Horaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Horaire horaire = await db.Horaire.FindAsync(id);
            db.Horaire.Remove(horaire);
            await db.SaveChangesAsync();
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
