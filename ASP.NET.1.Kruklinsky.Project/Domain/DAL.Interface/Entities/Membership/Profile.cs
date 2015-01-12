using System;

namespace DAL.Interface.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
