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
    public class SocietesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();
        private MED_TRABELSI db1 = new MED_TRABELSI();

        // GET: /Societes/
        public ActionResult Index()
        {
            return View(db.Societes.ToList());
        }

        // GET: /Societes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societes societes = db.Societes.Find(id);
            if (societes == null)
            {
                return HttpNotFound();
            }
            return View(societes);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult LockScreen()
        {
            return View();
        }
        // GET: /Societes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Societes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SociID,NOM,CODE_ACCES,TiersID,CentreID,Capitale")] Societes societes)
        {
            if (ModelState.IsValid)
            {
                db.Societes.Add(societes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(societes);
        }


        //
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult SendAdd(string id)
        {
            int idd = int.Parse(id);
            ViewBag.id = idd;
            return RedirectToAction("Add");
        }
        // POST: /Societes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "SociID,NOM,CODE_ACCES,TiersID,CentreID,Capitale,Direction,SCoorID")] Societes societes, [Bind(Include = "SCoorID,NOM,CODE_ACCES,EMAIL")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
            
                db.Societes.Add(societes);
                db1.Inscription.Add(inscription);
                db.SaveChanges();
                db1.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(societes);
        }



        // GET: /Societes/Edit/5

        public ActionResult Login()
        {
            List<SocieteLogo> listSociete = db.SocieteLogo.ToList();
            ViewBag.ste = listSociete;
            
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Tour()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Inscription s)
        {
            string soc = Request["soc"] != null ? Request["soc"].ToString() : string.Empty;
            Session["Soclogo"] = soc;
            int idSteCnx = db.SocieteLogo.Where(f => f.Nom_Societe == soc).FirstOrDefault().id;
            Session["SoclogoId"] = idSteCnx;
            using (MED_TRABELSI db = new MED_TRABELSI())
            {
                try
                {
                    var usr = db.Inscription.Single(u => u.NOM == s.NOM && u.CODE_ACCES == s.CODE_ACCES);

                    if (usr != null)
                    {
                        Session["ID"] = usr.InsriID.ToString();
                        Session["NOM"] = usr.NOM.ToString();
                        Session["Utilisateur"] = usr.NOM.ToString();
                        
                        Session["Direction"] = usr.Direction;
                        //Session["Direction"] = usr.Direction.ToString();
                        //Session["Nom_Soc"] = usr.CoordonneesSoc.Nom_Soc.ToString();
                        // Session["SCoorID"] = usr.CoordonneesSoc.SCoorID.ToString();
                        Session["emai"] = usr.Email.ToString();
                        Session["etat"] = usr.etat.ToString();

                        //Session["code"] = usr.CoordonneesSoc.SCoorID.ToString();
                        //Session["nm"] = usr.CoordonneesSoc.Nom_Soc.ToString();

                        //if (@Session["Direction"].ToString().Equals("da") || @Session["Direction"].ToString().Equals("dt") || @Session["etat"].ToString().Equals("1"))
                        if ((usr.etat==1) || @Session["Direction"].ToString().Equals("admin"))
                        {
                            ViewBag.direction = usr.Direction;
                            return Redirect("~/");
                        }
                        else if (@Session["Direction"].ToString().Equals("df") || @Session["Direction"].ToString().Equals("dg") || @Session["Direction"].ToString().Equals("admin"))

                        { return Redirect("~/"); }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Code acces Erroné");

                    }
                }
                catch (Exception e) {
                    
                }
               
               
               
            }
            return View();
        }

        public ActionResult LoggedIn()
        {

            if (Session["ID"] != null)
            {
                return View();

            }
            else return RedirectToAction("Login");

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societes societes = db.Societes.Find(id);
            if (societes == null)
            {
                return HttpNotFound();
            }
            return View(societes);
        }

        // POST: /Societes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SociID,NOM,CODE_ACCES,TiersID,CentreID,Capitale,Direction")] Societes societes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(societes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(societes);
        }

        // GET: /Societes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societes societes = db.Societes.Find(id);
            if (societes == null)
            {
                return HttpNotFound();
            }
            return View(societes);
        }
       
        public JsonResult GetAllSociete()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Direction> ListeSocieteLogo = db.Direction.ToList();
            List<string> list = new List<string>();
            foreach(Direction d in ListeSocieteLogo)
            {
                list.Add(d.Nom);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        

        // POST: /Societes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Societes societes = db.Societes.Find(id);
            db.Societes.Remove(societes);
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
