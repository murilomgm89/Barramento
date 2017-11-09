using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class PlanRegulation
    {
        public static IEnumerable<Entity.PlanRegulation> GetPlanRegulations()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.PlanRegulations
                            .Include(e => e.PriceGroup)                            
                            orderby c.type
                            select c;
                return query.ToList();
            }
        }

        public static IEnumerable<Entity.PlanRegulation> GetPlanRegulations(int idPriceGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.PlanRegulations
                            .Include(e=> e.PriceGroup)
                            where c.idPriceGroup == idPriceGroup
                            orderby c.type
                            select c;
                return query.ToList();
            }
        }

        public static void Insert(Entity.PlanRegulation data)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PlanRegulations.Add(data);
                context.SaveChanges();
            }
        }

        public static void Remove(int idPlanRegulation)
        {
            using (var context = new Entity.OiWeb())
            {
                var data = context.PlanRegulations.Find(idPlanRegulation);
                if (data != null)
                {
                    context.PlanRegulations.Remove(data);
                    context.SaveChanges();
                }
            }
        }

        public static void Update(Entity.PlanRegulation data)
        {
            using (var context = new Entity.OiWeb())
            {

                var planregulation = context.PlanRegulations.Find(data.idPlanRegulation);

                if (planregulation != null)
                {
                    context.PlanRegulations.Attach(planregulation);
                    var entry = context.Entry(planregulation);
                    entry.Property(e => e.name).IsModified = true;
                    entry.Property(e => e.link).IsModified = true;
                    entry.Property(e => e.type).IsModified = true;

                    context.SaveChanges();
                }
               
                
            }
        }


        public static Entity.PlanRegulation GetById(int idPlanRegulation)
        {
            using (var context = new Entity.OiWeb())
            {
              return  context.PlanRegulations.Find(idPlanRegulation);
              
            }
        }
    }
}
