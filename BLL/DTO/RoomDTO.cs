using System;
using DLL.Entities;

namespace BLL.DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
