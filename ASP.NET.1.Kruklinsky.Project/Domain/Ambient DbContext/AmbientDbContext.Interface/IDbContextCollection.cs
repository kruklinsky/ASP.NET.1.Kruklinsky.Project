using System;
using System.Data.Entity;

namespace AmbientDbContext.Interface
{
    public interface IDbContextCollection : IDisposable
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
