using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class Result
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public string UserId { get; set; }

        public TimeSpan Time { get; set; }
    }
}
