using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;

namespace BLL.Concrete
{
    public class KnowledgeService: IKnowledgeService
    {
        private ISubjectRepository subjectRepository;
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;

        public KnowledgeService(ISubjectRepository subjectRepository, IQuestionRepository questionRepository, ITestRepository testRepository)
        {
            if(subjectRepository == null)
            {
                throw new System.ArgumentNullException("subjectRepository", "Subject repository is null.");
            }
            if (questionRepository == null)
            {
                throw new System.ArgumentNullException("questionRepository", "Question repository is null.");
            }
            if (testRepository == null)
            {
                throw new System.ArgumentNullException("testRepository", "Test repository is null.");
            }
            this.subjectRepository = subjectRepository;
            this.questionRepository = questionRepository;
            this.testRepository = testRepository;
        }

        #region Subject

        public Subject GetSubject(int id)
        {
            this.GetIdExceptions(id);
            Subject result = null;
            var subject = this.subjectRepository.GetSubject(id);
            if(subject != null)
            {
                result = subject.ToBll();
            }
            return result;
        }
        public IEnumerable<Subject> GetAllSubjects()
        {
            IEnumerable<Subject> result = new List<Subject>();
            var subjects = this.subjectRepository.Data;
            if (subjects.Count() != 0)
            {
                result = subjects.Select(s => s.ToBll()).ToList();
            }
            return result;
        }

        public void CreateSubject(string name, string description)
        {
            this.GetSubjectNameExceptions(name);
            this.GetSubjectDescriptionExceptions(description);
            var newSubject = new Subject 
            { 
                Name = name,
                Description = description 
            };
            this.subjectRepository.Add(newSubject.ToDal());
        }
        public bool DeleteSubject(int id)
        {
            this.GetIdExceptions(id);
            bool result = false;
            var subject = this.GetSubject(id);
            if (subject != null)
            {
                this.subjectRepository.Delete(subject.ToDal());
                result = true;
            }
            return result;
        }
        public void UpdateSubject(Subject subjects)
        {
            this.GetIdExceptions(subjects.Id);
            this.subjectRepository.Update(subjects.ToDal());
        }

        #endregion

        #region Question

