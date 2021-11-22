using BLL.DTO;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork Database { get; set; }

        public RoomService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        public OperationDetails AddRoom(RoomDTO item)
        {
            if (ExistRoom(item)) 
            {
                return new OperationDetails(false, "Такая комната существует");
            }

            Room room = new Room() 
            {
                Id = item.Id,
                Number = item.Number,
                CategoryId = item.CategoryId
            };

            Database.Rooms.Create(room);
            Database.Save();

            return new OperationDetails(true, "Комната создана");
        }

        public OperationDetails DeleteRoom(Guid id)
        {
            Room room = Database.Rooms.Get(id);
            if (room == null) 
            {
                return new OperationDetails(false, "Данной комнаты не существует");
            }

            Database.Rooms.Delete(id);
            Database.Save();
            return new OperationDetails(true, "Комната удалена");
        }

        public OperationDetails UpdateRoom(RoomDTO item) 
        {
            //if (ExistRoom(item))
            //{
            //    return new OperationDetails(false, "Такая комната существует");
            //}

            Room room = new Room()
            {
                Id = item.Id,
                Number = item.Number,
                CategoryId =item.CategoryId
            };
            Database.Rooms.Update(room);
            Database.Save();
            return new OperationDetails(true, "Данные обновлены");
        }

        public async Task<RoomDTO> FindRoomByIdAsync(Guid id)
        {
            Room room = await Task.Run(() => Database.Rooms.Get(id));

            if (room == null)
                return null;

            RoomDTO roomDTO = new RoomDTO() 
            {
                Id = room.Id,
                Number = room.Number,
                CategoryId = room.CategoryId
            };

            return roomDTO;
        }

        public IEnumerable<RoomDTO> GetRooms()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public bool ExistRoom(RoomDTO item)
        {
            Room room = Database.Rooms.Get(item.Id);
            var list = Database.Rooms.GetAll().Where(x => x.Number == item.Number).ToList();
            if (room != null | list.Count > 0)
                return true;
            return false;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
