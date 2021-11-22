using BLL.DTO;

namespace Hotel.WEB.Model
{
    public class SettlementCreateModel
    {
        public RoomDTO Room { get; set; }

        public CategoryDTO Category { get; set; }

        public GuestDTO Guest { get; set; }

        public SettlementDTO Settlement { get; set; }
    }
}
