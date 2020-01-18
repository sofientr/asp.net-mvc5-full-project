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
    
    public partial class COMMANDES_FOURNISSEURS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMMANDES_FOURNISSEURS()
        {
            this.BONS_RECEPTIONS_FOURNISSEURS = new HashSet<BONS_RECEPTIONS_FOURNISSEURS>();
            this.FACTURES_FOURNISSEURS = new HashSet<FACTURES_FOURNISSEURS>();
            this.LIGNES_COMMANDES_FOURNISSEURS = new HashSet<LIGNES_COMMANDES_FOURNISSEURS>();
        }
    
        public int ID { get; set; }
        public string CODE { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODE_PAIEMENT { get; set; }
        public int FOURNISSEUR { get; set; }
        public int Societes { get; set; }
        public Nullable<decimal> THT { get; set; }
        public Nullable<decimal> TTVA { get; set; }
        public Nullable<decimal> NHT { get; set; }
        public decimal TTC { get; set; }
        public Nullable<decimal> TNET { get; set; }
        public bool VALIDER { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public Nullable<int> Tiers { get; set; }
        public Nullable<int> Devis_Frs { get; set; }
        public string Designation { get; set; }
        public string ConditionPaiement { get; set; }
        public string LieuLivraison { get; set; }
        public Nullable<int> RespAchat { get; set; }
        public Nullable<int> RespFina { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BONS_RECEPTIONS_FOURNISSEURS> BONS_RECEPTIONS_FOURNISSEURS { get; set; }
        public virtual COMMANDES_FOURNISSEURS COMMANDES_FOURNISSEURS1 { get; set; }
        public virtual COMMANDES_FOURNISSEURS COMMANDES_FOURNISSEURS2 { get; set; }
        public virtual DEVIS_FOURNISSEURS DEVIS_FOURNISSEURS { get; set; }
        public virtual FOURNISSEURS FOURNISSEURS { get; set; }
        public virtual Personnels Personnels { get; set; }
        public virtual Personnels Personnels1 { get; set; }
        public virtual SocieteLogo SocieteLogo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURES_FOURNISSEURS> FACTURES_FOURNISSEURS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIGNES_COMMANDES_FOURNISSEURS> LIGNES_COMMANDES_FOURNISSEURS { get; set; }
    }
}