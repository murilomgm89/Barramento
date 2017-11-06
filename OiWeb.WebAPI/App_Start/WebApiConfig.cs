using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OiWeb.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {           
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "WebAPI_CustomData", action = "GetJson", id = RouteParameter.Optional }
            );
            
            config.EnableCors();
        }
    }
}