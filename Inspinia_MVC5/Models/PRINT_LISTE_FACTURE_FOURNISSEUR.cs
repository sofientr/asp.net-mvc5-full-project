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
    
    public partial class PRINT_LISTE_FACTURE_FOURNISSEUR
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODE_PAIEMENT { get; set; }
        public Nullable<decimal> TTVA { get; set; }
        public Nullable<decimal> NHT { get; set; }
        public Nullable<decimal> THT { get; set; }
        public int Societes { get; set; }
        public decimal TTC { get; set; }
        public int FOURNISSEUR { get; set; }
        public Nullable<decimal> TNET { get; set; }
        public bool VALIDER { get; set; }
        public bool PAYEE { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public Nullable<decimal> TIMBRE { get; set; }
        public Nullable<int> Tiers { get; set; }
        public int Expr1 { get; set; }
        public string Expr2 { get; set; }
        public string NOM { get; set; }
        public string Expr4 { get; set; }
        public string CODE_ACCES { get; set; }
        public string Direction { get; set; }
        public string Email { get; set; }
        public int Expr6 { get; set; }
        public int SociID { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string Expr3 { get; set; }
        public string ADRESSE { get; set; }
        public string ID_FISCAL { get; set; }
        public string AI { get; set; }
        public string NIS { get; set; }
        public string RC { get; set; }
        public string RIB { get; set; }
        public string SITE_WEB { get; set; }
        public string MAIL { get; set; }
        public byte[] logo { get; set; }
    }
}