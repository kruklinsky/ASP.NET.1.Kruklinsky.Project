using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class QuestionExceptionsHelper
    {
        public static void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }
        public static void GetLevelExceptions(int level)
        {
            if (level < 0)
            {
                throw new System.ArgumentOutOfRangeException("level", level, "Question level is less than zero.");
            }
        }
        public static void GetTopicExcetpions(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException("Question topic is null, empty or consists only of white-space characters.", "topic");
            }
        }
        public static void GetTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Question text is null, empty or consists only of white-space characters.", "text");
            }
        }

        public static void GetQuestionsEceptions(IEnumerable<Question> questions)
        {
            if (questions == null)
            {
                throw new System.ArgumentNullException("questions", "Questions is null.");
            }
            foreach (var item in questions)
            {
                QuestionExceptionsHelper.GetIdExceptions(item.Id);
                QuestionExceptionsHelper.GetLevelExceptions(item.Level);
                QuestionExceptionsHelper.GetTopicExcetpions(item.Topic);
                QuestionExceptionsHelper.GetTextExceptions(item.Text);
                if (item.Answers != null) AnswerExceptionsHelper.GetAnswersExceptions(item.Answers);
                if (item.Fakes != null) AnswerExceptionsHelper.GetFakesExceptions(item.Fakes);
            }
        }
        public static void GetQuestionsEceptions(params Question[] questions)
        {
            if (questions == null)
            {
                throw new System.ArgumentNullException("questions", "Questions is null.");
            }
            foreach (var item in questions)
            {
                QuestionExceptionsHelper.GetIdExceptions(item.Id);
                QuestionExceptionsHelper.GetLevelExceptions(item.Level);
                QuestionExceptionsHelper.GetTopicExcetpions(item.Topic);
                QuestionExceptionsHelper.GetTextExceptions(item.Text);
                if (item.Answers != null) AnswerExceptionsHelper.GetAnswersExceptions(item.Answers);
                if (item.Fakes != null) AnswerExceptionsHelper.GetFakesExceptions(item.Fakes);
            }
        }
    }
}
