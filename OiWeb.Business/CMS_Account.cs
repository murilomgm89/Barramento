using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace OiWeb.Business
{
    public class CMS_Account
    {
        public static Entity.CMS_Account IsValid(Entity.CMS_Account account)
        {
            //Convert String to 64
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(account.password);
            account.password = System.Convert.ToBase64String(plainTextBytes);
            //Convert 64 to String
            //var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.CMS_Account
                            where o.email == account.email && o.password == account.password
                            select o;
                return query.FirstOrDefault();
            }
        }

        public static Entity.CMS_Account IsValidEmail(Entity.CMS_Account account)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.CMS_Account
                            where o.email == account.email
                            select o;
                return query.FirstOrDefault();
            }
        }

        public static IEnumerable<Entity.CMS_Account> GetUsers()
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.CMS_Account                            
                            select o;                             
                return query.ToList();
            }
        }

        public static Entity.CMS_Account GetUser(int idAccount)
        {
            using (var context = new Entity.OiWeb())
            {
                var query = from o in context.CMS_Account
                            where o.idAccount == idAccount
                            select o;                
                return query.First();
            }
        }

        public static void UpdateUser(Entity.CMS_Account account)
        {
            if (account.password != null)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(account.password);
                account.password = System.Convert.ToBase64String(plainTextBytes);
            }           
            using (var context = new Entity.OiWeb())
            {
                var query = (from c in context.CMS_Account
                             where c.idAccount == account.idAccount
                             select c).First();


                query.name = account.name;
                if (account.password == "" || account.password == null)
                {
                    account.password = query.password;
                }                
                query.password = account.password;
                query.email = account.email;
                query.isActive = account.isActive;                
                context.SaveChanges();
            }
        }

        public static void SaveUser(Entity.CMS_Account account)
        {
            using (var context = new Entity.OiWeb())
            {
                account.dtCreate = DateTime.Now;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(account.password);
                account.password = System.Convert.ToBase64String(plainTextBytes);
                context.CMS_Account.Add(account);
                context.SaveChanges();
            }
        }
    }
}
