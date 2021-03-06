using DLL.EF;
using DLL.Entities;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DLL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly HotelContext db;
        private RoomRepositories roomRepositories;
        private UserRepositories userRepositories;
        private GuestRepositories guestRepositories;
        private CategoryRepositories categoryRepositories;
        private SettlementRepositories settlementRepositories;

        public EFUnitOfWork(HotelContext context) 
        {
            db = context;
        }

        public IRepositories<Room> Rooms 
        {
            get 
            {
                if (roomRepositories == null)
                    roomRepositories = new RoomRepositories(db);
                return roomRepositories;
            }
        }

        public IRepositories<Guest> Guests
        {
            get 
            {
                if (guestRepositories == null)
                    guestRepositories = new GuestRepositories(db);
                return guestRepositories;
            }
        }

        public UserRepositories Users
        {
            get 
            {
                if (userRepositories == null)
                    userRepositories = new UserRepositories(db);
                return userRepositories;
            }
        }

        public IRepositories<Category> Categories
        {
            get 
            {
                if (categoryRepositories == null)
                    categoryRepositories = new CategoryRepositories(db);
                return categoryRepositories;
            }
        }

        public SettlementRepositories Settlements
        {
            get 
            {
                if (settlementRepositories == null)
                    settlementRepositories = new SettlementRepositories(db);
                return settlementRepositories;
            }
        }

        public void Save() 
        {
            db.SaveChanges();
        }

        private bool disosed = false;

        public virtual void Dispose(bool disposing) 
        {
            if (!this.disosed) 
            {
                db.Dispose();
            }
            this.disosed = true;
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
