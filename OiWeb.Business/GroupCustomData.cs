using System;
using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class GroupCustomData
    {
        public static IEnumerable<Entity.GroupCustomData> GetGroupCustomDatas()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from _G in context.GroupCustomDatas 
                            select _G;                
                return query.ToList();
            }
        }
        public static Entity.GroupCustomData GetGroupCustomData(int idGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from c in context.GroupCustomDatas
                            where c.idGroup == idGroup
                            select c;                
                query = query.Include(a => a.GroupCustomDataPages);
                query = query.Include(a => a.GroupCustomDataPages.Select(b => b.City));
                query = query.Include(a => a.GroupCustomDataPages.Select(b => b.Page));
                return query.FirstOrDefault();
            }
        }
        public static void Create(Entity.GroupCustomData groupCustomData)
        {
            using (var context = new Entity.OiWeb())
            {
                groupCustomData.isActive = true;
                groupCustomData.dtCreate = DateTime.Now;
                context.GroupCustomDatas.Add(groupCustomData);
                context.SaveChanges();
            }
        }
        public static void Update(Entity.GroupCustomData groupCustomData)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = (from c in context.GroupCustomDatas
                             where c.idGroup == groupCustomData.idGroup
                             select c).First();

                query.name = groupCustomData.name;
                query.description = groupCustomData.description;
                     
                context.SaveChanges();
            }
        }      
        public static void ExcludeCustomData(int idGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                var dataGroupCustomDataPages = context.GroupCustomDataPages.Where(p => p.idGroup == idGroup).ToList();
                if (dataGroupCustomDataPages != null)
                {
                    context.GroupCustomDataPages.RemoveRange(dataGroupCustomDataPages);
                    context.SaveChanges();
                }

                var dataGroupCustomData = context.GroupCustomDatas.Find(idGroup);
                if (dataGroupCustomData != null)
                {
                    context.GroupCustomDatas.Remove(dataGroupCustomData);
                    context.SaveChanges();
                }
            }
        }    
    }
}
