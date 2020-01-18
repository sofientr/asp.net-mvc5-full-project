namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Personnel
    {
         public Personnel()
        {
            this.AffaireCommerciales = new HashSet<AffaireCommerciale>();
            this.PersonnelProjets = new HashSet<PersonnelProjet>();
        }

        public int PersonnelId { get; set; }

        [Display(Name = "Nom")]
        //[Required(ErrorMessage = "Nom est obligatoire")]
        public string Nom { get; set; }


        [Display(Name = "Role")]
       // [Required(ErrorMessage = "Role  est obligatoire")]
        public string Role { get; set; }

        [Display(Name = "Email")]
       //[Required(ErrorMessage = "Email  est obligatoire")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Veuillez entrer format email correcte")]
        public string Email { get; set; }

      


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "Téléphone mobile")]
        [DataType(DataType.PhoneNumber)]
      // [Required(ErrorMessage = "Téléphone mobile est obligatoire")]
        public long Tel { get; set; }

        //[Display(Name = "Coût horaire")]
        //[DataType(DataType.Currency)]
        //// [Required(ErrorMessage = "Téléphone mobile est obligatoire")]
        public double Cout_hor { get; set; }

        public virtual ICollection<AffaireCommerciale> AffaireCommerciales { get; set; }
        public virtual ICollection<PersonnelProjet> PersonnelProjets { get; set; }


        public override string ToString()
        {
            return "{this.Nom}";
        }
    }
}
