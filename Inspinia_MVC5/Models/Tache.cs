namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tache
    {
       public Tache()
        {
            this.Documents = new HashSet<Document>();
            this.Ressources = new HashSet<Ressource>();
        }

        public int TacheId { get; set; }

        [Required(ErrorMessage = " Designation est obligatoire")]
        [Display(Name = "Designation")]
        public String Designation { get; set; }

        [Required(ErrorMessage = " Dur�e Moyenne est obligatoire")]
        [Display(Name = "Dur�e")]
        public int Duree { get; set; }


        [Required(ErrorMessage = " Statut Tache est obligatoire")]
        [Display(Name = "Statut")]
        public string Statut { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date D�but Tache  est obligatoire")]
        [Display(Name = "Date d�but")]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date fin  est obligatoire")]
        [Display(Name = "Date fin")]
        public DateTime DateFin { get; set; }

        [Required(ErrorMessage = " Tache pr�cedente est obligatoire")]
        [Display(Name = "Tache pr�cedente")]
        public string tachePre { get; set; }

        [Required(ErrorMessage = " Tache suivante est obligatoire")]
        [Display(Name = "Tache")]
        public string TacheSui { get; set; }

        [Required(ErrorMessage = " D�pendance est obligatoire")]
        [Display(Name = "D�pendance")]
        public string Dependance { get; set; }

        public int ModuleId { get; set; }
    
        public virtual ICollection<Document> Documents { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Ressource> Ressources { get; set; }
    }
}
