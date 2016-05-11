using MVCRealWorld.Models.EntityManager;
using MVCRealWorld.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRealWorld.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpView model)
        {
            if(ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                if (!UM.IsLoginNameExist(model.UserName))
                {
                    //添加
                    UM.AddUserAccount(model);
                    //创建身份验证票据
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "登陆名已经存在");
                }
            }

            return View();
        }
    }
}