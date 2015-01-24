using AmbientDbContext.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public abstract class RepositoryService<T>
    {
        protected T repository;
        protected IDbContextScopeFactory dbContextScopeFactory;

        public RepositoryService(T repository, IDbContextScopeFactory dbContextScopeFactory)
        {
            if (repository == null)
            {
                throw new System.ArgumentNullException("repository", "Repository is null.");
            }
            if (dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.repository = repository;
            this.dbContextScopeFactory = dbContextScopeFactory;
        }
    }
}
