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
        Question GetQuestion(int Id, out IEnumerable<Answer> answers, out IEnumerable<Fake> fakes);
        void Add(Question item, IEnumerable<Answer> answers, IEnumerable<Fake> fakes);

        IEnumerable<Answer> GetQuestionAnswers(int Id);
        void AddQuestionAnswer(int Id, Answer answer);
        void DeleteAnswer(int Id);
        void UpdateAnswer(int Id, string text);

        IEnumerable<Fake> GetQuestionFakes(int Id);
        void AddQuestionFake(int Id, Fake fake);
        void DeleteFake(int Id);
        void UpdateFake(int Id, string text);
    }
}
