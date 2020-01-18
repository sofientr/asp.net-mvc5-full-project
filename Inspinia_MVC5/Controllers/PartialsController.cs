using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    [ChildActionOnly]
    public class PartialsController : Controller
    {
        private MED_TRABELSI db = new MED_TRABELSI();
        public ActionResult Navigation()
        {
            var viewModel = new MonViewModel();
            viewModel.Modele1s = db.Decaissement;
            viewModel.Modele2s = db.Sous_Categorie;
            viewModel.Modele3s = db.Categorie;
            viewModel.Modele4s = db.Projets;
            viewModel.Modele5s = db.Facturation;
            viewModel.Modele7s = db.Societes;
            viewModel.Modele8s = db.Direction;
            viewModel.Modele9s = db.Inscription;
            return View(viewModel);
        }

    }
}