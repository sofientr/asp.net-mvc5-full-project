//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRINT_COMMANDES_FOURNISSEURS
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODE_PAIEMENT { get; set; }
        public int Societes { get; set; }
        public Nullable<decimal> THT { get; set; }
        public Nullable<decimal> TTVA { get; set; }
        public Nullable<decimal> NHT { get; set; }
        public decimal TTC { get; set; }
        public Nullable<decimal> TNET { get; set; }
        public bool VALIDER { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public Nullable<int> Tiers { get; set; }
        public string DESIGNATION_PRODUIT { get; set; }
        public Nullable<int> QUANTITE { get; set; }
        public int Prix_achat { get; set; }
        public Nullable<decimal> PRIX_UNITAIRE_HT { get; set; }
        public Nullable<decimal> Expr1 { get; set; }
        public Nullable<decimal> TOTALE_REMISE_HT { get; set; }
        public Nullable<decimal> TOTALE_HT { get; set; }
        public Nullable<int> TVA { get; set; }
        public Nullable<decimal> TOTALE_TTC { get; set; }
        public Nullable<int> COMMANDE_FOURNISSEUR { get; set; }
        public string Categorie { get; set; }
        public string Sous_Categorie { get; set; }
        public string Marque { get; set; }
        public string Unite { get; set; }
        public string Devise { get; set; }
        public int DiretionID { get; set; }
        public string Nom { get; set; }
        public Nullable<decimal> Budget { get; set; }
        public Nullable<int> Année { get; set; }
        public Nullable<int> SociID { get; set; }
        public Nullable<int> SCoorID { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string Expr2 { get; set; }
        public string ADRESSE { get; set; }
        public string MAIL { get; set; }
        public string SITE_WEB { get; set; }
        public string ID_FISCAL { get; set; }
        public string AI { get; set; }
        public string NIS { get; set; }
        public string RC { get; set; }
        public string RIB { get; set; }
        public string Expr3 { get; set; }
        public string CODE_ACCES { get; set; }
        public Nullable<int> TiersID { get; set; }
        public Nullable<int> FRSID { get; set; }
        public string Direction { get; set; }
        public Nullable<int> Expr4 { get; set; }
        public string Email { get; set; }
    }
}
