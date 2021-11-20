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
            services.AddScoped<HotelContext>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddDbContext<HotelContext>(option => option.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HotelDB;Trusted_Connection=True;"));
            return services;
        }
    }
}
