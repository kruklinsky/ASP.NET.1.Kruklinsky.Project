using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ResultRepository : IResultRepository
    {
        #region IRepository

        private readonly DbContext context;
        public ResultRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Result> Data
        {
            get
            {
                IEnumerable<ORM.Model.Result> result = this.context.Set<ORM.Model.Result>();
                return result.Select(t => t.ToDal());
            }
        }
        public void Add(Result item)
        {
            this.context.Set<ORM.Model.Result>().Add(item.ToOrm());
            this.context.SaveChanges();
        }
        public void Delete(Result item)
        {
            var result = this.GetOrmResult(item.Id);
            if (result != null)
            {
                this.context.Set<ORM.Model.Result>().Remove(result);
                this.context.SaveChanges();
            }
        }
        public void Update(Result item)
        {
            throw new System.NotSupportedException("Impossible to update test results.");
        }

        #endregion

        #region IResultRepository

        public Result GetResult(int id)
        {
            Result result = null;
            var ormResult = this.GetOrmResult(id);
            if (ormResult != null)
            {
                result = ormResult.ToDal();
            }
            return result;
        }
        public Result GetResult(int id, out IEnumerable<UserAnswer> answers)
        {
            Result result = null;
            answers = new List<UserAnswer>();
            var ormResult = this.GetOrmResult(id);
            if (ormResult != null)
            {
                result = ormResult.ToDal();
                if (ormResult.Answers != null) answers = ormResult.Answers.Select(a => a.ToDal()).ToList();
            }
            return result;
        }

        public IEnumerable<UserAnswer> GetUserAnswers(int id)
        {
            IEnumerable<UserAnswer> result = new List<UserAnswer>();
            var ormResult = this.GetOrmResult(id);
            if (ormResult != null && ormResult.Answers != null)
            {
                result = ormResult.Answers.Select(a => a.ToDal()).ToList();
            }
            return result;
        }
        public void AddUserAnswer(int id, UserAnswer answer)
        {
            var result = this.GetOrmResult(id);
            if (result != null)
            {
                if (result.Answers == null) result.Answers = new List<ORM.Model.UserAnswer>();
                result.Answers.Add(answer.ToOrm());
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Result> GetUserResults(string userId)
        {
            IEnumerable<Result> result = new List<Result>();
            var results = this.GetResults(userId);
            if(results.Count() != 0)
            {
                result = results.Select(r => r.ToDal()).ToList();
            }
            return result;
        }

        #endregion

        #region Private Methods

        private IEnumerable<ORM.Model.Result> GetResults (string userId)
        {
            IEnumerable<ORM.Model.Result> result = new List<ORM.Model.Result>();
            var query = this.context.Set<ORM.Model.Result>().Where(r => r.UserId.ToString() == userId);
            if (query.Count() != 0)
            {
                result = query.ToList();
            }
            return result;
        }

        private ORM.Model.Result GetOrmResult(int resultId)
        {
            ORM.Model.Result result = null;
            var query = this.context.Set<ORM.Model.Result>().Where(q => q.ResultId == resultId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion

    }
}
