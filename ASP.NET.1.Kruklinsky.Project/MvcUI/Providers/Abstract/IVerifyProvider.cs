using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Providers.Abstract
{
    public interface IVerifyProvider
    {
        bool IsEmail(string email);
        bool IsVerifying(string email);
        bool IsApproved(string email);
        void SendVerifyEmail(string email);
        bool Verify(string email, string secretCode);
    }
}
