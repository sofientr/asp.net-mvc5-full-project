namespace Inspinia_MVC5.Controllers
{
    internal class ProjetTechnique
    {
        public ProjetTechnique()
        {
        }

        public object ProjetTechniqueId { get; set; }
        public string ReferenceTech { get; set; }
        public object DateDebut { get; set; }
        public object DateFin { get; set; }
        public object Cout { get; set; }
        public object ClientId { get; set; }
        public object PersonnelId { get; set; }
        public object Designation { get; set; }
        public string Statut { get; set; }
    }
}