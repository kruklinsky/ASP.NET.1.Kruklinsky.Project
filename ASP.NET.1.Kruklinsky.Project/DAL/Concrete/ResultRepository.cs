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

        public Result GetResult(int Id)
        {
            Result result = null;
            var ormResult = this.GetOrmResult(Id);
            if (ormResult != null)
            {
                result = ormResult.ToDal();
            }
            return result;
        }
        public Result GetResult(int Id, out IEnumerable<UserAnswer> answers)
        {
            Result result = null;
            answers = new List<UserAnswer>();
            var ormResult = this.GetOrmResult(Id);
            if (ormResult != null)
            {
                result = ormResult.ToDal();
                if (ormResult.Answers != null) answers = ormResult.Answers.Select(a => a.ToDal()).ToList();
            }
            return result;
        }

        public IEnumerable<UserAnswer> GetResultAnswers(int Id)
        {
            IEnumerable<UserAnswer> result = new List<UserAnswer>();
            var ormResult = this.GetOrmResult(Id);
            if (ormResult != null && ormResult.Answers != null)
            {
                result = ormResult.Answers.Select(a => a.ToDal()).ToList();
            }
            return result;
        }
        public void AddResultAnswer(int Id, UserAnswer answer)
        {
            var result = this.GetOrmResult(Id);
            if (result != null)
            {
                if (result.Answers == null) result.Answers = new List<ORM.Model.UserAnswer>();
                result.Answers.Add(answer.ToOrm());
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Private Methods

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
