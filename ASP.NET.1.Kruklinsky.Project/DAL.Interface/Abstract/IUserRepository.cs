using DAL.Interface.Entities;
using System.Collections.Generic;


namespace DAL.Interface.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string id);

        User GetUser(string id, out Profile profile, out IEnumerable<Role> roles);

        Profile GetUserProfile(string id);

        IEnumerable<Role> GetUserRoles(string id);

        void UpdateUserProfile(string id, Profile profile);

        void AddUserRole(string id, string roleName);

        void DeleteUserRole(string id, string roleName);
    }
}