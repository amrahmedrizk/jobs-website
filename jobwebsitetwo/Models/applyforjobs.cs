using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace jobwebsitetwo.Models
{
    public class applyforjobs
    {
        public int id { get; set; }
        public string massage { get; set; }
        public DateTime applaydate { get; set; }
        public virtual jobs jobs { get; set; }
        public int jobsid { get; set; }
        public virtual ApplicationUser user { get; set; }
        public string userid { get; set; }
    }
}