using MVCRealWorld.Models.EntityManager;
using MVCRealWorld.Models.ViewModel;
using MVCRealWorld.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRealWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        [AuthorizeRole("Admin")]   
        public ActionResult AdminOnly()
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        //[AuthorizeRole("Admin")]   //暂时注释
        public ActionResult ManageUserPartial()
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                UserManager UM = new UserManager();
                UserDataView UDV= UM.GetUserDataView(loginName);
                return PartialView(UDV); 
            }
            return View();
        }
    }
}