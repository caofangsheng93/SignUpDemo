using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.ViewModel
{
    public class UserSignUpView
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name="LoginName")]
        [Required(ErrorMessage="*")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name="Password")]
        [Required(ErrorMessage="*")]
        public string UserPassword { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }  
    }
}