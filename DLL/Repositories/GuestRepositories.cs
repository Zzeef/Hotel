using DLL.EF;
using DLL.Entities;

namespace DLL.Repositories
{
    class GuestRepositories : Repositories<Guest>
    {
        readonly HotelContext context;

        public GuestRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
