using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class AnswerExceptionsHelper
    {
        public static void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }

        public static void GetAnswerTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Answer text is null, empty or consists only of white-space characters.");
            }
        }
        public static void GetFakeTextExceptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentException("Fake text is null, empty or consists only of white-space characters.");
            }
        }

        public static void GetAnswersExceptions(IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                throw new System.ArgumentNullException("answers", "Answers is null.");
            }
            foreach (var item in answers)
            {
                GetAnswerTextExceptions(item.Text);
            }
        }
        public static void GetFakesExceptions(IEnumerable<Fake> fakes)
        {
            if (fakes == null)
            {
                throw new System.ArgumentNullException("fakes", "Fakes is null.");
            }
            foreach (var item in fakes)
            {
                GetFakeTextExceptions(item.Text);
            }
        }
    }
}
