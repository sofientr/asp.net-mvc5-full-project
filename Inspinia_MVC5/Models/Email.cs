using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class Email
    {
        public int EmailId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int SenderId { get; set; }
    }
}