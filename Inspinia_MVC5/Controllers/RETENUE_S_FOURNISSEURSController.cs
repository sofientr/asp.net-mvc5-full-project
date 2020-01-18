using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class RETENUE_S_FOURNISSEURSController : Controller
    {
        private Tr db = new Tr();

        // GET: RETENUE_S_FOURNISSEURS
        public ActionResult Index()
        {
            if(Session["SoclogoId"]==null)
            {
                return RedirectToAction("Login", "Societes");
            }
            int idste = (int)Session["SoclogoId"];
            var rETENUE_S_FOURNISSEURS = db.RETENUE_S_FOURNISSEURS.Include(r => r.FACTURES_FOURNISSEURS);
            return View(rETENUE_S_FOURNISSEURS.Where(f=>f.FACTURES_FOURNISSEURS.Societes== idste).ToList());
        }

        // GET: RETENUE_S_FOURNISSEURS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS = db.RETENUE_S_FOURNISSEURS.Find(id);
            if (rETENUE_S_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(rETENUE_S_FOURNISSEURS);
        }

        // GET: RETENUE_S_FOURNISSEURS/Create
        public ActionResult Create()
        {
            ViewBag.ID_FACT = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE");
            return View();
        }

        // POST: RETENUE_S_FOURNISSEURS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_FACT,TTC,Retenue,Date_Retenue,Montant_Brut,Taux_Retenue,Montant_net,Declaration,Date_Declaration")] RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
                db.RETENUE_S_FOURNISSEURS.Add(rETENUE_S_FOURNISSEURS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_FACT = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", rETENUE_S_FOURNISSEURS.ID_FACT);
            return View(rETENUE_S_FOURNISSEURS);
        }

        // GET: RETENUE_S_FOURNISSEURS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS = db.RETENUE_S_FOURNISSEURS.Find(id);
            if (rETENUE_S_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_FACT = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", rETENUE_S_FOURNISSEURS.ID_FACT);
            return View(rETENUE_S_FOURNISSEURS);
        }

        // POST: RETENUE_S_FOURNISSEURS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_FACT,TTC,Retenue,Date_Retenue,Montant_Brut,Taux_Retenue,Montant_net,Declaration,Date_Declaration")] RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rETENUE_S_FOURNISSEURS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_FACT = new SelectList(db.FACTURES_FOURNISSEURS, "ID", "CODE", rETENUE_S_FOURNISSEURS.ID_FACT);
            return View(rETENUE_S_FOURNISSEURS);
        }

        // GET: RETENUE_S_FOURNISSEURS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS = db.RETENUE_S_FOURNISSEURS.Find(id);
            if (rETENUE_S_FOURNISSEURS == null)
            {
                return HttpNotFound();
            }
            return View(rETENUE_S_FOURNISSEURS);
        }

        // POST: RETENUE_S_FOURNISSEURS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RETENUE_S_FOURNISSEURS rETENUE_S_FOURNISSEURS = db.RETENUE_S_FOURNISSEURS.Find(id);
            db.RETENUE_S_FOURNISSEURS.Remove(rETENUE_S_FOURNISSEURS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeclarationRetenue(FormCollection formCollection)
        {
            string[] ids = formCollection["affComId"].Split(new char[] { ',' });
            string date2 = Request["date2"] != null ? Request["date2"].ToString() : string.Empty;

            foreach (string Id in ids)
            {
                int Idd = int.Parse(Id);
                RETENUE_S_FOURNISSEURS avoir = db.RETENUE_S_FOURNISSEURS.Where(f => f.ID == Idd).FirstOrDefault();
                avoir.Declaration = true;
                avoir.Date_Declaration = DateTime.Parse(date2);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult PrintRetenue(string id)
        {
            int ID = int.Parse(id);
            dynamic dt;
            ConvertisseurChiffresLettres convert = new ConvertisseurChiffresLettres();
            List<FACTURES_FOURNISSEURS> Liste = db.FACTURES_FOURNISSEURS.ToList();
            FACTURES_FOURNISSEURS UnDevis = db.FACTURES_FOURNISSEURS.Where(cmd => cmd.ID == ID).FirstOrDefault();
            string matfisc2 = UnDevis.FOURNISSEURS.ID_FISCAL;
            if(matfisc2!=null && matfisc2!="")
            { 
            string matfisc = matfisc2.Replace("/", "");
            string id_fisc = "";
            int i = 0;
            while ((matfisc[i] == ('0')) || (matfisc[i] == ('1')) || (matfisc[i] == ('2')) || (matfisc[i] == ('3')) || (matfisc[i] == ('4')) || (matfisc[i] == ('5')) || (matfisc[i] == ('6')) || (matfisc[i] == ('7')) || (matfisc[i] == ('8')) || (matfisc[i] == ('9')))
            {

                id_fisc += matfisc[i];
                i++;
            }

            int len = id_fisc.Length;
            int len2 = matfisc.Length;
            int len3 = len2 - len;
            string s2 = matfisc.Substring(len, len3);
            string s3 = s2.Substring(0, 1);
            id_fisc += s3;
            string s4 = s2.Substring(1, 1);
            string s5 = s2.Substring(2, 1);
            int len4 = (id_fisc.Length) + 2;
            int len5 = len2 - len4;
            string s6 = s2.Substring(3, len5);
             dt = from cmd in Liste
                         select new
                         {
                             CODE = UnDevis.Num_Fact,
                             DATE = UnDevis.DATE.ToShortDateString(),
                             NOM = UnDevis.FOURNISSEURS.NOM,
                             ADRESSE = UnDevis.FOURNISSEURS.ADRESSE,
                             ID_FISCAL = id_fisc,
                             AI = s4,
                             NIS = s5,
                             RC = s6,
                             TNET = UnDevis.TNET ?? 0,
                             THT = UnDevis.Redevance ?? 0,

                         };
            }
            else
            {
                 dt = from cmd in Liste
                             select new
                             {
                                 CODE = UnDevis.Num_Fact,
                                 DATE = UnDevis.DATE.ToShortDateString(),
                                 NOM = UnDevis.FOURNISSEURS.NOM,
                                 ADRESSE = UnDevis.FOURNISSEURS.ADRESSE,
                                 TNET = UnDevis.TNET ?? 0,
                                 THT = UnDevis.Redevance ?? 0,

                             };
            }
            ReportDocument rptH = new ReportDocument();
            string FileName = Server.MapPath("/Reports/Retenue.rpt");
            rptH.Load(FileName);
            rptH.SummaryInfo.ReportTitle = "Retenue Fournisseur";
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
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
