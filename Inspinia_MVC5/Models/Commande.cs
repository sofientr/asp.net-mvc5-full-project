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
    
    public partial class Commande
    {
        public int CmdID { get; set; }
        public Nullable<System.DateTime> Date_Passasation { get; set; }
        public Nullable<decimal> Prix_Totale { get; set; }
        public Nullable<int> DecaissID { get; set; }
        public Nullable<int> FRSID { get; set; }
    
        public virtual Decaissement Decaissement { get; set; }
        public virtual Fournisseur Fournisseur { get; set; }
        public virtual Facturation Facturation { get; set; }
    }
}
