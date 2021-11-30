using System;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        OperationDetails AddUser(UserDTO item);
        OperationDetails DeleteUser(Guid id);
        Task<UserDTO> FindUserByIdAsync(Guid id);
        UserDTO FindUserByLogin(string login);
        bool ExistUser(UserDTO item);
        void Dispose();
    }
}
