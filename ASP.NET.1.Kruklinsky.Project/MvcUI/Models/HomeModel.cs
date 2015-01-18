using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Testing
    {
        public Test Test { get; set; }
        public List<QuestionEditor> Questions { get; set; }
        public List<Answers> Answers { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class Answers
    {
        public List<AnswerPair> UserAnswers { get; set; }
    }

    public class AnswerPair
    {
        public bool IsRight { get; set; }
        public bool UserAnswer { get; set; }
        public string Text { get; set; }
    }
}