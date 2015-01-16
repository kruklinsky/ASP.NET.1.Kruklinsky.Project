using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using WebUI.Providers.Entities;

namespace WebUI.Providers
{
    public class WebUIProfileProvider : ProfileProvider
    {
        IUserService userService;

        public WebUIProfileProvider() : this((IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService))) { }

        public WebUIProfileProvider(IUserService userService)
        {
            if (userService == null)
            {
                throw new System.ArgumentNullException("userService", "User service is null.");
            }
            this.userService = userService;
        }

        #region Overridden

        public override string ApplicationName { get; set; }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context", "Settings context is null.");
            }
            if (collection == null)
            {
                throw new System.ArgumentNullException("collection", "Settings property collection is null.");
            }
            var result = new SettingsPropertyValueCollection();
            var username = (string)context["UserName"];
            if (!String.IsNullOrEmpty(username))
            {
                result = this.GetPropertyValues(username, context, collection);
            }
            return result;
        }
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context", "Settings context is null.");
            }
            if (collection == null)
            {
                throw new System.ArgumentNullException("collection", "Settings property collection is null.");
            }
            var username = (string)context["UserName"];
            if (!String.IsNullOrEmpty(username))
            {
                this.SetPropertyValues(username, context, collection);
            }
        }

        #endregion

        #region Not suported

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }
        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }
        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }
        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption,
            string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private SettingsPropertyValueCollection GetPropertyValues(string email, SettingsContext context, SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();
            var user = this.userService.GetUser(email);
            if (user != null)
            {
                if (user.Profile.Value != null)
                {
                    result = this.GetPropertyValues(user.Profile.Value.ToWeb(), context, collection);
                }
            }
            return result;
        }
        private SettingsPropertyValueCollection GetPropertyValues(Profile profile, SettingsContext context, SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();
            foreach (SettingsProperty property in collection)
            {
                var value = new SettingsPropertyValue(property)
                {
                    PropertyValue = profile.GetType().GetProperty(property.Name).GetValue(profile)
                };
                result.Add(value);
            }
            return result;
        }

        private void SetPropertyValues(string email, SettingsContext context, SettingsPropertyValueCollection collection)
        {
            var user = this.userService.GetUser(email);
            if (user != null)
            {
                Profile result = user.Profile.Value == null ? new Profile() : user.Profile.Value.ToWeb();
                this.SetPropertyValues(result, user.Id, context, collection);
            }
        }
        private void SetPropertyValues(Profile profile, string userId, SettingsContext context, SettingsPropertyValueCollection collection)
        {
            foreach (SettingsPropertyValue value in collection)
            {
                profile.GetType().GetProperty(value.Property.Name).SetValue(profile, value.PropertyValue);
            }
            this.userService.UpdateUserProfile(userId, profile.ToBll());
        }

        #endregion
    }
}