using MvcUI.Infrastructure.Abstract;
using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Infrastructure.Concrete
{
    public class TestSessionFactory : ITestSessionFactory
    {
        public ITestSession GetTestSession()
        {
            ITestSession result = (ITestSession)HttpContext.Current.Session["TestSession"];
            if (result == null)
            {
                result = new TestSession();
                HttpContext.Current.Session["TestSession"] = result;
            }
            return result;
        }
    }

    public class TestSession : ITestSession
    {
        private bool isStarted;
        private DateTime startTime;
        private int resultId;

        public bool IsStarted
        {
            get
            {
                return this.isStarted;
            }
        }
        public DateTime StartTime
        {
            get
            {
                return this.startTime.ToLocalTime();
            }
        }
        public int ResultId
        {
            get
            {
                return this.resultId;
            }
        }

        public Testing Test { get; private set; }

        public void Start(Testing test, int resultId)
        {
            this.startTime = DateTime.UtcNow;
            this.isStarted = true;
            this.Test = test;
            this.resultId = resultId;
            this.Test.Answers = PrepareAnswers();
            this.Test.StartTime = this.startTime;
        }
        public IEnumerable<BLL.Interface.Entities.UserAnswer> Finish(List<Answers> answers)
        {
            var result = new List<BLL.Interface.Entities.UserAnswer>();
            for (int i = 0; i < this.Test.Answers.Count(); i++)
            {
                result.Add(GetAnswer(answers, i));
            }
            this.FinishCart();
            return result;
        }

        #region Private methods

        private BLL.Interface.Entities.UserAnswer GetAnswer(List<Answers> answers, int i)
        {
            var result = new BLL.Interface.Entities.UserAnswer
            {
                ResultId = this.resultId,
                IsRight = this.IsRight(answers, i),
                QuestionId = this.Test.Questions[i].Question.Id
            };
            return result;
        }
        private bool IsRight(List<Answers> answers, int i)
        {
            for (int j = 0; j < Test.Answers[i].UserAnswers.Count(); j++)
            {
                Test.Answers[i].UserAnswers[j].UserAnswer = answers[i].UserAnswers[j].UserAnswer;
            }
            return Test.Answers[i].UserAnswers.Where(a => a.IsRight != a.UserAnswer).Count() == 0;
        }
        private void FinishCart()
        {
            this.isStarted = false;
            this.startTime = DateTime.MinValue;
            this.resultId = -1;
            this.Test = null;
        }

        private List<Answers> PrepareAnswers()
        {
            List<Answers> result = new List<Answers>();
            for (int i = 0; i < this.Test.Questions.Count(); i++)
            {
                Answers answers = new Answers { UserAnswers = new List<AnswerPair>() };
                answers.UserAnswers.AddRange(GetAnswers(i));
                answers.UserAnswers.AddRange(GetFakes(i));
                answers.UserAnswers.Shuffle();
                result.Add(answers);
            }
            return result;
        }
        private List<AnswerPair> GetFakes(int i)
        {
            var result = new List<AnswerPair>();
            for (int j = 0; j < this.Test.Questions[i].Fakes.Count(); j++)
            {
                result.Add(new AnswerPair
                {
                    IsRight = false,
                    UserAnswer = false,
                    Text = this.Test.Questions[i].Fakes[j].Text
                });
            }
            return result;
        }
        private List<AnswerPair> GetAnswers(int i)
        {
            var result = new List<AnswerPair>();
            for (int j = 0; j < this.Test.Questions[i].Answers.Count(); j++)
            {
                result.Add(new AnswerPair
                {
                    IsRight = true,
                    UserAnswer = false,
                    Text = this.Test.Questions[i].Answers[j].Text
                });
            }
            return result;
        }

        #endregion
    }

    public static class GBR
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}