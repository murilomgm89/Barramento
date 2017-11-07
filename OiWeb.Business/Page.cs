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
            using (var context = new OiWebDB())
            {
                var query = from c in context.Pages                            
                            select c;
                return query.ToList();
            }
        }
        public static Entity.Page GetPage(int idPage)
        {
            using (var context = new OiWebDB())
            {
                var query = from c in context.Pages
                            where c.idPage == idPage                          
                            select c;
                return query.FirstOrDefault();
            }
        }
        public static void Create(Entity.Page page)
        {
            using (var context = new OiWebDB())
            {
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
        public static void Update(Entity.Page page)
        {
            using (var context = new OiWebDB())
            {
                var query = (from c in context.Pages
                             where c.idPage == page.idPage
                             select c).First();

                query.name = page.name;
                query.description = page.description;
                query.isCommon = page.isCommon;                    
                context.SaveChanges();
            }
        }         
    }
}
