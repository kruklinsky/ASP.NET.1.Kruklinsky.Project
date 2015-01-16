using BLL.Concrete;
using BLL.Interface.Abstract;
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

namespace DependencyResolver
{
    public class RevolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EFDbContext>().InSingletonScope();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISubjectRepository>().To<SubjectRepository>();
            Bind<IQuestionRepository>().To<QuestionRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<IResultRepository>().To<ResultRepository>();

            Bind<IUserService>().To<UserService>();
            Bind<IKnowledgeService>().To<KnowledgeService>();
            Bind<ITestService>().To<TestService>();
        }
    }
}
