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
    
    public partial class PlanGroup
    {
        public int idPlanGroup { get; set; }
        public int idPlan { get; set; }
        public int idPriceGroup { get; set; }
    
        public virtual PlanProduct PlanProduct { get; set; }
    }
}
