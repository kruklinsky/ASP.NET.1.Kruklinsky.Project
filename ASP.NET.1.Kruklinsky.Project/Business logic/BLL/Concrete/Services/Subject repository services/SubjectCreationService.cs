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
    public class SubjectCreationService : RepositoryService<ISubjectRepository>, ISubjectCreationService
    {
        public SubjectCreationService(ISubjectRepository subjectRepository, IDbContextScopeFactory dbContextScopeFactory) : base(subjectRepository, dbContextScopeFactory) { }

        #region ISubjectCreationService

        public void CreateSubject(string name, string description)
        {
            SubjectExceptionsHelper.GetNameExceptions(name);
            var newSubject = new Subject
            {
                Name = name,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Add(newSubject.ToDal());
                context.SaveChanges();
            }
        }
        public bool DeleteSubject(int id)
        {
            SubjectExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var subject = this.repository.GetSubject(id);
                if (subject != null)
                {
                    this.repository.Delete(subject);
                    result = true;
                }
                context.SaveChanges();
            }
            return result;
        }
        public void UpdateSubject(Subject subject)
        {
            SubjectExceptionsHelper.GetIdExceptions(subject.Id);
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Update(subject.ToDal());
                context.SaveChanges();
            }
        }

        #endregion
    }
}
