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
    
    public partial class Facturation
    {
        public int FactID { get; set; }
        public string NomFact { get; set; }
        public Nullable<int> PrID { get; set; }
        public Nullable<int> EncaissID { get; set; }
        public Nullable<System.DateTime> DateFact { get; set; }
    
        public virtual Commande Commande { get; set; }
        public virtual Encaissement Encaissement { get; set; }
        public virtual Projets Projets { get; set; }
    }
}
