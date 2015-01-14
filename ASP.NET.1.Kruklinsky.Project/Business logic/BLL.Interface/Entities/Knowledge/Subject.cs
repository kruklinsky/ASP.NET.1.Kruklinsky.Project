using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }


        public virtual ICollection<Test> Tests { get; set; }
    }
}
