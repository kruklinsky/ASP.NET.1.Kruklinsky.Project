using System.Data.Entity;
using AmbientDbContext.Interface;

namespace AmbientDbContext
{
    /// <summary>
    /// Gets TDbContext from DbContextScope if it already created, otherwise adds TDbContext into DbContextScope.
    /// </summary>
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        #region IAmbientDbContextLocator

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            TDbContext result = ambientDbContextScope == null
                ? null
                : ambientDbContextScope.DbContexts.Get<TDbContext>();
            return result;
        }

        #endregion
    }
}
