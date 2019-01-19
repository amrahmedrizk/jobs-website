using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace jobwebsitetwo.Models
{
    public class jobs
    {
        public int id { get; set; }
        [Display(Name ="job name")]
        [Required]
        public string jobtitle { get; set; }
        [Display(Name =  "job content")]
        [AllowHtml]
        public string jobcontent { get; set; }
        [Display(Name = "job image")]
        public string jobimage { get; set; }
        [Display(Name = "job category")]
        public int categoryid { get; set; }
        public virtual category category { get; set; }
        public string userid { get; set; }
        public ApplicationUser user { get; set; }

    }
}