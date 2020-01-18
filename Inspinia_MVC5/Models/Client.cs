namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Client
    {
        public Client()
        {
            this.AffaireCommerciales = new HashSet<AffaireCommerciale>();
        }

        public int ClientId { get; set; }

        [Display(Name = "Nom")]
       // [Required(ErrorMessage = "Nom est obligatoire")]
        public string Nom { get; set; }

        [Display(Name = "Responsable")]
      //  [Required(ErrorMessage = "Responsable  est obligatoire")]
        public string Responsable { get; set; }

        [Display(Name = "Adresse")]
        //[Required(ErrorMessage = "Adresse  est obligatoire")]
        public string Adresse { get; set; }

        [Display(Name = "Téléphone mobile")]
        //[Required(ErrorMessage = "Téléphone mobile est obligatoire")]
        [DataType(DataType.PhoneNumber)]
        public long TelePhone { get; set; }


        [Display(Name = "Téléphone fix")]
        [DataType(DataType.PhoneNumber)]
       // [Required(ErrorMessage = "Téléphone fix est obligatoire")]
        public long TelFix { get; set; }


        [Display(Name = "Email")]
        //[Required(ErrorMessage = "Email  est obligatoire")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Veuillez saisir votre Email")]
        public string Email { get; set; }

        public virtual ICollection<AffaireCommerciale> AffaireCommerciales { get; set; }
    }
}
