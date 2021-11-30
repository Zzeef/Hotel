using BLL.DTO;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GuestService : IGuestService
    {
        private IUnitOfWork Database { get; set; }

        public GuestService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        public OperationDetails AddGuest(GuestDTO item)
        {
            if (ExistGuest(item)) 
            {
                return new OperationDetails(false, "Такой профиль уже есть");
            }
            Guest guest = new Guest()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic,
                BithDate = item.BithDate,
                PassportId = item.PassportId
            };

            Database.Guests.Create(guest);
            Database.Save();

            return new OperationDetails(true, "Профиль создан");
        }

        public OperationDetails DeleteGuest(Guid id)
        {
            Guest guest = Database.Guests.Get(id);
            if (guest == null) 
            {
                return new OperationDetails(false, "Такого профиля не существует");
            }

            Database.Guests.Delete(id);
            Database.Save();

            return new OperationDetails(true, "Профиль удален");
        }

        public async Task<GuestDTO> FindGuestByIdAsync(Guid id)
        {
            Guest guest = await Task.Run(() => Database.Guests.Get(id));

            if (guest == null)
                return null;

            GuestDTO guestDTO = new GuestDTO() 
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Patronymic = guest.Patronymic,
                BithDate = guest.BithDate,
                PassportId = guest.PassportId
            };

            return guestDTO;
        }

        public IEnumerable<GuestDTO> GetGuests()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Guest>, List<GuestDTO>>(Database.Guests.GetAll());
        }

        public OperationDetails UpdateGuest(GuestDTO item)
        {
            Guest guest = new Guest()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic,
                BithDate = item.BithDate,
                PassportId = item.PassportId
            };

            Database.Guests.Update(guest);
            Database.Save();
            return new OperationDetails(true, "Данные обновлены");
        }

        public bool ExistGuest(GuestDTO item)
        {
            Guest guest = Database.Guests.Get(item.Id);
            if (guest != null)
                return true;
            return false;
        }

        public void Dispose() 
        {
            Database.Dispose();
        }
    }
}
