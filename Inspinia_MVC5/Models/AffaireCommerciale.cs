namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AffaireCommerciale
    {
      public AffaireCommerciale()
        {
            this.FeuilleEstimations = new HashSet<FeuilleEstimation>();
            
        }

        public int AffaireCommercialeId { get; set; }

        [Display(Name = "R�f�rence")]
        //[Required(ErrorMessage = "R�f�rence est obligatoire")]
        public string Reference { get; set; }

        [Display(Name = "D�signation")]
        [Required(ErrorMessage = "D�signation est obligatoire")]
        public string Designation { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description est obligatoire")]
        public string Description { get; set; }

        [Display(Name = "Date D�but")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date D�but est obligatoire")]
        public DateTime DateDebut { get; set; }

        [Display(Name = "Date Fin")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Fin est obligatoire")]
        public DateTime DateFin { get; set; }


        [Display(Name = "Etat")]
        [Required(ErrorMessage = "Etat est obligatoire")]
        public string EtatSoum { get; set; }

        [Display(Name = "Cout")]
        [Required(ErrorMessage = "Cout est obligatoire")]
        public float Cout { get; set; }



        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Display(Name = "Responsable commerciale")]
        public int PersonnelId { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Personnel Personnel { get; set; }

        public virtual ICollection<FeuilleEstimation> FeuilleEstimations { get; set; }
      

    }
}
