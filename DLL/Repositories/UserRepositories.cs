using DLL.EF;
using DLL.Entities;
using System.Linq;

namespace DLL.Repositories
{
    public class UserRepositories : Repositories<User>
    {
        readonly HotelContext context;

        public User FindByLogin(string login) 
        {
            return context.Users.Where(x => x.Login == login).First();
        }

        public UserRepositories(HotelContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}
