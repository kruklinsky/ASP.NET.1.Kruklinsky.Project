using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ResultQueryService: RepositoryService<IResultRepository>, IResultQueryService
    {
        public ResultQueryService(IResultRepository resultRepository, IDbContextScopeFactory dbContextScopeFactory) : base(resultRepository, dbContextScopeFactory) { }

        #region IResultQueryService

        public Result GetResult(int id)
        {
            ResultExceptionsHelper.GetIdExceptions(id);
            Result result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var dalResult = this.repository.GetResult(id);
                if (dalResult != null)
                {
                    result = dalResult.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<Result> GetAllResults()
        {
            IEnumerable<Result> result = new List<Result>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var results = this.repository.Data;
                if (results.Count() != 0)
                {
                    result = results.Select(r => r.ToBll()).ToList();
                }
            }
            return result;
        }
        public IEnumerable<Result> GetUserResults(string userId)
        {
            UserExceptionsHelper.GetIdExceptions(userId);
            IEnumerable<Result> result = new List<Result>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var results = this.repository.GetUserResults(userId);
                if (results.Count() != 0)
                {
                    result = results.Select(r => r.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion
    }
}
