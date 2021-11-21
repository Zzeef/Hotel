using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        OperationDetails Add(User item);
        OperationDetails Delete(Guid id);
        Task<User> FindByIdAsync(Guid id);
    }
}
