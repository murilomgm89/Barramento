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
    
    public partial class PriceGroupCity
    {
        public int idPriceGroupCities { get; set; }
        public int idCity { get; set; }
        public int idProduct { get; set; }
        public int idPriceGroup { get; set; }
    
        public virtual City City { get; set; }
        public virtual PriceGroup PriceGroup { get; set; }
        public virtual Product Product { get; set; }
    }
}
