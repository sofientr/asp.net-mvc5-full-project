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
    
    public partial class LIGNES_DESCRIPTION_ACCESOIRE_BL
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> QTE { get; set; }
        public Nullable<decimal> PUHT { get; set; }
        public Nullable<decimal> PTHT { get; set; }
        public Nullable<decimal> PTTC { get; set; }
        public Nullable<int> TVA { get; set; }
        public Nullable<int> ID_ACC { get; set; }
        public Nullable<int> ID_ART { get; set; }
        public Nullable<int> ID_SSCAT { get; set; }
        public Nullable<int> ID_LigneBL { get; set; }
    
        public virtual LIGNES_CUISINE_BONLIVRAISON_CLIENTS LIGNES_CUISINE_BONLIVRAISON_CLIENTS { get; set; }
    }
}
