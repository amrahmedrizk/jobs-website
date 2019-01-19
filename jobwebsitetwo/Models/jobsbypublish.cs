using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobwebsitetwo.Models
{
    public class jobsbypublish
    {
        public int id { get; set; }
        public string hh { get; set; }
        public IEnumerable<applyforjobs> items { get; set; }
    }
}