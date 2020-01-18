using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class SocieteLogoesController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();

        // GET: SocieteLogoes
        public ActionResult Index()
        {
            return View(db.SocieteLogo.ToList());
        }
        public ActionResult Societe()
        {
            return PartialView("Societe");
        }
        public ActionResult Direction(string ste)
        {
            List<LigneDirectionTable> list = new List<LigneDirectionTable>();
            List<TABLES_BD> list1 = db.TABLES_BD.ToList();
            foreach (TABLES_BD t in list1)
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
            //ViewBag.ste = ste;
            int idste = db.SocieteLogo.Max(p => p.id);
            string nameste = db.SocieteLogo.Where(f => f.id == idste).FirstOrDefault().Nom_Societe;
            ViewBag.ste = nameste;
            return PartialView("Direction");
        }
        public ActionResult Prefixe()
        {
            return PartialView("Prefixe");
        }
        public ActionResult PrefixeEdit(int? id)
        {
            ViewBag.id = id;
            Session["IdPrefixe"] = id;
            return View();
        }
        public ActionResult PrefixeEditValidation(int? id)
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
            //ViewBag.id = idprefixe;
            return View(prefixeTable);
        }
        public ActionResult PrefixeEditValidation2([Bind(Include = "Id_Table,Id_Ste,Prefixe,Id")] PrefixeTable prefixeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prefixeTable).State = EntityState.Modified;
                db.SaveChanges();
                int id = (int)Session["IdPrefixe"];
                return RedirectToAction("PrefixeEdit",new{id=id});
            }
            return View(prefixeTable);
        }
        public ActionResult Admin()
        {
            int idste = db.Direction.Max(p => p.DiretionID);
            string nameste = db.Direction.Where(f => f.DiretionID == idste).FirstOrDefault().Nom;
            ViewBag.direction = nameste;
            return PartialView("Admin");
        }
        // GET: SocieteLogoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocieteLogo societeLogo = db.SocieteLogo.Find(id);
            if (societeLogo == null)
            {
                return HttpNotFound();
            }
            return View(societeLogo);
        }

        // GET: SocieteLogoes/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Tour()
        {
            return View();
        }
        public ActionResult CreatePrefixe()
        {
            return View("CreatePrefixe");
        }
        // POST: SocieteLogoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom_Societe,logo,id,file,Adresse,mail,RC,MF,IBAN,RIB,Agence")] SocieteLogo societeLogo)
        {

            //HttpPostedFileBase image = HttpPostedFileBase Request["file"];
            string fileName = Path.GetFileNameWithoutExtension(societeLogo.file.FileName);
            string extention = Path.GetExtension(societeLogo.file.FileName);
            societeLogo.file.SaveAs(Path.Combine(Server.MapPath("/Images"), (societeLogo+extention)));
            
            if (ModelState.IsValid)
            {
                db.SocieteLogo.Add(societeLogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(societeLogo);
        }
        public ActionResult SendSte([Bind(Include = "Nom_Societe,logo,id,file,Adresse,mail,RC,MF,IBAN,RIB,Agence")] SocieteLogo societeLogo)
        {
            
            if (ModelState.IsValid)
            {
                db.SocieteLogo.Add(societeLogo);
                db.SaveChanges();
                Session["NewSociete"] = societeLogo.Nom_Societe;
                Session["NewSociete2"] = societeLogo.Nom_Societe;
                Session["IdNewSociete"] = societeLogo.id;
                ViewBag.Ste = societeLogo.Nom_Societe;
                return RedirectToAction("Tour");
            }

            return PartialView("Societe");
        }
        public ActionResult SendDirection([Bind(Include = "DiretionID,Nom,SociID,Budget,Année,SocilogoID")] Direction direction)
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
                Session["NewDirection"] = direction.Nom;
                Session["IdNewDirection"] = direction.DiretionID;
                return RedirectToAction("Tour");
            }

            ViewBag.SociID = new SelectList(db.Societes, "SociID", "NOM", direction.SociID);
            ViewBag.SCoorID = new SelectList(db.CoordonneesSoc, "SCoorID", "Nom_Soc", direction.SCoorID);

            return PartialView("Direction");
        }

        public ActionResult SendPrefixe(FormCollection formCollection)
        {
            string[] ids = formCollection["pref"].Split(new char[] { ',' });
            List<TABLES_BD> ListTablesBD = db.TABLES_BD.ToList();
            int i = 0;
            foreach(TABLES_BD T in ListTablesBD)
            {
                PrefixeTable PreF = new PrefixeTable();
                PreF.Prefixe = ids[i];
                PreF.Id_Table = T.Id;
                PreF.Id_Ste =db.SocieteLogo.Max(p => p.id); ;
                db.PrefixeTable.Add(PreF);
                db.SaveChanges();
                i++;
            }
            Session["Prefixe"] = 1;
            return RedirectToAction("Tour");
        }

        public ActionResult SendAdmin([Bind(Include = "PersonnelId,Role,Email,Nom,Password,Tel")] Personnels personnel)
        {
            //int iddirection = (int)Session["IdNewDirection"];
            int idste = db.Direction.Max(p => p.DiretionID);
            PERSONNEL_SOCIETE PersSteDirecRole = new PERSONNEL_SOCIETE();
            PersSteDirecRole.Id_Direction =db.Direction.Max(p => p.DiretionID);
            PersSteDirecRole.Id_Personnel = personnel.PersonnelId;
            PersSteDirecRole.Id_Societe =db.SocieteLogo.Max(p => p.id); ;
            db.PERSONNEL_SOCIETE.Add(PersSteDirecRole);
            db.SaveChanges();
            Inscription insc = new Inscription();
            insc.NOM = personnel.Nom;
            insc.Email = personnel.Email;
            insc.CODE_ACCES = personnel.Password;
            insc.Direction = db.Direction.Where(f => f.DiretionID == idste).FirstOrDefault().Nom;
            //insc.Direction=
            //insc.t = long.Parse(Tel, CultureInfo.InvariantCulture);
            //insc.Direction = Direction;
            //insc.
            insc.etat = 1;
            db.Inscription.Add(insc);
            db.SaveChanges();
            Session["Admin"] = personnel.Nom;
            if (ModelState.IsValid)
            {
                db.Personnels.Add(personnel);
                db.SaveChanges();
                return RedirectToAction("Tour");
            }

            return View(personnel);
        }
        public ActionResult saveSoc(SocieteLogo societeLogo)
        {

            string fileName = Path.GetFileNameWithoutExtension(societeLogo.file.FileName);
            string extention = Path.GetExtension(societeLogo.file.FileName);
            societeLogo.file.SaveAs(Path.Combine(Server.MapPath("/Images"), (fileName + extention)));
            SocieteLogo stelogo = new SocieteLogo();
            stelogo.logo = (fileName + extention);
            stelogo.Nom_Societe = societeLogo.Nom_Societe;
            stelogo.RC = societeLogo.RC;
            stelogo.MF = societeLogo.MF;
            stelogo.mail = societeLogo.mail;
            stelogo.RIB = societeLogo.RIB;
            stelogo.IBAN = societeLogo.IBAN;
            stelogo.Agence = societeLogo.Agence;
            stelogo.Adresse = societeLogo.Adresse;
            stelogo.Tel = societeLogo.Tel;

            if (ModelState.IsValid)
            {

                db.SocieteLogo.Add(stelogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(societeLogo);

        }
        // GET: SocieteLogoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocieteLogo societeLogo = db.SocieteLogo.Find(id);
            if (societeLogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.file = societeLogo.logo;
            return View(societeLogo);
        }

        // POST: SocieteLogoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SocieteLogo societeLogo)
        {
            if (ModelState.IsValid)
            {
                if (societeLogo.file == null)
                {
                    SocieteLogo socilogo1 = db.SocieteLogo.Where(f => f.id == societeLogo.id).FirstOrDefault();
                    //societeLogo.logo = socilogo1.logo;
                    socilogo1.Adresse = societeLogo.Adresse;
                    socilogo1.Agence = societeLogo.Agence;
                    socilogo1.IBAN = societeLogo.IBAN;
                    socilogo1.mail = societeLogo.mail;
                    socilogo1.MF = societeLogo.MF;
                    socilogo1.RC = societeLogo.RC;
                    socilogo1.RIB = societeLogo.RIB;
                    socilogo1.Tel = societeLogo.Tel;
                    socilogo1.Web = societeLogo.Web;
                    socilogo1.Nom_Societe = societeLogo.Nom_Societe;
                    db.SaveChanges();
                    db.SocieteLogo.Add(societeLogo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(societeLogo.file.FileName);
                    string extention = Path.GetExtension(societeLogo.file.FileName);
                    societeLogo.file.SaveAs(Path.Combine(Server.MapPath("/Images"), (fileName + extention)));
                    societeLogo.logo = (fileName + extention);
                    db.Entry(societeLogo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(societeLogo);
        }

        // GET: SocieteLogoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocieteLogo societeLogo = db.SocieteLogo.Find(id);
            if (societeLogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id=societeLogo.id;
            return View(societeLogo);
        }

        // POST: SocieteLogoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public string DeleteConfirmed(string id)
        {
            int idd = int.Parse(id);
            string soc = (string)Session["Soclogo"];

            SocieteLogo societeLogo = db.SocieteLogo.Where(f => f.id == idd).FirstOrDefault();
            if(societeLogo.Nom_Societe!=soc)
            {
                SocieteLogo societeLogo1 = db.SocieteLogo.Find(idd);
                db.SocieteLogo.Remove(societeLogo1);
                db.SaveChanges();
                return string.Empty;
            }
            else {
                return "NO";

            }
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
