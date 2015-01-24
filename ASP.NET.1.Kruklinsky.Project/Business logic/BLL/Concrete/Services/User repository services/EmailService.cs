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
    public abstract class EmailService : RepositoryService<IUserRepository>
    {
        protected Regex emailValidationRegex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");

        public EmailService(IUserRepository repository, IDbContextScopeFactory dbContextScopeFactory) : base(repository, dbContextScopeFactory) { }
        public EmailService(IUserRepository repository, IDbContextScopeFactory dbContextScopeFactory, string emailRegularExpression)
            : base(repository, dbContextScopeFactory)
        {
            this.emailValidationRegex = emailRegularExpression == null ? null : new Regex(emailRegularExpression);
        }
    }
}

