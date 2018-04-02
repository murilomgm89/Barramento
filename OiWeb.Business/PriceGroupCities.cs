using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class PriceGroupCities
    {
        public static List<Entity.PriceGroupCity> GetPriceGroupCity(int idCity, int? idProduct)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _G in context.PriceGroupCities                            
                            where _G.idCity == idCity
                            select _G;
                query = query.Include(g => g.Product);
                query = query.Where(g => g.Product.idProduct == idProduct);
                return query.ToList();
            }
        }

        public static void Insert(List<PriceGroupCity> datas)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PriceGroupCities.AddRange(datas);
                context.SaveChanges();
            }
        }

        public static void Insert(PriceGroupCity data)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PriceGroupCities.Add(data);
                context.SaveChanges();
            }
        }

        public static void RemoveByIdPriceGroup(int id)
        {
            using (var context = new Entity.OiWeb())
            {
                var datas = context.PriceGroupCities.Where(w => w.idPriceGroup == id).ToList();

                context.PriceGroupCities.RemoveRange(datas);
                context.SaveChanges();
            }
        }
    }

}
