﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RoleBasedManageDBEntities : DbContext
    {
        public RoleBasedManageDBEntities()
            : base("name=RoleBasedManageDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RoleTable> RoleTable { get; set; }
        public virtual DbSet<UserProfileTable> UserProfileTable { get; set; }
        public virtual DbSet<UserSystemRoleTable> UserSystemRoleTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
    }
}
