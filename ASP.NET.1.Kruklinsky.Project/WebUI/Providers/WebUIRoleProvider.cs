using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace MvcUI.Providers
{
    public class WebUIRoleProvider : RoleProvider
    {
        IUserService userService;

        public WebUIRoleProvider() : this((IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService))) { }

        public WebUIRoleProvider(IUserService userService)
        {
            if (userService == null)
            {
                throw new System.ArgumentNullException("userService", "User service is null.");
            }
            this.userService = userService;
        }

        #region Overridden

        public override string ApplicationName { get; set; }


        public override string[] GetRolesForUser(string email)
        {
            List<string> result = new List<string>();
            var user = this.userService.GetUser(email);
            if (user != null)
            {
                result = user.Roles.Value.Select(r => r.Name).ToList();
            }
            return result.ToArray();
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            return userService.IsUserInRole(email,roleName);
        }
        public override string[] GetUsersInRole(string roleName)
        {
            return userService.GetUsersInRole(roleName);
        }
        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            if (userNames == null)
            {
                throw new System.ArgumentNullException("userNames", "User names is null.");
            }
            if (roleNames == null)
            {
                throw new System.ArgumentNullException("roleNames", "Role names is null.");
            }
            foreach (var email in userNames)
            {
                foreach (var roleName in roleNames)
                {
                    this.userService.AddUserToRole(email, roleName);
                }
            }
        }
        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            if (userNames == null)
            {
                throw new System.ArgumentNullException("userNames", "User names is null.");
            }
            if (roleNames == null)
            {
                throw new System.ArgumentNullException("roleNames", "Role names is null.");
            }
            foreach (var email in userNames)
            {
                foreach (var roleName in roleNames)
                {
                    this.userService.RemoveUserFromRole(email, roleName);
                }
            }
        }

        public override string[] GetAllRoles()
        {
            return this.userService.GetAllRoles();
        }
        public override bool RoleExists(string roleName)
        {
            return this.userService.RoleExists(roleName);
        }

        #endregion

        #region Not supported

        public override void CreateRole(string roleName)
        {
            throw new System.NotSupportedException("Role creation is not supported.");
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotSupportedException("Role deleting is not supported.");
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotSupportedException("Finding users in role is not supported.");
        }

        #endregion
    }
}