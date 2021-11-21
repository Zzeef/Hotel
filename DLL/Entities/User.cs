using System;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public enum UserRoles
    {
        Admin,
        User
    }

    public class User
    {
        [Key]
        public string Login { get; set; }

        public string Password { get; set; }

        public UserRoles Role { get; set; }

        public Guid GuestId { get; set; }

        public virtual Guest Guest { get; set; }
    }
}
