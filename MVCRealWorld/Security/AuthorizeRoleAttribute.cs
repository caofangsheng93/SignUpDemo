using MVCRealWorld.Models.DB;
using MVCRealWorld.Models.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRealWorld.Security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute  //using System.Web.Mvc;
    {
        private readonly string[] userAssignedRoles;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                UserManager UM = new UserManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.isUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }  
    }
}