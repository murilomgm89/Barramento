using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.BarramentoWebAPI.Models
{
    public class CatalogViewModel
    {
        public Catalog catalog { get; set; }       
        public Plan plan { get; set; }

        public CatalogViewModel()
        {
            this.catalog = new Catalog();
            this.plan = new Plan();           
        }

        public class Catalog
        {
            public Product Product { get; set; }
            public List<Plan> Plans { get; set; }
        }

        public class Product
        {
            public int idProduct { get; set; }
            public string name { get; set; }
            public string productFamily { get; set; }
            List<Plan> Plans { get; set; }
        }

        public class Plan
        {
            public int SKU { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }            
        }
      
    }    
}