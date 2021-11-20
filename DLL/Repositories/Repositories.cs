using DLL.EF;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    class Repositories<T> : IRepositories<T> where T : class
    {
        readonly HotelContext context;
        readonly DbSet<T> db;

        public Repositories(HotelContext context) 
        {
            this.context = context;
            db = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return db;
        }

        public async Task<T> GetByIdAsync(Guid id) 
        {
            return await db.FindAsync(id);
        }

        public void Create(T item) 
        {
            db.Add(item);
        }

        public void Update(T item) 
        {
            context.Entry<T>(item).State = EntityState.Modified;    
        }

        public void Delete(Guid id) 
        {
            T item = db.Find(id);
            if (item != null) 
            {
                db.Remove(item);
            }
        }

        public async Task<int> SaveChangeAsync() 
        {
            return await context.SaveChangesAsync();
        }
    }
}
