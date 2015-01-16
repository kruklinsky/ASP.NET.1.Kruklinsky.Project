﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        #region IRepository

        private readonly DbContext context;
        public UserRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<User> Data
        {
            get 
            {
                IEnumerable<ORM.Model.User> result = this.context.Set<ORM.Model.User>();
                return result.Select(u => u.ToDal()).ToList();
            }
        }
        public void Add(User item)
        {
            var result = item.ToOrm();
            result.Profile = new ORM.Model.Profile();
            this.context.Set<ORM.Model.User>().Add(result);
            this.context.SaveChanges();
        }
        public void Delete(User item)
        {
            var result = this.GetUser(item.Id.ToGuid());
            if (result != null)
            {
                this.context.Set<ORM.Model.Profile>().Remove(result.Profile);
                this.context.Set<ORM.Model.User>().Remove(result);
                this.context.SaveChanges();
            }
        }

        public void Update(User item)
        {
            var result = this.GetUser(item.Id.ToGuid());
            if (result != null)
            {
                result.Password = item.Password;
                result.IsApproved = item.IsApproved;
                this.context.SaveChanges();
            }
        }

        #endregion

        #region IUserRepository

        public User GetUser(string email)
        {
            User result = null;
            var user = this.GetOrmUser(email);
            if (user != null)
            {
                result = user.ToDal();
            }
            return result;
        }
        public User GetUserById(string id)
        {
            User result = null;
            var user = this.GetUser(id.ToGuid());
            if (user != null)
            {
                result = user.ToDal();
            }
            return result;
        }

        public Profile GetUserProfile(string id)
        {
            Profile result = null;
            var user = this.GetUser(id.ToGuid());
            if (user != null)
            {
                result = user.Profile.ToDal();
            }
            return result;
        }
        public void UpdateUserProfile(string id, Profile profile)
        {
            var user = this.GetUser(id.ToGuid());
            if (user != null)
            {
                var result = user.Profile;
                if (result == null)
                {
                    user.Profile = new ORM.Model.Profile();
                }
                result.FirstName = profile.FirstName;
                result.SecondName = profile.SecondName;
                result.Birthday = profile.Birthday;
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Role> GetUserRoles(string email)
        {
            IEnumerable<Role> result = new List<Role>();
            var user = this.GetOrmUser(email);
            if (user != null && user.Roles != null)
            {
                var roles = user.Roles;
                result = roles.Select(r => r.ToDal()).ToList();
            }
            return result;
        }
        public IEnumerable<User> GetUsersInRole (string roleName)
        {
            IEnumerable<User> result = new List<User>();
            var users = this.GetOrmUsersInRole(roleName);
            if(users.Count() != 0)
            {
                result = users.Select(u => u.ToDal()).ToList();
            }
            return result;
        }
        public void AddUserRole(string email, string roleName)
        {
            var role = this.GetRole(roleName);
            var user = this.GetOrmUser(email);
            if (role != null && user != null)
            {
                if (user.Roles == null) user.Roles = new List<ORM.Model.Role>();
                user.Roles.Add(role);
                context.SaveChanges();
            }
        }
        public void DeleteUserRole(string email, string roleName)
        {
            var role = this.GetRole(roleName);
            var user = this.GetOrmUser(email);
            if (role != null && user != null)
            {
                user.Roles.Remove(role);
                context.SaveChanges();
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<ORM.Model.Role> result = this.context.Set<ORM.Model.Role>();
            return result.Select(r => r.ToDal()).ToList();
        }
        public bool RoleExists(string roleName)
        {
            return this.GetRole(roleName) != null;
        }

        #endregion

        #region Private methods

        private ORM.Model.User GetOrmUser(string email)
        {
            ORM.Model.User result = null;
            var query = this.context.Set<ORM.Model.User>().Where(u => u.Email == email);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
        private ORM.Model.User GetUser(Guid userId)
        {
            ORM.Model.User result = null;
            var query = this.context.Set<ORM.Model.User>().Where(u => u.UserId == userId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
        private IEnumerable<ORM.Model.User> GetOrmUsersInRole (string roleName)
        {
            IEnumerable<ORM.Model.User> result = new List<ORM.Model.User>();
            var query = this.context.Set<ORM.Model.Role>().Where(r => r.RoleName == roleName);
            if(query.Count() != 0)
            {
                result = query.First().Users.ToList();
            }
            return result;
        }

        private ORM.Model.Role GetRole(string roleName)
        {
            ORM.Model.Role result = null;
            var query = this.context.Set<ORM.Model.Role>().Where(r => r.RoleName == roleName);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion
    }
}
