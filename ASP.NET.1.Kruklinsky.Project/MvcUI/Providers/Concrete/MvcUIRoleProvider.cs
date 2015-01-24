using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace MvcUI.Providers
{
    public class MvcUIRoleProvider : RoleProvider
    {
        private IUserQueryService userQueryService;
        private IRoleQueryService roleQueryService;
        private IUserRolesQueryService userRolesQueryService;
        private IUserRolesManagementService userRolesManagementService;

        public MvcUIRoleProvider ()
            : this(
            (IUserQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserQueryService)),
            (IRoleQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleQueryService)),
            (IUserRolesQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRolesQueryService)),
            (IUserRolesManagementService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRolesManagementService))
            ){ }

        public MvcUIRoleProvider(IUserQueryService userQueryService, IRoleQueryService roleQueryService, IUserRolesQueryService userRolesQueryService, IUserRolesManagementService userRolesManagementService)
        {
            if(userQueryService == null)
            {
                throw new System.ArgumentNullException("userQueryService", "User query service is null.");
            }
            if (roleQueryService == null)
            {
                throw new System.ArgumentNullException("roleQueryService", "Role query service is null.");
            }
            if (userRolesQueryService == null)
            {
                throw new System.ArgumentNullException("userRolesQueryService", "User roles query service is null.");
            }
            if (userRolesManagementService == null)
            {
                throw new System.ArgumentNullException("userRolesManagementService", "User roles management service is null.");
            }
            this.userQueryService = userQueryService;
            this.roleQueryService = roleQueryService;
            this.userRolesQueryService = userRolesQueryService;
            this.userRolesManagementService = userRolesManagementService;
        }

        #region Overridden

        public override string ApplicationName { get; set; }


        public override string[] GetRolesForUser(string email)
        {
            List<string> result = new List<string>();
            var user = this.userQueryService.GetUserByEmail(email);
            if (user != null)
            {
                result = user.Roles.Select(r => r.Name).ToList();
            }
            return result.ToArray();
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            return this.userRolesQueryService.IsUserInRole(email, roleName);
        }
        public override string[] GetUsersInRole(string roleName)
        {
            return this.userRolesQueryService.GetUsersInRole(roleName);
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
                    this.userRolesManagementService.AddUserToRole(email, roleName);
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
                    this.userRolesManagementService.RemoveUserFromRole(email, roleName);
                }
            }
        }

        public override string[] GetAllRoles()
        {
            return this.roleQueryService.GetAllRoles();
        }
        public override bool RoleExists(string roleName)
        {
            return this.roleQueryService.RoleExists(roleName);
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