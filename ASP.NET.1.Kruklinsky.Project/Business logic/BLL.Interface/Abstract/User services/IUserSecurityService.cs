using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserSecurityService
    {
        bool ValidateUser(string email, string password, IEqualityComparer<string> passwordComparer);
        bool ChangePassword(string email, string oldPassword, string newPassword, IEqualityComparer<string> passwordComparer);
    }
}
