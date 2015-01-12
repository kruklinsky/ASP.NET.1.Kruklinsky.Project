using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ORM.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
