using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SettlementService : ISettlementService
    {
        private IUnitOfWork Database { get; set; }

        public SettlementService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        public OperationDetails AddSettlement(SettlementDTO item)
        {
            if (ExistSettlement(item)) 
            {
                return new OperationDetails(false, "");
            }

            Settlement settlement = new Settlement() 
            {
                Id = item.Id,
                RoomId = item.RoomId,
                GuestId = item.GuestId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CheckIn = item.CheckIn
            };

            Database.Settlements.Create(settlement);
            Database.Save();

            return new OperationDetails(true, "");
        }

        public OperationDetails DeleteSettlement(Guid id)
        {
            Settlement settlement = Database.Settlements.Get(id);
            if (settlement == null) 
            {
                return new OperationDetails(false, "");
            }

            Database.Settlements.Delete(id);
            Database.Save();
            return new OperationDetails(true, "");
        }

        public OperationDetails UpdateSettlement(SettlementDTO item)
        {
            Settlement settlement = new Settlement()
            {
                Id = item.Id,
                RoomId = item.RoomId,
                GuestId = item.GuestId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CheckIn = item.CheckIn
            };
            Database.Settlements.Update(settlement);
            Database.Save();
            return new OperationDetails(true, "Данные обновлены");
        }

        public async Task<SettlementDTO> FindSettlementByIdAsync(Guid id)
        {
            Settlement settlement = await Task.Run(() => Database.Settlements.Get(id));

            if (settlement == null)
                return null;

            SettlementDTO settlementDTO = new SettlementDTO() 
            {
                Id = settlement.Id,
                RoomId = settlement.RoomId,
                GuestId = settlement.GuestId,
                StartDate = settlement.StartDate,
                EndDate = settlement.EndDate,
                CheckIn = settlement.CheckIn
            };

            return settlementDTO;
        }

        public SettlementDTO FindSettlementsByGuestId(Guid id)
        {
            Settlement settlement = Database.Settlements.FindByGuestId(id);

            if (settlement == null)
                return null;

            SettlementDTO settlementDTO = new SettlementDTO()
            {
                Id = settlement.Id,
                RoomId = settlement.RoomId,
                GuestId = settlement.GuestId,
                StartDate = settlement.StartDate,
                EndDate = settlement.EndDate,
                CheckIn = settlement.CheckIn
            };

            return settlementDTO;
        }

        public IEnumerable<SettlementDTO> GetSettlements()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Settlement, SettlementDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Settlement>, List<SettlementDTO>>(Database.Settlements.GetAll());
        }

        public bool ExistSettlement(SettlementDTO item)
        {
            Settlement settlement = Database.Settlements.Get(item.Id);
            if (settlement != null)
                return true;
            return false;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
