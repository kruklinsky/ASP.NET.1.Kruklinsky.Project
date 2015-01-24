using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class UserQueryService : EmailService, IUserQueryService
    {
        public UserQueryService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }
        public UserQueryService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression) : base(userRepository, dbContextScopeFactory, emailRegularExpression) { }

        #region IUserQueryService

        public User GetUser(string id)
        {
            UserExceptionsHelper.GetIdExceptions(id);
            User result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.repository.GetUser(id);
                if (user != null)
                {
                    result = user.ToBll();
                }
            }
            return result;
        }
        public User GetUserByEmail(string email)
        {
            UserExceptionsHelper.GetEmailExceptions(email, this.emailValidationRegex);
            User result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var user = this.repository.GetUserByEmail(email);
                if (user != null)
                {
                    result = user.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result = new List<User>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var users = this.repository.Data;
                if (users.Count() != 0)
                {
                    result = users.Select(u => u.ToBll());
                }
            }
            return result;
        }

        #endregion
    }
}
