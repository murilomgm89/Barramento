//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OiWeb.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceGroup
    {
        public PriceGroup()
        {
            this.PlanRegulations = new HashSet<PlanRegulation>();
            this.PriceGroupCities = new HashSet<PriceGroupCity>();
        }
    
        public int idPriceGroup { get; set; }
        public int idProduct { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> dtCreate { get; set; }
        public bool isActive { get; set; }
    
        public virtual ICollection<PlanRegulation> PlanRegulations { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<PriceGroupCity> PriceGroupCities { get; set; }
    }
}
