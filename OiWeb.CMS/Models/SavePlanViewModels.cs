using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.CMS.Models
{
    public class SavePlanViewModels
    {
        public int SKU { get; set; }
        public int idProduct { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isVisible { get; set; }
        public bool defaultSKU { get; set; }       
    }
}