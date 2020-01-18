using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class ScaffoldingContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ScaffoldingContext() : base("name=Tr")
        {
        }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Worker> Workers { get; set; }
        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Societes> Societes { get; set; }
        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Inscription> Inscription { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Facturation> Facturation { get; set; }
        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.AffaireCommerciale> AffaireCommerciales { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Personnel> Personnels { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.ProjetTechnique> ProjetTechniques { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Document> Documents { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.FeuilleEstimation> FeuilleEstimations { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Module> Modules { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Ressource> Ressources { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Tache> Taches { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Task> Tasks { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Parametrage> Parametrages { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Link> Links { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Email> Emails { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Receiver> Receivers { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.PersonnelProjet> PersonnelProjets { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.Staff> Staffs { get; set; }

        public System.Data.Entity.DbSet<Inspinia_MVC5.Models.ParametrageSemaines> ParametrageSemaines { get; set; }
    }
}
