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
    
    public partial class FACADE__LAQUE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FACADE__LAQUE()
        {
            this.DESCRIPTION_FAC_LAQUE = new HashSet<DESCRIPTION_FAC_LAQUE>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ID_FAC { get; set; }
        public Nullable<decimal> PTHT { get; set; }
        public Nullable<decimal> PTTC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESCRIPTION_FAC_LAQUE> DESCRIPTION_FAC_LAQUE { get; set; }
        public virtual FACADE FACADE { get; set; }
    }
}
