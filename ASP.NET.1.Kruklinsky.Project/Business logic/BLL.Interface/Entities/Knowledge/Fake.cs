using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Fake
    {
        public int FakeId { get; set; }

        public string Text { get; set; }


        public virtual Question Question { get; set; }
    }
}
