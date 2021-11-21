using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetRooms();
        OperationDetails AddRoom(RoomDTO item);
        OperationDetails DeleteRoom(Guid id);
        Task<RoomDTO> FindRoomByIdAsync(Guid id);
        bool ExistRoom(RoomDTO item);
        void Dispose();
    }
}
