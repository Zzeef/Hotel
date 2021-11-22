using System;

namespace DLL.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public Guid GuestId { get; set; }

        public virtual Guest Guest { get; set; }
    }
}
