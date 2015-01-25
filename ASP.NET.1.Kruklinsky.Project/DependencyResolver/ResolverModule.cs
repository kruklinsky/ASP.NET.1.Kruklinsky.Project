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
            #region Context
            Bind<IAmbientDbContextLocator>().To<AmbientDbContextLocator>();
            Bind<IDbContextScopeFactory>().To<DbContextScopeFactory>().WithConstructorArgument((IDbContextFactory)null);
            #endregion
            #region Repositories
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISubjectRepository>().To<SubjectRepository>();
            Bind<IQuestionRepository>().To<QuestionRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<IResultRepository>().To<ResultRepository>();
            #endregion
            #region Services
            #region Membership
            #region User
            Bind<IUserQueryService>().To<UserQueryService>();
            Bind<IUserCreationService>().To<UserCreationService>();
            Bind<IUserSecurityService>().To<UserSecurityService>();
            #endregion
            #region Role
            Bind<IRoleQueryService>().To<RoleQueryService>();
            Bind<IUserRolesQueryService>().To<UserRolesQueryService>();
            Bind<IUserRolesManagementService>().To<UserRolesManagementService>();
            #endregion
            #region Profile
            Bind<IProfileService>().To<ProfileService>();
            #endregion
            #endregion
            #region Knowlendge
            #region Subject
            Bind<ISubjectCreationService>().To<SubjectCreationService>();
            Bind<ISubjectQueryService>().To<SubjectQueryService>();
            #endregion
            #region Test
            Bind<ITestQueryService>().To<TestQueryService>();
            Bind<ITestCreationService>().To<TestCreationService>();
            Bind<ITestQuestionsManagementService>().To<TestQuestionsManagementService>();
            #endregion
            #region Question
            Bind<IQuestionCreationService>().To<QuestionCreationServise>();
            Bind<IQuestionQueryService>().To<QuestionQueryService>();
            #endregion
            #region Answers
            Bind<IQuestionAnswersManagmentService>().To<QuestionAnswersManagmentServise>();
            Bind<IAnswersQueryService>().To<AnswersQueryServise>();
            #endregion
            #endregion
            #region Test
            Bind<IResultQueryService>().To<ResultQueryService>();
            Bind<ITestingService>().To<TestingService>();
            #endregion
            #endregion
        }
    }
}
