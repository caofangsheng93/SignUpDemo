//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCRealWorld.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTable
    {
        public UserTable()
        {
            this.UserProfileTable = new HashSet<UserProfileTable>();
            this.UserSystemRoleTable = new HashSet<UserSystemRoleTable>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int CreatedUserID { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public int ModifiedUserID { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
    
        public virtual ICollection<UserProfileTable> UserProfileTable { get; set; }
        public virtual ICollection<UserSystemRoleTable> UserSystemRoleTable { get; set; }
    }
}
