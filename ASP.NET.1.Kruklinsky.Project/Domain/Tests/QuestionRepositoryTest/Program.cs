using AmbientDbContext.Interface;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuestionRepositoryTest
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
        static ISubjectRepository InjectSubjectRepository()
        {
            Console.Write("Inject repository: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<ISubjectRepository>();
        }
        static IDbContextScopeFactory InjectScopeFactory()
        {
            Console.Write("Inject factory: ");
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            Console.WriteLine("Ok");
            return kernel.Get<IDbContextScopeFactory>();
        }
        static void Prepare(IQuestionRepository repository,ISubjectRepository subjectRepository)
        {
            Console.Write("Prepare TestDb: ");
            List<Question> questions = repository.Data.ToList();
            for (int i = 0; i < questions.Count(); i++)
            {
                repository.Delete(questions[i]);
            }
            List<Subject> subjects = subjectRepository.Data.ToList();
            for (int i = 0; i < subjects.Count(); i++)
            {
                subjectRepository.Delete(subjects[i]);
            }
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

        static void AddQuestions(IQuestionRepository repository,ISubjectRepository subjectRepository)
        {
            Console.Write("Add questions: ");
            Subject subject = subjectRepository.Data.First();
            Answer[] answers = new Answer[] { new Answer { Text = "a"} };
            Fake[] fakes = new Fake[] { new Fake{ Text = "b"}, new Fake{Text = "c"}};
            Question question = new Question
            {
                SubjectId = subject.Id,
                Level = 1,
                Topic = "Topic",
                Text = "Question a",
                Description = "no"
            };
            repository.Add(question, answers, fakes);
            question = new Question
            {
                SubjectId = subject.Id,
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
            foreach(var item in questions)
            {
                Question question = repository.GetQuestion(item.Id);
                IEnumerable<Answer> answers = question.Answers.Value;
                IEnumerable<Fake> fakes = question.Fakes.Value;
                Console.WriteLine(item.Text);
                foreach(var item2 in answers)
                {
                    Console.WriteLine("- " + item2.Text);
                }
                foreach (var item2 in fakes)
                {
                    Console.WriteLine("- " + item2.Text);
                }
            }
            Console.WriteLine();
        }

        static void UpdateQuestion (IQuestionRepository repository)
        {
            Console.WriteLine("Update question: ");
            Question question = repository.Data.First();
            question.Text = "Updated";
            repository.Update(question);
            GetQuestions(repository);
        }

        static void GetQuestionAnswers(IQuestionRepository repository, Question question)
        {
            Console.WriteLine(question.Text);
            foreach(var item in repository.GetQuestionAnswers(question.Id))
            {
                Console.WriteLine("-" + item.Text);
            }
        }
        static void AddQuestionAnswer(IQuestionRepository repository)
        {
            Console.WriteLine("Add question answer: ");
            Question question = repository.Data.First();
            repository.AddQuestionAnswer(question.Id, new Answer { Text = "e" });
            GetQuestionAnswers(repository, question);
            Console.WriteLine();
        }
        static void DeleteQuestionAnswer(IQuestionRepository repository)
        {
            Console.WriteLine("Delete question answer: ");
            Question question = repository.Data.First();
            Answer answer = repository.GetQuestionAnswers(question.Id).Last();
            repository.DeleteAnswer(answer.Id);
            GetQuestionAnswers(repository, question);
            Console.WriteLine();
        }
        static void UpdateQuestionAnswer (IQuestionRepository repository)
        {
            Console.WriteLine("Update question answer: ");
            Question question = repository.Data.First();
            Answer answer = repository.GetQuestionAnswers(question.Id).Last();
            repository.UpdateAnswer(answer.Id, "e");
            GetQuestionAnswers(repository, question);
            Console.WriteLine();
        }

        static void GetQuestionFakes(IQuestionRepository repository, Question question)
        {
            Console.WriteLine(question.Text);
            foreach (var item in repository.GetQuestionFakes(question.Id))
            {
                Console.WriteLine("-" + item.Text);
            }
        }
        static void AddQuestionFake(IQuestionRepository repository)
        {
            Console.WriteLine("Add question fake: ");
            Question question = repository.Data.First();
            repository.AddQuestionFake(question.Id, new Fake { Text = "f" });
            GetQuestionFakes(repository, question);
            Console.WriteLine();
        }
        static void DeleteQuestionFake(IQuestionRepository repository)
        {
            Console.WriteLine("Delete question fake: ");
            Question question = repository.Data.First();
            Fake fake = repository.GetQuestionFakes(question.Id).Last();
            repository.DeleteFake(fake.Id);
            GetQuestionFakes(repository, question);
            Console.WriteLine();
        }
        static void UpdateQuestionFake(IQuestionRepository repository)
        {
            Console.WriteLine("Update question fake: ");
            Question question = repository.Data.First();
            Fake fake= repository.GetQuestionFakes(question.Id).Last();
            repository.UpdateFake(fake.Id, "f");
            GetQuestionFakes(repository, question);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Question repository test");
            IQuestionRepository repository = InjectQuestionRepository();
            ISubjectRepository subjectRepository = InjectSubjectRepository();
            IDbContextScopeFactory dbContextScopeFactory = InjectScopeFactory();
            using (var context = dbContextScopeFactory.Create())
            {
                Prepare(repository, subjectRepository);
                AddSubjects(subjectRepository);
                AddQuestions(repository, subjectRepository);
                GetQuestions(repository);
                AddQuestionAnswer(repository);
                DeleteQuestionAnswer(repository);
                UpdateQuestionAnswer(repository);
                AddQuestionFake(repository);
                DeleteQuestionFake(repository);
                UpdateQuestionFake(repository);
                UpdateQuestion(repository);
                context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
