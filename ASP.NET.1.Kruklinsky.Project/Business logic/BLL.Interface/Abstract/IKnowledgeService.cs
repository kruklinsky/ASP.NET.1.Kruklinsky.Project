using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IKnowledgeService
    {
        #region Subject

        Subject GetSubject(int id);
        IEnumerable<Subject> GetAllSubjects();

        void CreateSubject(string name, string description);
        bool DeleteSubject(int id);
        void UpdateSubject(Subject subjects);

        #endregion

        #region Question

        Question GetQuestion(int id);
        IEnumerable<Question> GetAllQuestions();

        void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description);
        void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description,
            IEnumerable<Answer> answers, IEnumerable<Fake> fakes);
        bool DeleteQuestion(int id);
        void UpdateQuestion(Question question);

        int AddNewQuestionAnswers(int id, IEnumerable<Answer> answers);
        int DeleteQuestionAnswers(int id, IEnumerable<Answer> answers);

        int AddNewQuestionFakes(int id, IEnumerable<Fake> fakes);
        int DeleteQuestionFakes(int id, IEnumerable<Fake> fakes);

        void UpdateAnswer(int id, string text);
        void UpdateFake(int id, string text);

        #endregion

        #region Test

        Test GetTest(int id);
        IEnumerable<Test> GetAllTests();

        void CreateTest(int subjectId, string name, string topic, string description);
        bool DeleteTest(int id);
        void UpdateTest(Test test);

        int AddTestQuestions(int id, IEnumerable<int> questionsId);
        int AddNewTestQuestions(int id, IEnumerable<Question> questions);
        int DeleteTestQuestions(int id, IEnumerable<int> questionsId);

        #endregion
    }
}
