using MvcUI.Providers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcUI.Providers.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool LogIn(string email, string password, bool rememberMe)
        {
            if (Membership.ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, rememberMe);
                return true;
            }
            return false;
        }

        public bool SingUp(string email, string password, bool isApproved, out string error)
        {
            error = "";
            MembershipCreateStatus createStatus;
            Membership.CreateUser(email, password, email, password, password, isApproved, out createStatus);
            if (createStatus == MembershipCreateStatus.Success)
            {
                return true;
            }
            else
            {
                error = ErrorCodeToString(createStatus);
            }
            return false;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "E-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}