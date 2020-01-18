using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class LigneDirectionTable
    {
        // GET: LigneDirectionTable
        public int id;
        public int iddirection;
        public int idtable;
        public Boolean ajout;
        public Boolean suppression;
        public Boolean modification;
        public Boolean affichage;
    }
}