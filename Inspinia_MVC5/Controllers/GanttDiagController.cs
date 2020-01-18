using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Data.Entity;

namespace Inspinia_MVC5.Controllers
{
    public class GanttDiagController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();


        //Diagramme de gantt normal
        public ActionResult GanttSecond()
        {
            return View();
        }



        // fonction qui retourne le nombre de jours total (travail+vacances)
        public int calculTemps(DateTime date, int duree)
        {
            int id = (int)Session["pt_id"];
            List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id && p.jourTravail == false).ToList();
            List<DayOfWeek> jourRepos = new List<DayOfWeek>();
            int weekend = 0;
            foreach (ParametrageSemaines p in week)
            {
                if (p.JourId == 0)
                {
                    jourRepos.Add(DayOfWeek.Sunday);
                }
                else if (p.JourId == 1)
                {
                    jourRepos.Add(DayOfWeek.Monday);
                }
                else if (p.JourId == 2)
                {
                    jourRepos.Add(DayOfWeek.Tuesday);
                }
                else if (p.JourId == 3)
                {
                    jourRepos.Add(DayOfWeek.Wednesday);
                }
                else if (p.JourId == 4)
                {
                    jourRepos.Add(DayOfWeek.Thursday);
                }
                else if (p.JourId == 5)
                {
                    jourRepos.Add(DayOfWeek.Friday);
                }
                else if (p.JourId == 6)
                {
                    jourRepos.Add(DayOfWeek.Saturday);
                }
            }

