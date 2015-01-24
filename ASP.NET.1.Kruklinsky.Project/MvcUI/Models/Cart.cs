using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Cart
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


        public void Start(Testing test,int resultId)
        {
            this.startTime = DateTime.UtcNow;
            this.isStarted = true;
            this.Test = test;
            this.resultId = resultId;
            this.Test.Answers = PrepareAnswers(this.Test);
            this.Test.StartTime = this.startTime;
        }

        public IEnumerable<BLL.Interface.Entities.UserAnswer> Finish (NameValueCollection form)
        {
            var result = new List<BLL.Interface.Entities.UserAnswer>();
            for (int i = 0; i < Test.Answers.Count(); i++)
            {
                var answer = new BLL.Interface.Entities.UserAnswer { ResultId = this.resultId};
                for (int j = 0; j < Test.Answers[i].UserAnswers.Count(); j++)
                {
                    string userAnser = form["Answers[" + i + "].UserAnswers[" + j + "].UserAnswer"];
                    Test.Answers[i].UserAnswers[j].UserAnswer = userAnser != "false";
                }
                answer.IsRight = Test.Answers[i].UserAnswers.Where(a => a.IsRight != a.UserAnswer).Count() == 0;
                answer.QuestionId = Test.Questions[i].Question.Id;
                result.Add(answer);
            }
            this.isStarted = false;
            this.startTime = DateTime.MinValue;
            this.resultId = -1;
            this.Test = null;
            return result;
        }

        private List<Answers> PrepareAnswers(Testing test)
        {
            List<Answers> result = new List<Answers>();
            for (int i = 0; i < test.Questions.Count(); i++)
            {
                Answers answers = new Answers { UserAnswers = new List<AnswerPair>() };
                for (int j = 0; j < test.Questions[i].Answers.Count(); j++)
                {
                    answers.UserAnswers.Add(new AnswerPair { IsRight = true, UserAnswer = false, Text = test.Questions[i].Answers[j].Text });
                }
                for (int j = 0; j < test.Questions[i].Fakes.Count(); j++)
                {
                    answers.UserAnswers.Add(new AnswerPair { IsRight = false, UserAnswer = false, Text = test.Questions[i].Fakes[j].Text });
                }
                answers.UserAnswers.Shuffle();
                result.Add(answers);
            }
            return result;
        }
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