using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }


        public IEnumerable<Test> Tests { get; set; }
    }
}
