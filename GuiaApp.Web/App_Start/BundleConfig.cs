using System.Web;
using System.Web.Optimization;

namespace GuiaApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-1.11.0.js",
                         "~/Scripts/jquery.validate*",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/respond.js",
                         "~/Scripts/jquery.dataTables.js",
                         "~/Scripts/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/demo_table.css",
                      "~/Content/demo_table_jui.css",
                      "~/Content/jquery-ui-1.10.4.custom.css"));
        }
    }
}
