using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class PlanRegulation
    {
        public static IEnumerable<Entity.PlanRegulation> GetPlanRegulations(int idPriceGroup)
        {
            using (var context = new OiWebDB())
            {
                var query = from c in context.PlanRegulations
                            where c.idPriceGroup == idPriceGroup
                            orderby c.type
                            select c;
                return query.ToList();
            }
        }       
    }
}
