using BLL.DTO;
using System.Collections.Generic;

namespace Hotel.WEB.Model
{
    public class RoomCreateModel
    {
        public IEnumerable<CategoryDTO> CategoriesList { get; set; }
        //public Category SelectCategory { get; set; }
        public RoomDTO Room { get; set; }
    }
}
