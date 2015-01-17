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

        public UserService(IUserRepository userRepository)
        {
            if(userRepository == null)
            {
                throw new System.ArgumentNullException("userRepository", "User repository is null.");
            }
            this.userRepository = userRepository;
        }
        public UserService(IUserRepository userRepository, string emailRegularExpression)
        {
            if (userRepository == null)
            {
                throw new System.ArgumentNullException("userRepository", "User repository is null.");
            }
            emailValidationRegex = emailRegularExpression == null ? null : new Regex(emailRegularExpression);
            this.userRepository = userRepository;
        }

        #region IUserService

        #region User

        public User GetUser(string email)
        {
            this.GetEmailExceptions(email);
            User result = null;
            var user = this.userRepository.GetUser(email);
            if(user != null)
            {
                result = user.ToBll();
            }
            return result;
        }
        public User GetUserById(string id)
        {
            this.GetIdExceptions(id);
            User result = null;
            var user = this.userRepository.GetUserById(id);
            if (user != null)
            {
                result = user.ToBll();
            }
            return result;
        }
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result = new List<User>();
            var users = this.userRepository.Data;
            if(users.Count() != 0)
            {
                result = users.Select(u => u.ToBll());
            }
            return result;
        }

        public User CreateUser(string email, string password, bool isApproved)
        {
            this.GetEmailExceptions(email);
            this.GetPasswordExceptions(password);
            this.CreateUser(email, password, isApproved, DateTime.Now);
            return this.GetUser(email);
        }
        public bool DeleteUser(string email)
        {
            this.GetEmailExceptions(email);
            User user = this.GetUser(email);
            if (user != null)
            {
                    this.userRepository.Delete(user.ToDal());
                    return true;
            }
            return false;
        }
        public void UpdateUser(User user)
        {
            this.GetIdExceptions(user.Id);
            var dalUser = this.userRepository.GetUserById(user.Id);
            if (dalUser != null)
            {
                dalUser.IsApproved = user.IsApproved;
                this.userRepository.Update(dalUser);
            }
        }

        public bool ValidateUser(string email, string password, IEqualityComparer<string> passwordComparer)
        {
            this.GetEmailExceptions(email);
            this.GetPasswordExceptions(password);
            bool result = false;
            var user = this.userRepository.GetUser(email);
            if(user != null)
            {
                result = passwordComparer.Equals(user.Password, password);
            }
            return result;
        }
        public bool ChangePassword(string email, string oldPassword, string newPassword,IEqualityComparer<string> passwordComparer)
        {
            this.GetEmailExceptions(email);
            this.GetPasswordExceptions(oldPassword,"oldPassword");
            this.GetPasswordExceptions(newPassword, "newPassword");
            bool result = false;
            var user = this.userRepository.GetUser(email);
            if (user != null)
            {
                if(passwordComparer.Equals(user.Password, oldPassword))
                {
                    user.Password = newPassword;
                    this.userRepository.Update(user);
                    result = true;
                }
            }
            return result;
        }

        #endregion

        #region Role

        public string[] GetRolesForUser(string email)
        {
            this.GetEmailExceptions(email);
            List<string> result = null;
            User user = this.GetUser(email);
            if(user != null)
            {
                result = user.Roles.Value.Select(r => r.Name).ToList();
            }
            return result == null ? null : result.ToArray();
        }
        public string[] GetUsersInRole(string roleName)
        {
            this.GetRoleNameExceptions(roleName);
            List<string> result = new List<string>();
            var usersInRole = this.userRepository.GetUsersInRole(roleName);
            if(usersInRole.Count() != 0)
            {
                result = usersInRole.Select(u => u.Email).ToList();
            }
            return result.ToArray();
        }
        public bool IsUserInRole(string email, string roleName)
        {
            this.GetEmailExceptions(email);
            this.GetRoleNameExceptions(roleName);
            bool result = false;
            User user = this.GetUser(email);
            if (user != null)
            {
                result = user.Roles.Value.Where(r => r.Name == roleName).Count() > 0;
            }
            return result;
        }

        public void AddUserToRole(string email, string roleName)
        {
            this.GetEmailExceptions(email);
            this.GetRoleNameExceptions(roleName);
            this.userRepository.AddUserRole(email, roleName);
        }
        public void RemoveUserFromRole(string email, string roleName)
        {
            this.GetEmailExceptions(email);
            this.GetRoleNameExceptions(roleName);
            this.userRepository.DeleteUserRole(email, roleName);
        }

        public string[] GetAllRoles()
        {
            List<string> result = new List<string>();
            var roles = this.userRepository.GetAllRoles();
            if (roles.Count() != 0)
            {
                result = roles.Select(r => r.Name).ToList();
            }
            return result.ToArray();
        }
        public bool RoleExists(string roleName)
        {
            this.GetRoleNameExceptions(roleName);
            return this.userRepository.RoleExists(roleName);
        }

        #endregion

        #region Profile

        public Profile GetUserProfile(string id)
        {
            this.GetIdExceptions(id);
            Profile result = null;
            var profile = this.userRepository.GetUserProfile(id);
            if(profile != null)
            {
                result = profile.ToBll();
            }
            return result;
        }
        public void UpdateUserProfile(string id, Profile profile)
        {
            this.GetIdExceptions(id);
            if (profile == null)
            {
                throw new System.ArgumentNullException("profile", "Profile is null.");
            }
            this.userRepository.UpdateUserProfile(id, profile.ToDal());
        }

        #endregion

        private void CreateUser(string email, string password, bool isApproved, DateTime createDate, string roleName = "user")
        {
            User result = new User
            {
                Email = email,
                IsApproved = isApproved,
                CreateDate = DateTime.Now
            };
            this.userRepository.Add(result.ToDal(password));
            this.userRepository.AddUserRole(email, roleName);
        }

        #endregion

        #region Private methods

        private void GetEmailExceptions(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new System.ArgumentException("Email is null, empty or consists only of white-space characters.", "email");
            }
            if (!this.IsEmail(email))
            {
                string message = string.Format("Email: {0} does not satisfy the expression: \"{1}\".", email, this.emailValidationRegex.ToString());
                throw new System.ArgumentException(message, "email");
            }
        }
        private bool IsEmail(string email)
        {
            bool result = true;
            if (this.emailValidationRegex != null)
            {
                result = this.emailValidationRegex.IsMatch(email);
            }
            return result;
        }

        private void GetIdExceptions(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new System.ArgumentException("User id is null, empty or consists only of white-space characters.", "id");
            }
            if (!IsGuid(id))
            {
                throw new System.ArgumentException("Cannot convert user id to guid.", "id");
            }
        }
        private bool IsGuid(string id)
        {
            Guid temp;
            return Guid.TryParse(id, out temp);
        }

        private void GetPasswordExceptions(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("Password is null, empty or consists only of white-space characters.", "password");
            }
        }
        private void GetPasswordExceptions(string password, string paramName)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("Password is null, empty or consists only of white-space characters.",paramName);
            }
        }

        private void GetRoleNameExceptions(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new System.ArgumentException("Role name is null, empty or consists only of white-space characters.", "roleName");
            }
        }

        #endregion
    }
}
