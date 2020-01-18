using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class LignesServices
	{
        // GET: LignesServices
        
        public int ID;
        public int Code;
        public string REFSERVICE;
        public string DescriptionSERVICE;
        public string UNITE;
        public decimal PRIX_VENTE_HT;
        public decimal REMISE;
        public decimal QUANTITE;
        public decimal PTHT;
        public int TVA;
        public decimal TTC;
        public List<int> RESSOURCE=new List<int>();
        public List<string> RESSOURCE2 = new List<string>();


    }
}