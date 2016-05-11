using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.ViewModel
{
    public class UserLoginView
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// 用户名【登录名】
        /// </summary>
        [Required(ErrorMessage="*")]
        [Display(Name="LoginName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage="*")]
        public string UserPassword { get; set; }


    }
}