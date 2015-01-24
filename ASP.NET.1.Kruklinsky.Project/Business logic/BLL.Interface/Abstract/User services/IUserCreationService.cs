using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserCreationService
    {
        User CreateUser(string email, string password, bool isApproved);
        bool DeleteUser(string email);
        void UpdateUser(User user);
    }
}
