using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class Groups
    {
        public static List<Entity.PriceGroup> GetPriceGroups()
        {
            using (var context = new Entity.OiWeb())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var query = from _PG in context.PriceGroups
                            where _PG.isActive == true
                            select _PG;
                //query = query.Include(p => p.Product);   
                query = query.Include(a => a.Product);
                return query.ToList();
            }
        }
        public static Entity.PriceGroup GetPriceGroup(int idPriceGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var query = from _PG in context.PriceGroups
                            where _PG.idPriceGroup == idPriceGroup
                            select _PG;                  
                query = query.Include(a => a.Product);
                query = query.Include(a => a.Product.PlanProducts);
                query = query.Include(a => a.Product.PlanProducts.Select(b => b.Prices));
                return query.FirstOrDefault();
            }
        }
        public static void Desvincular(int idProduct, int idPriceGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var dataPrices = context.Prices.Where(p => p.idPriceGroup == idProduct).ToList();
                if (dataPrices != null)
                {
                    context.Prices.RemoveRange(dataPrices);
                    context.SaveChanges();
                }
                var dataPriceGroupCities = context.PriceGroupCities.Where(p => p.idProduct == idProduct && p.idPriceGroup == idPriceGroup).ToList();
                if (dataPriceGroupCities != null)
                {
                    context.PriceGroupCities.RemoveRange(dataPriceGroupCities);
                    context.SaveChanges();
                }
            }
        }
        public static void Save(Entity.PriceGroup group)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PriceGroups.Add(group);
                context.SaveChanges();
            }
        }
    }
}
