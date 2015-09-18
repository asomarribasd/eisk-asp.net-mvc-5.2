using System.Web.Optimization;

namespace Eisk
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/eisk").Include(
                "~/Scripts/eisk.js",
                "~/Scripts/eisk-menu.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/default/css/site.css",
                "~/Content/themes/default/css/eisk.css",
                "~/Content/themes/default/css/bootstrap.css",
                "~/Content/themes/default/css/bootstrap-theme-3.3.4.min.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
