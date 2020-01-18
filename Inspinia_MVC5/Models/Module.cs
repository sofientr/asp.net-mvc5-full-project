namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Module
    {
       public Module()
        {
            this.Taches = new HashSet<Tache>();
        }

        public int ModuleId { get; set; }

        [Required(ErrorMessage = " Designation est obligatoire")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = " Nombre de tache est obligatoire")]
        [Display(Name = "Nombre de tache")]
        public int NbTache { get; set; }

        [Required(ErrorMessage = " Statut est obligatoire")]
        [Display(Name = "Statut")]
        public string Statut { get; set; }

        [Required(ErrorMessage = " Avancement est obligatoire")]
        [Display(Name = "Avancement")]
        public int Avancement { get; set; }

        [Required(ErrorMessage = " Cout est obligatoire")]
        [Display(Name = "Cout")]
        public double Cout { get; set; }


        public int ProjetTechniqueId { get; set; }
    
        public virtual ProjetTechnique ProjetTechnique { get; set; }
        public virtual ICollection<Tache> Taches { get; set; }
    }
}
