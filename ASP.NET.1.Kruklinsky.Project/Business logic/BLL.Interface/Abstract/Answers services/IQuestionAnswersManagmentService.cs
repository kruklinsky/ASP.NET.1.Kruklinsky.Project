using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IQuestionAnswersManagmentService
    {
        int AddNewQuestionAnswers(int id, IEnumerable<Answer> answers);
        int DeleteQuestionAnswers(int id, IEnumerable<Answer> answers);

        int AddNewQuestionFakes(int id, IEnumerable<Fake> fakes);
        int DeleteQuestionFakes(int id, IEnumerable<Fake> fakes);

        void UpdateAnswer(int id, string text);
        void UpdateFake(int id, string text);
    }
}
