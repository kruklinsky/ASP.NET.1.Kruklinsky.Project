using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string id);

        Profile GetUserProfile(string id);

        IEnumerable<Role> GetUserRoles(string id);

        void UpdateUserProfile(string id, Profile profile);

        void AddUserRole(string id, string roleName);

        void DeleteUserRole(string id, string roleName);
    }
}