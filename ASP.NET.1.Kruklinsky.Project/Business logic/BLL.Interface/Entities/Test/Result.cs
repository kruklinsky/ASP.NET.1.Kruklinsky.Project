using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class Result
    {
        public int ResultId { get; set; }

        public int TestId { get; set; }

        public Guid UserId { get; set; }

        public TimeSpan Time { get; set; }


        public virtual Test Test { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<UserAnswer> Answers { get; set; }
    }
}
