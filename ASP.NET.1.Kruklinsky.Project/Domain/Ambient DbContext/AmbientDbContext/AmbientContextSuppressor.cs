using System;

namespace AmbientDbContext
{
    /// <summary>
    /// Protects DbContextScope from the parallel disposing.
    /// </summary>
    public class AmbientContextSuppressor : IDisposable
    {
        private DbContextScope savedScope;
        private bool disposed;

        public AmbientContextSuppressor()
        {
            this.savedScope = DbContextScope.GetAmbientScope();
            DbContextScope.HideAmbientScope();
        }

        #region IDisposable

        public void Dispose()
        {
            if (!this.disposed)
            {
                if (savedScope != null)
                {
                    DbContextScope.SetAmbientScope(savedScope);
                    savedScope = null;
                }
                this.disposed = true;
            }
        }

        #endregion
    }
}
