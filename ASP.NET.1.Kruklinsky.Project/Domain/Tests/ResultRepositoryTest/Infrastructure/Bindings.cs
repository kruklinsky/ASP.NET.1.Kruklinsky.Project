using DAL.Concrete;
using DAL.Interface.Abstract;
using Ninject.Modules;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultRepositoryTest.Infrastructure
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EFDbContext>().InSingletonScope();
            Bind<ISubjectRepository>().To<SubjectRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<IQuestionRepository>().To<QuestionRepository>();
            Bind<IResultRepository>().To<ResultRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
