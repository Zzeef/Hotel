using DLL.Entities;
using System;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositories<Guest> Guests { get; }
        IRepositories<User> Users { get; }
        IRepositories<Settlement> Settlements { get; }
        IRepositories<Category> Categories { get; }
        void Save();
    }
}
