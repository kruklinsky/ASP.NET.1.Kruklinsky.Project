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
        Test GetTest(int id);

        IEnumerable<Question> GetTestQuestions(int id);
        void AddTestQuestion(int id, int questionId);
        void AddTestQuestion(int id, Question question, IEnumerable<Answer> answers, IEnumerable<Fake> fakes);
        void DeleteTestQuestion(int id, int questionId);
    }
}
