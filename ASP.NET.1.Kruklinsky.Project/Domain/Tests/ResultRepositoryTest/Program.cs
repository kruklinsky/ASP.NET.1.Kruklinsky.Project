using AmbientDbContext.Interface;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using Ninject;
//using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResultRepositoryTest
{
    class Program
    {
        static void Prepare(ITestRepository testRepository, IQuestionRepository questionRepository, ISubjectRepository subjectRepository,IResultRepository resultRepository,IUserRepository userRepository)
        {

            Console.Write("Prepare TestDb: ");
            #region Delete users answers
            //EFDbContext context = new EFDbContext();
            //List<ORM.Model.UserAnswer> answers = context.UsersAnswers.ToList();
            #endregion
            #region Delete results
            List<Result> results = resultRepository.Data.ToList();
            for (int i = 0; i < results.Count();i++)
            {
                resultRepository.Delete(results[i]);
            }
            #endregion
            #region Delete tests
            List<Test> tests = testRepository.Data.ToList();
            for (int i = 0; i < tests.Count(); i++)
            {
                testRepository.Delete(tests[i]);
            }
            #endregion
            #region Delete questions
            List<Question> questions = questionRepository.Data.ToList();
            for (int i = 0; i < questions.Count(); i++)
            {
                questionRepository.Delete(questions[i]);
            }
            #endregion
            #region Delete subjects
            List<Subject> subjects = subjectRepository.Data.ToList();
            for (int i = 0; i < subjects.Count(); i++)
            {
                subjectRepository.Delete(subjects[i]);
            }
            #endregion
            #region Delete users
            List<User> users = userRepository.Data.ToList();
            foreach(var item in users)
            {
                userRepository.Delete(item);
            }
            #endregion
            Console.WriteLine("Ok");
        }

        static void AddUsers(IUserRepository repository)
        {
            Console.Write("Add users: ");
            repository.Add(new User
            {
                Email = "apsedianm@mail.ru",
                Password = "DFyufhtibnNJ1687",
                IsApproved = false,
                CreateDate = DateTime.Now
            });
            repository.Add(new User
            {
                Email = "apsedianm@gmail.com",
                Password = "DFyufhtibnNJ1687",
                IsApproved = false,
                CreateDate = DateTime.Now
            });
            Console.WriteLine("Ok");
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
        static void AddQuestions(IQuestionRepository questionRepository, ISubjectRepository subjectRepository)
        {
            Console.Write("Add questions: ");
            var subject = subjectRepository.Data.First();
            List<Answer> answers = new List<Answer>() { new Answer { Text = "a" } };
            List<Fake> fakes = new List<Fake>() { new Fake { Text = "b" }, new Fake { Text = "c" } };
            Question question = new Question
            {
                SubjectId = subject.Id,
                Level = 1,
                Topic = "Topic",
                Text = "Question a",
                Description = "no"
            };
            questionRepository.Add(question, answers, fakes);
            question = new Question
            {
                SubjectId = subject.Id,
                Level = 2,
                Topic = "Topic",
                Text = "Question b",
                Description = "no"
            };
            questionRepository.Add(question, answers, fakes);
            Console.WriteLine("Ok");
        }
        static void AddTest(ITestRepository testRepository, ISubjectRepository subjectRepository)
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
            Console.WriteLine("Ok");
        }
        static void AddTestQuestion(ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            Console.Write("Add test question: ");
            var test = testRepository.Data.First();
            var question = questionRepository.Data.First();
            testRepository.AddTestQuestion(test.Id, question.Id);
            Console.WriteLine("Ok");
        }
        static void AddTestQuestion(ITestRepository testRepository, ISubjectRepository subjectRepository)
        {
            Console.Write("Add test question: ");
            var subject = subjectRepository.Data.First();
            var test = testRepository.Data.First();
            List<Answer> answers = new List<Answer>() { new Answer { Text = "a" } };
            List<Fake> fakes = new List<Fake>() { new Fake { Text = "b" }, new Fake { Text = "c" } };
            Question question = new Question
            {
                SubjectId = subject.Id,
                Level = 1,
                Topic = "Topic",
                Text = "Question c",
                Description = "no"
            };
            testRepository.AddTestQuestion(test.Id, question, answers, fakes);
            Console.WriteLine("Ok");
        }

        static void AddResult(IResultRepository resultRepository, ITestRepository testRepository, IUserRepository userRepository)
        {
            Console.Write("Add result: ");
            var test = testRepository.Data.First();
            var user = userRepository.Data.First();
            resultRepository.Add(new Result
                {
                    TestId = test.Id,
                    UserId = user.Id,
                    Start = DateTime.UtcNow,
                    Time = new TimeSpan(0, 2, 15)
                });
            Console.WriteLine("Ok");
        }

        static void AddUserAnswer (IResultRepository resultRepository, ITestRepository testRepository)
        {
            Console.Write("Add user answer: ");
            var result = resultRepository.Data.First();
            var test = testRepository.Data.First();
            resultRepository.AddUserAnswer(result.Id, new UserAnswer { IsRight = true, QuestionId = testRepository.GetTestQuestions(test.Id).First().Id});
            resultRepository.AddUserAnswer(result.Id, new UserAnswer { IsRight = false, QuestionId = testRepository.GetTestQuestions(test.Id).Last().Id });
            Console.WriteLine("Ok");
        }
        static void GetUsersAnswers(IResultRepository resultRepository,IQuestionRepository questionRepository)
        {
            Console.WriteLine("Get users answers: ");
            var result = resultRepository.Data.First();
            List<UserAnswer> answers = resultRepository.GetUserAnswers(result.Id).ToList();
            foreach(var item in answers)
            {
                Console.Write(" "+questionRepository.GetQuestion(item.QuestionId).Text + ": ");
                Console.WriteLine(item.IsRight);
            }
            Console.WriteLine();
        }

        static void GetUserResults (IResultRepository resultRepository, IQuestionRepository questionRepository,IUserRepository userRepostiroy)
        {
            Console.WriteLine("Get user results: ");
            //var result = resultRepository.Data.First();
            var user = userRepostiroy.Data.First();
            List<Result> results = resultRepository.GetUserResults(user.Id).ToList();
            Console.WriteLine(user.Email);
            foreach (var result in results)
            {
                Console.WriteLine(" test id: " + result.TestId);
                List<UserAnswer> answers = resultRepository.GetUserAnswers(result.Id).ToList();
                foreach (var item in answers)
                {
                    Console.Write("  " + questionRepository.GetQuestion(item.QuestionId).Text + ": ");
                    Console.WriteLine(item.IsRight);
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Result repository test");
            #region Inject
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IQuestionRepository questionRepository = kernel.Get<IQuestionRepository>();
            ITestRepository testRepository = kernel.Get<ITestRepository>();
            ISubjectRepository subjectRepository = kernel.Get<ISubjectRepository>();
            IResultRepository resultRepository = kernel.Get<IResultRepository>();
            IUserRepository userRepository = kernel.Get<IUserRepository>();
            IDbContextScopeFactory dbContextScopeFactory = kernel.Get<IDbContextScopeFactory>();
            #endregion
            using (var context = dbContextScopeFactory.Create())
            {
                Prepare(testRepository, questionRepository, subjectRepository, resultRepository, userRepository);
                AddUsers(userRepository);
                AddSubjects(subjectRepository);
                AddQuestions(questionRepository, subjectRepository);
                AddTest(testRepository, subjectRepository);
                AddTestQuestion(testRepository, questionRepository);
                AddTestQuestion(testRepository, subjectRepository);
                AddResult(resultRepository, testRepository, userRepository);
                AddUserAnswer(resultRepository, testRepository);
                GetUsersAnswers(resultRepository, questionRepository);
                GetUserResults(resultRepository, questionRepository, userRepository);
                context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
