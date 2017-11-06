using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.CMS.Models
{
    public class CitiesViewModel
    {
        public IEnumerable<Entity.City> cities { get; set; }
        public Entity.PriceGroup group { get; set; }
        public IEnumerable<Entity.PlanProduct> plans { get; set; }
        
    }
}