using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Eisk.DataAccess;
using Eisk.Helpers;
using Eisk.Models;

namespace Eisk.Controllers
{
    [RoutePrefix("Account")]
    public class SecurityDemoController : Controller
    {
        [Authorize]
        [Route("")]
        public ViewResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("MemberHome")]
        public ViewResult Member()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [Route("AdminHome")]
        public ViewResult Admin()
        {
            return View();
        }

        public ViewResult LogOn()
        {
            if (Request.QueryString["ReturnUrl"] != null)
                this.ShowMessage("You need to log-on first with appropriate role to access the page you requested.",
                    MessageType.Info, false);
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel loginViewModel)
        {
            var user = UserDataAccess.Validate(loginViewModel.Email, loginViewModel.Password);

            if (user != null) //if the log-in is successful
            {
                FormsAuthentication.RedirectFromLoginPage(user.UserName, loginViewModel.RememberMe);

                if (Request.QueryString["ReturnUrl"] != null)
                    Response.Redirect(System.Web.HttpContext.Current.Request.QueryString["ReturnUrl"]);
                else
                {
                    if (user.UserRole == UserRole.Administrator.ToString())
                        return RedirectToAction("Admin");
                    return RedirectToAction("Member");
                }
            }
            else
                this.ShowMessage("Invalid user name or password", MessageType.Danger, false);

            return View();
        }

        public ViewResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User = null;
            this.ShowMessage("You have been signed out successfully", MessageType.Info, false);
            return View("Index");
        }

        public JsonResult GetAllUsers()
        {
            Thread.Sleep(2000);
            return new JsonResult
            {
                Data = UserDataAccess.GetAll(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}