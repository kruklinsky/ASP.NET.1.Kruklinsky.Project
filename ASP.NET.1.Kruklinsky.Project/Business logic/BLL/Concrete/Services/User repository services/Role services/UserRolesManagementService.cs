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
    public class UserRolesManagementService : EmailService, IUserRolesManagementService
    {
        public UserRolesManagementService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }
        public UserRolesManagementService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression) : base(userRepository, dbContextScopeFactory, emailRegularExpression) { }

        #region IUserRolesManagementService

        public void AddUserToRole(string email, string roleName)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.AddUserRole(email, roleName);
                context.SaveChanges();
            }
        }
        public void RemoveUserFromRole(string email, string roleName)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.DeleteUserRole(email, roleName);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
