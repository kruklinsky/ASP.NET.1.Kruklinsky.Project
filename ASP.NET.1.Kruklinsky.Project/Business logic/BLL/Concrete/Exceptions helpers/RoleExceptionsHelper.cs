using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class RoleExceptionsHelper
    {
        public static void GetNameExceptions(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new System.ArgumentException("Role name is null, empty or consists only of white-space characters.", "roleName");
            }
        }
    }
}
