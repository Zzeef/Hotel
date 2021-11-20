using DLL.Entities;
using System;

namespace DLL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepositories<Guest> Guests { get; }
        IRepositories<User> Users { get; }
        IRepositories<Settlement> Settlemets { get; }
        IRepositories<Category> Categories { get; }
        void Save();
    }
}
