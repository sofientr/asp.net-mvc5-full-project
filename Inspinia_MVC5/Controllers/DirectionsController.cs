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
    public class DirectionsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Directions/
        public ActionResult Index()
        {
            var direction = db.Direction.Include(d => d.SocieteLogo)/*.Include(d => d.CoordonneesSoc)*/;
            Session["DroitAcces"] = null;
            return View(direction.ToList());
        }
        public ActionResult BudgetTotal()
        {
            var direction = db.Direction.Include(d => d.Societes).Include(d => d.CoordonneesSoc);
            return View(direction.ToList());
        }

        public ActionResult BudgetParDirection()
        {
            var direction = db.Direction.Include(d => d.Societes).Include(d => d.CoordonneesSoc);
            return View(direction.ToList());
        }
        public string AddLignesDroitAcces(string droit)
        {
            string[] droit1 = droit.Split('T');
            string droitAcces = droit1[0];
            string table = droit1[1];
            int idtable = int.Parse(table);
            List<LigneDirectionTable> ListeDesDroit = new List<LigneDirectionTable>();
            if (Session["DroitAcces"] != null)
            {
                ListeDesDroit = (List<LigneDirectionTable>)Session["DroitAcces"];
            }
            LigneDirectionTable d = ListeDesDroit.Where(f => f.idtable == idtable).FirstOrDefault();
            if(droitAcces == "ajout")
            {
                d.ajout = true;
            }
            if (droitAcces == "Modif")
            {
                d.modification = true;

            }
            if (droitAcces == "supp")
            {
                d.suppression = true;

            }
            if (droitAcces == "aff")
            {
                d.affichage = true;

            }
            Session["DroitAcces"] = ListeDesDroit;
            return string.Empty;
        }

        public string DeleteLignesDroitAcces(string droit)
        {
            string[] droit1 = droit.Split('T');
            string droitAcces = droit1[0];
            string table = droit1[1];
            int idtable = int.Parse(table);
            List<LigneDirectionTable> ListeDesDroit = new List<LigneDirectionTable>();
            if (Session["DroitAcces"] != null)
            {
                ListeDesDroit = (List<LigneDirectionTable>)Session["DroitAcces"];
            }
            LigneDirectionTable d = ListeDesDroit.Where(f => f.idtable == idtable).FirstOrDefault();
            if (droitAcces == "ajout")
            {
                d.ajout = false;
            }
            if (droitAcces == "Modif")
            {
                d.modification = false;

            }
            if (droitAcces == "supp")
            {
                d.suppression = false;

            }
            if (droitAcces == "aff")
            {
                d.affichage = false;

            }
            Session["DroitAcces"] = ListeDesDroit;
            return string.Empty;
        }

        public ActionResult BudgetParDirectionSecteur()
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

        public ActionResult BudgetParCC()
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
        // GET: /Directions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            //List<LigneDirectionTable> listDroit = new List<LigneDirectionTable>();
            //List<DROIT_ACCES> listeDroitAcces = db.DROIT_ACCES.Where(f => f.Id_Direction == id).ToList();
            //if(listeDroitAcces.Count!=0)
            //{
            //    foreach (DROIT_ACCES d in listeDroitAcces)
            //    {
            //        LigneDirectionTable ligne = new LigneDirectionTable();
            //        ligne.idtable = (int)d.Id_Table;
            //        ligne.iddirection = (int)d.Id_Direction;
            //        ligne.ajout = (bool)d.Ajout;
            //        ligne.modification = (bool)d.Modification;
            //        ligne.suppression = (bool)d.Suppression;
            //        listDroit.Add(ligne);
            //    }
            //}
            ViewBag.id = id;
            //Session["DroitAcces"] = listDroit;
            return View(direction);
        }

        // GET: /Directions/Create
        public ActionResult Create()
        {
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM");
            ViewBag.SCoorID = new SelectList(db.CoordonneesSoc, "SCoorID", "Nom_Soc");
            List<LigneDirectionTable> list = new List<LigneDirectionTable>();
            List<TABLES_BD> list1 = db.TABLES_BD.ToList();
            foreach(TABLES_BD t in list1)
            {
                LigneDirectionTable d = new LigneDirectionTable();
                d.idtable = t.Id;
                d.ajout = false;
                d.suppression = false;
                d.modification = false;
                d.affichage = false;
                list.Add(d);
               
            }
            Session["DroitAcces"] = list;
            return View();
        }

        // POST: /Directions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "DiretionID,Nom,SociID,Budget,Année,SocilogoID")] Direction direction)
        {
            string Ste = Request["Ste"] != null ? Request["Ste"].ToString() : string.Empty;
            int idste = db.SocieteLogo.Where(f => f.Nom_Societe == Ste).FirstOrDefault().id;
            direction.SocilogoID = idste;
            if (ModelState.IsValid)
            {
                db.Direction.Add(direction);
                db.SaveChanges();
                List<LigneDirectionTable> ListeDesDirection = new List<LigneDirectionTable>();
                if (Session["DroitAcces"] != null)
                {
                    ListeDesDirection = (List<LigneDirectionTable>)Session["DroitAcces"];
                }
                foreach (LigneDirectionTable dt in ListeDesDirection)
                {
                    DROIT_ACCES d = new DROIT_ACCES();
                    d.Id_Table = dt.idtable;
                    d.Id_Direction = direction.DiretionID;
                    d.Ajout = dt.ajout;
                    d.Modification = dt.modification;
                    d.Suppression = dt.suppression;
                    d.Affichage = dt.affichage;
                    db.DROIT_ACCES.Add(d);
                    db.SaveChanges();
                }
                Session["DroitAcces"] = null;
                return RedirectToAction("Index");
            }

            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", direction.SociID);
            ViewBag.SCoorID = new SelectList(db.CoordonneesSoc, "SCoorID", "Nom_Soc", direction.SCoorID);
           
            return View(direction);
        }

        // GET: /Directions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", direction.SociID);
            ViewBag.SCoorID = new SelectList(db.CoordonneesSoc, "SCoorID", "Nom_Soc", direction.SCoorID);
            return View(direction);
        }

        // POST: /Directions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DiretionID,Nom,SociID,Budget,Année,SCoorID")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", direction.SociID);
            ViewBag.SCoorID = new SelectList(db.CoordonneesSoc, "SCoorID", "Nom_Soc", direction.SCoorID);
            return View(direction);
        }

        // GET: /Directions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // POST: /Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direction direction = db.Direction.Find(id);
            db.Direction.Remove(direction);
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
