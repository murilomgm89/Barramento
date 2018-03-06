using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class Page
    {
        public static IEnumerable<Entity.Page> GetPages()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.Pages                            
                            select c;
                return query.ToList();
            }
        }
        public static Entity.Page GetPage(int idPage)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = context.Pages.Find(idPage);
                return query;
            }
        }
        public static Entity.Page GetGroupsPage(int idPage)
        {
            using (var context = new Entity.OiWeb())
            {                
                var query = from _PG in context.Pages
                            where _PG.idPage == idPage
                            select _PG;               
                query = query.Include(a => a.GroupCustomDataPages);                
                return query.FirstOrDefault();
            }
        }
        public static IEnumerable<Entity.Page> GetPagesInGroupCustomData(int idGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.Pages                            
                            join _GP in context.GroupCustomDataPages on c.idPage equals _GP.idPage
                            where _GP.idGroup == idGroup
                            group c by new {c.idPage} into myGroup
                            select myGroup.FirstOrDefault();                
                return query.ToList();
            }
        }
        public static void Create(Entity.Page page)
        {
            using (var context = new Entity.OiWeb())
            {
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
        public static void Update(Entity.Page page)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = (from c in context.Pages
                             where c.idPage == page.idPage
                             select c).First();

                query.name = page.name;
                query.description = page.description;
                query.isCommon = page.isCommon;
                query.isActive = page.isActive;    
                context.SaveChanges();
            }
        }

        public static void Delete(int idPage)
        {
            using (var context = new Entity.OiWeb())
            {

                var dataGroupCustomDataPages = context.GroupCustomDataPages.Where(p => p.idPage == idPage).ToList();
                if (dataGroupCustomDataPages != null)
                {
                    context.GroupCustomDataPages.RemoveRange(dataGroupCustomDataPages);
                    context.SaveChanges();
                }

                var dataGroupModalPages = context.GroupModalPages.Where(p => p.idPage == idPage).ToList();
                if (dataGroupModalPages != null)
                {
                    context.GroupModalPages.RemoveRange(dataGroupModalPages);
                    context.SaveChanges();
                }
                
                var dataPage = context.Pages.Find(idPage);
                if (dataPage != null)
                {
                    context.Pages.Remove(dataPage);
                    context.SaveChanges();
                }
            }
        }
    }
}
