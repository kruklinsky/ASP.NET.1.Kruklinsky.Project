using DependencyResolver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.Providers.Abstract;
using MvcUI.Providers.Concrete;
using MvcUI.Infrastructure.Abstract;
using MvcUI.Infrastructure.Concrete;

namespace MvcUI.Infrastructure
{
    internal class WebResolverModule : ResolverModule
    {
        public override void Load()
        {
            base.Load();
            Bind<IAuthProvider>().To<FormsAuthProvider>();
            Bind<IVerifyProvider>().To<EmailVerifyProvider>().WithConstructorArgument("verifyUrl","http://localhost:3868//Verify/Verify");
            Bind<ITestSessionFactory>().To<TestSessionFactory>();
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel(new WebResolverModule());
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }

}