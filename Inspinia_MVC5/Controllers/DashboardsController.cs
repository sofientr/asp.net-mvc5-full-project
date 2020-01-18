using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class DashboardsController : Controller
    {
		private MED_TRABELSI db = new MED_TRABELSI();
		public ActionResult Dashboard_1()
        {
			List<Inspinia_MVC5.Models.AffaireCommerciales> Liste = db.AffaireCommerciales.ToList();

			List<Inspinia_MVC5.Models.AffaireCommerciales> Liste1 = new List<Inspinia_MVC5.Models.AffaireCommerciales>();
			//int a = 0;
			int moins = 0;
			int moinsd = 0;
			int moinsdd = 0;
			int moinsddd= 0;
			int moinsdddd = 0;
			int moinsddddd = 0;
			int moinsdddddd = 0;
			int moinsddddddd = 0;
			int moinsdddddddd = 0;
			int moinsddddddddd = 0;
			int moinsdddddddddd = 0;
			int moinsddddddddddd = 0;
			int moinsdddddddddddd = 0;
			int totale = 0;
			ViewBag.acctu = DateTime.Now.Month;

			

			

			foreach (var item in Liste)
			{
				DateTime aa = (DateTime)item.Date_de_Soumission;
				int moisannéder = @ViewBag.acctu - 11; if (moisannéder <= 0) { moisannéder = 12 - (11 - @ViewBag.acctu); };
				int dif = @ViewBag.acctu - aa.Month; if (dif < 0) { dif = 12 - (aa.Month - @ViewBag.acctu); };
				if (item.AffaireObtenue== "Obtenue"&&item.Date_de_Soumission<= DateTime.Now&& (aa.Year == DateTime.Now.Year || (aa.Year == DateTime.Now.Year - 1) && (aa.Month >= moisannéder)))
				{
					if (dif== 0  )
					{
						moins++;
						totale++;
					}
					if (dif==1)
					{
						moinsd++;
						totale++;
					}
					if (dif == 2)
					{
						moinsdd++;
						totale++;
					}
					if (dif == 3 )
					{
						moinsddd++;
						totale++;
					}
					if (dif == 4 )
					{
						moinsdddd++;
						totale++;
					}
					if (dif == 5 )
					{
						moinsddddd++;
						totale++;
					}
					if (dif == 6 )
					{
						moinsdddddd++;
						totale++;
					}
					if (dif== 7 )
					{
						moinsddddddd++;
						totale++;
					}
					if (dif == 8 )
					{
						moinsdddddddd++;
						totale++;
					}
					if (dif == 9 )
					{
						moinsddddddddd++;
						totale++;
					}
					if (dif  == 10 )
					{
						moinsdddddddddd++;
						totale++;
					}
					if (dif  == 11 )
					{
						moinsddddddddddd++;
						totale++;
					}
				}
				
			}
			ViewBag.moins = moins;
			ViewBag.moinsd = moinsd;
			ViewBag.moinsdd = moinsdd;
			ViewBag.moinsddd = moinsddd;
			ViewBag.moinsdddd = moinsdddd;
			ViewBag.moinsddddd = moinsddddd;
			ViewBag.moinsdddddd = moinsdddddd;
			ViewBag.moinsddddddd = moinsddddddd;
			ViewBag.moinsdddddddd = moinsdddddddd;
			ViewBag.moinsddddddddd = moinsddddddddd;
			ViewBag.moinsdddddddddd = moinsdddddddddd;
			ViewBag.moinsddddddddddd = moinsddddddddddd;
			
			ViewBag.tot = totale;
			return View();
        }

        public ActionResult Dashboard_2()
        {
            return View();
        }

        public ActionResult Dashboard_3()
        {
            return View();
        }
        
        public ActionResult Dashboard_4()
        {
            return View();
        }
        
        public ActionResult Dashboard_4_1()
        {
            return View();
        }

        public ActionResult Dashboard_5()
        {
            return View();
        }

    }
}