using BLL.DTO;
using System.Collections.Generic;

namespace Hotel.WEB.Model
{
    public class SettlementModel
    {
        public IEnumerable<RoomDTO> Rooms { get; set; }
        public IEnumerable<GuestDTO> Guests { get; set; }
        public IEnumerable<SettlementDTO> Settlements { get; set; }
    }
}
