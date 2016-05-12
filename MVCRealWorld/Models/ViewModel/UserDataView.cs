using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.ViewModel
{
    public class UserDataView
    {
        public IEnumerable<UserProfileView> UserProfile { get; set; }

        public UserRoles UserRoles { get; set; }

        public UserGender UserGender { get; set; }

    }
}