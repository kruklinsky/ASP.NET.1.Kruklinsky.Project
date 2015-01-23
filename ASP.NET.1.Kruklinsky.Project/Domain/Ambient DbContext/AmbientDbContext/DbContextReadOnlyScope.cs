using AmbientDbContext.Interface;

namespace AmbientDbContext
{
    /// <summary>
    /// Protects DbContextScope from save changes.
    /// </summary>
    public class DbContextReadOnlyScope : IDbContextReadOnlyScope
    {
        private DbContextScope internalScope;

        public DbContextReadOnlyScope(IDbContextFactory dbContextFactory = null)
        {
            this.internalScope = new DbContextScope(true, dbContextFactory);
        }

        #region IDbContextReadOnlyScope

        public IDbContextCollection DbContexts
        {
            get
            {
                return this.internalScope.DbContexts;
            }
        }
        public void Dispose()
        {
            this.internalScope.Dispose();
        }

        #endregion
    }
}
