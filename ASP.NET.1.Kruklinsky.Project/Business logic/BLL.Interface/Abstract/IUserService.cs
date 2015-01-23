using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserService
    {
        #region User

        User GetUser(string id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();

        User CreateUser(string email, string password, bool isApproved);
        bool DeleteUser(string email);
        void UpdateUser(User user);

        bool ValidateUser(string email, string password, IEqualityComparer<string> passwordComparer);
        bool ChangePassword(string email, string oldPassword, string newPassword, IEqualityComparer<string> passwordComparer);

        #endregion

        #region Role

        string[] GetRolesForUser(string email);
        string[] GetUsersInRole(string roleName);
        bool IsUserInRole(string email, string roleName);

        void AddUserToRole(string email, string roleName);
        void RemoveUserFromRole(string email, string roleName);

        string[] GetAllRoles();
        bool RoleExists(string roleName);

        #endregion

        #region Profile

        Profile GetUserProfile(string id);
        void UpdateUserProfile(string id, Profile profile);

        #endregion
    }
}
