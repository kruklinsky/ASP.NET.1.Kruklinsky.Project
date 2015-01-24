using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class UserExceptionsHelper
    {
        public static void GetEmailExceptions(string email, Regex emailValidationRegex)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new System.ArgumentException("Email is null, empty or consists only of white-space characters.", "email");
            }
            if (!IsEmail(email, emailValidationRegex))
            {
                string message = string.Format("Email: {0} does not satisfy the expression: \"{1}\".", email, emailValidationRegex.ToString());
                throw new System.ArgumentException(message, "email");
            }
        }
        private static bool IsEmail(string email, Regex emailValidationRegex)
        {
            bool result = true;
            if (emailValidationRegex != null)
            {
                result = emailValidationRegex.IsMatch(email);
            }
            return result;
        }

        public static void GetIdExceptions(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new System.ArgumentException("User id is null, empty or consists only of white-space characters.", "id");
            }
            if (!IsGuid(id))
            {
                throw new System.ArgumentException("Cannot convert user id to guid.", "id");
            }
        }
        private static bool IsGuid(string id)
        {
            Guid temp;
            return Guid.TryParse(id, out temp);
        }

        public static void GetPasswordExceptions(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("Password is null, empty or consists only of white-space characters.", "password");
            }
        }
        public static void GetPasswordExceptions(string password, string paramName)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("Password is null, empty or consists only of white-space characters.", paramName);
            }
        }
    }
}
