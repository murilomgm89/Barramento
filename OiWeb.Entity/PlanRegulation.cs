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
    
    public partial class PlanRegulation
    {
        public int idPlanRegulation { get; set; }
        public int idPriceGroup { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string type { get; set; }
    
        public virtual PriceGroup PriceGroup { get; set; }
    }
}