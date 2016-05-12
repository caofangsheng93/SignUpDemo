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

        /// <summary>
        /// 根据用户名，判断角色名
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool isUserInRole(string loginName, string roleName)
        {

            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {

              UserTable loginUser = db.UserTable.Where(s => s.UserName.ToLower().Equals(loginName)).FirstOrDefault();
              if (loginUser != null)
              {
                  //linq to sql好好学习一下。
                  var roles = from q in db.UserSystemRoleTable
                              join r in db.RoleTable on q.RoleID equals r.RoleID
                              where r.RoleName.Equals(roleName) && q.UserID.Equals(loginUser.UserID)
                              select r.RoleName;

                  if (roles != null)
                  {
                      return roles.Any();
                  }
              }
             
              return false;
              
             

            }
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public List<RoleView> GetAllRoles()
        {
            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                var roles = db.RoleTable.Select(s => new RoleView {
                RoleID=s.RoleID,
                RoleName=s.RoleName,
                RoleDescription=s.RoleDescription
                
                }).ToList();

                return roles;
            }
        }

        /// <summary>
        /// 根据用户名获取用户ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int GetUserID(string loginName)
        {
            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                var user = db.UserTable.Where(s => s.UserName.Equals(loginName));

                if (user.Any())
                {
                    return user.FirstOrDefault().UserID;
                }
                else
                {
                    return 0;
                }
            
            }
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        public List<UserProfileView> GetAllUserProfiles()
        {
            List<UserProfileView> userProfilesVIew = new List<UserProfileView>();
            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                UserProfileView UPV;
                //获取所有的用户
                var users = db.UserTable.ToList();

                foreach (var item in users)
                {
                    UPV = new UserProfileView();
                    UPV.UserID = item.UserID;
                    UPV.UserName = item.UserName;
                    UPV.UserPassword = item.UserPassword;

                    //根据用户ID查用户详情
                    var userProfile = db.UserProfileTable.Find(item.UserID);

                    if (userProfile != null)
                    {
                        UPV.FirstName = userProfile.FirstName;
                        UPV.LastName = userProfile.LastName;
                        UPV.Gender = userProfile.Gender;
                    }

                    //根据用户ID查角色
                    var roles = db.UserSystemRoleTable.Where(s => s.UserID.Equals(item.UserID));

                    if (roles.Any())
                    {
                        var userRole = roles.FirstOrDefault();
                        UPV.RoleID = userRole.RoleID;
                        UPV.RoleName = userRole.RoleTable.RoleName;
                        UPV.IsRoleActive = userRole.IsActive;
                    }

                    userProfilesVIew.Add(UPV);
                }

                return userProfilesVIew;

            }
        
        }


        public UserDataView GetUserDataView(string loginName)
        {
            UserDataView userDataView = new UserDataView();
            //获取用户详情
            List<UserProfileView> userProfiles = GetAllUserProfiles();

            //获取所有的角色
            List<RoleView> roleView= GetAllRoles();

            int? userAssignedRoleID = 0;
            int userID = 0;
            string userGender = string.Empty;

            userID= GetUserID(loginName);

            using (RoleBasedManageDBEntities db = new RoleBasedManageDBEntities())
            {
                //获取用户的角色ID
                userAssignedRoleID = db.UserSystemRoleTable.Where(s => s.UserID.Equals(userID)).FirstOrDefault().RoleID;
                //获取用户的性别
                userGender = db.UserProfileTable.Where(s => s.UserID.Equals(userID)).FirstOrDefault().Gender;
            }

            List<Gender> genders = new List<Gender>() 
            {
            
            new Gender(){Text="Male",Value="M"},
            new Gender(){Text="Female",Value="F"}
            };

            //用户详情
            userDataView.UserProfile = userProfiles;
            //用户角色
            userDataView.UserRoles = new UserRoles() 
            {
            SelectedRoleID=userAssignedRoleID,
            UserRoleList = roleView
            
            };
            //用户性别
            userDataView.UserGender = new UserGender() 
            { 
            
            SelectedGender=userGender,
            Gender=genders
            
            };
            return userDataView;

        }

    }
}