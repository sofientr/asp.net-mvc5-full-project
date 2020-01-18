namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProjetTechnique
    {
        public ProjetTechnique()
        {
            this.Modules = new HashSet<Module>();
            this.PersonnelProjets = new HashSet<PersonnelProjet>();
        }



        public int ProjetTechniqueId { get; set; }

        [Display(Name = "Réference technique")]
        // [Required(ErrorMessage = "Réference est obligatoire")]
        public string ReferenceTech { get; set; }

        [Display(Name = "Date Début")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Début est obligatoire")]
        public DateTime DateDebut { get; set; }

        [Display(Name = "Date Fin")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Fin est obligatoire")]
        public DateTime DateFin { get; set; }


        [Display(Name = "Cout")]
        [Required(ErrorMessage = "Cout est obligatoire")]
        public float Cout { get; set; }



        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Display(Name = "Responsable commerciale")]
        public int PersonnelId { get; set; }


        [Display(Name = "Statut")]
        [Required(ErrorMessage = "Statut est obligatoire")]
        public string Statut { get; set; }


        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Designation est obligatoire")]
        public string Designation { get; set; }

        [Display(Name = "CoutReel")]
        [Required(ErrorMessage = "Cout est obligatoire")]
        public float CoutReel { get; set; }

        [Display(Name = "Date Début Réel")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Début est obligatoire")]
        public DateTime DateDebutReel { get; set; }

        [Display(Name = "Date Fin Réel")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Fin est obligatoire")]
        public DateTime DateFinReel { get; set; }


        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<PersonnelProjet> PersonnelProjets { get; set; }



    }
}
