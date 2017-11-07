

namespace OiWeb.Business
{
    public class CMS_AccountLoginLog
    {
        public static void Create(Entity.CMS_AccountLoginLog accountLoginLog)
        {            
            using (var context = new Entity.OiWeb())
            {               
                context.CMS_AccountLoginLog.Add(accountLoginLog);
                context.SaveChanges();
            }
        }
    }
}
