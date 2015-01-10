using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface ITestRepository: IRepository<Test>
    {
        Test GetTest(int Id);
        Test GetTest(int Id, out IEnumerable<Question> questions);

        IEnumerable<Question> GetTestQuestions(int Id);
        void AddTestQuestion(int Id, int questionId);
        void AddTestQuestion(int Id, Question question, IEnumerable<Answer> answers, IEnumerable<Fake> fakes);
        void DeleteTestQuestion(int Id, int questionId);
    }
}
