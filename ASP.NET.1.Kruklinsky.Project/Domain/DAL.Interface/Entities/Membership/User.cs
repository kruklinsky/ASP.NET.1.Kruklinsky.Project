using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Interface.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreateDate { get; set; }


        public Lazy<Profile> Profile { get; set; }
        public Lazy<IEnumerable<Role>> Roles { get; set; }
    }
}
