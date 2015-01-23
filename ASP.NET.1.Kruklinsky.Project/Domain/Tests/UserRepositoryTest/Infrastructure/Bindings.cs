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
using AmbientDbContext;
using AmbientDbContext.Interface;

namespace TestConsoleUI.Infrastructure
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            //Bind<DbContext>().To<EFDbContext>().InSingletonScope();

            Bind<IAmbientDbContextLocator>().To<AmbientDbContextLocator>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IDbContextScopeFactory>().To<DbContextScopeFactory>().WithConstructorArgument((IDbContextFactory)null);
        }
    }
}
