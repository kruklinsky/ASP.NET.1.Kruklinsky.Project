using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Providers.Abstract
{
    public interface IAuthProvider
    {
        bool LogIn(string email, string password, bool rememberMe);
        bool SingUp(string email, string password, bool isApproved, out string error);
        void LogOut();
    }
}
