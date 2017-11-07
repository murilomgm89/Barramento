using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class ComponentType
    {
        public static IEnumerable<Entity.ComponentType> GetAllComponentTypes()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.ComponentTypes                           
                            select o;
                return query.ToList();
            }
        }
        public static Entity.ComponentType GetComponentType(string nameComponent)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.ComponentTypes
                            where
                                o.identifier == nameComponent 
                            select o;
                return query.FirstOrDefault();
            }
        }

        public static Entity.ComponentType GetIdentifierParent(string nameComponent, int idGroup, int idVersion)
        {            
            using (var context = new Entity.OiWeb())
            {
                return context.ComponentTypes
                    .Where(cw => cw.identifier == nameComponent).FirstOrDefault();                
            }
        }
    }
}
