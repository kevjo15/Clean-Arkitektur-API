using Infrastructure.Database;
using Infrastructure.Database.Repository;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<RealDatabase>();
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql("Server=127.0.0.1;Database=clean-api2;User=root;Password=Bajsan123;",
                  new MySqlServerVersion(new Version(8, 2, 0)));

            });
            return services;
        }
    }
}
