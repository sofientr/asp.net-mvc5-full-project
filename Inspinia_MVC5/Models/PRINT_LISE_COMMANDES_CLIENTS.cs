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
    
    public partial class PRINT_LISE_COMMANDES_CLIENTS
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public string CODE { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODE_PAIEMENT { get; set; }
        public int CLIENT { get; set; }
        public Nullable<decimal> THT { get; set; }
        public Nullable<decimal> TTVA { get; set; }
        public Nullable<decimal> NHT { get; set; }
        public decimal TTC { get; set; }
        public Nullable<decimal> TNET { get; set; }
        public bool VALIDER { get; set; }
        public Nullable<int> DEVIS_CLIENT { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public int Societes { get; set; }
        public Nullable<int> Tiers { get; set; }
        public int TiersID { get; set; }
        public int Expr1 { get; set; }
        public string Expr2 { get; set; }
        public string Expr3 { get; set; }
        public string NOM { get; set; }
        public string ADRESSE { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
    }
}