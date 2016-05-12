using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.ViewModel
{
    public class UserGender
    {
        public string SelectedGender { get; set; }

        public IEnumerable<Gender> Gender { get; set; }
    }
}