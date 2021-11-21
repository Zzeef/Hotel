using System;

namespace BLL.DTO
{
    public class GuestDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime BithDate { get; set; }

        public int PassportId { get; set; }
    }
}
