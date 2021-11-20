using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface ISettlementService
    {
        IEnumerable<Settlement> GetAll { get; }
        OperationDetails Add(Settlement item);
        OperationDetails Delete(Guid id);
        Task<Settlement> FindByIdAsync(Guid id);
    }
}
