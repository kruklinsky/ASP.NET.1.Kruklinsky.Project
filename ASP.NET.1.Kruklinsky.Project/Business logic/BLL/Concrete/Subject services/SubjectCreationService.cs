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
    public class SubjectCreationService : ISubjectCreationService
    {
        private ISubjectRepository subjectRepository;
        private IDbContextScopeFactory dbContextScopeFactory;

        public SubjectCreationService(ISubjectRepository subjectRepository, IDbContextScopeFactory dbContextScopeFactory)
        {
            if (subjectRepository == null)
            {
                throw new System.ArgumentNullException("subjectRepository", "Subject repository is null.");
            }
            if (dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.subjectRepository = subjectRepository;
            this.dbContextScopeFactory = dbContextScopeFactory;
        }

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
                this.subjectRepository.Add(newSubject.ToDal());
                context.SaveChanges();
            }
        }
        public bool DeleteSubject(int id)
        {
            SubjectExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var subject = this.subjectRepository.GetSubject(id);
                if (subject != null)
                {
                    this.subjectRepository.Delete(subject);
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
                this.subjectRepository.Update(subject.ToDal());
                context.SaveChanges();
            }
        }

        #endregion
    }
}
