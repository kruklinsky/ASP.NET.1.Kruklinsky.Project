using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class RoleQueryService : RepositoryService<IUserRepository>, IRoleQueryService
    {
        public RoleQueryService(IUserRepository userRepository, IDbContextScopeFactory dbContextScopeFactory) : base(userRepository, dbContextScopeFactory) { }

        #region IRoleQueryService

        public string[] GetAllRoles()
        {
            List<string> result = new List<string>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var roles = this.repository.GetAllRoles();
                if (roles.Count() != 0)
                {
                    result = roles.Select(r => r.Name).ToList();
                }
            }
            return result.ToArray();
        }
        public bool RoleExists(string roleName)
        {
            RoleExceptionsHelper.GetNameExceptions(roleName);
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                return this.repository.RoleExists(roleName);
            }
        }

        #endregion
    }
}
