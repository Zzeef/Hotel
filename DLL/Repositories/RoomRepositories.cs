using DLL.EF;
using DLL.Entities;

namespace DLL.Repositories
{
    class RoomRepositories : Repositories<Room>
    {
        readonly HotelContext context;

        public RoomRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
