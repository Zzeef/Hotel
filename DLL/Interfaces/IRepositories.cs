using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepositories<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);      
        void Create(T item);
        void Update(T item);    
        void Delete(Guid id);
        Task<int> SaveChangeAsync();
    }
}
