using System;

namespace AmbientDbContext.Interface
{
    public interface IDbContextScopeFactory
    {
        IDbContextScope Create();

        IDbContextReadOnlyScope CreateReadOnly();

        IDisposable SuppressAmbientContext();
    }
}
