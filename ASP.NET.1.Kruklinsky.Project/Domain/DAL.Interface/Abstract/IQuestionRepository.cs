using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Question GetQuestion(int id);
        void Add(Question item, IEnumerable<Answer> answers, IEnumerable<Fake> fakes);

        Answer GetAnswer(int id);
        IEnumerable<Answer> GetQuestionAnswers(int id);
        void AddQuestionAnswer(int id, Answer answer);
        void DeleteAnswer(int id);
        void UpdateAnswer(int id, string text);

        Fake GetFake(int id);
        IEnumerable<Fake> GetQuestionFakes(int id);
        void AddQuestionFake(int id, Fake fake);
        void DeleteFake(int id);
        void UpdateFake(int id, string text);
    }
}
