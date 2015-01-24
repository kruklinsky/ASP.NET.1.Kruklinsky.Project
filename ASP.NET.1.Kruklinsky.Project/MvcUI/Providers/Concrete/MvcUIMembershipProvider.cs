//Select Assemblies - > Extensions -> System.Web.Helpers
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using System.Collections.Specialized;
using System.Web.Hosting;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;
using BLL.Interface.Abstract;
using MvcUI.Providers.Entities;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MvcUI.Providers
{

    public class MvcUIMembershipProvider : MembershipProvider
    {
        private string providerDescription = "";

        private string emailRegularExpression = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
        private bool enablePasswordReset = true;
        private int minRequiredPasswordLength = 6;
        private int minRequiredNonalphanumericCharacters = 0;
        private string passwordStrengthRegularExpression = string.Empty;

        private IUserQueryService userQueryService;
        private IUserCreationService userCreationService;
        private IUserSecurityService userSecurityService;

        public MvcUIMembershipProvider() : this(
            (IUserQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserQueryService)),
            (IUserCreationService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserCreationService)),
            (IUserSecurityService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserSecurityService))
            ) { }

        public MvcUIMembershipProvider(IUserQueryService userQueryService,IUserCreationService userCreationService, IUserSecurityService userSecurityService)
        {
            if(userQueryService == null)
            {
                throw new System.ArgumentNullException("userQueryService", "User query service is null.");
            }
            if (userCreationService == null)
            {
                throw new System.ArgumentNullException("userCreationService", "User creation service is null.");
            }
            if (userSecurityService == null)
            {
                throw new System.ArgumentNullException("userSecurityService", "User security service is null.");
            }
            this.userQueryService = userQueryService;
            this.userCreationService = userCreationService;
            this.userSecurityService = userSecurityService;
        }

        #region Added

        public bool IsDuplicateEmail(string email)
        {
            bool result = false;
            if (IsValidEmail(email))
            {
                var user = this.userQueryService.GetUserByEmail(email);
                result = user != null;
            }
            return result;
        }

        #endregion

        #region Overridden

        #region Filds

        public override string ApplicationName { get; set; }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }
        public override int PasswordAttemptWindow
        {
            get { return -1; }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { return -1; }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override bool EnablePasswordReset
        {
            get { return this.enablePasswordReset; }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this.minRequiredNonalphanumericCharacters; }
        }
        public override int MinRequiredPasswordLength
        {
            get { return this.minRequiredPasswordLength; }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { return this.passwordStrengthRegularExpression; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "DefaultMembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", this.providerDescription);
            }
            base.Initialize(name, config);
            if (!string.IsNullOrEmpty(config["applicationName"]))
            {
                this.ApplicationName = config["applicationName"];
            }
            else
            {
                this.ApplicationName = GetDefaultAppName();
            }
            config.Remove("connectionStringName");
            if (config["enablePasswordReset"] != null)
            {
                this.enablePasswordReset = Convert.ToBoolean(config["enablePasswordReset"], CultureInfo.InvariantCulture);
            }
            if (config["minRequiredNonalphanumericCharacters"] != null)
            {
                this.minRequiredNonalphanumericCharacters = Convert.ToInt32(config["minRequiredNonalphanumericCharacters"], CultureInfo.InvariantCulture);
            }
            if (config["minRequiredPasswordLength"] != null)
            {
                this.minRequiredPasswordLength = Convert.ToInt32(config["minRequiredPasswordLength"], CultureInfo.InvariantCulture);
            }
            if (config["passwordStrengthRegularExpression"] != null)
            {
                this.passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            }
            if (config["emailRegularExpression"] != null)
            {
                this.emailRegularExpression = config["emailRegularExpression"];
            }
        }

        #endregion

        #region Methods

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
            {
                throw new System.ArgumentNullException("providerUserKey", "User key is null.");
            }
            User result = null;
            var user = this.userQueryService.GetUser(providerUserKey.ToString());
            if (user != null)
            {
                result = user.ToWeb();
            }
            return result;
        }
        public override string GetUserNameByEmail(string email)
        {
            return email;
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            User result = null;
            var user = this.userQueryService.GetUserByEmail(username);
            if (user != null)
            {
                result = user.ToWeb();
            }
            return result;
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection result = new MembershipUserCollection();
            var allUsers = this.userQueryService.GetAllUsers();
            if (allUsers.Count() != 0)
            {
                foreach (var item in allUsers.Select(u => u.ToWeb()).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize))
                {
                    result.Add(item);
                }
            }
            totalRecords = allUsers.Count();
            return result;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (!IsValidPassword(password))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (!IsValidEmail(email))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }
            if (IsDuplicateEmail(email))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            var result = this.userCreationService.CreateUser(email, Crypto.HashPassword(password), isApproved);
            status = MembershipCreateStatus.Success;
            return result.ToWeb();
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return this.userCreationService.DeleteUser(username);
        }
        public override void UpdateUser(MembershipUser user)
        {
            this.userCreationService.UpdateUser(user.ToBll());
        }

        public override bool ValidateUser(string username, string password)
        {
            return this.userSecurityService.ValidateUser(username, password, new PasswordComparer(Crypto.VerifyHashedPassword));
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return this.userSecurityService.ChangePassword(username, oldPassword, newPassword, new PasswordComparer(Crypto.VerifyHashedPassword));
        }

        #endregion

        #endregion

        #region Not supported

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotSupportedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new System.NotSupportedException("Password retrieval is not supported.");
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotSupportedException("Password generation is not supported.");
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotSupportedException("Question and answer are not supported.");
        }
        public override bool UnlockUser(string userName)
        {
            throw new System.NotSupportedException("Locking of users is not supported.");
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotSupportedException("Not unique emails are not supported.");
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotSupportedException("Username are equal to email. Not unique emails are not supported.");
        }

        #endregion

        #region Private methods

        private static string GetDefaultAppName()
        {
            try
            {
                string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
                if (string.IsNullOrEmpty(applicationVirtualPath))
                {
                    applicationVirtualPath = Process.GetCurrentProcess().MainModule.ModuleName;
                    int index = applicationVirtualPath.IndexOf('.');
                    if (index != -1)
                    {
                        applicationVirtualPath = applicationVirtualPath.Remove(index);
                    }
                }
                if (string.IsNullOrEmpty(applicationVirtualPath))
                {
                    return "/";
                }
                return applicationVirtualPath;
            }
            catch (Exception)
            {
                return "/";
            }
        }
        private static ConnectionStringSettings GetConnectionString(string connectionstringName)
        {
            if (string.IsNullOrEmpty(connectionstringName))
            {
                throw new System.ArgumentNullException("connectionstringName", "Connectionstring name is null.");
            }
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionstringName];
            if (settings == null)
            {
                throw new System.InvalidOperationException("Configuration manager returned null.");
            }
            return settings;
        }

        bool IsValidPassword (string password)
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(password))
            {
                result = false;
            } 
            if (password.Length < this.MinRequiredPasswordLength)
            {
                result = false;
            } 
            if (this.MinRequiredNonAlphanumericCharacters > 0)
            {
                int num = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (!char.IsLetterOrDigit(password[i]))
                    {
                        num++;
                    }
                }
                if (num < this.MinRequiredNonAlphanumericCharacters)
                {
                    result = false;
                }
            }
            if (!String.IsNullOrWhiteSpace(this.passwordStrengthRegularExpression))
            {
                Regex regex = new Regex(this.passwordStrengthRegularExpression);
                if (!regex.IsMatch(password))
                {
                    result = false;
                }
            }
            return result;
        }

        bool IsValidEmail(string email)
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(email))
            {
                result = false;
            }
            if (!IsEmail(email))
            {
                result = false;
            }
            return result;
        }
        private bool IsEmail(string email)
        {
            bool result = true;
            if (!String.IsNullOrWhiteSpace(this.emailRegularExpression))
            {
                Regex regex = new Regex(this.emailRegularExpression);
                result = regex.IsMatch(email);
            }
            return result;
        }

        #endregion

        private class PasswordComparer : IEqualityComparer<string>
        {
            private Func<string, string, bool> Compare;

            public PasswordComparer(Func<string, string, bool> comparefunction)
            {
                this.Compare = comparefunction;
            }

            public bool Equals(string x, string y)
            {
                return this.Compare(x, y);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
