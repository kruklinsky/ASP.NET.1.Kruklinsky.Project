using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class Result
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public Guid UserId { get; set; }

        public TimeSpan Time { get; set; }


        public Lazy<Test> Test { get; set; }

        public Lazy<User> User { get; set; }

        public Lazy<IEnumerable<UserAnswer>> Answers { get; set; }
    }
}
