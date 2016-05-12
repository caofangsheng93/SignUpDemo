using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.ViewModel
{
    public class UserRoles
    {
        public int? SelectedRoleID { get; set; }

        public IEnumerable<RoleView> UserRoleList { get; set; }
    }
}