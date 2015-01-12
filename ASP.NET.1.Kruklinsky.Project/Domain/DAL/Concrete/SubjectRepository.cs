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
    public class SubjectRepository: ISubjectRepository
    {
        #region IRepository

        private readonly DbContext context;
        public SubjectRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Subject> Data
        {
            get
            {
                IEnumerable<ORM.Model.Subject> result = this.context.Set<ORM.Model.Subject>();
                return result.Select(t => t.ToDal());
            }
        }
        public void Add(Subject item)
        {
            this.context.Set<ORM.Model.Subject>().Add(item.ToOrm());
            this.context.SaveChanges();
        }
        public void Delete(Subject item)
        {
            var result = this.GetOrmSubject(item.Id);
            if (result != null)
            {
                this.context.Set<ORM.Model.Subject>().Remove(result);
                this.context.SaveChanges();
            }
        }
        public void Update(Subject item)
        {
            var result = this.GetOrmSubject(item.Id);
            if (result != null)
            {
                result.Description = item.Description;
                this.context.SaveChanges();
            }
        }

        #endregion

        #region ISubjectRepository

        public Subject GetSubject(int id)
        {
            Subject result = null;
            var subject = this.GetOrmSubject(id);
            if (subject != null)
            {
                result = subject.ToDal();
            }
            return result;
        }
        public Subject GetSubject(int id, out IEnumerable<Test> tests)
        {
            Subject result = null;
            tests = new List<Test>();
            var subject = this.GetOrmSubject(id);
            if (subject != null)
            {
                result = subject.ToDal();
                if (subject.Tests != null) tests = subject.Tests.Select(t => t.ToDal());
            }
            return result;
        }
        public IEnumerable<Test> GetSubjectTests(int id)
        {
            IEnumerable<Test> result = new List<Test>();
            var subject = this.GetOrmSubject(id);
            if (subject != null && subject.Tests != null)
            {
                result = subject.Tests.Select(t => t.ToDal());
            }
            return result;
        }

        #endregion

        #region Private methods

        private ORM.Model.Subject GetOrmSubject(int subjectId)
        {
            ORM.Model.Subject result = null;
            var query = this.context.Set<ORM.Model.Subject>().Where(q => q.SubjectId == subjectId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion
    }
}
