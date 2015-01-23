using System.Data.Entity;

namespace AmbientDbContext.Interface
{
    public interface IDbContextFactory
    {
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
    }
}
