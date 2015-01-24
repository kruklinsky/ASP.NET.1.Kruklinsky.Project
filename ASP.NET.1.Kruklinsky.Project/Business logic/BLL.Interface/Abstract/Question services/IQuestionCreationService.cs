using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IQuestionCreationService
    {
        void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description);
        void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description,
            IEnumerable<Answer> answers, IEnumerable<Fake> fakes);
        bool DeleteQuestion(int id);
        void UpdateQuestion(Question question);
    }
}
