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
    
    public partial class AVOIRS_CLIENTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AVOIRS_CLIENTS()
        {
            this.LIGNES_AVOIRS_CLIENTS = new HashSet<LIGNES_AVOIRS_CLIENTS>();
            this.LIGNES_CUISINE_AVOIR_CLIENTS = new HashSet<LIGNES_CUISINE_AVOIR_CLIENTS>();
        }
    
        public int ID { get; set; }
        public string Designation { get; set; }
        public string CODE { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODE_PAIEMENT { get; set; }
        public int CLIENT { get; set; }
        public int Societes { get; set; }
        public Nullable<decimal> THT { get; set; }
        public Nullable<decimal> TTVA { get; set; }
        public Nullable<decimal> NHT { get; set; }
        public decimal TTC { get; set; }
        public Nullable<decimal> TNET { get; set; }
        public bool VALIDER { get; set; }
        public Nullable<int> FACTURE_CLIENT { get; set; }
        public Nullable<decimal> REMISE { get; set; }
        public Nullable<int> Tiers { get; set; }
    
        public virtual CLIENTS CLIENTS { get; set; }
        public virtual FACTURES_CLIENTS FACTURES_CLIENTS { get; set; }
        public virtual Societes Societes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIGNES_AVOIRS_CLIENTS> LIGNES_AVOIRS_CLIENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIGNES_CUISINE_AVOIR_CLIENTS> LIGNES_CUISINE_AVOIR_CLIENTS { get; set; }
        public virtual SocieteLogo SocieteLogo { get; set; }
    }
}
