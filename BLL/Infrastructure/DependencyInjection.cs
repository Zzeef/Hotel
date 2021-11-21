using BLL.Interfaces;
using BLL.Services;
using DLL.EF;
using DLL.Interfaces;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<HotelContext>(option => option
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HotelDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IGuestService, GuestService>();

            return services;    
        }
    }
}
