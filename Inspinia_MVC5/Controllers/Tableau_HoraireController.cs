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
    public class Tableau_HoraireController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: Tableau_Horaire
        public ActionResult Index()
        {
            return View(db.Tableau_Horaire.ToList());
        }
        public ActionResult Horaire(string id, DateTime Date_Deb, DateTime Date_Fin)
        {
            ViewBag.id = id;
            ViewBag.Date_Deb = Date_Deb;
            ViewBag.Date_Fin = Date_Fin;
            return View(db.Horaire.ToList());
        }
        public ActionResult HoraireEdit(string id)
        {
            ViewBag.id = id;
            return View(db.Horaire.ToList());
        }
        // GET: Tableau_Horaire/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = await db.Tableau_Horaire.FindAsync(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Create
        public ActionResult Create()
        {
            int id = db.Tableau_Horaire.Count() + 1;
            ViewBag.id = id;
            //ViewBag.Mode = Mode;
            return View();
        }

        // POST: Tableau_Horaire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nom,Date_Deb,Date_Fin,id")] Tableau_Horaire tableau_Horaire)
        {
            if (ModelState.IsValid)
            {
                tableau_Horaire.id = db.Tableau_Horaire.Count();
                db.Tableau_Horaire.Add(tableau_Horaire);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = await db.Tableau_Horaire.FindAsync(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // POST: Tableau_Horaire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Nom,Date_Deb,id")] Tableau_Horaire tableau_Horaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableau_Horaire).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tableau_Horaire);
        }

        // GET: Tableau_Horaire/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tableau_Horaire tableau_Horaire = await db.Tableau_Horaire.FindAsync(id);
            if (tableau_Horaire == null)
            {
                return HttpNotFound();
            }
            return View(tableau_Horaire);
        }

        // POST: Tableau_Horaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tableau_Horaire tableau_Horaire = await db.Tableau_Horaire.FindAsync(id);
            List<horaire_jour> listt = db.horaire_jour.ToList();
            foreach (horaire_jour j in listt)
            {
                if (j.table_horaire == id)
                {
                    db.horaire_jour.Remove(j);
                    db.SaveChanges();
                }
            }
            db.Tableau_Horaire.Remove(tableau_Horaire);
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
        [HttpPost]
        public ActionResult Valider(FormCollection formCollection, string id, string Date_Deb, string Date_Fin)
        {
            int idd = int.Parse(id);
            string hor = Request["idcheck"] != null ? Request["idcheck"].ToString() : string.Empty;
            string lundi = Request["lundi"] != null ? Request["lundi"].ToString() : string.Empty;
            string Mardi = Request["Mardi"] != null ? Request["Mardi"].ToString() : string.Empty;
            string Mercredi = Request["Mercredi"] != null ? Request["Mercredi"].ToString() : string.Empty;
            string jeudi = Request["jeudi"] != null ? Request["jeudi"].ToString() : string.Empty;
            string vendredi = Request["vendredi"] != null ? Request["vendredi"].ToString() : string.Empty;
            string Samedi = Request["Samedi"] != null ? Request["Samedi"].ToString() : string.Empty;
            string Dimanche = Request["Dimanche"] != null ? Request["Dimanche"].ToString() : string.Empty;
            int hor1 = int.Parse(hor);
            int ddbj = db.Jours.Count();
            if (ddbj == 0)
            {
                db.Jours.Add(new Jours() { id = 0, Jour = "Dimanche" });
                db.Jours.Add(new Jours() { id = 1, Jour = "Lundi" });
                db.Jours.Add(new Jours() { id = 2, Jour = "Mardi" });
                db.Jours.Add(new Jours() { id = 3, Jour = "Mercredi" });
                db.Jours.Add(new Jours() { id = 4, Jour = "Jeudi" });
                db.Jours.Add(new Jours() { id = 5, Jour = "Vendredi" });
                db.Jours.Add(new Jours() { id = 6, Jour = "Samedi" });
                db.SaveChanges();
            }
            if (lundi != "")
            {
            
                horaire_jour horjour = new horaire_jour();
                List<horaire_jour> list = new List<horaire_jour>();
                int idj = db.Jours.Where(fou => fou.Jour == "lundi").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();

            }
            if (Mardi != "")
            {
                horaire_jour horjour1 = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "Mardi").FirstOrDefault().id;

                horjour1.table_horaire = idd;
                horjour1.horaire = hor1;
                horjour1.jour = idj;
                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour1.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour1.id = count;
                }
                db.horaire_jour.Add(horjour1);
                db.SaveChanges();

            }
            if (Mercredi != "")
            {
                horaire_jour horjour = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "Mercredi").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();

            }
            if (jeudi != "")
            {
                horaire_jour horjour = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "jeudi").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                //List<int> listid = new List<int>();
                //foreach(horaire_jour hj in db.horaire_jour)
                //{
                //    listid.Add(hj.id);
                //}

                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();
            }
            if (vendredi != "")
            {
                horaire_jour horjour = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "vendredi").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();

            }
            if (Samedi != "")
            {
                horaire_jour horjour = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "Samedi").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                int count = db.horaire_jour.Count();
                if(count>0)
                { 
                horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();

            }
            if (Dimanche != "")
            {
                horaire_jour horjour = new horaire_jour();
                int idj = db.Jours.Where(fou => fou.Jour == "Dimanche").FirstOrDefault().id;
                horjour.table_horaire = idd;
                horjour.horaire = hor1;
                horjour.jour = idj;
                int count = db.horaire_jour.Count();
                if (count > 0)
                {
                    horjour.id = db.horaire_jour.Max(p => p.id) + 1;
                }
                else
                {
                    horjour.id = count;
                }
                db.horaire_jour.Add(horjour);
                db.SaveChanges();

            }

            //List<LigneHorJour> ListeHorJour = new List<LigneHorJour>();
            //if (Session["ProduitsHoraireJour"] != null)
            //{
            //    ListeHorJour = (List<LigneHorJour>)Session["ProduitsHoraireJour"];
            //}
            //foreach (LigneHorJour ligne in ListeHorJour)
            //{
            //    horaire_jour horjour = new horaire_jour();
            //    int idj = db.Jours.Where(fou => fou.Jour == ligne.jour).FirstOrDefault().id;
            //    horjour.table_horaire = idd;
            //    horjour.horaire = ligne.hor;
            //    horjour.jour = idj;
            //    horjour.id = db.horaire_jour.Count();
            //    db.horaire_jour.Add(horjour);
            //    db.SaveChanges();

            //}
            Session["ProduitsHoraireJour"] = null;
            return RedirectToAction("Horaire", "Tableau_Horaire", new { @id = idd, Date_Deb , Date_Fin });
        }
        //[HttpPost]
        //public ActionResult Valider(FormCollection formCollection, string id)
        //{
        //    int idd = int.Parse(id);
        //    List<LigneHorJour> ListeHorJour = new List<LigneHorJour>();
        //    if (Session["ProduitsHoraireJour"] != null)
        //    {
        //        ListeHorJour = (List<LigneHorJour>)Session["ProduitsHoraireJour"];
        //    }
        //    foreach(LigneHorJour ligne in ListeHorJour)
        //    {
        //        horaire_jour horjour = new horaire_jour();
        //        int idj = db.Jours.Where(fou => fou.Jour == ligne.jour).FirstOrDefault().id;
        //        horjour.table_horaire = idd;
        //        horjour.horaire = ligne.hor;
        //        horjour.jour = idj;
        //        horjour.id = db.horaire_jour.Count();
        //        db.horaire_jour.Add(horjour);
        //        db.SaveChanges();

        //    }
        //    Session["ProduitsHoraireJour"] = null;
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Valider(FormCollection formCollection,string id)
        //{
        //    string[] hor = formCollection["affComId"].Split(new char[] { ',' });
        //    string lundi = Request["lundi"] != null ? Request["lundi"].ToString() : string.Empty;
        //    string Mardi = Request["Mardi"] != null ? Request["Mardi"].ToString() : string.Empty;
        //    string Mercredi = Request["Mercredi"] != null ? Request["Mercredi"].ToString() : string.Empty;
        //    string jeudi = Request["jeudi"] != null ? Request["jeudi"].ToString() : string.Empty;
        //    string vendredi = Request["vendredi"] != null ? Request["vendredi"].ToString() : string.Empty;
        //    string Samedi = Request["Samedi"] != null ? Request["Samedi"].ToString() : string.Empty;
        //    string Dimanche = Request["Dimanche"] != null ? Request["Dimanche"].ToString() : string.Empty;

        //    int idd = int.Parse(id);
        //    //List<Tableau_Horaire> list = db.Tableau_Horaire.ToList();
        //    foreach (string code in hor)
        //    {
        //        int code1 = int.Parse(code);
        //        if(lundi!=null)
        //        { 
        //        horaire_jour horjour = new horaire_jour();
        //        int idj = db.Jours.Where(fou => fou.Jour == "lundi").FirstOrDefault().id;
        //        horjour.table_horaire = idd;
        //        horjour.horaire = code1;
        //        horjour.jour = idj;
        //        horjour.id = db.horaire_jour.Count();
        //        db.horaire_jour.Add(horjour);
        //        db.SaveChanges();

        //        }
        //        if (Mardi != null)
        //        {
        //            horaire_jour horjour1 = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "Mardi").FirstOrDefault().id;

        //            horjour1.table_horaire = idd;
        //            horjour1.horaire = code1;
        //            horjour1.jour = idj;
        //            horjour1.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour1);
        //            db.SaveChanges();

        //        }
        //        if (Mercredi != null)
        //        {
        //            horaire_jour horjour = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "Mercredi").FirstOrDefault().id;
        //            horjour.table_horaire = idd;
        //            horjour.horaire = code1;
        //            horjour.jour = idj;
        //            horjour.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour);
        //            db.SaveChanges();

        //        }
        //        if (jeudi != null)
        //        {
        //            horaire_jour horjour = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "jeudi").FirstOrDefault().id;
        //            horjour.table_horaire = idd;
        //            horjour.horaire = code1;
        //            horjour.jour = idj;
        //            horjour.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour);
        //            db.SaveChanges();

        //        }
        //        if (vendredi != null)
        //        {
        //            horaire_jour horjour = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "vendredi").FirstOrDefault().id;
        //            horjour.table_horaire = idd;
        //            horjour.horaire = code1;
        //            horjour.jour = idj;
        //            horjour.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour);
        //        }
        //        if (Samedi != null)
        //        {
        //            horaire_jour horjour = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "Samedi").FirstOrDefault().id;
        //            horjour.table_horaire = idd;
        //            horjour.horaire = code1;
        //            horjour.jour = idj;
        //            horjour.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour);
        //        }
        //        if (Dimanche != null)
        //        {
        //            horaire_jour horjour = new horaire_jour();
        //            int idj = db.Jours.Where(fou => fou.Jour == "Dimanche").FirstOrDefault().id;
        //            horjour.table_horaire = idd;
        //            horjour.horaire = code1;
        //            horjour.jour = idj;
        //            horjour.id = db.horaire_jour.Count();
        //            db.horaire_jour.Add(horjour);
        //        }

        //    }
        //    //string[] jours = formCollection["affComId1"].Split(new char[] { ',' });
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public string AddLineHoraire(string lundi, string Mardi, string Mercredi, string jeudi, string vendredi, string Samedi, string Dimanche, string Horaire)
        {
            List<LigneHorJour> ListeHorJour = new List<LigneHorJour>();
            if (Session["ProduitsHoraireJour"] != null)
            {
                ListeHorJour = (List<LigneHorJour>)Session["ProduitsHoraireJour"];
            }
            if (lundi!="")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = lundi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (Mardi != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = Mardi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (Mercredi != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = Mercredi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (jeudi != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = jeudi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (vendredi != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = vendredi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (Samedi != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = Samedi;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            if (Dimanche != "")
            {
                LigneHorJour ligne = new LigneHorJour();
                ligne.jour = Dimanche;
                ligne.hor = int.Parse(Horaire);
                ListeHorJour.Add(ligne);

            }
            Session["ProduitsHoraireJour"] = ListeHorJour;
            return string.Empty;

        }
        public JsonResult GetJourParHor(string hor)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int horr = int.Parse(hor);
            List<LigneHorJour> ListeHorJour = new List<LigneHorJour>();
            List<LigneHorJour> ListeHorJour2 = new List<LigneHorJour>();
            LigneHorJour ligne1 = new LigneHorJour();
            ligne1.hor = horr;
            ListeHorJour2.Add(ligne1);
            if (Session["ProduitsHoraireJour"] != null)
            {
                ListeHorJour = (List<LigneHorJour>)Session["ProduitsHoraireJour"];
               foreach (LigneHorJour lighor in ListeHorJour)
               {
                    if(lighor.hor== horr)
                    {
                        ListeHorJour2.Add(lighor);
                    }
               }

            }
            return Json(ListeHorJour2, JsonRequestBehavior.AllowGet);

        }
    }
}
