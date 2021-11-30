using System;
using BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetGuests();
        OperationDetails AddGuest(GuestDTO item);
        OperationDetails DeleteGuest(Guid id);
        Task<GuestDTO> FindGuestByIdAsync(Guid id);
        OperationDetails UpdateGuest(GuestDTO item);
        bool ExistGuest(GuestDTO id);
        void Dispose();
    }
}
