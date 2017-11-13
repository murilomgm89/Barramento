using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class City
    {
        public static IEnumerable<Entity.City> GetCities()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.Cities                     
                            select c;
                return query.ToList();
            }
        }
        public static Entity.City GetCity(int idCity)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _C in context.Cities
                            join _CG in context.PriceGroupCities on _C.idCity equals _CG.idCity
                            where _C.idCity == idCity
                            select _C;
                query = query.Include(cds => cds.PriceGroupCities);
                query = query.Include(cds => cds.PriceGroupCities.Select(p => p.PriceGroup));
                query = query.Include(cds => cds.PriceGroupCities.Select(p => p.Product));
                query = query.Include(cds => cds.PriceGroupCities.Select(p => p.Product.PlanProducts));
                query = query.Include(cds => cds.PriceGroupCities.Select(p => p.Product.PlanProducts.Select(pp => pp.Prices)));
                query = query.Include(cds => cds.GroupCustomDataPages);
                query = query.Include(cds => cds.GroupCustomDataPages.Select(p => p.GroupCustomData));
                query = query.Include(cds => cds.GroupCustomDataPages.Select(p => p.Page));
                return query.FirstOrDefault();
            }
        }
        public static IEnumerable<Entity.City> GetCities(int idPriceGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _C in context.Cities
                            join _CG in context.PriceGroupCities on _C.idCity equals _CG.idCity
                            where
                                _CG.idPriceGroup == idPriceGroup
                            select _C;
                return query.ToList();
            }
        }

        public static IEnumerable<Entity.City> GetCitiesCustomData(int idGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _C in context.Cities
                            join _CG in context.GroupCustomDataPages on _C.idCity equals _CG.idCity
                            where
                                _CG.idGroup == idGroup
                            select _C;
                query = query.Include(cds => cds.GroupCustomDataPages);
                query = query.Include(cds => cds.GroupCustomDataPages.Select(gp => gp.Page));
                return query.ToList();
            }
        }

        public static List<Entity.City> GetCititesByDdd(int ddd)
        {
            using (var context = new Entity.OiWeb())
            {
                return context.Cities.Where(w => w.DDD == ddd).ToList();
            }
        }
    }
}
