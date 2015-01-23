using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Interface.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreateDate { get; set; }

        public IEnumerable<Role> Roles { get; set; }
        public Profile Profile { get; set; }
    }
}