            for (int i = 0; i < duree; i++)
            {
                foreach (DayOfWeek d in jourRepos)
                {
                    if (date.AddDays(i).DayOfWeek == d)
                    {
                        weekend += 1;
                    }
                }
            }
            if (weekend > 0)
            {
                weekend += calculTemps(date.AddDays(duree), weekend);
            }
            return weekend;
        }




        // fonction qui retourne le nombre des heures de travail
        public int calculHeures(DateTime date, int duree)
        {
            int id = (int)Session["pt_id"];
            List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            List<int> hor = new List<int>();
            for (int i = 0; i <= week.Count(); i++)
                hor.Add(week[i].jourTravail == false ? 0 : (week[i].doubleSeance == false ? (int.Parse(week[i].seance1Fin) - int.Parse(week[i].seance1Debut)) : ((int.Parse(week[i].seance1Fin) - int.Parse(week[i].seance1Debut)) + (int.Parse(week[i].seance2Fin) - int.Parse(week[i].seance2Debut)))));
            int jour = 0;
            int heures = 0;
            do
            {
                switch (date.AddDays(jour).DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        if (hor[0] == 0)
                            duree++;
                        jour++;
                        heures += hor[0];
                        break;
                    case DayOfWeek.Monday:
                        if (hor[1] == 0)
                            duree++;
                        jour++;
                        heures += hor[1];
                        break;
                    case DayOfWeek.Tuesday:
                        if (hor[2] == 0)
                            duree++;
                        jour++;
                        heures += hor[2];
                        break;
                    case DayOfWeek.Wednesday:
                        if (hor[3] == 0)
                            duree++;
                        jour++;
                        heures += hor[3];
                        break;
                    case DayOfWeek.Thursday:
                        if (hor[4] == 0)
                            duree++;
                        jour++;
                        heures += hor[4];
                        break;
                    case DayOfWeek.Friday:
                        if (hor[5] == 0)
                            duree++;
                        jour++;
                        heures += hor[5];
                        break;
                    case DayOfWeek.Saturday:
                        if (hor[6] == 0)
                            duree++;
                        jour++;
                        heures += hor[6];
                        break;
                }

            } while (jour < duree);
            return heures;
        }


        // Fonction qui retourne la date fin d'une tâche (durée H)
        //public DateTime calculDateFin(int id, DateTime debut, int heures)
        //{
        //    //int id = (int)Session["pt_id"];
        //    List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
        //    int i = 0;
        //    int compt = 0;
        //    do
        //    {
        //        switch (debut.AddHours(i).DayOfWeek)
        //        {
        //            case DayOfWeek.Sunday:
        //                if (week[0].jourTravail != false)
        //                {
        //                    string[] seance1 = week[0].seance1Debut.Split(':');
        //                    int seance11 = int.Parse(seance1[0]);
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[0].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[0].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[0].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[0].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[0].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Monday:
        //                if (week[1].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[1].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[1].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[1].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[1].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[1].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Tuesday:
        //                if (week[2].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[2].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[2].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[2].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[2].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[2].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Wednesday:
        //                if (week[3].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[3].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[3].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[3].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[3].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[3].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Thursday:
        //                if (week[4].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[4].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[4].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[4].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[4].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[4].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Friday:
        //                if (week[5].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[5].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[5].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[5].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[5].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[5].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;
        //            case DayOfWeek.Saturday:
        //                if (week[6].jourTravail != false)
        //                {
        //                    if (debut.AddHours(i).Hour >= int.Parse(week[6].seance1Debut) && debut.AddHours(i).Hour < int.Parse(week[6].seance1Fin))
        //                    {
        //                        heures--;

        //                    }
        //                    else
        //                    {
        //                        if ((week[6].doubleSeance == true) && (debut.AddHours(i).Hour >= int.Parse(week[6].seance2Debut) && debut.AddHours(i).Hour < int.Parse(week[6].seance2Fin)))
        //                            heures--;
        //                    }
        //                }
        //                break;


        //        }
        //        compt++;
        //        i++;
        //    }
        //    while (heures > 0);
        //    DateTime d = debut.AddHours(compt);
        //    return d;

        //}
        public DateTime calculDateFin(int id, DateTime debut, int heures)
        {
            //int id = (int)Session["pt_id"];
            List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            List<Tableau_Horaire> week2 = db.Tableau_Horaire.ToList();
            int i = 0;

            int compt = 0;
            do
            {
                for (int j = 0; j < week.Count; j++)
                {
                    for (DateTime t = (DateTime)week[j].Date_Deb; t < week[j].Date_Fin; t = t.AddDays(1))
                    {
                        //string d2;
                        //if ((t.Day == 31) && ((t.Month == 1) || (t.Month == 3) || (t.Month == 5) || (t.Month == 7) || (t.Month == 8) || (t.Month == 10) || (t.Month == 12)))
                        //{
                        //    d2 = t.Day.ToString() + "/" + t.Month.ToString() + "/" + t.Year.ToString();
                        //    t.Day.ToString().Replace(t.Day.ToString(), "1");
                        //    int mois = t.Month + 1;
                        //    t.Month.ToString().Replace(t.Month.ToString(), mois.ToString());
                        //}
                        switch (debut.AddHours(i).DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                int idj = db.Jours.Where(fou => fou.Jour == "Samedi").FirstOrDefault().id;
                                int idh = db.ParametrageSemaines.Where(fou => fou.JourId == idj).FirstOrDefault().Id;
                                ParametrageSemaines paaa1 = db.ParametrageSemaines.Where(f => f.Id == idh).FirstOrDefault();
                                if (paaa1.jourTravail != false)
                                {
                                    string[] seanceD1 = paaa1.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa1.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa1.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa1.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {

                                        if ((paaa1.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Monday:
                                int idjm = db.Jours.Where(fou => fou.Jour == "Lundi").FirstOrDefault().id;
                                int idhm = db.ParametrageSemaines.Where(fou => fou.JourId == idjm).FirstOrDefault().Id;
                                ParametrageSemaines paaa2 = db.ParametrageSemaines.Where(f => f.Id == idhm).FirstOrDefault();
                                if (paaa2.jourTravail != false)
                                {
                                    string[] seanceD1 = paaa2.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa2.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa2.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa2.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa2.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Tuesday:
                                int idjt = db.Jours.Where(fou => fou.Jour == "Mardi").FirstOrDefault().id;
                                int idht = db.ParametrageSemaines.Where(fou => fou.JourId == idjt).FirstOrDefault().Id;
                                ParametrageSemaines paaa3 = db.ParametrageSemaines.Where(f => f.Id == idht).FirstOrDefault();
                                if (paaa3.jourTravail != false)
                                {
                                    string[] seanceD1 = paaa3.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa3.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa3.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa3.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa3.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Wednesday:
                                int idjmr = db.Jours.Where(fou => fou.Jour == "Mercredi").FirstOrDefault().id;
                                int idhmr = db.ParametrageSemaines.Where(fou => fou.JourId == idjmr).FirstOrDefault().Id;
                                ParametrageSemaines paaa4 = db.ParametrageSemaines.Where(f => f.Id == idhmr).FirstOrDefault();
                                if (paaa4.jourTravail != false)
                                {

                                    string[] seanceD1 = paaa4.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa4.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa4.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa4.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa4.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Thursday:
                                int idjth = db.Jours.Where(fou => fou.Jour == "Jeudi").FirstOrDefault().id;
                                int idhth = db.ParametrageSemaines.Where(fou => fou.JourId == idjth).FirstOrDefault().Id;
                                ParametrageSemaines paaa = db.ParametrageSemaines.Where(f => f.Id == idhth).FirstOrDefault();
                                if (paaa.jourTravail != false)
                                {

                                    string[] seanceD1 = paaa.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Friday:
                                int idjf = db.Jours.Where(fou => fou.Jour == "Vendredi").FirstOrDefault().id;
                                int idhf = db.ParametrageSemaines.Where(fou => fou.JourId == idjf).FirstOrDefault().Id;
                                ParametrageSemaines paaa5 = db.ParametrageSemaines.Where(f => f.Id == idhf).FirstOrDefault();
                                if (paaa5.jourTravail != false)
                                {

                                    string[] seanceD1 = paaa5.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa5.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa5.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa5.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa5.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;
                            case DayOfWeek.Saturday:
                                int idjs = db.Jours.Where(fou => fou.Jour == "Samedi").FirstOrDefault().id;
                                int idhs = db.ParametrageSemaines.Where(fou => fou.JourId == idjs).FirstOrDefault().Id;
                                ParametrageSemaines paaa6 = db.ParametrageSemaines.Where(f => f.Id == idhs).FirstOrDefault();
                                if (paaa6.jourTravail != false)
                                {
                                    string[] seanceD1 = paaa6.seance1Debut.Split(':');
                                    int seanceD11 = int.Parse(seanceD1[0]);
                                    string[] seanceF1 = paaa6.seance1Fin.Split(':');
                                    int seanceF11 = int.Parse(seanceF1[0]);
                                    string[] seanceD2 = paaa6.seance2Debut.Split(':');
                                    int seanceD21 = int.Parse(seanceD1[0]);
                                    string[] seanceF2 = paaa6.seance2Fin.Split(':');
                                    int seanceF21 = int.Parse(seanceF1[0]);
                                    if (debut.AddHours(i).Hour >= seanceD11 && debut.AddHours(i).Hour < seanceF11)
                                    {
                                        heures--;

                                    }
                                    else
                                    {
                                        if ((paaa6.doubleSeance == true) && (debut.AddHours(i).Hour >= seanceD21) && (debut.AddHours(i).Hour < seanceF21))
                                            heures--;
                                    }
                                }
                                break;


                        }
                        compt++;
                        i++;
                    }

                }
            }
            while ((heures > 0) && (i == 69963104) && (compt == 69963104));
            DateTime d = debut.AddHours(compt);
            return d;

        }


        // fonction qui retourne le nombre des jours de travail à partir du nombre des heures
        public int calculJours(DateTime date, int heures)
        {
            int id = (int)Session["pt_id"];
            DateTime fin = calculDateFin(id, date, heures);
            List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            List<int> hor = new List<int>();
            for (int i = 0; i < week.Count(); i++)
            {
                string[] seanceD1 = week[i].seance1Debut.Split(':');
                int seanceD11 = int.Parse(seanceD1[0]);
                string[] seanceF1 = week[i].seance1Fin.Split(':');
                int seanceF11 = int.Parse(seanceF1[0]);
                string[] seanceD2 = week[i].seance2Debut.Split(':');
                int seanceD21 = int.Parse(seanceD1[0]);
                string[] seanceF2 = week[i].seance2Fin.Split(':');
                int seanceF21 = int.Parse(seanceF1[0]);
                hor.Add(week[i].jourTravail == false ? 0 : (week[i].doubleSeance == false ? (seanceF11 - seanceD11) : ((seanceF11 - seanceD11 )+ (seanceF21 - seanceD21))));
            }

            int jour = 0;
            //int i = 0;
            int j = 0;
            do
            {
                switch (date.AddDays(j).DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        if (hor[0] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Monday:
                        if (hor[1] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Tuesday:
                        if (hor[2] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Wednesday:
                        if (hor[3] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Thursday:
                        if (hor[4] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Friday:
                        if (hor[5] != 0)
                            jour++;
                        break;
                    case DayOfWeek.Saturday:
                        if (hor[6] != 0)
                            jour++;
                        break;
                }
                j++;
            }
            while (DateTime.Compare(fin, date.AddDays(j)) > 0);
            return jour;
        }


        //fonction qui calcule les heures de travail entre 2 dates
        public int CalculHeuresTravail(int id, DateTime date1, DateTime date2)
        {
            // int id = (int)Session["pt_id"];
            List<ParametrageSemaines> week = db.ParametrageSemaines.Where(p => p.projetTechniqueID == id).ToList();
            int i = 0;
            int compt = 0;
            do
            {
                switch (date1.AddHours(i).DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        if (week[0].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[0].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[0].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[0].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[0].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[0].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Monday:
                        if (week[1].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[1].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[1].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[1].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[1].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[1].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Tuesday:
                        if (week[2].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[2].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[2].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[2].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[2].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[2].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Wednesday:
                        if (week[3].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[3].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[3].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[3].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[3].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[3].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Thursday:
                        if (week[4].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[4].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[4].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[4].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[4].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[4].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Friday:
                        if (week[5].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[5].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[5].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[5].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[5].seance2Debut)&& date1.AddHours(i).Hour < int.Parse(week[5].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                    case DayOfWeek.Saturday:
                        if (week[0].jourTravail != false)
                        {
                            if (date1.AddHours(i).Hour >= int.Parse(week[6].seance1Debut) && date1.AddHours(i).Hour < int.Parse(week[6].seance1Fin))
                            {
                                compt++;
                            }
                            else
                            {
                                if ((week[6].doubleSeance == true) && (date1.AddHours(i).Hour >= int.Parse(week[6].seance2Debut) && date1.AddHours(i).Hour < int.Parse(week[6].seance2Fin)))
                                    compt++;
                            }
                        }
                        break;
                }
                i++;
            }
            while (date1.AddHours(i) < date2);
            return compt;
        }


        //Fonction qui calcule le nombre des heures totales entre 2 dates
        public int CalculHeuresTotal(DateTime date1, DateTime date2)
        {
            int i = 0;
            do
            {
                i++;
            }
            while (DateTime.Compare(date1.AddHours(i), date2) < 0);
            return i;
        }




        //Gantt de planification
        public ActionResult GanttPlanification()
        {

            return View();
        }


        //gantt heures réalisation
        public ActionResult GanttHeures()
        {

            return View();
        }


        //gantt heures planification
        public ActionResult GanttHeuresPlanification()
        {

            return View();
        }



        //Gantt de planifié VS réalisé
        public ActionResult GanttComparaison()
        {
            return View();
        }

        //Gantt de Plan de charge employé
        public ActionResult GanttPlanCharge()
        {
            return View();
        }


        //Gantt de Plan de charge employé Comparaison
        public ActionResult GanttPlanChargeComparaison()
        {
            return View();
        }



        //Gantt Ressources Réalisation
        public ActionResult GanttRealisation()
        {
            return View();
        }

        //Création du planification
        public ActionResult GanttPlanning()
        {
            return View();
        }




        //Retour au projet technique
        public ActionResult projTech()
        {
            return RedirectToAction("Details/" + Session["pt_id"], "ProjetTechniques");
        }



        //Index
        public ActionResult Index()

        {
            ViewBag.ProjetTechniqueId = new SelectList(db.ProjetTechniques, "ProjetTechniqueId", "Designation");
            return View(db.ProjetTechniques.ToList());
        }




        //Fonction qui retourne la liste des employés JSON
        [HttpGet]
        public JsonResult Ressources()
        {
            var jsonData =
                (
                   from s in db.Personnels.AsEnumerable()
                   select new
                   {
                       key = s.PersonnelId,
                       label = s.Nom,
                       department = 0
                   }
               ).ToArray();

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Retour parametrage jours et horaires de travail JSON
        [HttpGet]
        public JsonResult ParametrageJRSPlanCharge()
        {


            int id = (int)Session["pt_id"];

            var jsonData1 =
              (
                   from s in db.ParametrageSemaines.AsEnumerable()
                   where (s.jourTravail == false && s.projetTechniqueID == id)
                   select new
                   {
                       day = s.JourId,
                       hours = s.jourTravail
                   }
               ).ToArray();
            var jsonData2 =
              (
                   from s in db.ParametrageSemaines.AsEnumerable()
                   where (s.jourTravail != false && s.projetTechniqueID == id)
                   select new
                   {
                       day = s.JourId,
                       hours = s.doubleSeance == false ? (new int[] { int.Parse(s.seance1Debut), int.Parse(s.seance1Fin) }) : (new int[] { int.Parse(s.seance1Debut), int.Parse(s.seance1Fin), int.Parse(s.seance2Debut), int.Parse(s.seance2Fin) })
                   }
               ).ToArray();
            List<Object> jsonData = new List<object>();
            for (int i = 0; i < jsonData1.Length; i++)
            {
                jsonData.Add(jsonData1.GetValue(i));
            }
            for (int i = 0; i < jsonData2.Length; i++)
            {
                jsonData.Add(jsonData2.GetValue(i));
            }

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        // get les jours par date
        [HttpGet]
        public JsonResult getdateparjour( string day, string date11)
        {
            int id1 = int.Parse(day);
            string jour="";
            if(id1 == 0)
            {
                jour = "Sunday";
            }
            if (id1 == 1)
            {
                jour = "Monday";
            }
            if (id1 == 2)
            {
                jour= "Tuesday";
            }
            if (id1 == 3)
            {
                jour = "Wednesday";
            }
            if (id1 == 4)
            {
                jour = "Thursday";
            }
            if (id1 == 5)
            {
                jour = "Friday";
            }
            if (id1 == 6)
            {
                jour = "Saturday";
            }
            string[] Period = date11.Split(',');
            string date1 = Period[0] + '/' +Period[1]+ '/' + Period[2];
            string date2 = Period[3] + '/' + Period[4] + '/' + Period[5];
            DateTime date12 = DateTime.Parse(date1);
            DateTime date22 = DateTime.Parse(date2);
            List<DateTime> list = new List<DateTime>();
            ParametrageSemaines f = new ParametrageSemaines();
            f = db.ParametrageSemaines.Where(ff => ff.Date_Deb == date12 && ff.Date_Fin == date22 && ff.JourId== id1).FirstOrDefault();
            string[] HD1 = f.seance1Debut.Split(':');
            string[] HF1 = f.seance1Fin.Split(':');
            string[] HD2 = f.seance2Debut.Split(':');
            string[] HF2 = f.seance2Fin.Split(':');
            string hd11 = HD1[0];
            string hf11 = HF1[0];
            string hd22 = HD2[0];
            string hf22 = HF2[0];

            int[] hours = f.doubleSeance == false ? (new int[] { int.Parse(hd11), int.Parse(hf11) }) : (new int[] { int.Parse(hd11), int.Parse(hf11), int.Parse(hd22), int.Parse(hf22) });
            List<Object> jsonData = new List<object>();
            for (DateTime d= date12; d< date22;d=d.AddDays(1))
            {
                if (jour == d.DayOfWeek.ToString())
                {
                    string[] dd = d.ToString().Split(' ');
                    string[] dd1 = dd[0].ToString().Split('/');
                    int p = int.Parse(dd1[1]);
                    int pp = p;
                    int pp1= int.Parse(dd1[2]);
                    int pp2 = int.Parse(dd1[0]);
                    string dd2 = dd1[2] +','+ pp + ',' + dd1[0];
                    Object jsonData1 =new {
                           aaaa = dd1[2],
                           mm= dd1[1],
                           jj = dd1[0],
                           hours = hours
                    };
                    jsonData.Add(jsonData1);


                }
            }
           
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Retour parametrage jours et horaires de travail JSON
        [HttpGet]
        public JsonResult ParametrageJRS()
        {


            int id = (int)Session["pt_id"];

            var jsonData1 =
              (
                   from s in db.ParametrageSemaines.AsEnumerable()
                   where (s.jourTravail == false && s.projetTechniqueID == id)
                   select new
                   {
                       day = s.JourId,
                       hours = s.jourTravail
                   }
               ).ToArray();
            List<ParametrageSemaines> list = new List<ParametrageSemaines>();
            foreach (ParametrageSemaines par in db.ParametrageSemaines)
            {
                string[] seanceD1 = par.seance1Debut.Split(':');
                string[] seanceF1 = par.seance1Fin.Split(':');
                string[] seanceD2 = par.seance2Debut.Split(':');
                string[] seanceF2 = par.seance2Fin.Split(':');
                string[] seanceDDeb = par.Date_Deb.ToString().Split(' ');
                string[] seanceDFin = par.Date_Fin.ToString().Split(' ');
                ParametrageSemaines pa = par;
                pa.seance1Debut=seanceD1[0];
                pa.seance1Fin = seanceF1[0];
                pa.seance2Debut = seanceD2[0];
                pa.seance2Fin = seanceF2[0];
                string date_deb = seanceDDeb[0].Replace('/', ',');
                string date_fin = seanceDFin[0].Replace('/', ',');
                //string[] seanceDDeb11 = date_deb.Split(',');
                //string[] seanceDFin11 = date_fin.Split(',');
                //int ss1 = int.Parse(seanceDDeb11[0]);
                //int ss2 = int.Parse(seanceDFin11[0]);
                //ss1--;
                //ss2--;
                //seanceDDeb11[0].Replace(seanceDDeb11[0], ss1.ToString());
                //seanceDFin11[0].Replace(seanceDFin11[0], ss2.ToString());
                
                pa.Date_DebString = date_deb;
                pa.Date_FinString = date_fin;

                list.Add(pa);
            }
            var jsonData2 =
              (
                   from s in list.AsEnumerable() /*db.ParametrageSemaines.AsEnumerable()*/
                   where (s.jourTravail != false && s.projetTechniqueID == id)
                   select new
                   {
                       day = s.JourId,
                       hours = s.doubleSeance == false ? (new int[] {int.Parse(s.seance1Debut), int.Parse(s.seance1Fin)}) : (new int[] { int.Parse(s.seance1Debut), int.Parse(s.seance1Fin), int.Parse(s.seance2Debut), int.Parse(s.seance2Fin) }),
                       date = (new string[] { s.Date_DebString, s.Date_FinString })
                   }
             ).ToArray();
             
            List<Object> jsonData = new List<object>();
            for (int i = 0; i < jsonData1.Length; i++)
            {
                jsonData.Add(jsonData1.GetValue(i));
            }
            for (int i = 0; i < jsonData2.Length; i++)
            {
                jsonData.Add(jsonData2.GetValue(i));
            }

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Retour parametrage jours et horaires de travail JSON
        [HttpGet]
        public JsonResult ParametrageCalendrier()
        {


            int id = 1;
            var jsonData = (from s in db.ParametrageSemaines.AsEnumerable()
                            where (s.jourTravail == true && s.projetTechniqueID == id)
                            select new
                            {
                                dow = new int[] { (int)s.JourId },
                                start = int.Parse(s.seance1Debut) < 10 ? "0" + s.seance1Debut.ToString() + ":00" : s.seance1Debut.ToString() + ":00",
                                end = s.doubleSeance == false ? (int.Parse(s.seance1Fin) < 10 ? "0" + s.seance1Fin.ToString() + ":00" : s.seance1Fin.ToString() + ":00") : (int.Parse(s.seance2Fin) < 10 ? "0" + s.seance2Fin.ToString() + ":00" : s.seance2Fin.ToString() + ":00")
                            }
                           ).ToArray();
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Retour parametrage Weekend
        [HttpGet]
        public JsonResult ParametrageWeekend()
        {


            int id = (int)Session["pt_id"];

            var jsonData =
              (
                   from s in db.ParametrageSemaines.AsEnumerable()
                   where (s.jourTravail == false && s.projetTechniqueID == id)
                   select new
                   {
                       day = s.JourId,
                       hours = s.jourTravail
                   }
               ).ToArray();



            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Ressources pour Resource2
        [HttpGet]
        public JsonResult Ressources2()
        {
            var jsonData =
                (
                   from s in db.Personnels.AsEnumerable()
                   select new
                   {
                       id = s.PersonnelId,
                       text = s.Nom,
                       parent = 0
                   }
               ).ToArray();

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Plan de charge Calendrier
        public JsonResult DataCalendrier()
        {
            int id = (int)Session["emp_id"];
            var jsonData =
           (
                    from t in db.Tasks.AsEnumerable()
                    where t.owner_id == id
                    select new
                    {
                        title = db.ProjetTechniques.First(a => a.ProjetTechniqueId == t.ProjetTechniquesID).Designation + " - " + t.Text,
                        start = t.planned_start.ToString("u"),
                        end = t.planned_end.ToString("u")


                    }
                ).ToArray();

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Plan de charge Calendrier par projet
        public JsonResult DataCalendrierProj()
        {
            int idProj = (int)Session["proj_id"];
            int id = (int)Session["emp_id"];
            var jsonData =
           (
                    from t in db.Tasks.AsEnumerable()
                    where (t.owner_id == id && t.ProjetTechniquesID == idProj)
                    select new
                    {
                        title = t.Text,
                        start = t.planned_start.ToString("u"),
                        end = t.planned_end.ToString("u")


                    }
                ).ToArray();

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }



        //Fonction qui retourne DATA de gantt à planifier
        public JsonResult DataPlanning()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.planned_start.ToString(),
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



        //Fonction qui retourne DATA de gantt à planifier
        public JsonResult Export()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        tache = t.Text,
                        deb_plan = t.planned_start.ToString(),
                        fin_plan = t.planned_end.ToString(),
                        duree = t.duration_h_planning,
                        employe = db.Personnels.Find( t.owner_id).Nom,
                        progres = t.Progress,
                        deb_reel = t.StartDate.ToString(),
                        fin_reel = t.EndDate.ToString(),
                        retard = CalculHeuresTravail(id,(DateTime)  t.planned_end,(DateTime) t.EndDate)

                    }
                ).ToArray()
               
            };

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }



        //Retourne Data pour Gantt de planification
        public JsonResult DataPlanification()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where (t.ProjetTechniquesID == id)
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.planned_start.ToString(),
                        duration = t.duration_planning,
                        order = t.SortOrder,
                        owner_id = t.owner_id,
                        progress = t.Progress,
                        color = t.color,
                        //open = true,
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




        //fonction qui retourne les tâches du diagramme de gantt Réalisation
        public JsonResult DataRealisation()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
                        order = t.SortOrder,
                        owner_id = t.owner_id,
                        progress = t.Progress,
                        color = t.color,
                        //open = true,
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



        //Fonction qui retourne DATA de gantt Comparaison
        public JsonResult DataComparaison()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.duration_h,
                        duration_p = t.duration_h_planning,
                        retard = (DateTime.Compare((DateTime)   t.planned_end, calculDateFin(id, t.StartDate,(int) t.duration_h)) < 0) ? CalculHeuresTravail(id,(DateTime) t.planned_end, calculDateFin(id, t.StartDate,(int) t.duration_h)) : 0,

                        //retard = t.retard,
                        planned_start = t.planned_start.ToString("u"),
                        planned_end = t.planned_end.ToString("u"),
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





        //fonction qui retourne les tâches du diagramme de gantt normal
        public JsonResult Data()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
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



        //fonction qui retourne les tâches du diagramme de gantt heures de réalisation
        public JsonResult DataHeures()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.duration_h,
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



        //fonction qui retourne les tâches du diagramme de gantt heures de planification
        public JsonResult DataHeuresPlanification()
        {
            int id = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.ProjetTechniquesID == id
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.planned_start.ToString("u"),
                        duration = t.duration_h_planning,
                        order = t.SortOrder,
                        owner_id = t.owner_id,
                        color = t.color,
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


        public JsonResult DataPlanChargeComparaison()
        {
            int id = (int)Session["emp_id"];
            int pt = (int)Session["pt_id"];
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable()
                    where t.owner_id == id
                    select new
                    {
                        id = t.Id,
                        // text = (from p in db.ProjetTechniques.AsEnumerable() where p.ProjetTechniqueId==t.ProjetTechniquesID select new { proj = p.Designation }),
                        text = db.ProjetTechniques.First(a => a.ProjetTechniqueId == t.ProjetTechniquesID).Designation + " - " + t.Text,
                        start_date = t.StartDate.ToString("u"),
                        // duration = t.Duration_h,/////
                        duration = CalculHeuresTotal(t.StartDate, calculDateFin(pt, t.StartDate, t.duration_h)),
                        duration_p = CalculHeuresTotal(t.planned_start, t.planned_end),
                        retard = (DateTime.Compare(t.planned_end, calculDateFin(pt, t.StartDate, t.duration_h)) < 0) ? CalculHeuresTravail(pt, t.planned_end, calculDateFin(pt, t.StartDate, t.duration_h)) : 0,

                        //duration_p = t.duration_h_planning,/////
                        planned_start = t.planned_start.ToString("u"),/////
                        planned_end = t.planned_end.ToString("u"),/////
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




        public JsonResult DataPlanCharge()
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
                        // text = (from p in db.ProjetTechniques.AsEnumerable() where p.ProjetTechniqueId==t.ProjetTechniquesID select new { proj = p.Designation }),
                        text = db.ProjetTechniques.First(a => a.ProjetTechniqueId == t.ProjetTechniquesID).Designation + " - " + t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
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



        public JsonResult DataPlanChargeHeures()
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
                        // text = (from p in db.ProjetTechniques.AsEnumerable() where p.ProjetTechniqueId==t.ProjetTechniquesID select new { proj = p.Designation }),
                        text = db.ProjetTechniques.First(a => a.ProjetTechniqueId == t.ProjetTechniquesID).Designation + " - " + t.Text,
                        start_date = t.planned_start.ToString("u"),
                        duration = t.duration_h_planning,
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


        //Fonction qui sauvegarde les taches
        [HttpPost]
        public ContentResult Save(FormCollection form)
        {
            var dataActions = GanttRequest.Parse(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                ganttData.UpdatedTask.ProjetTechniquesID = id;
                                ganttData.UpdatedTask.planned_start = ganttData.UpdatedTask.StartDate;
                                ganttData.UpdatedTask.owner_id = 0;
                                ganttData.UpdatedTask.planned_end = ganttData.UpdatedTask.StartDate.AddDays((int)ganttData.UpdatedTask.Duration);
                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                // ganttData.UpdatedTask.Id = db.Tasks.Last().Id;
                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }


        //Fonction qui sauvegarde les données planification
        [HttpPost]
        public ContentResult SavePlanning(FormCollection form)
        {
            var dataActions = GanttRequest.ParsePlanning(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                ganttData.UpdatedTask.ProjetTechniquesID = id;
                                ganttData.UpdatedTask.duration_h = ganttData.UpdatedTask.Duration * 8;
                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                // ganttData.UpdatedTask.Id = db.Tasks.Last().Id;
                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }



        // Fonction Save pour gantt Heures de réalisation
        [HttpPost]
        public ContentResult SaveHeures(FormCollection form)
        {
            var dataActions = GanttRequest.ParseHeures(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                if (ganttData.Action != GanttAction.Deleted)
                                {
                                    ganttData.UpdatedTask.ProjetTechniquesID = id;
                                    switch (ganttData.Action)
                                    {
                                        case GanttAction.Inserted:

                                            ganttData.UpdatedTask.planned_start = ganttData.UpdatedTask.StartDate;
                                            ganttData.UpdatedTask.planned_end = ganttData.UpdatedTask.planned_start.AddDays((int)calculJours((DateTime)ganttData.UpdatedTask.planned_start,(int) ganttData.UpdatedTask.duration_h));
                                            ganttData.UpdatedTask.Duration = calculJours((DateTime)ganttData.UpdatedTask.planned_start,(int) ganttData.UpdatedTask.duration_h);
                                            ganttData.UpdatedTask.duration_planning = ganttData.UpdatedTask.Duration;
                                            ganttData.UpdatedTask.duration_h_planning = ganttData.UpdatedTask.duration_h;
                                            ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate, (int)ganttData.UpdatedTask.duration_h);
                                           // ganttData.UpdatedTask.retard = 0;

                                            ganttData.UpdatedTask.Progress = 0;
                                            break;
                                        default:
                                            Tasks task = db.Tasks.Find(ganttData.UpdatedTask.Id);
                                            ganttData.UpdatedTask.owner_id = task.owner_id;
                                            ganttData.UpdatedTask.planned_start = task.planned_start;
                                            ganttData.UpdatedTask.planned_end = task.planned_end;
                                            ganttData.UpdatedTask.duration_planning = task.duration_planning;
                                            ganttData.UpdatedTask.duration_h_planning = task.duration_h_planning;
                                            ganttData.UpdatedTask.Duration = calculJours((DateTime) ganttData.UpdatedTask.planned_start,(int) ganttData.UpdatedTask.duration_h);
                                            ganttData.UpdatedTask.color = task.color;
                                            ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);

                                            //ganttData.UpdatedTask.retard = (DateTime.Compare(task.planned_end, calculDateFin(id, ganttData.UpdatedTask.StartDate, ganttData.UpdatedTask.Duration_h)) < 0) ? CalculHeuresTravail(id, task.planned_end, ganttData.UpdatedTask.StartDate) : 0;
                                            break;

                                    }
                                    List<Links> links = db.Links.Where(lnk => lnk.SourceTaskId == ganttData.UpdatedTask.Id || lnk.TargetTaskId == ganttData.UpdatedTask.Id).ToList();
                                    foreach (Links ln in links)
                                    {
                                        if (ln.SourceTaskId == ganttData.UpdatedTask.Id)
                                        {
                                            if (DateTime.Compare(calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h), db.Tasks.First(t => t.Id == ln.TargetTaskId).StartDate) > 0)
                                            {
                                                Tasks tsk = db.Tasks.First(t => t.Id == ln.TargetTaskId);
                                                tsk.StartDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);
                                                tsk.EndDate = calculDateFin(id, tsk.StartDate,(int) tsk.duration_h);
                                                db.Entry(db.Tasks.Find(tsk.Id)).CurrentValues.SetValues(tsk);
                                                db.SaveChanges();
                                            }
                                        }
                                        else if (ln.TargetTaskId == ganttData.UpdatedTask.Id)
                                        {
                                            Tasks tsk = db.Tasks.First(t => t.Id == ln.SourceTaskId);
                                            if (DateTime.Compare(ganttData.UpdatedTask.StartDate, calculDateFin(id, tsk.StartDate,(int) tsk.duration_h)) < 0)
                                            {

                                                ganttData.UpdatedTask.StartDate = calculDateFin(id, tsk.StartDate, (int)tsk.duration_h);
                                                ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);


                                            }
                                        }
                                    }

                                }
                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                if (ganttData.UpdatedTask.ParentId != null)
                                {
                                    Tasks parent = db.Tasks.Find(ganttData.UpdatedTask.ParentId);
                                    

                                    parent.Duration = calculJours(parent.StartDate,(int) parent.duration_h);
                                    List<Tasks> sous = db.Tasks.Where(t => t.ParentId == ganttData.UpdatedTask.ParentId).ToList();
                                    if (sous != null)
                                    {
                                        decimal progress = 0;
                                        int i = 0;
                                        //parent.Duration_h=
                                        //calculDateFin
                                        DateTime fin = new DateTime();
                                        DateTime debut = new DateTime();
                                        int duree1 = 0;
                                        foreach (Tasks t in sous)
                                        {
                                            if (t == sous[0])
                                            {
                                                fin = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                                debut = t.StartDate;
                                            }
                                            else
                                            {
                                                if (DateTime.Compare(fin, calculDateFin(id, t.StartDate,(int) t.duration_h)) < 0)
                                                {
                                                    fin = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                                }
                                                if (DateTime.Compare(t.StartDate, debut) < 0)
                                                {
                                                    duree1 = CalculHeuresTravail(id, t.StartDate, parent.StartDate);
                                                    debut = t.StartDate;
                                                }
                                            }

                                            progress += t.Progress;
                                            i++;
                                        }
                                        if (debut != parent.StartDate)
                                        {
                                            parent.StartDate = debut;
                                            parent.duration_h += duree1;
                                        }
                                        parent.duration_h = CalculHeuresTravail(id, parent.StartDate, fin);
                                        //parent.Duration=
                                        progress = (progress == 0) ? 0 : progress / i;
                                        parent.Progress = progress;
                                    }
                                    parent.Duration = calculJours(parent.StartDate,(int) parent.duration_h);

                                    db.Entry(db.Tasks.Find(parent.Id)).CurrentValues.SetValues(parent);
                                    db.SaveChanges();


                                }
                                List<Tasks> task_list = db.Tasks.Where(t => t.ProjetTechniquesID == id).ToList();
                                ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
                                DateTime deb = task_list[0].StartDate;
                                DateTime fin1 = calculDateFin(id, task_list[0].StartDate,(int) task_list[0].duration_h);
                                foreach (Tasks t in task_list)
                                {
                                    if (DateTime.Compare(deb, t.StartDate) > 0)
                                    {
                                        deb = t.StartDate;
                                    }
                                    if (DateTime.Compare(fin1, calculDateFin(id, t.StartDate,(int) t.duration_h)) < 0)
                                    {
                                        fin1 = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                    }
                                }
                                projetTechnique.DateDebutReel = deb;
                                projetTechnique.DateFinReel = fin1;
                                db.Entry(projetTechnique).State = EntityState.Modified;
                                db.SaveChanges();

                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }




        // Fonction Save pour gantt Heures de planification
        [HttpPost]
        public ContentResult SaveHeuresPlanification(FormCollection form)
        {
            var dataActions = GanttRequest.ParseHeuresPlanification(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            //try
            //{
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                if (ganttData.Action != GanttAction.Deleted)
                                {
                                    ganttData.UpdatedTask.ProjetTechniquesID = id;

                                ganttData.UpdatedTask.planned_end = calculDateFin(id, (DateTime)ganttData.UpdatedTask.planned_start, (int)ganttData.UpdatedTask.duration_h_planning); //ganttData.UpdatedTask.planned_start.AddDays(calculJours(ganttData.UpdatedTask.planned_start, ganttData.UpdatedTask.duration_h_planning));
                                ganttData.UpdatedTask.duration_planning = calculJours((DateTime)ganttData.UpdatedTask.planned_start, (int)ganttData.UpdatedTask.duration_h_planning);
                                ganttData.UpdatedTask.Duration = (int)ganttData.UpdatedTask.duration_planning;
                                ganttData.UpdatedTask.duration_h = (int)ganttData.UpdatedTask.duration_h_planning;
                                ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate, (int)ganttData.UpdatedTask.duration_h);

                                List<Links> links = db.Links.Where(lnk => lnk.SourceTaskId == ganttData.UpdatedTask.Id || lnk.TargetTaskId == ganttData.UpdatedTask.Id).ToList();
                                    foreach (Links ln in links)
                                    {
                                        if (ln.SourceTaskId == ganttData.UpdatedTask.Id)
                                        {
                                            if (DateTime.Compare((DateTime)ganttData.UpdatedTask.planned_end, (DateTime)db.Tasks.First(t => t.Id == ln.TargetTaskId).planned_start) > 0)
                                            {
                                                Tasks tsk = db.Tasks.First(t => t.Id == ln.TargetTaskId);
                                                tsk.StartDate = (DateTime)ganttData.UpdatedTask.planned_end;
                                                tsk.EndDate = calculDateFin(id, tsk.StartDate, (int)tsk.duration_h);

                                                tsk.planned_start = ganttData.UpdatedTask.planned_end;
                                                tsk.planned_end = calculDateFin(id,(DateTime) tsk.planned_start,(int) tsk.duration_h_planning);
                                                db.Entry(db.Tasks.Find(tsk.Id)).CurrentValues.SetValues(tsk);
                                                //db.Entry(tsk).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                        }
                                        else if (ln.TargetTaskId == ganttData.UpdatedTask.Id)
                                        {
                                            if (DateTime.Compare((DateTime)ganttData.UpdatedTask.planned_start, (DateTime)db.Tasks.First(t => t.Id == ln.SourceTaskId).planned_end) < 0)
                                            {

                                                Tasks tsk = db.Tasks.First(t => t.Id == ln.SourceTaskId);
                                                ganttData.UpdatedTask.StartDate = (DateTime)tsk.planned_end;
                                                ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);

                                                ganttData.UpdatedTask.planned_start = tsk.planned_end;
                                                ganttData.UpdatedTask.planned_end = calculDateFin(id, (DateTime)ganttData.UpdatedTask.planned_start, (int)ganttData.UpdatedTask.duration_h_planning);
                                                //db.Entry(tsk).State = EntityState.Modified;
                                                //db.SaveChanges();
                                            }
                                        }
                                    }
                                    List<Tasks> tasks = db.Tasks.Where(tsk => tsk.owner_id == ganttData.UpdatedTask.owner_id).ToList();
                                    Boolean test = true;
                                    foreach (Tasks tsk in tasks)
                                    {
                                        if (tsk.Id != ganttData.UpdatedTask.Id)
                                        {
                                            int comp = DateTime.Compare((DateTime)tsk.planned_start, (DateTime)ganttData.UpdatedTask.planned_end);
                                            int comp2 = DateTime.Compare((DateTime)tsk.planned_end, (DateTime)ganttData.UpdatedTask.planned_start);


                                            if (comp < 0 && comp2 > 0)
                                            {
                                                ganttData.UpdatedTask.color = "#FFA500";
                                                test = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (test == true)
                                    {
                                        ganttData.UpdatedTask.color = "";
                                    }
                                }

                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                if (ganttData.UpdatedTask.ParentId != null)
                                {
                                    Tasks parent = db.Tasks.Find(ganttData.UpdatedTask.ParentId);

                                    parent.Duration = calculJours(parent.StartDate, (int)parent.duration_h);
                                    List<Tasks> sous = db.Tasks.Where(t => t.ParentId == ganttData.UpdatedTask.ParentId).ToList();
                                    if (sous != null)
                                    {
                                        decimal progress = 0;
                                        int i = 0;
                                        //parent.Duration_h=
                                        //calculDateFin
                                        DateTime fin = new DateTime();
                                        DateTime debut = new DateTime();
                                        int duree1 = 0;
                                        foreach (Tasks t in sous)
                                        {
                                            if (t == sous[0])
                                            {
                                                fin = calculDateFin(id,(DateTime) t.planned_start,(int) t.duration_h_planning);
                                                debut =(DateTime) t.planned_start;
                                            }
                                            else
                                            {
                                                if (DateTime.Compare(fin,(DateTime) t.planned_end) < 0)
                                                {
                                                    fin =(DateTime) t.planned_end; 
                                                }
                                                if (DateTime.Compare((DateTime) t.planned_start, debut) < 0)
                                                {
                                                    duree1 = CalculHeuresTravail(id, (DateTime)t.planned_start, (DateTime)parent.planned_start);
                                                    debut = (DateTime)t.planned_start;
                                                }
                                            }


                                            progress += t.Progress;
                                            i++;
                                        }
                                        if (debut != parent.planned_start)
                                        {
                                            parent.planned_start = debut;
                                            parent.duration_h_planning += duree1;
                                        }
                                        parent.duration_h_planning = CalculHeuresTravail(id, (DateTime)parent.planned_start, fin);
                                        //parent.Duration=
                                        progress = (progress == 0) ? 0 : progress / i;
                                        parent.Progress = progress;
                                    }
                                    parent.duration_planning = calculJours((DateTime)parent.planned_start,(int) parent.duration_h_planning);
                                    parent.Duration =(int) parent.duration_planning;
                                    parent.StartDate = (DateTime)parent.planned_start;
                                    parent.EndDate = calculDateFin(id, parent.StartDate,(int) parent.duration_h);

                                    parent.planned_end = calculDateFin(id, (DateTime)parent.planned_start,(int) parent.duration_h_planning);
                                    parent.duration_h = (int)parent.duration_h_planning;

                                    db.Entry(db.Tasks.Find(parent.Id)).CurrentValues.SetValues(parent);
                                    db.SaveChanges();


                                }
                                List<Tasks> task_list = db.Tasks.Where(t => t.ProjetTechniquesID == id).ToList();
                                ProjetTechniques projetTechnique = db.ProjetTechniques.Where(fou=>fou.Devis_clt==id).FirstOrDefault();
                                DateTime deb = (DateTime)task_list[0].planned_start;
                                DateTime fin1 = (DateTime)task_list[0].planned_end;
                                foreach (Tasks t in task_list)
                                {
                                    if (DateTime.Compare(deb, (DateTime)t.planned_start) > 0)
                                    {
                                        deb = (DateTime)t.planned_start;
                                    }
                                    if (DateTime.Compare(fin1, (DateTime)t.planned_end) < 0)
                                    {
                                        fin1 = (DateTime)t.planned_end;
                                    }
                                }
                                projetTechnique.DateDebut = deb;
                                projetTechnique.DateFin = fin1;
                                db.Entry(projetTechnique).State = EntityState.Modified;
                                db.SaveChanges();

                                break;
                            }
                        case GanttMode.Links:
                            {
                                if (ganttData.Action != GanttAction.Deleted)
                                {
                                    Tasks src = db.Tasks.Find(ganttData.UpdatedLink.SourceTaskId);
                                    Tasks tar = db.Tasks.Find(ganttData.UpdatedLink.TargetTaskId);
                                    if (DateTime.Compare((DateTime)src.planned_end, (DateTime)tar.planned_start) > 0)
                                    {
                                        tar.StartDate = (DateTime)src.planned_end;
                                        tar.EndDate = calculDateFin(id, tar.StartDate,(int) tar.duration_h);

                                        tar.planned_start = src.planned_end;
                                        tar.planned_end = calculDateFin(id, (DateTime)tar.planned_start, (int)tar.duration_h_planning);
                                        db.Entry(db.Tasks.Find(tar.Id)).CurrentValues.SetValues(tar);
                                        db.SaveChanges();

                                    }
                                }
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            //}
            //catch
            //{
            //    // return error to client if something went wrong
            //    dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            //}

            return GanttRespose(dataActions);
        }



        //Fonction qui modifie les infos de planification
        [HttpPost]
        public ContentResult SaveComparaison(FormCollection form)
        {
            var dataActions = GanttRequest.ParseComparaison(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                ganttData.UpdatedTask.ProjetTechniquesID = id;
                                Tasks task = db.Tasks.Find(ganttData.UpdatedTask.Id);
                                ganttData.UpdatedTask.owner_id = task.owner_id;
                                ganttData.UpdatedTask.duration_h_planning = CalculHeuresTravail(id, (DateTime)ganttData.UpdatedTask.planned_start, (DateTime)ganttData.UpdatedTask.planned_end);
                                ganttData.UpdatedTask.Duration = task.Duration;
                                ganttData.UpdatedTask.duration_planning = task.duration_planning;
                                ganttData.UpdatedTask.owner_id = task.owner_id;
                                ganttData.UpdatedTask.color = task.color;
                                ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);

                                //ganttData.UpdatedTask.retard = (DateTime.Compare(task.planned_end, calculDateFin(id, ganttData.UpdatedTask.StartDate, ganttData.UpdatedTask.Duration_h)) < 0) ? CalculHeuresTravail(id, task.planned_end, calculDateFin(id, ganttData.UpdatedTask.StartDate, ganttData.UpdatedTask.Duration_h)) : 0;

                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                // ganttData.UpdatedTask.Id = db.Tasks.Last().Id;
                                if (ganttData.UpdatedTask.ParentId != null)
                                {
                                    Tasks parent = db.Tasks.Find(ganttData.UpdatedTask.ParentId);
             

                                    parent.Duration = calculJours(parent.StartDate,(int) parent.duration_h);
                                    List<Tasks> sous = db.Tasks.Where(t => t.ParentId == ganttData.UpdatedTask.ParentId).ToList();
                                    if (sous != null)
                                    {
                                        decimal progress = 0;
                                        int i = 0;
                                        //parent.Duration_h=
                                        //calculDateFin
                                        DateTime fin = new DateTime();
                                        DateTime debut = new DateTime();
                                        int duree1 = 0;
                                        foreach (Tasks t in sous)
                                        {
                                            if (t == sous[0])
                                            {
                                                fin = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                                debut = t.StartDate;
                                            }
                                            else
                                            {
                                                if (DateTime.Compare(fin, calculDateFin(id, t.StartDate,(int) t.duration_h)) < 0)
                                                {
                                                    fin = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                                }
                                                if (DateTime.Compare(t.StartDate, debut) < 0)
                                                {
                                                    duree1 = CalculHeuresTravail(id, t.StartDate, parent.StartDate);
                                                    debut = t.StartDate;
                                                }
                                            }


                                            progress += t.Progress;
                                            i++;
                                        }
                                        if (debut != parent.StartDate)
                                        {
                                            parent.StartDate = debut;
                                            parent.EndDate = calculDateFin(id, parent.StartDate, (int)parent.duration_h);

                                            parent.duration_h += duree1;
                                        }
                                        parent.duration_h = CalculHeuresTravail(id, parent.StartDate, fin);
                                        //parent.Duration=
                                        progress = (progress == 0) ? 0 : progress / i;
                                        parent.Progress = progress;
                                    }
                                    parent.Duration = calculJours(parent.StartDate,(int) parent.duration_h);

                                    db.Entry(db.Tasks.Find(parent.Id)).CurrentValues.SetValues(parent);
                                    db.SaveChanges();


                                }
                                List<Tasks> task_list = db.Tasks.Where(t => t.ProjetTechniquesID == id).ToList();
                                ProjetTechniques projetTechnique = db.ProjetTechniques.Find(id);
                                DateTime deb = task_list[0].StartDate;
                                DateTime fin1 = calculDateFin(id, task_list[0].StartDate,(int) task_list[0].duration_h);
                                foreach (Tasks t in task_list)
                                {
                                    if (DateTime.Compare(deb, t.StartDate) > 0)
                                    {
                                        deb = t.StartDate;
                                    }
                                    if (DateTime.Compare(fin1, calculDateFin(id, t.StartDate,(int) t.duration_h)) < 0)
                                    {
                                        fin1 = calculDateFin(id, t.StartDate,(int) t.duration_h);
                                    }
                                }
                                projetTechnique.DateDebutReel = deb;
                                projetTechnique.DateFinReel = fin1;
                                db.Entry(projetTechnique).State = EntityState.Modified;
                                db.SaveChanges();
                                List<Links> links = db.Links.Where(lnk => lnk.SourceTaskId == ganttData.UpdatedTask.Id || lnk.TargetTaskId == ganttData.UpdatedTask.Id).ToList();
                                foreach (Links ln in links)
                                {
                                    if (ln.SourceTaskId == ganttData.UpdatedTask.Id)
                                    {
                                        if (DateTime.Compare(calculDateFin(id, ganttData.UpdatedTask.StartDate, (int)ganttData.UpdatedTask.duration_h), db.Tasks.First(t => t.Id == ln.TargetTaskId).StartDate) > 0)
                                        {
                                            Tasks tsk = db.Tasks.First(t => t.Id == ln.TargetTaskId);
                                            tsk.StartDate = calculDateFin(id, ganttData.UpdatedTask.StartDate, (int)ganttData.UpdatedTask.duration_h);
                                            tsk.EndDate = calculDateFin(id, tsk.StartDate,(int) tsk.duration_h);
                                            db.Entry(db.Tasks.Find(tsk.Id)).CurrentValues.SetValues(tsk);
                                            db.SaveChanges();
                                        }
                                    }
                                    else if (ln.TargetTaskId == ganttData.UpdatedTask.Id)
                                    {
                                        Tasks tsk = db.Tasks.First(t => t.Id == ln.SourceTaskId);
                                        if (DateTime.Compare(ganttData.UpdatedTask.StartDate, calculDateFin(id, tsk.StartDate,(int) tsk.duration_h)) < 0)
                                        {

                                            ganttData.UpdatedTask.StartDate = calculDateFin(id, tsk.StartDate,(int) tsk.duration_h);
                                            ganttData.UpdatedTask.EndDate = calculDateFin(id, ganttData.UpdatedTask.StartDate,(int) ganttData.UpdatedTask.duration_h);


                                        }
                                    }
                                }

                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }




        //Save diag de diagramme de planification
        [HttpPost]
        public ContentResult SavePlanification(FormCollection form)
        {
            var dataActions = GanttRequest.ParsePlanification(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                if (ganttData.Action != GanttAction.Deleted)
                                {
                                    Tasks t = db.Tasks.Find(ganttData.UpdatedTask.Id);
                                    ganttData.UpdatedTask.planned_start = t.planned_start;
                                    ganttData.UpdatedTask.planned_end = t.planned_end;
                                    ganttData.UpdatedTask.StartDate = t.StartDate;
                                    ganttData.UpdatedTask.Duration = t.Duration;
                                    ganttData.UpdatedTask.duration_h = t.duration_h;
                                    ganttData.UpdatedTask.duration_h_planning = t.duration_h_planning;
                                    ganttData.UpdatedTask.duration_planning = t.duration_planning;
                                    ganttData.UpdatedTask.ProjetTechniquesID = id;
                                    List<Tasks> tasks = db.Tasks.Where(tsk => tsk.owner_id == ganttData.UpdatedTask.owner_id).ToList();
                                    Boolean test = true;
                                    foreach (Tasks tsk in tasks)
                                    {
                                        if (tsk.Id != ganttData.UpdatedTask.Id)
                                        {
                                            int comp = DateTime.Compare((DateTime) tsk.planned_start, (DateTime)ganttData.UpdatedTask.planned_end);
                                            int comp2 = DateTime.Compare((DateTime)tsk.planned_end, (DateTime)ganttData.UpdatedTask.planned_start);


                                            if (comp < 0 && comp2 > 0)
                                            {
                                                ganttData.UpdatedTask.color = "#FFA500";
                                                test = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (test == true)
                                    {
                                        ganttData.UpdatedTask.color = "";
                                    }
                                    //ganttData.UpdatedTask.planned_end = ganttData.UpdatedTask.StartDate.AddDays(ganttData.UpdatedTask.Duration);
                                }
                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }




        //Save diag de réalisation
        [HttpPost]
        public ContentResult SaveRealisation(FormCollection form)
        {
            var dataActions = GanttRequest.ParseRealisation(form, Request.QueryString["gantt_mode"]);
            int id = (int)Session["pt_id"];
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            {
                                if (ganttData.Action != GanttAction.Deleted)
                                {
                                    ganttData.UpdatedTask.ProjetTechniquesID = id;
                                    //ganttData.UpdatedTask.planned_start = ganttData.UpdatedTask.StartDate;
                                    //ganttData.UpdatedTask.owner_id = 0;
                                    Tasks task = db.Tasks.Find(ganttData.UpdatedTask.Id);
                                    ganttData.UpdatedTask.planned_start = task.planned_start;
                                    ganttData.UpdatedTask.planned_end = task.planned_end;
                                    ganttData.UpdatedTask.StartDate = task.StartDate;
                                    ganttData.UpdatedTask.Duration = task.Duration;
                                    ganttData.UpdatedTask.duration_h = task.duration_h;
                                    ganttData.UpdatedTask.duration_h_planning = task.duration_h_planning;
                                    ganttData.UpdatedTask.duration_planning = task.duration_planning;
                                    ganttData.UpdatedTask.color = task.color;
                                }
                                UpdateTasks(ganttData);
                                db.SaveChanges();
                                // ganttData.UpdatedTask.Id = db.Tasks.Last().Id;
                                break;
                            }
                        case GanttMode.Links:
                            {
                                UpdateLinks(ganttData);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }

            return GanttRespose(dataActions);
        }




        /// <summary>
        /// Update gantt tasks
        /// </summary>
        /// <param name="ganttData">GanttData object</param>
        private void UpdateTasks(GanttRequest ganttData)
        {

            switch (ganttData.Action)
            {
                case GanttAction.Inserted:
                    // add new gantt task entity

                    db.Tasks.Add(ganttData.UpdatedTask);

                    break;
                case GanttAction.Deleted:
                    // remove gantt tasks
                    db.Tasks.Remove(db.Tasks.Find(ganttData.SourceId));
                    break;
                case GanttAction.Updated:
                    // update gantt task

                    //ganttData.UpdatedTask.owner_id = 0;
                    db.Entry(db.Tasks.Find(ganttData.UpdatedTask.Id)).CurrentValues.SetValues(ganttData.UpdatedTask);
                    break;
                default:
                    ganttData.Action = GanttAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Update gantt links
        /// </summary>
        /// <param name="ganttData">GanttData object</param>
        private void UpdateLinks(GanttRequest ganttData)
        {
            switch (ganttData.Action)
            {
                case GanttAction.Inserted:
                    // add new gantt link
                    db.Links.Add(ganttData.UpdatedLink);
                    break;
                case GanttAction.Deleted:
                    // remove gantt link
                    db.Links.Remove(db.Links.Find(ganttData.SourceId));
                    break;
                case GanttAction.Updated:
                    // update gantt link
                    db.Entry(db.Links.Find(ganttData.UpdatedLink.Id)).CurrentValues.SetValues(ganttData.UpdatedLink);
                    break;
                default:
                    ganttData.Action = GanttAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Create XML response for gantt
        /// </summary>
        /// <param name="ganttData">Gantt data</param>
        /// <returns>XML response</returns>
        private ContentResult GanttRespose(List<GanttRequest> ganttDataCollection)
        {
            var actions = new List<XElement>();
            foreach (var ganttData in ganttDataCollection)
            {
                var action = new XElement("action");
                action.SetAttributeValue("type", ganttData.Action.ToString().ToLower());

                action.SetAttributeValue("tid", (ganttData.Action != GanttAction.Inserted) ? ganttData.SourceId :
                    (ganttData.Mode == GanttMode.Tasks) ? ganttData.UpdatedTask.Id : ganttData.UpdatedLink.Id);
                action.SetAttributeValue("sid", ganttData.SourceId);
                //action.SetAttributeValue("progress", 0);
                actions.Add(action);
            }

            var data = new XDocument(new XElement("data", actions));
            data.Declaration = new XDeclaration("1.0", "utf-8", "true");
            return Content(data.ToString(), "text/xml");

        }
    }
}