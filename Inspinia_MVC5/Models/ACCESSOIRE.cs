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
    
    public partial class ACCESSOIRE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCESSOIRE()
        {
            this.CAISSON = new HashSet<CAISSON>();
            this.CAISSON_COLONNE = new HashSet<CAISSON_COLONNE>();
            this.CAISSON_HAUT = new HashSet<CAISSON_HAUT>();
        }
    
        public int ID { get; set; }
        public string NOM { get; set; }
        public Nullable<decimal> PTHT { get; set; }
        public Nullable<decimal> PTTC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAISSON> CAISSON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAISSON_COLONNE> CAISSON_COLONNE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAISSON_HAUT> CAISSON_HAUT { get; set; }
    }
}
