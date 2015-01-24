using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class SubjectExceptionsHelper
    {
        public static void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }
        public static void GetNameExceptions(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("Subject name is null, empty or consists only of white-space characters.", "name");
            }
        }
    }
}
