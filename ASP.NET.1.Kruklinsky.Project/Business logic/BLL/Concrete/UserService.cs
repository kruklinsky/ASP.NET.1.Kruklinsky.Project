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
    public class UserService : IUserService
    {
        private Regex emailValidationRegex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
        private IUserRepository userRepository;
        private IDbContextScopeFactory dbContextScopeFactory;

        public UserService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory)
        {
            if (userRepository == null)
            {
                throw new System.ArgumentNullException("userRepository", "User repository is null.");
            }
            if(dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.userRepository = userRepository;
            this.dbContextScopeFactory = dbContextScopeFactory;
        }
        public UserService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression)
        {
            if (userRepository == null)
            {
                throw new System.ArgumentNullException("userRepository", "User repository is null.");
            }
            if (dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.emailValidationRegex = emailRegularExpression == null ? null : new Regex(emailRegularExpression);
            this.userRepository = userRepository;
        }

        #region IUserService

        #region User

        #region Query

        public User GetUser(string id)
        {
            UserExceptionsHelper.GetIdExceptions(id);
            User result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.userRepository.GetUser(id);
                if (user != null)
                {
                    result = user.ToBll();
                }
            }
            return result;
        }
        public User GetUserByEmail(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            User result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    result = user.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result = new List<User>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var users = this.userRepository.Data;
                if (users.Count() != 0)
                {
                    result = users.Select(u => u.ToBll());
                }
            }
            return result;
        }

        #endregion

        #region Creation

        public User CreateUser(string email, string password, bool isApproved)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            UserExceptionsHelper.GetPasswordExceptions(password);
            this.CreateUser(email, password, isApproved, DateTime.Now);
            return this.GetUserByEmail(email);
        }
        public bool DeleteUser(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            User user = this.GetUserByEmail(email);
            if (user != null)
            {
                using (var context = dbContextScopeFactory.Create())
                {
                    this.userRepository.Delete(user.ToDal());
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }
        public void UpdateUser(User user)
        {
            UserExceptionsHelper.GetIdExceptions(user.Id);
            using (var context = dbContextScopeFactory.Create())
            {
                var dalUser = this.userRepository.GetUser(user.Id);
                if (dalUser != null)
                {
                    dalUser.IsApproved = user.IsApproved;
                    this.userRepository.Update(dalUser);
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
                this.userRepository.Add(result.ToDal(password));
                this.userRepository.AddUserRole(email, roleName);
                context.SaveChanges();
            }
        }

        #endregion

        #region UserPasswordSecurity

        public bool ValidateUser(string email, string password, IEqualityComparer<string> passwordComparer)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            UserExceptionsHelper.GetPasswordExceptions(password);
            bool result = false;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    result = passwordComparer.Equals(user.Password, password);
                }
            }
            return result;
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword, IEqualityComparer<string> passwordComparer)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            UserExceptionsHelper.GetPasswordExceptions(oldPassword, "oldPassword");
            UserExceptionsHelper.GetPasswordExceptions(newPassword, "newPassword");
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var user = this.userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    if (passwordComparer.Equals(user.Password, oldPassword))
                    {
                        user.Password = newPassword;
                        this.userRepository.Update(user);
                        result = true;
                    }
                }
                context.SaveChanges();
            }
            return result;
        }

        #endregion

        #endregion

        #region Role

        #region Query

        public string[] GetAllRoles()
        {
            List<string> result = new List<string>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var roles = this.userRepository.GetAllRoles();
                if (roles.Count() != 0)
                {
                    result = roles.Select(r => r.Name).ToList();
                }
            }
            return result.ToArray();
        }
        public bool RoleExists(string roleName)
        {
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                return this.userRepository.RoleExists(roleName);
            }
        }

        #endregion

        #region UserRolesQueryService

        public string[] GetRolesForUser(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            List<string> result = null;
            User user = this.GetUserByEmail(email);
            if (user != null)
            {
                result = user.Roles.Select(r => r.Name).ToList();
            }
            return result == null ? null : result.ToArray();
        }
        public string[] GetUsersInRole(string roleName)
        {
            RoleExceptionsHelper.GetNameExceptions(roleName);
            List<string> result = new List<string>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var usersInRole = this.userRepository.GetUsersInRole(roleName);
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
            User user = this.GetUserByEmail(email);
            if (user != null)
            {
                result = user.Roles.Where(r => r.Name == roleName).Count() > 0;
            }
            return result;
        }

        #endregion

        #region UserRolesManagementService

        public void AddUserToRole(string email, string roleName)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.Create())
            {
                this.userRepository.AddUserRole(email, roleName);
                context.SaveChanges();
            }
        }
        public void RemoveUserFromRole(string email, string roleName)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.Create())
            {
                this.userRepository.DeleteUserRole(email, roleName);
                context.SaveChanges();
            }
        }

        #endregion

        #endregion

        #region Profile

        public Profile GetUserProfile(string id)
        {
            UserExceptionsHelper.GetIdExceptions(id);
            Profile result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var profile = this.userRepository.GetUserProfile(id);
                if (profile != null)
                {
                    result = profile.ToBll();
                }
            }
            return result;
        }
        public void UpdateUserProfile(string id, Profile profile)
        {
            UserExceptionsHelper.GetIdExceptions(id);
            if (profile == null)
            {
                throw new System.ArgumentNullException("profile", "Profile is null.");
            }
            using (var context = dbContextScopeFactory.Create())
            {
                this.userRepository.UpdateUserProfile(id, profile.ToDal());
                context.SaveChanges();
            }
        }

        #endregion

        #endregion
    }
}
