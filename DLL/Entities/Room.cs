using System;

namespace DLL.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
