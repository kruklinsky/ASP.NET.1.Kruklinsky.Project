using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Reflection;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using AmbientDbContext.Interface;

namespace TestConsoleUI
{
    class Program
    {
        static IUserRepository InjectUserRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<IUserRepository>();
        }
        static IDbContextScopeFactory InjectScopeFactory()
        {
            Console.Write("Inject factory: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<IDbContextScopeFactory>();
        }
        static void Prepare (IUserRepository repository)
        {
            Console.Write("Prepare TestDb: ");
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                repository.Delete(users[i]);
            }
            Console.WriteLine("Ok");
        }

        static void GetUsers(IUserRepository repository)
        {
            Console.WriteLine("Get users: ");
            User result = repository.GetUserByEmail("Nonexistent");
            if (result != null)
            {
                Console.WriteLine("No");
                return;
            }
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                result = repository.GetUserByEmail(users[i].Email);
                Console.WriteLine(" " + result.Id);
                Console.WriteLine(" role count: " +result.Roles.Value.Count());
            }
            Console.WriteLine("");
        }
        static void AddUsers (IUserRepository repository)
        {
            Console.Write("Add users: ");
            repository.Add(new User
            {
                Email = "apsedianm@mail.ru",
                Password = "DFyufhtibnNJ1687",
                IsApproved = false,
                CreateDate = DateTime.Now
            });
            repository.AddUserRole(repository.Data.Last().Email, "User");
            repository.Add(new User
            {
                Email = "apsedianm@gmail.com",
                Password = "DFyufhtibnNJ1687",
                IsApproved = false,
                CreateDate = DateTime.Now
            });
            repository.AddUserRole(repository.Data.Last().Email, "User");
            Console.WriteLine("Ok");
        }
        static void UpdateUsers(IUserRepository repository)
        {
            Console.Write("Update users: ");
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                users[i].IsApproved = true;
                repository.Update(users[i]);
            }
            Console.WriteLine("Ok");
        }

        static void UpdateUsersProfiles(IUserRepository repository)
        {
            Console.Write("Update users profiles: ");
            Profile result = repository.GetUserProfile("Nonexistent");
            if (result != null)
            {
                Console.WriteLine("No");
                return;
            }
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                result = repository.GetUserProfile(users[i].Id);
                result.FirstName = i.ToString();
                result.SecondName = i.ToString();
                result.Birthday = DateTime.Now;
                repository.UpdateUserProfile(users[i].Id, result);
            }
            Console.WriteLine("Ok");
        }
        static void GetUsersProfiles(IUserRepository repository)
        {
            Console.WriteLine("Get users profiles: ");
            Profile result = repository.GetUserProfile("Nonexistent");
            if (result != null)
            {
                Console.WriteLine("No");
                return;
            }
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                result = repository.GetUserProfile(users[i].Id);
                Console.WriteLine(" " + users[i].Email + ": " + result.FirstName + " " + result.SecondName + " " + result.Birthday.ToString());
            }
            Console.WriteLine("");
        }


        static void GetRoles(IUserRepository repository)
        {
            Console.WriteLine("Get users roles: ");
            List<User> users = new List<User>(repository.Data);
            for (int i = 0; i < users.Count(); i++)
            {
                Console.Write(" " + users[i].Email + ": ");
                foreach (var item in repository.GetUserRoles(users[i].Email))
                {
                    Console.Write(item.Name + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
        }
        static void AddUserRole (IUserRepository repository)
        {
            Console.WriteLine("Add user role: ");
            repository.Data.First();
            repository.AddUserRole(repository.Data.First().Email, "Admin");
            Console.WriteLine("");
        }
        static void DeleteUserRole(IUserRepository repository)
        {
            Console.WriteLine("Delete user role: ");
            repository.Data.First();
            repository.DeleteUserRole(repository.Data.First().Email, "Admin");
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("User repository test");
            IUserRepository repository = InjectUserRepository();
            IDbContextScopeFactory dbContextScopeFactory = InjectScopeFactory();
            using (var context = dbContextScopeFactory.Create())
            {
                Prepare(repository);
                AddUsers(repository);
                UpdateUsers(repository);
                GetUsers(repository);
                UpdateUsersProfiles(repository);
                GetUsersProfiles(repository);
                GetRoles(repository);
                AddUserRole(repository);
                GetRoles(repository);
                DeleteUserRole(repository);
                GetRoles(repository);
                context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
