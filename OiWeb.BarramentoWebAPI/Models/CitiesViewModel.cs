using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OiWeb.BarramentoWebAPI.Models
{
    public class CitiesViewModel
    {
        public List<CitiesListClass> CitiesList { get; set; }
        public CitiesListClass Cities { get; set; }

        public CitiesViewModel()
        {
            this.CitiesList = new List<CitiesListClass>();
            this.Cities = new CitiesListClass();   
        }

        public class CitiesListClass
        {
            public int id { get; set; }
            public int idGroup { get; set; }
            public int idPriceGroupTV { get; set; }
            public int idPriceGroupFixo { get; set; } 
            public int idPriceGroupBandaLarga { get; set; }
            public int idPriceGroupCombo { get; set; }   
            public int idPriceGroupCelularPos { get; set; }
            public int idPriceGroupCelularPre { get; set; }   
            public int idPriceGroupCelularControle { get; set; }
        }
      
    }    
}