using DLL.Entities;
using DLL.Repositories;
using System;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositories<Room> Rooms { get; }
        IRepositories<Guest> Guests { get; }
        UserRepositories Users { get; }
        SettlementRepositories Settlements { get; }
        IRepositories<Category> Categories { get; }
        void Save();
    }
}
