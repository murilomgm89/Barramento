using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class PlanProduct
    {
        public static Entity.PlanProduct GetPlan(int idPlan)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.PlanProducts
                            where c.idPlan == idPlan                          
                            select c;
                return query.FirstOrDefault();
            }
        }
        public static void Create(Entity.PlanProduct planProduct)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PlanProducts.Add(planProduct);
                context.SaveChanges();
            }
        }
        public static void Update(Entity.PlanProduct planProduct)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = (from c in context.PlanProducts
                            where c.idPlan == planProduct.idPlan
                             select c).First();

                query.isVisible = planProduct.isVisible;
                query.linkEcommerce = planProduct.linkEcommerce;
                query.name = planProduct.name;
                query.defaultSKU = planProduct.defaultSKU;
                query.description = planProduct.description;               
                context.SaveChanges();
            }
        }

        public static void CreatePlanGroup(Entity.PlanGroup planGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                context.PlanGroups.Add(planGroup);
                context.SaveChanges();                
            }
        }
         
    }
}
