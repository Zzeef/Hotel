using DLL.Entities;
using System;

namespace BLL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public Guid GuestId { get; set; }

        public virtual Guest Guest { get; set; }
    }
}
