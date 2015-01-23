using System;

namespace AmbientDbContext.Interface
{
    public interface IDbContextReadOnlyScope : IDisposable
    {
        IDbContextCollection DbContexts { get; }
    }
}
