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
    public class SubjectQueryService : RepositoryService<ISubjectRepository>, ISubjectQueryService
    {
        public SubjectQueryService(ISubjectRepository subjectRepository, IDbContextScopeFactory dbContextScopeFactory) : base(subjectRepository, dbContextScopeFactory) { }

        #region ISubjectQueryService

        public Subject GetSubject(int id)
        {
            SubjectExceptionsHelper.GetIdExceptions(id);
            Subject result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var subject = this.repository.GetSubject(id);
                if (subject != null)
                {
                    result = subject.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<Subject> GetAllSubjects()
        {
            IEnumerable<Subject> result = new List<Subject>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var subjects = this.repository.Data;
                if (subjects.Count() != 0)
                {
                    result = subjects.Select(s => s.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion
    }
}
