using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class GroupCustomDataPage
    {
        public static List<Entity.GroupCustomDataPage> GetGroupCustomDataPage(int idCity)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _G in context.GroupCustomDataPages                            
                            where _G.idCity == idCity
                            select _G;
                query = query.Include(g => g.Page);
                return query.ToList();
            }
        }

        public static void RemoveByIdGroupAndPage(int idGroup, int idPage)
        {
            using (var context = new Entity.OiWeb())
            {
                var datas = context.GroupCustomDataPages.Where(w => w.idGroup == idGroup && w.idPage == idPage).ToList();

                context.GroupCustomDataPages.RemoveRange(datas);
                context.SaveChanges();
            }
        }

        public static void Insert(List<Entity.GroupCustomDataPage> datas)
        {
            using (var context = new Entity.OiWeb())
            {
                context.GroupCustomDataPages.AddRange(datas);
                context.SaveChanges();
            }
        }

        public static void Insert(Entity.GroupCustomDataPage data)
        {
            using (var context = new Entity.OiWeb())
            {
                context.GroupCustomDataPages.Add(data);
                context.SaveChanges();
            }
        }
    }
}
