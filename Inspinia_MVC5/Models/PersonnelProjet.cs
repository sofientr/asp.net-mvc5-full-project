using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;

    public partial class PersonnelProjet
    {
        public int Id { get; set; }
        public bool Vue { get; set; }

        public virtual ProjetTechnique ProjetTechnique { get; set; }
        public virtual Personnel Personnel { get; set; }
    }
}