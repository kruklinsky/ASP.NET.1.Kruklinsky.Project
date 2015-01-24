using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserRolesQueryService
    {
        string[] GetRolesForUser(string email);
        string[] GetUsersInRole(string roleName);
        bool IsUserInRole(string email, string roleName);
    }
}
