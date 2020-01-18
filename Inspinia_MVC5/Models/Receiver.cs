using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class Receiver
    {
        [Key]
        [Column(Order = 0)]
        public int ReceiverId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int EmailId { get; set; }
    }
}