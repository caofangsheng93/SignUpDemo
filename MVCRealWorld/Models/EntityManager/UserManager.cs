using MVCRealWorld.Models.DB;
using MVCRealWorld.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRealWorld.Models.EntityManager
{
    public class UserManager
    {
        /// <summary>
        /// 添加User
        /// </summary>
        /// <param name="model"></param>
        public void AddUserAccount(UserSignUpView model)
        {
            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                UserTable userTable = new UserTable()
                {
               UserName=model.UserName,
               UserPassword=model.UserPassword,
               CreatedUserID=model.UserID>0?model.UserID:1,
               ModifiedUserID=model.UserID>0?model.UserID:1,
               CreateDateTime=DateTime.Now,
               ModifiedDateTime=DateTime.Now
                
                };

                
                db.UserTable.Add(userTable);
                db.SaveChanges();

                UserProfileTable userProfileTable = new UserProfileTable() 
                {

                UserID = userTable.UserID,  //这里要使用userTable.UserID
                FirstName=model.FirstName,
                LastName=model.LastName,
                Gender=model.Gender,
                CreatedUserID=model.UserID>0?model.UserID:1,
                ModifiedUserID=model.UserID>0?model.UserID:1,
                CreateDateTime=DateTime.Now,
                ModifiedDateTime=DateTime.Now

                };

                db.UserProfileTable.Add(userProfileTable);
                db.SaveChanges();

                if (model.RoleID > 0)
                {
                    //用户系统角色表。
                    UserSystemRoleTable userSystemRoleTable=new UserSystemRoleTable()
                    {
                    RoleID=model.RoleID,
                    UserID=model.UserID,
                    IsActive=true,
                    CreatedUserID=model.UserID>0?model.UserID:1,
                    ModifiedUserID=model.UserID>0?model.UserID:1,
                    CreateDateTime=DateTime.Now,
                    ModifiedDateTime=DateTime.Now
                    };

                    db.UserSystemRoleTable.Add(userSystemRoleTable);
                    db.SaveChanges();
                }


                
            }
        }


        /// <summary>
        /// 判断登陆名是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool IsLoginNameExist(string loginName)
        {
            using (RoleBasedManageDBEntities db=new RoleBasedManageDBEntities())
            {
                return db.UserTable.Where(s => s.UserName.Equals(loginName)).Any();
            }
        }

        /// <summary>
        /// 获取用户的密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string GetUserPassword(string loginName)
        {

            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                var user = db.UserTable.Where(s => s.UserName.ToLower().Equals(loginName));

                if (user.Any())
                {
                    return user.FirstOrDefault().UserPassword;
                }
                else
                {
                    return string.Empty;
                }
            
            }
        }
    }
}