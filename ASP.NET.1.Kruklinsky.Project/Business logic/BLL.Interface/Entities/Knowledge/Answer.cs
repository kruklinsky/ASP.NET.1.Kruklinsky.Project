using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public string Text { get; set; }


        public virtual Question Question { get; set; }
    }
}
