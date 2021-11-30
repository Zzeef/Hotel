using DLL.EF;
using DLL.Entities;
using System;
using System.Linq;

namespace DLL.Repositories
{
    public class SettlementRepositories : Repositories<Settlement>
    {
        readonly HotelContext context;

        public Settlement FindByGuestId(Guid id) 
        {
            return context.Settlements.Where(x => x.GuestId == id).First();
        }

        public SettlementRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
