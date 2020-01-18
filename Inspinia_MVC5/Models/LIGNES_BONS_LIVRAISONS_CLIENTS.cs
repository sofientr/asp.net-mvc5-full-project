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
    
    public partial class LIGNES_BONS_LIVRAISONS_CLIENTS
    {
        public int ID { get; set; }
        public int Prix_achat { get; set; }
        public string DESIGNATION_PRODUIT { get; set; }
        public Nullable<double> QUANTITE { get; set; }
        public Nullable<double> QTERES { get; set; }
        public Nullable<decimal> PRIX_UNITAIRE_HT { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public Nullable<decimal> TOTALE_REMISE_HT { get; set; }
        public Nullable<decimal> TOTALE_HT { get; set; }
        public Nullable<int> TVA { get; set; }
        public Nullable<decimal> TOTALE_TTC { get; set; }
        public Nullable<int> BON_LIVRAISON_CLIENT { get; set; }
        public string Categorie { get; set; }
        public string Sous_Categorie { get; set; }
        public string Marque { get; set; }
        public string Unite { get; set; }
        public string Devise { get; set; }
        public Nullable<double> STOCK { get; set; }
        public Nullable<System.DateTime> Date_offre_de_prix { get; set; }
        public Nullable<int> Duree_de_validite { get; set; }
        public string N_Offre_de_Prix { get; set; }
        public string Libelle_Prd { get; set; }
        public Nullable<decimal> PRIX_UNITAIRE_HTVente { get; set; }
        public Nullable<decimal> MARGE { get; set; }
    
        public virtual BONS_LIVRAISONS_CLIENTS BONS_LIVRAISONS_CLIENTS { get; set; }
    }
}
