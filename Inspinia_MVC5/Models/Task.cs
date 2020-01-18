using System;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5.Models
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public int SortOrder { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public int owner_id { get; set; }
        public int ProjetTechniquesID { get; set; }
        public DateTime planned_start { get; set; }
        public DateTime planned_end { get; set; }
        public String color { get; set; }
        public int Duration_h { get; set; }
        public int duration_planning { get; set; }
        public int duration_h_planning { get; set; }
        public DateTime EndDate { get; set; }
    }
}