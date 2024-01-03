using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Infrastructure.Database.Repositories.Cats;
using Infrastructure.Database.Repositories.Users;
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
            services.AddSingleton<FakeDatabase>();
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IBirdRepository, BirdRepository>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql("Server=127.0.0.1;Database=clean-api2;User=root;Password=Bajsan123;",
                  new MySqlServerVersion(new Version(8, 2, 0)));

            });
            return services;
        }
    }
}
