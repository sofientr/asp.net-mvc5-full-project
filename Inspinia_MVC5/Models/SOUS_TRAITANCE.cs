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
    
    public partial class SOUS_TRAITANCE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOUS_TRAITANCE()
        {
            this.lIGNES_SERVICESSSTRAITANCE = new HashSet<lIGNES_SERVICESSSTRAITANCE>();
            this.lIGNES_SERVICESSSTRAITANCE_FACTURE = new HashSet<lIGNES_SERVICESSSTRAITANCE_FACTURE>();
        }
    
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NOM { get; set; }
        public string ADRESSE { get; set; }
        public string TELEPHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string SITE_WEB { get; set; }
        public string ID_FISCAL { get; set; }
        public string AI { get; set; }
        public string NIS { get; set; }
        public string RC { get; set; }
        public string RIB { get; set; }
        public string CONTACT { get; set; }
        public Nullable<bool> Etat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lIGNES_SERVICESSSTRAITANCE> lIGNES_SERVICESSSTRAITANCE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lIGNES_SERVICESSSTRAITANCE_FACTURE> lIGNES_SERVICESSSTRAITANCE_FACTURE { get; set; }
    }
}
