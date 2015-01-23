using System.Data.Entity;

namespace AmbientDbContext.Interface
{
    public interface IAmbientDbContextLocator
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
