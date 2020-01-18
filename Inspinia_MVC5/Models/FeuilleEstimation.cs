namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FeuilleEstimation
    {

        public int FeuilleEstimationId { get; set; }


        [Display(Name = "Module")]
        [Required(ErrorMessage = "Module est obligatoire")]
        public string Module { get; set; }


        [Display(Name = "Prix")]
        [Required(ErrorMessage = "Prix est obligatoire")]
        public string Prix { get; set; }


        [Display(Name = "Durée")]
        [Required(ErrorMessage = "Durée est obligatoire")]
        public string Duree { get; set; }


        public int AffaireCommercialeId { get; set; }
    
        public virtual AffaireCommerciale AffaireCommerciale { get; set; }
    }
}
