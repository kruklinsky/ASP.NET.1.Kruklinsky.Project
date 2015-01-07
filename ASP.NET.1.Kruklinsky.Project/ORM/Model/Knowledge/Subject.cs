using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ORM.Model
{
    public class Subject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }


        public virtual ICollection<Test> Tests { get; set; }
    }
}
