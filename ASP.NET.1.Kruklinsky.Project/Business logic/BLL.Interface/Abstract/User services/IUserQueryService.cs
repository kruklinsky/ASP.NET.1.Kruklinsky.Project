using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserQueryService
    {
        User GetUser(string id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
    }
}
