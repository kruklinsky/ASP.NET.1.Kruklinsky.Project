using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserRolesManagementService
    {
        void AddUserToRole(string email, string roleName);
        void RemoveUserFromRole(string email, string roleName);
    }
}
