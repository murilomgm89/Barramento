using System.Collections.Generic;

namespace OiWeb.Business
{
    public class GroupModalCities
    {
        public static void Insert(List<Entity.GroupModalCity> datas)
        {
            using (var context = new Entity.OiWeb())
            {
                context.GroupModalCities.AddRange(datas);
                context.SaveChanges();
            }
        }
    }
}