        public Question GetQuestion(int id)
        {
            this.GetIdExceptions(id);
            Question result = null;
            var question = this.questionRepository.GetQuestion(id);
            if(question != null)
            {
                result = question.ToBll();
            }
            return result;
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            IEnumerable<Question> result = new List<Question>();
            var questions = this.questionRepository.Data;
            if (questions.Count() != 0)
            {
                result = questions.Select(q => q.ToBll()).ToList();
            }
            return result;
        }

        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description)
        {
            this.GetIdExceptions(subjectId, "subjectId");
            this.GetQuestionLevelExceptions(level);
            this.GetQuestionTopicExcetpions(topic);
            this.GetQuestionTextExceptions(text);
            this.GetQuestionExampleExceptions(example);
            this.GetQuestionDescriptionExceptions(description);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            this.questionRepository.Add(newQuestion.ToDal());
        }
        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            this.GetIdExceptions(subjectId, "subjectId");
            this.GetQuestionLevelExceptions(level);
            this.GetQuestionTopicExcetpions(topic);
            this.GetQuestionTextExceptions(text);
            this.GetQuestionExampleExceptions(example);
            this.GetQuestionDescriptionExceptions(description);
            this.GetQuestionAnswersExceptions(answers);
            this.GetQuestionFakesExceptions(fakes);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            this.questionRepository.Add(newQuestion.ToDal(), answers.Select(a => a.ToDal()).ToList(), fakes.Select(f => f.ToDal()).ToList());
        }
        public bool DeleteQuestion(int id)
        {
            this.GetIdExceptions(id);
            bool result = false;
            var question = this.GetQuestion(id);
            if(question != null)
            {
                this.questionRepository.Delete(question.ToDal());
                result = true;
            }
            return result;
        }
        public void UpdateQuestion(Question question)
        {
            this.GetIdExceptions(question.Id);
            this.GetQuestionLevelExceptions(question.Level);
            this.GetQuestionTopicExcetpions(question.Topic);
            this.GetQuestionTextExceptions(question.Text);
            this.GetQuestionExampleExceptions(question.Example);
            this.GetQuestionDescriptionExceptions(question.Description);
            this.questionRepository.Update(question.ToDal());
        }

        public int AddNewQuestionAnswers(int id, IEnumerable<Answer> answers)
        {
            this.GetIdExceptions(id);
            this.GetQuestionAnswersExceptions(answers);
            int result = 0;
            foreach (var item in answers)
            {
                this.questionRepository.AddQuestionAnswer(id, item.ToDal());
                result++;
            }
            return result;
        }
        public int DeleteQuestionAnswers(int id, IEnumerable<Answer> answers)
        {
            this.GetIdExceptions(id);
            this.GetQuestionAnswersExceptions(answers);
            int result = 0;
            foreach (var item in answers)
            {
                this.questionRepository.DeleteAnswer(item.Id);
                result++;
            }
            return result;
        }

        public int AddNewQuestionFakes(int id, IEnumerable<Fake> fakes)
        {
            this.GetIdExceptions(id);
            this.GetQuestionFakesExceptions(fakes);
            int result = 0;
            foreach (var item in fakes)
            {
                this.questionRepository.AddQuestionFake(id, item.ToDal());
                result++;
            }
            return result;
        }
        public int DeleteQuestionFakes(int id, IEnumerable<Fake> fakes)
        {
            this.GetIdExceptions(id);
            this.GetQuestionFakesExceptions(fakes);
            int result = 0;
            foreach (var item in fakes)
            {
                this.questionRepository.DeleteFake(item.Id);
                result++;
            }
            return result;
        }

        public void UpdateAnswer(int id, string text)
        {
            this.GetIdExceptions(id);
            this.GetAnswerTextExceptions(text);
            this.questionRepository.UpdateAnswer(id, text);
        }
        public void UpdateFake(int id, string text)
        {
            this.GetIdExceptions(id);
            this.GetFakeTextExceptions(text);
            this.questionRepository.UpdateFake(id, text);
        }

        #endregion

        #region Test

        public Test GetTest(int id)
        {
            this.GetIdExceptions(id);
            Test result = null;
            var test = this.testRepository.GetTest(id);
            if (test != null)
            {
                result = test.ToBll();
            }
            return result;
        }
        public IEnumerable<Test> GetAllTests()
        {
            IEnumerable<Test> result = new List<Test>();
            var tests = this.testRepository.Data;
            if(tests.Count() != 0)
            {
                result = tests.Select(t => t.ToBll()).ToList();
            }
            return result;
        }

        public void CreateTest(int subjectId, string name, string topic, string description)
        {
            this.GetIdExceptions(subjectId, "subjectId");
            this.GetTestNameExceptions(name);
            this.GetTestTopicExceptions(topic);
            this.GetTestDescriptionExceptions(description);
            Test newTest = new Test
            {
                SubjectId = subjectId,
                Name = name,
                Topic = topic,
                Description = description
            };
            this.testRepository.Add(newTest.ToDal());
        }

        public bool DeleteTest(int id)
        {
            this.GetIdExceptions(id);
            bool result = false;
            var test = this.GetTest(id);
            if (test != null)
            {
                this.testRepository.Delete(test.ToDal());
                result = true;
            }
            return result;
        }
        public void UpdateTest(Test test)
        {
            this.GetIdExceptions(test.Id);
            this.GetTestNameExceptions(test.Name);
            this.GetTestTopicExceptions(test.Topic);
            this.GetTestDescriptionExceptions(test.Description);
            this.testRepository.Update(test.ToDal());
        }

        public int AddTestQuestions(int id, IEnumerable<int> questionsId)
        {
            this.GetIdExceptions(id);
            foreach (var item in questionsId)
            {
                this.GetIdExceptions(item, "questionId");
            }
            int result = 0;
            foreach (var item in questionsId)
            {
                this.testRepository.AddTestQuestion(id, item);
                result++;
            }
            return result;
        }
        public int AddNewTestQuestions(int id, IEnumerable<Question> questions)
        {
            this.GetIdExceptions(id);
            this.GetQuestionsEceptions(questions);
            int result = 0;
            foreach (var item in questions)
            {
                this.testRepository.AddTestQuestion(id, item.ToDal(), item.Answers.Value.Select(a => a.ToDal()).ToList(), item.Fakes.Value.Select(f => f.ToDal()).ToList());
                result++;
            }
            return result;
        }
        public int DeleteTestQuestions(int id, IEnumerable<int> questionsId)
        {
            this.GetIdExceptions(id);
            foreach (var item in questionsId)
            {
                this.GetIdExceptions(item, "questionId");
            }
            int result = 0;
            foreach (var item in questionsId)
            {
                this.testRepository.DeleteTestQuestion(id, item);
                result++;
            }
            return result;
        }

        #endregion

        #region Private methods

        private void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }
        private void GetIdExceptions(int id, string paramName)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException(paramName, id, "Id is less than zero.");
            }
        }

        #region Subject

        private void GetSubjectNameExceptions(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("Subject name is null, empty or consists only of white-space characters.", "name");
            }
        }
        private void GetSubjectDescriptionExceptions(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new System.ArgumentException("Subject description is null, empty or consists only of white-space characters.", "description");
            }
        }

        #endregion

        #region Question

        private void GetQuestionLevelExceptions(int level)
        {
            if (level < 0)
            {
                throw new System.ArgumentOutOfRangeException("level", level, "Question level is less than zero.");
            }
        }
        private void GetQuestionTopicExcetpions(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException("Question topic is null, empty or consists only of white-space characters.", "topic");
            }
        }
        private void GetQuestionTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Question text is null, empty or consists only of white-space characters.", "text");
            }
        }
        private void GetQuestionExampleExceptions(string example)
        {
            if (string.IsNullOrWhiteSpace(example))
            {
                throw new System.ArgumentException("Question example is null, empty or consists only of white-space characters.", "example");
            }
        }
        private void GetQuestionDescriptionExceptions(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new System.ArgumentException("Question description is null, empty or consists only of white-space characters.", "description");
            }
        }
        private void GetQuestionAnswersExceptions(IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                throw new System.ArgumentNullException("answers", "Answers is null.");
            }
            foreach (var item in answers)
            {
                this.GetAnswerTextExceptions(item.Text);
            }
        }
        private void GetQuestionFakesExceptions(IEnumerable<Fake> fakes)
        {
            if (fakes == null)
            {
                throw new System.ArgumentNullException("fakes", "Fakes is null.");
            }
            foreach (var item in fakes)
            {
                this.GetFakeTextExceptions(item.Text);
            }
        }

        private void GetAnswerTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Answer text is null, empty or consists only of white-space characters.");
            }
        }
        private void GetFakeTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Fake text is null, empty or consists only of white-space characters.");
            }
        }

        #endregion

        #region Test

        private void GetTestNameExceptions(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("Test name is null, empty or consists only of white-space characters.", "name");
            }
        }
        private void GetTestTopicExceptions(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException("Test topic is null, empty or consists only of white-space characters.", "topic");
            }
        }
        private void GetTestDescriptionExceptions(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new System.ArgumentException("Test description is null, empty or consists only of white-space characters.", "description");
            }
        }

        private void GetQuestionsEceptions(IEnumerable<Question> questions)
        {
            if (questions == null)
            {
                throw new System.ArgumentNullException("questions", "Questions is null.");
            }
            foreach (var item in questions)
            {
                this.GetIdExceptions(item.SubjectId, "subjectId");
                this.GetQuestionLevelExceptions(item.Level);
                this.GetQuestionTopicExcetpions(item.Topic);
                this.GetQuestionTextExceptions(item.Text);
                this.GetQuestionExampleExceptions(item.Example);
                this.GetQuestionDescriptionExceptions(item.Description);
                this.GetQuestionAnswersExceptions(item.Answers.Value);
                this.GetQuestionFakesExceptions(item.Fakes.Value);
            }

        }

        #endregion

        #endregion
    }
}
