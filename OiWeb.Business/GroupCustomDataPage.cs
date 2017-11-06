using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class GroupCustomDataPage
    {
        public static List<Entity.GroupCustomDataPage> GetGroupCustomDataPage(int idCity)
        {
            using (var context = new OiWebDB())
            {
                var query = from _G in context.GroupCustomDataPages                            
                            where _G.idCity == idCity
                            select _G;
                query = query.Include(g => g.Page);
                return query.ToList();
            }
        }
    }
}
