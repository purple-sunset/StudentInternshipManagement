using System.Web;
using System.Web.Optimization;

namespace StudentInternshipManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                "~/Content/font-awesome.min.css",
                "~/Content/jquery-ui.custom.min.css",
                "~/Content/fonts.googleapis.com.css",
                "~/Content/site.css",
                "~/Content/site2.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/2018.2.516/kendo.common.min.css",
                "~/Content/kendo/2018.2.516/kendo.mobile.all.min.css",
                "~/Content/kendo/2018.2.516/kendo.default.min.css"));

            bundles.Add(new ScriptBundle("~/Script/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Script/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Script/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Script/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Script/kendo").Include(
                "~/Scripts/kendo/2018.2.516/jszip.min.js",
                "~/Scripts/kendo/2018.2.516/kendo.all.min.js",
                "~/Scripts/kendo/2018.2.516/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/Script/site").Include(
                "~/Scripts/site-elements.js",
                "~/Scripts/site.js"));
        }
    }
}
