namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Designation est obligatoire")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Description est obligatoire")]
        public string Description { get; set; }

        [Display(Name = "Url Document")]
        [Required(ErrorMessage = "Responsable est obligatoire")]
        public string UrlDoc { get; set; }

        [Required(ErrorMessage = "Etat est obligatoire")]
        public string Etat { get; set; }

        [Required(ErrorMessage = "Type est obligatoire")]
        public string Type { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "DateEdition est obligatoire")]
        public DateTime DateEdition { get; set; }


        public int TacheId { get; set; }
    
        public virtual Tache Tache { get; set; }
    }
}
