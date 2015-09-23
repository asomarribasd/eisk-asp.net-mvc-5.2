using System;
using System.Web.Mvc;
using System.Web.WebPages;
using Eisk.Helpers;

namespace Eisk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }

        public ViewResult ExceptionDemo()
        {
            throw new InvalidOperationException("Invalid operation performed.");
        }

        public ActionResult ResetData()
        {
            try
            {
                TestDataHelper.InitializeSchemaAndData(
                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/TestData.xml"));
                this.ShowMessage("Test data generated successfully", MessageType.Success);
            }
            catch (Exception ex)
            {
                this.ShowMessage("Error on test data generation with the following details " + ex.Message,
                    MessageType.Danger);
            }

            return RedirectToAction("Index");
        }

        public RedirectResult SwitchView(bool mobile, string returnUrl)
        {
            if (Request.Browser.IsMobileDevice == mobile)
                HttpContext.ClearOverriddenBrowser();
            else
                HttpContext.SetOverriddenBrowser(mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);

            return Redirect(returnUrl);
        }
    }
}