using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ISettlementService
    {
        IEnumerable<SettlementDTO> GetSettlements();
        OperationDetails AddSettlement(SettlementDTO item);
        OperationDetails DeleteSettlement(Guid id);
        OperationDetails UpdateSettlement(SettlementDTO item);
        Task<SettlementDTO> FindSettlementByIdAsync(Guid id);
        SettlementDTO FindSettlementsByGuestId(Guid id);
        bool ExistSettlement(SettlementDTO item);
        void Dispose();
    }
}
