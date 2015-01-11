using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestRepositoryTest
{
    class Program
    {
        static IQuestionRepository InjectQuestionRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<IQuestionRepository>();
        }
        static ITestRepository InjectTestRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<ITestRepository>();
        }
        static void Prepare(ITestRepository repository, IQuestionRepository _repository)
        {
            
            Console.Write("Prepare TestDb: ");
            List<Test> tests = repository.Data.ToList();
            for (int i = 0; i < tests.Count(); i++)
            {
                repository.Delete(tests[i]);
            }
            List<Question> questions = _repository.Data.ToList();
            for (int i = 0; i < questions.Count(); i++)
            {
                _repository.Delete(questions[i]);
            }
            Console.WriteLine("Ok");
        }

        static void AddQuestions(IQuestionRepository repository)
        {
            Console.Write("Add questions: ");
            List<Answer> answers = new List<Answer>() { new Answer { Text = "a" } };
            List<Fake> fakes = new List<Fake>() { new Fake { Text = "b" }, new Fake { Text = "c" } };
            Question question = new Question
            {
                SubjectId = 1,
                Level = 1,
                Topic = "Topic",
                Text = "Question a",
                Description = "no"
            };
            repository.Add(question, answers, fakes);
            question = new Question
            {
                SubjectId = 1,
                Level = 2,
                Topic = "Topic",
                Text = "Question b",
                Description = "no"
            };
            repository.Add(question, answers, fakes);
            Console.WriteLine("Ok");
        }
        static void GetQuestions(IQuestionRepository repository)
        {
            Console.WriteLine("Get questions: ");
            List<Question> questions = repository.Data.ToList();
            foreach (var item in questions)
            {
                //IEnumerable<Answer> answers;
                //IEnumerable<Fake> fakes;
                //repository.GetQuestion(item.Id, out answers, out fakes);
                Console.WriteLine(item.Text);
                //foreach (var item2 in answers)
                //{
                //    Console.WriteLine("- " + item2.Text);
                //}
                //foreach (var item2 in fakes)
                //{
                //    Console.WriteLine("- " + item2.Text);
                //}
            }
            Console.WriteLine();
        }

        static void AddTest(ITestRepository repository)
        {
            Console.Write("Add test: ");
            repository.Add(new Test
            {
                SubjectId = 1,
                Name = "Hard test",
                Topic = "Some topic",
                Description = "It's hard but esy too."
            });
            Console.WriteLine("Ok");
        }
        static void GetTests(ITestRepository repository, IQuestionRepository _repository)
        {
            var tests = repository.Data.ToList();
            foreach(var item in tests)
            {
                Console.WriteLine(item.Name);
                GetTestQuestions(repository, _repository, item);
            }
        }
        static void GetTestQuestions(ITestRepository repository, IQuestionRepository _repository, Test test)
        {
            List<Question> questions = repository.GetTestQuestions(test.Id).ToList();
            foreach (var item in questions)
            {
                //IEnumerable<Answer> answers;
                //IEnumerable<Fake> fakes;
                //_repository.GetQuestion(item.Id, out answers, out fakes);
                Console.WriteLine(" "+item.Text);
                //foreach (var item2 in answers)
                //{
                //    Console.WriteLine(" - " + item2.Text);
                //}
                //foreach (var item2 in fakes)
                //{
                //    Console.WriteLine(" - " + item2.Text);
                //}
            }
            Console.WriteLine();
        }

        static void AddTestQuestion(ITestRepository repository, IQuestionRepository _repository)
        {
            Console.WriteLine("Add test question: ");
            var test = repository.Data.First();
            var question = _repository.Data.First();
            repository.AddTestQuestion(test.Id, question.Id);
            GetTests(repository, _repository);
            GetQuestions(_repository);
        }
        static void AddTestQuestionV2(ITestRepository repository, IQuestionRepository _repository)
        {
            Console.WriteLine("Add test questionV2: ");
            var test = repository.Data.First();
            List<Answer> answers = new List<Answer>() { new Answer { Text = "a" } };
            List<Fake> fakes = new List<Fake>() { new Fake { Text = "b" }, new Fake { Text = "c" } };
            Question question = new Question
            {
                SubjectId = 1,
                Level = 1,
                Topic = "Topic",
                Text = "Question c",
                Description = "no"
            };
            repository.AddTestQuestion(test.Id, question,answers,fakes);
            GetTests(repository, _repository);
            GetQuestions(_repository);
        }

        static void DeleteTestQuestion(ITestRepository repository, IQuestionRepository _repository)
        {
            Console.WriteLine("Delete test question: ");
            var test = repository.Data.First();
            var question = repository.GetTestQuestions(test.Id).First();
            repository.DeleteTestQuestion(test.Id, question.Id);
            GetTests(repository, _repository);
            GetQuestions(_repository);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Test repository test");
            IQuestionRepository repository = InjectQuestionRepository();
            ITestRepository testRepository = InjectTestRepository();
        Start:
            Console.Clear();
            Prepare(testRepository,repository);
            AddQuestions(repository);
            GetQuestions(repository);
            AddTest(testRepository);
            GetTests(testRepository, repository);
            AddTestQuestion(testRepository, repository);
            AddTestQuestionV2(testRepository, repository);
            DeleteTestQuestion(testRepository, repository);
            Console.ReadKey();
            goto Start;
        }
    }
}
