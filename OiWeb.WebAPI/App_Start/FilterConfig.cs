using OiWeb.WebAPI.Filters;
using System.Web;
using System.Web.Mvc;

namespace OiWeb.WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MvcErrorLogAttribute());
        }
    }
}