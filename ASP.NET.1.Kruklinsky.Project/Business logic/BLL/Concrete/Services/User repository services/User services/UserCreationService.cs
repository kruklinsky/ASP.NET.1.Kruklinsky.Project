using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class UserCreationService : EmailService, IUserCreationService
    {
        public UserCreationService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }
        public UserCreationService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression) : base(userRepository, dbContextScopeFactory, emailRegularExpression) { }

        #region IUserCreationService

        public User CreateUser(string email, string password, bool isApproved)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            UserExceptionsHelper.GetPasswordExceptions(password);
            this.CreateUser(email, password, isApproved, DateTime.Now);
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                return this.repository.GetUserByEmail(email).ToBll();
            }
        }
        public bool DeleteUser(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var user = this.repository.GetUserByEmail(email);
                if (user != null)
                {
                    this.repository.Delete(user);
                    result = true;
                }
                context.SaveChanges();
            }
            return result;
        }
        public void UpdateUser(User user)
        {
            UserExceptionsHelper.GetIdExceptions(user.Id);
            using (var context = dbContextScopeFactory.Create())
            {
                var dalUser = this.repository.GetUser(user.Id);
                if (dalUser != null)
                {
                    dalUser.IsApproved = user.IsApproved;
                    this.repository.Update(dalUser);
                }
                context.SaveChanges();
            }
        }

        private void CreateUser(string email, string password, bool isApproved, DateTime createDate, string roleName = "user")
        {
            User result = new User
            {
                Email = email,
                IsApproved = isApproved,
                CreateDate = DateTime.Now
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Add(result.ToDal(password));
                this.repository.AddUserRole(email, roleName);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
