﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int ResultId { get; set; }

        public bool IsRight { get; set; }


        public Lazy<Question> Question { get; set; }
    }
}
