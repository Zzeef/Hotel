using BLL.DTO;
using System.Collections.Generic;

namespace Hotel.WEB.Model
{
    public class RoomCategoryModel
    {
        public IEnumerable<RoomDTO> Rooms { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
