using System;

namespace DLL.Entities
{
    public class Guest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime BithDate { get; set; }

        public int PassportId { get; set; }
    }
}
