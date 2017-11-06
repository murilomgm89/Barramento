using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.CMS.Models
{
    public class SavePriceViewModels
    {
        public int idPlan { get; set; }
        public int idPrice { get; set; }
        public int idPriceGroup { get; set; }
        public int idTypeClient { get; set; }
        public int idPaymentMethod { get; set; }
        public int idPriceLoyalty { get; set; }        
        public decimal valueFid { get; set; }
        public decimal valueNoFid { get; set; }
        public decimal valueFidCombo { get; set; }
        public decimal valueNoFidCombo { get; set; }
    }
}