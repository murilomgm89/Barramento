using System.Collections.Generic;

namespace OiWeb.CMS.Models
{
    public class CitiesViewModel
    {
        public IEnumerable<Entity.City> cities { get; set; }
        public Entity.PriceGroup group { get; set; }
        public IEnumerable<Entity.Page> pages { get; set; }
        public Entity.GroupCustomData groupCustomData { get; set; }
        public IEnumerable<Entity.PlanProduct> plans { get; set; }    
    }
}