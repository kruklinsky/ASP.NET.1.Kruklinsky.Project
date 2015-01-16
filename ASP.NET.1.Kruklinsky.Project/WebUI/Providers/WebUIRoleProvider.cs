using BLL.Interface.Abstract;
using System;
using System.Linq;
using System.Web.Security;

namespace Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class WebUIRoleProvider : RoleProvider
    {
        IUserService userService;

        public WebUIRoleProvider(IUserService userService)
        {
            if (userService == null)
            {
                throw new System.ArgumentNullException("userService", "User service is null.");
            }
            this.userService = userService;
        }

        public override string ApplicationName { get; set; }


        public override string[] GetRolesForUser(string email)
        {
            throw new System.NotImplementedException();
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            throw new System.NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #region Not suported

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}