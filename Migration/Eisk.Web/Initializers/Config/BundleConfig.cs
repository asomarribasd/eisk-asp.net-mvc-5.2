using System.Web.Optimization;

namespace Eisk
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/eisk").Include(
                "~/Scripts/eisk.js"));

            bundles.Add(new ScriptBundle("~/bundles/eisk-editor").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/eisk-calender.js",
                "~/Scripts/eisk-required-if-validation.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/eisk.css",
                "~/Content/themes/default/bootstrap/bootstrap.css",
                "~/Content/themes/default/bootstrap/bootstrap-theme-3.3.4.min.css",
                "~/Content/themes/default/mvc-default.css"));

            bundles.Add(new StyleBundle("~/Content/css/jquery-ui").Include(
                "~/Content/themes/default/jquery-ui/theme.css",
                "~/Content/themes/default/jquery-ui/jquery-ui.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}