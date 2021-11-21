using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ISettlementService
    {
        IEnumerable<Settlement> GetAll();
        OperationDetails Add(Settlement item);
        OperationDetails Delete(Guid id);
        Task<Settlement> FindByIdAsync(Guid id);
    }
}
