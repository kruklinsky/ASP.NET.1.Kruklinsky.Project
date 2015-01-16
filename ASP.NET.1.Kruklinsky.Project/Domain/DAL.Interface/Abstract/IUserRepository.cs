using DAL.Interface.Entities;
using System.Collections.Generic;


namespace DAL.Interface.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string email);
        User GetUserById(string id);

        Profile GetUserProfile(string id);
        void UpdateUserProfile(string id, Profile profile);

        IEnumerable<Role> GetUserRoles(string email);
        void AddUserRole(string email, string roleName);
        void DeleteUserRole(string email, string roleName);
        IEnumerable<User> GetUsersInRole(string roleName);

        IEnumerable<Role> GetAllRoles();
        bool RoleExists(string roleName);
    }
}