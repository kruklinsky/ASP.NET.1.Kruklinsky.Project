using AmbientDbContext.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace AmbientDbContext
{
    /// <summary>
    /// Contains and manages dbContext instances.
    /// </summary>
    public class DbContextCollection : IDbContextCollection
    {
        private bool disposed;
        private bool completed;
        private bool readOnly;
        private Dictionary<Type, DbContext> initializedDbContexts;
        private Dictionary<DbContext, DbContextTransaction> transactions;
        private readonly IDbContextFactory dbContextFactory;

        internal Dictionary<Type, DbContext> InitializedDbContexts
        {
            get
            {
                return this.initializedDbContexts;
            }
        }

        private void Initialize()
        {
            this.disposed = false;
            this.completed = false;
            this.initializedDbContexts = new Dictionary<Type, DbContext>();
            this.transactions = new Dictionary<DbContext, DbContextTransaction>();
        }
        public DbContextCollection(bool readOnly = false, IDbContextFactory dbContextFactory = null)
        {
            this.Initialize();
            this.readOnly = readOnly;
            this.dbContextFactory = dbContextFactory;
        }

        #region Public methods

        #region IDbContextCollection

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("DbContextCollection", "DbContextCollection is disposed.");
            }
            var requestedType = typeof(TDbContext);
            if (!this.initializedDbContexts.ContainsKey(requestedType))
            {
                this.AddDbContext<TDbContext>(requestedType);
            }
            return this.initializedDbContexts[requestedType] as TDbContext;
        }
        public void Dispose()
        {
            if (!this.disposed)
            {
                if (!this.completed) Complete();
                foreach (var dbContext in this.initializedDbContexts.Values)
                {
                    this.DisposeDbContext(dbContext);
                }
                this.initializedDbContexts.Clear();
                this.disposed = true;
            }
        }

        #endregion

        public int Commit()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("DbContextCollection", "DbContextCollection is disposed.");
            }
            if (this.completed)
            {
                throw new InvalidOperationException("All the changes in the DbContext instances have already been saved or rollback and all database transactions have been completed and closed.");
            }
            var result = 0;
            ExceptionDispatchInfo lastError = null;
            foreach (var dbContext in this.initializedDbContexts.Values)
            {
                try
                {
                    if (!this.readOnly)
                    {
                        dbContext.SaveChanges();
                        result++;
                    }
                    this.CommitTransaction(dbContext);
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }
            this.transactions.Clear();
            this.completed = true;
            if (lastError != null)
            {
                lastError.Throw();
            }
            return result;
        }
        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }
        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null)
            {
                throw new ArgumentNullException("cancelToken", "Cancellation token is null.");
            }
            if (this.disposed)
            {
                throw new ObjectDisposedException("DbContextCollection", "DbContextCollection is disposed.");
            }
            if (this.completed)
            {
                throw new InvalidOperationException("All the changes in the DbContext instances have already been saved or rollback and all database transactions have been completed and closed.");
            }
            var result = 0;
            ExceptionDispatchInfo lastError = null;
            foreach (var dbContext in this.initializedDbContexts.Values)
            {
                try
                {
                    if (!this.readOnly)
                    {
                        result += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);
                    }
                    this.CommitTransaction(dbContext);
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }
            this.transactions.Clear();
            this.completed = true;
            if (lastError != null)
            {
                lastError.Throw();
            }
            return result;
        }

        public void Rollback()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("DbContextCollection", "DbContextCollection is disposed.");
            }
            if (this.completed)
            {
                throw new InvalidOperationException("All the changes in the DbContext instances have already been saved or rollback and all database transactions have been completed and closed.");
            }
            ExceptionDispatchInfo lastError = null;
            foreach (var dbContext in this.initializedDbContexts.Values)
            {
                var tran = this.GetValueOrDefault(this.transactions, dbContext);
                if (tran != null)
                {
                    try
                    {
                        tran.Rollback();
                        tran.Dispose();
                    }
                    catch (Exception e)
                    {
                        lastError = ExceptionDispatchInfo.Capture(e);
                    }
                }
            }
            this.transactions.Clear();
            this.completed = true;
            if (lastError != null)
            {
                lastError.Throw();
            }
        }

        #endregion

        #region Private methods

        private void Complete()
        {
            try
            {
                if (this.readOnly)
                {
                    this.Commit();
                }
                else
                {
                    this.Rollback();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        #region DbContext

        private void AddDbContext<TDbContext>(Type requestedType) where TDbContext : DbContext
        {
            var dbContext = this.dbContextFactory != null
                ? this.dbContextFactory.CreateDbContext<TDbContext>()
                : Activator.CreateInstance<TDbContext>();
            this.initializedDbContexts.Add(requestedType, dbContext);
            if (this.readOnly)
            {
                dbContext.Configuration.AutoDetectChangesEnabled = false;
            }
        }
        private void DisposeDbContext(DbContext dbContext)
        {
            try
            {
                dbContext.Dispose();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        #endregion

        #region Transaction

        private void CommitTransaction(DbContext dbContext)
        {
            var transaction = this.GetValueOrDefault(this.transactions, dbContext);
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
            }
        }
        private TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        #endregion

        #endregion
    }
}
