namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Ressource
    {
        public Ressource()
        {
            this.Taches = new HashSet<Tache>();
        }

        public int RessourceId { get; set; }

        [Required(ErrorMessage = "Désignation  est obligatoire")]
        [Display(Name = "Désignation ")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Type  est obligatoire")]
        [Display(Name = "Type ")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Date debut  est obligatoire")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Date début")]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "Date fin  est obligatoire")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Date fin")]
        public DateTime DateFin { get; set; }

        [Required(ErrorMessage = "Cout  est obligatoire")]
        [Display(Name = "Cout ")]
        public double cout { get; set; }

        public virtual ICollection<Tache> Taches { get; set; }
    }
}
