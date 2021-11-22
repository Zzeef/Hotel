using AutoMapper;
using BLL.DTO;
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
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        public OperationDetails AddUser(UserDTO item)
        {
            if (ExistUser(item)) 
            {
                return new OperationDetails(false, "Такой пользователь уже есть");
            }
            User user = new User()
            { 
                Id = item.Id,
                Login = item.Login,
                Password = item.Password,
                Role = item.Role,
                GuestId = item.GuestId
            };

            Database.Users.Create(user);
            Database.Save();

            return new OperationDetails(true, "Пользователь создан");
        }

        public OperationDetails DeleteUser(Guid id)
        {
            User user = Database.Users.Get(id);
            if (user == null) 
            {
                return new OperationDetails(false, "Такого пользовател не существует");
            }

            Database.Users.Delete(id);
            Database.Save();

            return new OperationDetails(true, "Пользователь удален");
        }

        public async Task<UserDTO> FindUserByIdAsync(Guid id)
        {
            User user = await Task.Run(() => Database.Users.Get(id));
           
            if (user == null)
                return null;

            UserDTO userDTO = new UserDTO() 
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                GuestId = user.GuestId
            };

            return userDTO;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public bool ExistUser(UserDTO item)
        {
            User user = Database.Users.Get(item.Id);
            if (user != null)
                return true;
            return false;
        }

        public void Dispose() 
        {
            Database.Dispose();
        }
    }
}
