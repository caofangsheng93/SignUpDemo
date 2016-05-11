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


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginView model,string returnUrl)
        {
            /*
             登陆逻辑，根据用户输入的用户名和密码，找出数据库中，对应用户的密码。
             1.如果密码为空，则判断是用户名或者密码错误。
             2.如果密码不为空，
              * 2.1  再接着判断用户输入的密码和数据库中查到的密码是否相等。相等的话， 这个时候就设置Cookie，登陆的票据等，然后跳转到登陆之后的页面。
              * 2.2   不相等的话，就判断密码错误。
             */
            if (ModelState.IsValid)
            {
                UserManager UM = new UserManager();
                //获取密码
                string password = UM.GetUserPassword(model.UserName);
                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "用户名或密码错误");

                }
                else
                {
                    if (model.UserPassword.Equals(password))
                    {

                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Welcome", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "密码错误");
                    }
                }
            }
            //如果登陆失败的话，就停留在Login页面上
            return View(model);
        }


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult SignOut()
        {
            //从浏览器删除身份验证票证
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}