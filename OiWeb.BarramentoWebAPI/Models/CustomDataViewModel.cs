using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.BarramentoWebAPI.Models
{
    public class CustomDataViewModel
    {
        public IdentifierParent identifierParent { get; set; }
        public Identifiers identifiers { get; set; }       
       
        public CustomDataViewModel()
        {
            this.identifierParent = new IdentifierParent();
            this.identifiers = new Identifiers();          
        }

        public class IdentifierParent
        {
            public string nameComponent { get; set; }  
            public List<Identifiers> identifiers { get; set; }            
        }
        public class Identifiers
        {
            public string nameComponent { get; set; }
            public string value { get; set; }
            public string valueTablet { get; set; }
            public string valueMobile { get; set; }   
        
        }
      
    }    
}