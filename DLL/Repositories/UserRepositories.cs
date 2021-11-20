using DLL.EF;
using DLL.Entities;

namespace DLL.Repositories
{
    class UserRepositories : Repositories<User>
    {
        readonly HotelContext context;

        public UserRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
