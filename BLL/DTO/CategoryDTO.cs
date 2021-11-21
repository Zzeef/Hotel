using System;

namespace BLL.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Bed { get; set; }

        public decimal Price { get; set; }
    }
}
