using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jobwebsitetwo.Models
{
    public class category
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="job category")]
        public string categoryname { get; set; }
        [Required]
        [Display(Name = "category description")]
        public string categorydescription { get; set; }
        public virtual ICollection<jobs> jobs { get; set; }
    }
}