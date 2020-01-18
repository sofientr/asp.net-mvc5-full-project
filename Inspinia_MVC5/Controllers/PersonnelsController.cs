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
    public class PersonnelsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: /Personnels/
        public ActionResult Index()
        {
            return View(db.Personnels.ToList());
        }


        public ActionResult GanttPlanCharge(int? id)
        {
            Session["emp_id"] = id;
            Session["pt_id"] = 2;
            return RedirectToAction("GanttPlanCharge", "GanttDiag");
        }


        public ActionResult GanttPlanChargeComparaison(int? id)
        {
            Session["emp_id"] = id;
            Session["pt_id"] = 2;

            return RedirectToAction("GanttPlanChargeComparaison", "GanttDiag");
        }

        public ActionResult PlanChargeCalendrier(int? id)
        {
            Session["emp_id"] = id;
            Session["pt_id"] = 2;

            //List< Task >tasks= db.Tasks.Where(t => t.owner_id == id).ToList();
            return View();
        }
        public ActionResult PlanChargePerso(int? id)
        {
            Session["proj_id"] = id;
            int pers = (int)Session["emp_id"];

            Session["calendrier"] = "DataCalendrierProj";
            return RedirectToAction("PlanChargeCalendrier/" + pers);
        }
        public ActionResult PlanChargeTotal()
        {
            Session["calendrier"] = "DataCalendrier";
            int pers = (int)Session["emp_id"];
            return RedirectToAction("PlanChargeCalendrier/" + pers);
        }
        //Plan de charge Calendrier
        public JsonResult DataCalendrier()
        {
            int id = (int)Session["emp_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.owner_id == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.planned_start.ToString("u"),
                        duration = t.duration_planning,
                        order = t.SortOrder,
                        owner_id = t.owner_id,
                        progress = t.Progress,

                        open = true,
                        parent = t.ParentId,
                        type = (t.Type != null) ? t.Type : String.Empty

                    }
                ).ToArray(),
                // create links array
                links = (
                    from l in db.Links.AsEnumerable()
                    select new
                    {
                        id = l.Id,
                        source = l.SourceTaskId,
                        target = l.TargetTaskId,
                        type = l.Type
                    }
                ).ToArray()
            };

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }




        // GET: /Personnels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: /Personnels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Personnels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonnelId,Role,Email,Nom,Password,Tel")] Personnels personnel)
        {
            if (ModelState.IsValid)
            {
                db.Personnels.Add(personnel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personnel);
        }

        // GET: /Personnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: /Personnels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonnelId,Role,Email,Nom,Password,Tel,Cout_hor")] Personnels personnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personnel);
        }

        // GET: /Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: /Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personnels personnel = db.Personnels.Find(id);
            db.Personnels.Remove(personnel);
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



        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Login(Personnel per)
        //{
        //    using (Tr db = new Tr())

        //    {
        //        try
        //        {
        //            var usr = db.Personnels.Single(u => u.Email == per.Email && u.Password == per.Password);
        //            if (usr != null)
        //            {
        //                Session["UserEmail"] = usr.Email.ToString();
        //                Session["UserPass"] = usr.Password.ToString();
        //                Session["UserId"] = usr.PersonnelId;
        //                Session["UserNom"] = usr.Nom.ToString();
        //                Session["UserRole"] = usr.Role.ToString();
        //                try
        //                {
        //                    Session["nbmessage"] = 0;
        //                    int r = int.Parse(Session["UserId"].ToString());
        //                    Session["nbmessage"] = (new Models.Tr()).PersonnelProjets.Where(x => x.Personnel.PersonnelId == r && !x.Vue).Count();
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //                return RedirectToAction("../");
        //            }

        //        }

        //        catch
        //        {
        //            ModelState.AddModelError("", "Email ou mot de passe invalide!");
        //            return View("Login");

        //        }


        //    }

        //    return View();


        //}


        public ActionResult LoggedIn()
        {
            if (Session["Userid"] != null)
            {
                return View();
            }

            return RedirectToAction("Login");

        }


    }
}
