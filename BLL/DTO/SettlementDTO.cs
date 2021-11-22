using DLL.Entities;
using System;

namespace BLL.DTO
{
    public class SettlementDTO
    {
        public Guid Id { get; set; }

        public Guid GuestId { get; set; }

        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool CheckIn { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }
    }
}
