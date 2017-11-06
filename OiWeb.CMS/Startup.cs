using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OiWeb.CMS.Startup))]
namespace OiWeb.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
