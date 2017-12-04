namespace OiWeb.CMS.Models
{
    public class SavePlanViewModels
    {
        public int SKU { get; set; }
        public int idProduct { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string linkEcommerce { get; set; }
        
        public bool isVisible { get; set; }
        public bool defaultSKU { get; set; }       
    }
}