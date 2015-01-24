using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete.ExceptionsHelpers
{
    public static class TestExceptionsHelper
    {
        public static void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }
        public static void GetNameExceptions(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("Test name is null, empty or consists only of white-space characters.", "name");
            }
        }
        public static void GetTopicExceptions(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new System.ArgumentException("Test topic is null, empty or consists only of white-space characters.", "topic");
            }
        }
        public static void GetDurationExceptions(int duration)
        {
            if (duration < 0)
            {
                throw new System.ArgumentOutOfRangeException("duration", duration, "Duration is less than zero.");
            }
        }
    }
}
