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
    
    public partial class Horaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horaire()
        {
            this.Jours = new HashSet<Jours>();
            this.Tableau_Horaire1 = new HashSet<Tableau_Horaire>();
        }
    
        public string Horaire1 { get; set; }
        public string Debut { get; set; }
        public string Sortie { get; set; }
        public int id { get; set; }
        public Nullable<int> id_Table_Horaire { get; set; }
        public string Debut1 { get; set; }
        public string Sortie2 { get; set; }
    
        public virtual Tableau_Horaire Tableau_Horaire { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jours> Jours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tableau_Horaire> Tableau_Horaire1 { get; set; }
    }
}
