using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class UserRolesQueryService : EmailService, IUserRolesQueryService
    {
        public UserRolesQueryService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }
        public UserRolesQueryService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression) : base(userRepository, dbContextScopeFactory, emailRegularExpression) { }

        #region IUserRolesQueryService

        public string[] GetRolesForUser(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            List<string> result = new List<string>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.repository.GetUserByEmail(email);
                if (user != null)
                {
                    result = user.Roles.Value.Select(r => r.Name).ToList();
                }
            }
            return result.ToArray();
        }
        public string[] GetUsersInRole(string roleName)
        {
            RoleExceptionsHelper.GetNameExceptions(roleName);
            List<string> result = new List<string>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var usersInRole = this.repository.GetUsersInRole(roleName);
                if (usersInRole.Count() != 0)
                {
                    result = usersInRole.Select(u => u.Email).ToList();
                }
            }
            return result.ToArray();
        }
        public bool IsUserInRole(string email, string roleName)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            RoleExceptionsHelper.GetNameExceptions(roleName);
            bool result = false;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.repository.GetUserByEmail(email);
                if (user != null)
                {
                    result = user.Roles.Value.Where(r => r.Name == roleName).Count() > 0;
                }
            }
            return result;
        }

        #endregion
    }
}
