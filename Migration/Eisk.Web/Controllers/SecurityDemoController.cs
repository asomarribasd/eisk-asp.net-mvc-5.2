/****************** Copyright Notice *****************
 
This code is licensed under Microsoft Public License (Ms-PL). 
You are free to use, modify and distribute any portion of this code. 
The only requirement to do that, you need to keep the developer name, as provided below to recognize and encourage original work:

=======================================================
   
Architecture Designed and Implemented By:
Mohammad Ashraful Alam
Microsoft Most Valuable Professional, ASP.NET 2007 – 2013
Twitter: http://twitter.com/AshrafulAlam | Blog: http://weblogs.asp.net/ashraful | Github: https://github.com/ashrafalam
   
*******************************************************/
namespace Eisk.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Helpers;
    using Models;
    using DataAccess;


    public class SecurityDemoController : Controller
    {
        [Authorize]
        public ViewResult Index()
        {
            return View();
        }

        [Authorize]
        public ViewResult Member()
        {
           return View();
        }

        [Authorize(Roles = "Administrator")]
        public ViewResult Admin()
        {
            return View();
        }

        public ViewResult LogOn()
        {
            if (Request.QueryString["ReturnUrl"] != null)
                this.ShowMessage("You need to log-on first with appropriate role to access the page you requested.", MessageType.Info, false);
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel loginViewModel)
        {
            UserInfo user = UserDataAccess.Validate(loginViewModel.Email, loginViewModel.Password);

            if (user != null)//if the log-in is successful
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
            System.Threading.Thread.Sleep(2000);
            return new JsonResult
            {
                Data = UserDataAccess.GetAll(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
