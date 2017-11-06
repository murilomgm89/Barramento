using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class PriceGroupCities
    {
        public static List<Entity.PriceGroupCity> GetPriceGroupCity(int idCity)
        {
            using (var context = new OiWebDB())
            {
                var query = from _G in context.PriceGroupCities                            
                            where _G.idCity == idCity
                            select _G;
                query = query.Include(g => g.Product);
                return query.ToList();
            }
        }
    }
}
