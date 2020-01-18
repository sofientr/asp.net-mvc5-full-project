using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Controllers
{
    public class LignesServicesSSTraitance
    {
        public int ID;
        public int Code;
        public string REFSERVICE;
        public string DescriptionSERVICE;
        public string UNITE;
        public decimal PRIX_VENTE_HT;
        public decimal Marge;
        public decimal PRIX_VENTE_HT2;
        public decimal REMISE;
        public decimal QUANTITE;
        public decimal PTHT;
        public int TVA;
        public decimal TTC;
        public List<int> SOUS_TRAITANCE = new List<int>();
        public List<string> SOUS_TRAITANCE2 = new List<string>();

    }
}