using System.Web;
using System.Web.Optimization;

namespace Password_Manager_WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-3.3.1.js",
                        "~/Scripts/popper-1.14.3.js",
                        "~/Scripts/bootstrap-4.1.3.js",
                        "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap-4.1.3.css",
                      "~/Content/font-awesome-5.3.1.css",
                      "~/Content/custom.css"));
        }
    }
}
