using System;
using System.Threading;
using System.Threading.Tasks;

namespace AmbientDbContext.Interface
{
    public interface IDbContextScope : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancelToken);

        IDbContextCollection DbContexts { get; }
    }
}
