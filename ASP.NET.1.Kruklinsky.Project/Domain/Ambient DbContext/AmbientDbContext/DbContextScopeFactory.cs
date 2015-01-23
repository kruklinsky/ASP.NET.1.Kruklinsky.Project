using System;
using AmbientDbContext.Interface;

namespace AmbientDbContext
{
    /// <summary>
    /// Creates DbContextScope instances.
    /// </summary>
    public class DbContextScopeFactory : IDbContextScopeFactory
    {
        private readonly IDbContextFactory dbContextFactory;

        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            this.dbContextFactory = dbContextFactory;
        }

        #region IDbContextScopeFactory

        public IDbContextScope Create()
        {
            return new DbContextScope(false, this.dbContextFactory);
        }
        public IDbContextReadOnlyScope CreateReadOnly()
        {
            return new DbContextReadOnlyScope(this.dbContextFactory);
        }
        public IDisposable SuppressAmbientContext()
        {
            return new AmbientContextSuppressor();
        }

        #endregion
    }
}
