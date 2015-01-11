using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SubjectRepositoryTest
{
    class Program
    {
        static ITestRepository InjectTestRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<ITestRepository>();
        }
        static ISubjectRepository InjectSubjectRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<ISubjectRepository>();
        }
        static void Prepare(ISubjectRepository subjectRepository, ITestRepository testRepository)
        {
            Console.Write("Prepare TestDb: ");
            List<Test> tests = testRepository.Data.ToList();
            for (int i = 0; i < tests.Count(); i++)
            {
                testRepository.Delete(tests[i]);
            }
            List<Subject> subjects = subjectRepository.Data.ToList();
            for (int i = 0; i < subjects.Count(); i++)
            {
                subjectRepository.Delete(subjects[i]);
            }
            Console.WriteLine("Ok");
        }

        static void AddTests(ITestRepository testRepository,ISubjectRepository subjectRepository)
        {
            Console.Write("Add test: ");
            var subject = subjectRepository.Data.First();
            testRepository.Add(new Test
            {
                SubjectId = subject.Id,
                Name = "Hard test",
                Topic = "Some topic",
                Description = "It's hard but esy too."
            });
            testRepository.Add(new Test
            {
                SubjectId = subject.Id,
                Name = "Esy test",
                Topic = "Some topic",
                Description = "It's esy but hard too."
            });
            Console.WriteLine("Ok");
        }
        static void GetTests(ITestRepository repository)
        {
            var tests = repository.Data.ToList();
            foreach (var item in tests)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
        }

        static void AddSubjects(ISubjectRepository repository)
        {
            Console.Write("Add subjects: ");
            repository.Add(new Subject
                {
                     Name = "Sql",
                });
            repository.Add(new Subject
            {
                Name = "C#",
            });
            repository.Add(new Subject
            {
                Name = "Java",
            });
            Console.WriteLine("Ok");
        }
        static void GetSubjects(ISubjectRepository repository)
        {
            var subjects = repository.Data.ToList();
            foreach (var item in subjects)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
        }
        static void GetSubjectTests(ISubjectRepository repository)
        {
            Console.WriteLine("Get subject tests: ");
            List<Subject> subjects = repository.Data.ToList();
            foreach (var item in subjects)
            {
                Console.WriteLine(item.Name);
                var tests = repository.GetSubjectTests(item.Id);
                foreach(var item2 in tests)
                {
                    Console.WriteLine("-" + item2.Name);
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Subject repository test");
            ISubjectRepository subjectRepository = InjectSubjectRepository();
            ITestRepository testRepository = InjectTestRepository();
            Prepare(subjectRepository, testRepository);
            AddSubjects(subjectRepository);
            GetSubjects(subjectRepository);
            AddTests(testRepository, subjectRepository);
            GetTests(testRepository);
            //subjectRepository = InjectSubjectRepository();
            GetSubjectTests(subjectRepository);
        }
    }
}
