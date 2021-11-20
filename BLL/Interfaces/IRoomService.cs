using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll { get; }
        OperationDetails Add(Room item);
        OperationDetails Delete(Guid id);
        Task<Room> FindByIdAsync(Guid id);
    }
}
