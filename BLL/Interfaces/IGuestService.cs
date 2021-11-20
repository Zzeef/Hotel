using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IGuestService
    {
        IEnumerable<Guest> GetAll { get; }
        OperationDetails Add(Guest item);
        OperationDetails Delete(Guid id);
        Task<Guest> FindByIdAsync(Guid id);
    }
}
