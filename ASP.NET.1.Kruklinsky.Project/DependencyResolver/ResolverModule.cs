using BLL.Concrete;
using BLL.Interface.Abstract;
using DAL.Concrete;
using DAL.Interface.Abstract;
using AmbientDbContext;
using AmbientDbContext.Interface;
using ORM;

using Ninject.Modules;
using System.Data.Entity;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EFDbContext>();
            Bind<IAmbientDbContextLocator>().To<AmbientDbContextLocator>();
            Bind<IDbContextScopeFactory>().To<DbContextScopeFactory>().WithConstructorArgument((IDbContextFactory)null);

            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISubjectRepository>().To<SubjectRepository>();
            Bind<IQuestionRepository>().To<QuestionRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<IResultRepository>().To<ResultRepository>();

            Bind<IUserService>().To<UserService>();
            Bind<IKnowledgeService>().To<KnowledgeService>();
            Bind<ISubjectCreationService>().To<SubjectCreationService>();
            Bind<ISubjectQueryService>().To<SubjectQueryService>();
            Bind<ITestService>().To<TestService>();
        }
    }
}
