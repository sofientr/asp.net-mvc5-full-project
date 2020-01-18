using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class NavigationViewModel
    {
        public IEnumerable<Decaissement> Modele1s { get; set; }
        public IEnumerable<Sous_Categorie> Modele2s { get; set; }
        public IEnumerable<Categorie> Modele3s { get; set; }

        public IEnumerable<Projets> Modele4s { get; set; }

        public IEnumerable<Facturation> Modele5s { get; set; }

        public IEnumerable<Tiers> Modele6s { get; set; }

        public IEnumerable<Societes> Modele7s { get; set; }
        public IEnumerable<Direction> Modele8s { get; set; }
        public IEnumerable<Inscription> Modele9s { get; set; }

    }
}