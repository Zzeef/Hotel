using DLL.EF;
using DLL.Entities;

namespace DLL.Repositories
{
    class CategoryRepositories : Repositories<Category>
    {
        readonly HotelContext context;

        public CategoryRepositories(HotelContext context)
            : base(context) 
        {
            this.context = context;
        }
    }
}
