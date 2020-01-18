using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class InscriptionsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Inscriptions/
        public ActionResult Index()
        {
            return View(db.Inscription.ToList());
        }
        public ActionResult AddDirection(string id)
        {
            ViewBag.id = int.Parse(id);
            Direction d = new Direction();
            return PartialView("AddDirection", d);

        }

        // GET: /Inscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // GET: /Inscriptions/Create

        public ActionResult EmailRequest()
        {
            return View();
        }
        public ActionResult ProcessRequest()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

       
        // POST: /Inscriptions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InsriID,NOM,CODE_ACCES,Email,etat")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Inscription.Add(inscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inscription);
        }
        [HttpPost]
        public ActionResult SendDirection(string Nom, string Budget, string An, string Ste, string id)
        {
            Direction direction = new Direction();
            direction.Nom = Nom;
            direction.Année = int.Parse(An);
            direction.Budget = decimal.Parse(Budget);
            direction.SocilogoID = db.SocieteLogo.Where(f => f.Nom_Societe == Ste).FirstOrDefault().id;
            db.Direction.Add(direction);
            db.SaveChanges();
            int idInsc = int.Parse(id);
            return RedirectToAction("Edit", new { id = idInsc });
            
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: /Inscriptions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "InsriID,NOM,CODE_ACCES,Email,etat")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Inscription.Add(inscription);
                db.SaveChanges();
                return RedirectToAction("Login", "Societes");
            }

            return View(inscription);
        }

        // GET: /Inscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nom = inscription.NOM;
            ViewBag.id = inscription.InsriID;
            return View(inscription);
        }

        // POST: /Inscriptions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "InsriID,NOM,CODE_ACCES,Email,etat")] Inscription inscription)
        {
            string Direction = Request["direc"] != null ? Request["direc"].ToString() : string.Empty;
            string Tel = Request["Tel"] != null ? Request["Tel"].ToString() : "0";
            Personnels p = new Personnels();
            p.Nom = inscription.NOM;
            p.Email = inscription.Email;
            p.Password = inscription.CODE_ACCES;
            p.Tel = long.Parse(Tel, CultureInfo.InvariantCulture);
            p.Role = Direction;
            db.Personnels.Add(p);
            db.SaveChanges();
            PERSONNEL_SOCIETE per_Soc = new PERSONNEL_SOCIETE();
            string societe= Session["Soclogo"].ToString();
            int idSoceteConnecte = db.SocieteLogo.Where(f => f.Nom_Societe == societe).FirstOrDefault().id;
            per_Soc.Id_Personnel = p.PersonnelId;
            per_Soc.Id_Societe = idSoceteConnecte;
            db.PERSONNEL_SOCIETE.Add(per_Soc);
            db.SaveChanges();
            inscription.Direction = Direction;
            if (ModelState.IsValid)
            {
                db.Entry(inscription).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(inscription);
        }
        public JsonResult GetAllDirection()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Direction> Liste = db.Direction.ToList();
            //List<string> list = new List<string>();
            return Json(Liste, JsonRequestBehavior.AllowGet);
        }
        // GET: /Inscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // POST: /Inscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscription inscription = db.Inscription.Find(id);
            db.Inscription.Remove(inscription);
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
