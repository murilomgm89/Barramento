namespace OiWeb.Business
{
    public class GroupModalPage
    {
        public static void Insert(Entity.GroupModalPage data)
        {
            using (var context = new Entity.OiWeb())
            {
                context.GroupModalPages.Add(data);
                context.SaveChanges();
            }
        }
    }
}