using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.EF
{
    public class HotelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Category> Categories { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
