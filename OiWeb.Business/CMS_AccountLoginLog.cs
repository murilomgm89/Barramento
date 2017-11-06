using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace OiWeb.Business
{
    public class CMS_AccountLoginLog
    {
        public static void Create(Entity.CMS_AccountLoginLog accountLoginLog)
        {            
            using (var context = new OiWebDB())
            {               
                context.CMS_AccountLoginLog.Add(accountLoginLog);
                context.SaveChanges();
            }
        }
    }
}
