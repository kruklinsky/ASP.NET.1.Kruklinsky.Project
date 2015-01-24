using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class QuestionCreationServise : RepositoryService<IQuestionRepository>, IQuestionCreationService
    {
        public QuestionCreationServise(IQuestionRepository questionRepository, IDbContextScopeFactory dbContextScopeFactory) : base(questionRepository,dbContextScopeFactory) {}

        #region IQuestionCreationServise

        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description)
        {
            SubjectExceptionsHelper.GetIdExceptions(subjectId);
            QuestionExceptionsHelper.GetLevelExceptions(level);
            QuestionExceptionsHelper.GetTopicExcetpions(topic);
            QuestionExceptionsHelper.GetTextExceptions(text);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Add(newQuestion.ToDal());
                context.SaveChanges();
            }
        }
        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            SubjectExceptionsHelper.GetIdExceptions(subjectId);
            QuestionExceptionsHelper.GetLevelExceptions(level);
            QuestionExceptionsHelper.GetTopicExcetpions(topic);
            QuestionExceptionsHelper.GetTextExceptions(text);
            AnswerExceptionsHelper.GetAnswersExceptions(answers);
            AnswerExceptionsHelper.GetFakesExceptions(fakes);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Add(newQuestion.ToDal(), answers.Select(a => a.ToDal()).ToList(), fakes.Select(f => f.ToDal()).ToList());
                context.SaveChanges();
            }
        }
        public bool DeleteQuestion(int id)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var question = this.repository.GetQuestion(id);
                if (question != null)
                {
                    this.repository.Delete(question);
                    result = true;
                }
                context.SaveChanges();
            }
            return result;
        }
        public void UpdateQuestion(Question question)
        {
            QuestionExceptionsHelper.GetQuestionsEceptions(question);
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.Update(question.ToDal());
                context.SaveChanges();
            }
        }

        #endregion
    }
}
