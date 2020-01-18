using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class Parametrage
    {
        public int ParametrageId { get; set; }
        public String RefTech { get; set; }
        public int CompteurTech { get; set; }
        public String RefCom { get; set; }
        public int CompteurCom { get; set; }
    }
}