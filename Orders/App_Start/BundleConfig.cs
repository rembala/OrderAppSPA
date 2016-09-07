using System.Web;
using System.Web.Optimization;

namespace Orders
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                        "~/Scripts/vendors/jquery-2.1.0.min.js",
                        "~/Scripts/vendors/bootstrap.min.js",
                        "~/Scripts/vendors/toastr.min.js",
                        "~/Scripts/vendors/angular.min.js",
                        "~/Scripts/vendors/angular-route.min.js",
                        "~/Scripts/vendors/angular-cookies.min.js",
                        "~/Scripts/vendors/angular-validator.min.js",
                        "~/Scripts/vendors/angular-base64.min.js",
                        "~/Scripts/vendors/angucomplete-alt.min.js",
                       "~/Scripts/vendors/ui-bootstrap-tpls-0.13.1.js",
                       "~/Scripts/vendors/jquery.fancybox.js",
                       "~/Scripts/vendors/loading-bar.min.js",
                       "~/Scripts/vendors/jquery-ui.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                        "~/Scripts/spa/Modules/common.core.js",
                        "~/Scripts/spa/Modules/common.ui.js",
                        "~/Scripts/spa/app.js",
                        "~/Scripts/spa/Home/rootCtrl.js",
                        "~/Scripts/spa/Home/indexCtrl.js",
                        "~/Scripts/spa/Services/apiService.js",
                        "~/Scripts/spa/Services/notificationService.js",
                        "~/Scripts/spa/Layout/topBar.directive.js",
                        "~/Scripts/spa/Layout/sideBar.directive.js",
                        "~/Scripts/spa/Order/orderCtrl.js",
                        "~/Scripts/spa/Order/orderProductModalCtrl.js",
                        "~/Scripts/spa/Layout/customPager.directive.js",
                        "~/Scripts/spa/Order/createOrderCtrl.js",
                       "~/Scripts/spa/Directives/datepickerjQueryUi.js",
                       "~/Scripts/spa/Product/productCtrl.js",
                       "~/Scripts/spa/Countries/countriesCtrl.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/loading-bar.min.css",
                      "~/Content/css/Site.css",
                      "~/Content/css/toastr.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/jquery-ui.min.css",
                      "~/Content/css/jquery-ui.theme.css"
                     ));
        }
    }
}