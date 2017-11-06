using System.Web;
using System.Web.Optimization;

namespace OiWeb.CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/bootstrapJS").Include(
                      "~/Content/bootstrap/js/bootstrap.js",
                      "~/Content/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Content/bootstrapCSS").Include(
                "~/Content/bootstrap/css/bootstrap.min.css.map",
                "~/Content/bootstrap/css/bootstrap-theme.css",
                "~/Content/bootstrap/css/bootstrap-theme.min.css",
                "~/Content/bootstrap/css/bootstrap-theme.min.css.map",
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/bootstrap/css/bootstrap.css.map",
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap/css/ bootstrap.min.css.map",
                "~/Content/bootstrap/css/bootstrap.js",
                "~/Content/bootstrap/css/bootstrap.min.js"));           

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
