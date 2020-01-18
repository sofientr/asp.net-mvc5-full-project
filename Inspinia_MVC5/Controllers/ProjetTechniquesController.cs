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
    public class ProjetTechniquesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /ProjetTechniques1/
        public ActionResult Index()
        {
            List<ProjetTechniques> pt = db.ProjetTechniques.ToList();
            foreach(ProjetTechniques p in pt)
            {
                List<Tasks> tsk = db.Tasks.Where(t => t.ProjetTechniquesID == t.ProjetTechniquesID).ToList();
                decimal progress = 0;
                if (tsk != null)
                {
                    foreach (Tasks t in tsk)
                    {
                        if (t.ParentId == null)
                        {
                            progress = (progress + t.Progress) / 2;
                        }
                    }
                }

                if (progress >= 1)
                {
                    p.Statut = "Cloturé";
                    db.Entry(db.ProjetTechniques.Find(p.ProjetTechniqueId)).CurrentValues.SetValues(p);
                }
                else if (progress > 0)
                {
                    p.Statut = "Démarré";
                    db.Entry(db.ProjetTechniques.Find(p.ProjetTechniqueId)).CurrentValues.SetValues(p);
                }
                //if (DateTime.Compare(p.DateFinReel, DateTime.Now) <= 0)
                //{
                //    p.Statut = "Cloturé";
                //    db.Entry(db.ProjetTechniques.Find(p.ProjetTechniqueId)).CurrentValues.SetValues(p);
                //}
                //else if ((DateTime.Compare(p.DateDebutReel, DateTime.Now) <= 0) && (DateTime.Compare(p.DateFinReel,DateTime.Now)>=0))
                //{
                //    p.Statut = "Démarré";
                //    db.Entry(db.ProjetTechniques.Find(p.ProjetTechniqueId)).CurrentValues.SetValues(p);

                //}
                db.SaveChanges();
            }
            return View(db.ProjetTechniques.ToList());
        }

        public ActionResult ArchiveProjets()
        {

            return View(db.ProjetTechniques.ToList());
        }

        public ActionResult ParametrageSem(int? id)
        {
            Session["pt_id"] = id;

            return RedirectToAction("ParametrageSemList");
        }



        public ActionResult ParametrageSemList()
        {
            int id=(int) Session["pt_id"];

            List<ParametrageSemaines> list = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            return View(list);
        }

        //EditParametrageSem
        public ActionResult EditParametrageSem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParametrageSemaines paramSem = db.ParametrageSemaines.Find(id);
            if (paramSem == null)
            {
                return HttpNotFound();
            }
            return View(paramSem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditParametrageSem([Bind(Include = "Id,jourId,jourLibelle,jourTravail,doubleSeance,seance1Debut,seance1Fin,seance2Debut,seance2Fin,projetTechniqueID")] ParametrageSemaines paramSem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paramSem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ParametrageSemList/"+paramSem.projetTechniqueID);
            }
            return View(paramSem);
        }


        public ActionResult Param(int? id)
        {
            Session["pt_id"] = id;

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            List<ParametrageSemaines> paramSem = db.ParametrageSemaines.Where(p=> p.projetTechniqueID==id).ToList();
            //if (paramSem == null)
            //{
            //    return HttpNotFound();
            //}
            return RedirectToAction("Parametrage");
        }

        public ActionResult Parametrage()
        {
            int id =(int) Session["pt_id"];
            List<ParametrageSemaines> paramSem = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            return View(paramSem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Parametrage([Bind(Include = "item_Id,item_jourId,item_jourLibelle,item_jourTravail,item_doubleSeance,item_seance1Debut,item_seance1Fin,item_seance2Debut,item_seance2Fin")] ParametrageSemaines param)
        //public ActionResult Parametrage([Bind]List<ParametrageSemaine>param)
        {
            if (ModelState.IsValid)
            {
                db.Entry(param).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Parametrage");
            }
            return View(param);
        }


        public ActionResult ChargeEmploye(int? id)
        {
            Session["pt_id"] = id;

            Session["charge_emp"] = db.Tasks.ToList();
            return View(db.Personnels.ToList());
        }


        //public ActionResult AffectationPerso(int id)
        //{
        //    var viewModel = new affectationPerso();
        //    viewModel.Personnel = db.Personnels;
        //    Session["idprojettech"] = id;



        //    return View(viewModel);
        //}

        //public ActionResult AffectationPerso()
        //{
        //    var viewModel = new affectationPerso();
        //    viewModel.Personnel = db.Personnels;



        //    return View(viewModel); 
//id}/{chefProjet}/{Personnels}
        //}


        //public ActionResult sendMail(int idproject, int chefProjet, string Personnels)
        //{
        //    ProjetTechniques projet = db.ProjetTechniques.Find(idproject);
        //    Personnels chef = db.Personnels.Find(chefProjet);
        //    projet.Statut = "Demarré";
        //    var ppc = new PersonnelProjets()
        //    {
        //        Personnel = chef,
        //        ProjetTechnique = projet,
        //        Vue = false
        //    };
        //    db.PersonnelProjets.Add(ppc);
        //    var pers = Personnels.Split('_');
        //    foreach (string personne in pers) {
        //        if (!string.IsNullOrWhiteSpace(personne))
        //        {
        //            int p = int.Parse(personne);
        //            Personnel per = db.Personnels.Find(p);
        //            var pp = new PersonnelProjet()
        //            {
        //                Personnel = per,
        //                ProjetTechnique = projet,
        //                Vue = false
        //            };
        //        db.PersonnelProjets.Add(pp);
        //        }
              

        //    }
        //    db.SaveChanges();
        //    Session["idprojettech"] = 0;
        //    Response.Redirect("~/AffaireCommerciales/Index");
        //    return null;
        //}

        





        //public void sendMail(string subject)
        //{
        //    Email email = new Email();
        //    email.Subject = subject;
        //    email.Text = "cc";
        //    email.SenderId = 12;
        //    db.Emails.Add(email);
        //    db.SaveChanges();
        //}

        // GET: /ProjetTechniques1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
            if (projetTechnique == null)
            {
                return HttpNotFound();
            }
            return View(projetTechnique);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Gantt(int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction( "GanttSecond", "GanttDiag");
        }


        public ActionResult GanttRessource(int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttRessources", "GanttDiag");
        }


        public ActionResult GanttPlanification (int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttPlanification", "GanttDiag");
        }

        public ActionResult Heures(int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttHeures", "GanttDiag");
        }

        public ActionResult HeuresPlanification(int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttHeuresPlanification", "GanttDiag");
        }



        public ActionResult GanttRealisation (int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttRealisation", "GanttDiag");
        }



        public ActionResult Compare (int? id)
        {
            Session["pt_id"] = id;
            return RedirectToAction("GanttComparaison", "GanttDiag");
        }




        // POST: /ProjetTechniques1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProjetTechniqueId,ReferenceTech,DateDebut,DateFin,Cout,ClientId,PersonnelId,Statut,Designation")] ProjetTechniques projetTechnique)
        {
            if (ModelState.IsValid)
            {
                Parametrages param = db.Parametrages.First(a => a.ParametrageId == a.ParametrageId);
                projetTechnique.ReferenceTech = param.RefTech;
                try
                {
                    var pt = db.ProjetTechniques
                        .OrderByDescending(p => p.ProjetTechniqueId)
                        .FirstOrDefault();
                    String ch = pt.ReferenceTech.ToString();

                    String ch1 = ch.Remove(0, 2);
                    int compteur = Int32.Parse(ch1);
                    compteur++;
                    projetTechnique.ReferenceTech += compteur;
                }
                catch
                {

                    projetTechnique.ReferenceTech += param.CompteurTech.ToString();
                }

                db.ProjetTechniques.Add(projetTechnique);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projetTechnique);
        }

        // GET: /ProjetTechniques1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
            if (projetTechnique == null)
            {
                return HttpNotFound();
            }
            return View(projetTechnique);
        }

        // POST: /ProjetTechniques1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProjetTechniqueId,ReferenceTech,DateDebut,DateFin,Cout,ClientId,PersonnelId,Statut,Designation")] ProjetTechniques projetTechnique)
        {
            if (ModelState.IsValid)
            {
                projetTechnique.DateDebutReel = projetTechnique.DateDebut;
                projetTechnique.DateFinReel = projetTechnique.DateFin;
                projetTechnique.CoutReel = projetTechnique.Cout + 50;
                    db.Entry(projetTechnique).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projetTechnique);
        }

        // GET: /ProjetTechniques1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
            if (projetTechnique == null)
            {
                return HttpNotFound();
            }
            return View(projetTechnique);
        }

        // POST: /ProjetTechniques1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
            db.ProjetTechniques.Remove(projetTechnique);
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
