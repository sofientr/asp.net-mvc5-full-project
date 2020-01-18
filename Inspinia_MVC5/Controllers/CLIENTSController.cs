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
    public class CLIENTSController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: CLIENTS
        public ActionResult Index()
        {
            
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login", "Societes");
            }
            int idste = (int)Session["SoclogoId"];

            return View(db.CLIENTS.Where(f=>f.Societe==idste).ToList());
        }

        // GET: CLIENTS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTS cLIENTS = db.CLIENTS.Find(id);
            if (cLIENTS == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTS);
        }

        // GET: CLIENTS/Create
        public ActionResult Create()
        {
            string Numero;
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login","Societes");
            }
            int idste = (int)Session["SoclogoId"];
           
            int Max = 0;
            PrefixeTable PrefixeTable = db.PrefixeTable.Where(f => f.Id_Ste == idste && f.Id_Table == 11).FirstOrDefault();
            DateTime d = DateTime.Today;
            if (PrefixeTable == null)
            {
                int count = db.CLIENTS.Max(p => p.ID) + 1;
                ViewBag.Numero = "C" + count;
            }
            else
            {
                 Max = db.CLIENTS.Where(f=>f.Societe== idste).Count() + 1;
                string PRF = PrefixeTable.Prefixe;
                string numPre = PRF.Replace("0000", Max.ToString("0000"));
                string count = "";
                string count1 = "";
                foreach (char c in numPre)
                {
                    if (c == 'y')
                    {
                        count += c;
                    }
                }
                foreach (char c in numPre)
                {
                    if (c == 'M')
                    {
                        count1 += c;
                    }
                }
                string date1 = d.ToString(count);
                string date2 = d.ToString(count1);
                Numero = numPre.Replace(count, date1);
                ViewBag.Numero = Numero.Replace(count1, date2);
            }
            return View();
        }

        // POST: CLIENTS/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT,Exttva")] CLIENTS cLIENTS)
        {
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login", "Societes");
            }
            if (ModelState.IsValid)
            {
                int idste = (int)Session["SoclogoId"];
                cLIENTS.Societe = idste;
                db.CLIENTS.Add(cLIENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cLIENTS);
        }

        // GET: CLIENTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTS cLIENTS = db.CLIENTS.Find(id);
            if (cLIENTS == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTS);
        }

        // POST: CLIENTS/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CODE,NOM,ADRESSE,TELEPHONE,FAX,EMAIL,SITE_WEB,ID_FISCAL,AI,NIS,RC,RIB,CONTACT,Exttva")] CLIENTS cLIENTS)
        {
            if (Session["SoclogoId"] == null)
            {
                return RedirectToAction("Login", "Societes");
            }
            if (ModelState.IsValid)
            {
                int idste = (int)Session["SoclogoId"];
                cLIENTS.Societe = idste;
                db.Entry(cLIENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLIENTS);
        }

        // GET: CLIENTS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTS cLIENTS = db.CLIENTS.Find(id);
            if (cLIENTS == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTS);
        }

        // POST: CLIENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLIENTS cLIENTS = db.CLIENTS.Find(id);
            db.CLIENTS.Remove(cLIENTS);
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
