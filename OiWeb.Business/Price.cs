using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class Price
    {
        public static ICollection<Entity.Price> GetPrices(int idPlan, int idPriceGroup)
        {
            using (var context = new OiWebDB())
            {
                var query = from _P in context.Prices
                            where _P.idPlan == idPlan && _P.idPriceGroup == idPriceGroup
                            select _P;
                query = query.Include(g => g.PlanProduct);                
                return query.ToList();
            }
        }
        public static void Update(Entity.Price price)
        {
            using (var context = new OiWebDB())
            {
                var query = (from c in context.Prices
                             where c.idPlan == price.idPlan
                             && c.idPriceGroup == price.idPriceGroup
                             && c.idPriceLoyalty == price.idPriceLoyalty
                             select c).FirstOrDefault();

                if (query == null)
                {
                    context.Prices.Add(price);
                }
                else
                {
                    query.value = price.value;
                    query.valueCombo = price.valueCombo;                    
                }
                context.SaveChanges();
            }
        }
        public static void Delete(int idPlan, int idPriceGroup)
        {
            using (var context = new OiWebDB())
            {
                var deletePrices =
                    from prices in context.Prices
                    where prices.idPlan == idPlan
                        && prices.idPriceGroup == idPriceGroup
                    select prices;

                foreach (var price in deletePrices)
                {
                    context.Prices.Remove(price);
                }

                context.SaveChanges();

                var deletePlanGroup =
                    from planGroup in context.PlanGroups
                    where planGroup.idPlan == idPlan
                        && planGroup.idPriceGroup == idPriceGroup
                    select planGroup;

                foreach (var planGroups in deletePlanGroup)
                {
                    context.PlanGroups.Remove(planGroups);
                }

                context.SaveChanges();
            }
        }
    }
}
