using Infrastructure.Database;
using Infrastructure.Database.Repositories.Users;
using Infrastructure.Database.Repository;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<RealDatabase>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDogRepository, DogRepository>();

            return services;
        }
    }
}
