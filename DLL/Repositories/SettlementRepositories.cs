using DLL.EF;
using DLL.Entities;

namespace DLL.Repositories
{
    class SettlementRepositories : Repositories<Settlement>
    {
        readonly HotelContext context;

        public SettlementRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
