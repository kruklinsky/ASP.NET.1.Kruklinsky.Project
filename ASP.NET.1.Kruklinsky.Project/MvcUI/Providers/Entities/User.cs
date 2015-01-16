using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcUI.Providers.Entities
{
    public class User : MembershipUser
    {
        private readonly string providerName = "WebUIMembershipProvider";
        private readonly bool isLockedOut = false;

        #region Added

        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Lazy<IEnumerable<Role>> Roles { get; set; }
        public Lazy<Profile> Profile { get; set; }

        #endregion

        #region Overridden

        public override string UserName
        {
            get
            {
                return this.Email;
            }
        }
        public override DateTime CreationDate
        {
            get
            {
                return this.CreateDate.ToLocalTime();
            }
        }
        public override object ProviderUserKey
        {
            get
            {
                return this.Id;
            }
        }
        public override string Email { get; set; }
        public override bool IsApproved { get; set; }

        public override string ProviderName
        {
            get
            {
                return this.providerName;
            }
        }
        public override bool IsLockedOut
        {
            get
            {
                return this.isLockedOut;
            }
        }

        #endregion

        #region Not supported

        public override bool IsOnline
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }
        public override string Comment
        {
            get
            {
                throw new System.NotSupportedException();
            }
            set
            {
                throw new System.NotSupportedException();
            }
        }
        public override DateTime LastActivityDate
        {
            get
            {
                throw new System.NotSupportedException();
            }
            set
            {
                throw new System.NotSupportedException();
            }
        }
        public override DateTime LastLockoutDate
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }
        public override DateTime LastLoginDate
        {
            get
            {
                throw new System.NotSupportedException();
            }
            set
            {
                throw new System.NotSupportedException();
            }
        }
        public override DateTime LastPasswordChangedDate
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }
        public override string PasswordQuestion
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }

        #endregion
    }
}