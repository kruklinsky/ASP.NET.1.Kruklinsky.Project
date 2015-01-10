using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using ORM.Model;

namespace KnowlegesTestConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            EFDbContext newOne = new EFDbContext();
            #region Add Questions
            //newOne.Questions.Add(new Question
            //{
            //    SubjectId = 1,
            //    Level = 1,
            //    Text = "What is",
            //    Topic = "asdf2",
            //    Answers = new List<Answer> { new Answer { Text = "asfd" }, new Answer { Text = "fdsa" }, },
            //    Fakes = new List<Fake> { new Fake { Text = "asfd" }, new Fake { Text = "fdsa" }, }
            //}
            //    );
            //newOne.SaveChanges();
            #endregion
            #region Get Questions
            //List<Question> questions = new List<Question>(newOne.Questions);
            //foreach (var item in questions)
            //{
            //    Console.WriteLine("Question: " + item.Text);
            //    Console.WriteLine("Subject: " + item.Subject.Name);
            //    foreach (var item2 in item.Answers)
            //    {
            //        Console.WriteLine("Answers: " + item2.Text);
            //    }
            //    foreach (var item2 in item.Fakes)
            //    {
            //        Console.WriteLine("Fakes: " + item2.Text);
            //    }
            //    Console.WriteLine();
            //}
            #endregion
            #region Add Tests
            //newOne.Tests.Add(new Test
            //{
            //    SubjectId = 1,
            //    Name = "Cool",
            //    Topic = "asdf2",
            //    Questions = new List<Question> (newOne.Questions.Where(q => q.Topic == "asdf2"))
            //});
            //newOne.SaveChanges();
            #endregion
            #region GetSubject tests
            //List<Subject> tests = new List<Subject>(newOne.Subjects);
            //foreach (var item in tests)
            //{
            //    Console.WriteLine("Subject: " + item.Name);
            //    foreach (var item2 in item.Tests)
            //    {
            //        Console.WriteLine("test: " + item2.Topic);
            //    }
            //}
            #endregion
            #region Get test
            List<Test> tests = new List<Test>(newOne.Tests);
            foreach (var item in tests)
            {
                Console.WriteLine("Test: " + item.Topic);
                foreach (var item2 in item.Questions)
                {
                    Console.WriteLine("Question: " + item2.Text);
                }
            }
            #endregion
            Console.WriteLine("Ok");
        }
    }
}
