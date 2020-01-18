using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Controllers
{
    public class LigneProduit
    {
        
        public int ID;
        public int Code;
        public string NumDevis;
		public string Projet_Technique;
		[Required(ErrorMessage = "LIBELLE is required")]
        public string LIBELLE;
        [Required(ErrorMessage = "DESIGNATION is required")]
        public string DESIGNATION;
        public string MARQUE;
        public string UNITE;
        public string DEVISE;
        public string CATEGORIE;
        public string Sous_CATEGORIE;
        public decimal QUANTITE;
        public decimal QUANTITELiv;
        public decimal QUANTITERES;
        public decimal STOCK;
        [Required(ErrorMessage = "PRIX UNITAIRE HT is required")]
        public decimal PRIX_VENTE_HT;
        public decimal PRIX_VENTE_HT2;

        public decimal REMISE;
        public decimal MARGE;

        public decimal PTHT;
        public int TVA;
        public decimal TTC;
        public string NUM_OFFRE;
        public string DATE_OFFRE;
        public int DUREE_VAL;
        public int ID_Bl;

    }
}