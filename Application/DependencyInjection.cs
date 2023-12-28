using FluentValidation;
using Infrastructure.Database.Repositories.Cats;
using Infrastructure.Database.Repositories.UserAnimalRepository;
using Infrastructure.Database.Repositories.Users;
using Infrastructure.Database.Repository;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAnimalRepository, UserAnimalRepository>();
            services.AddScoped<ICatRepository, CatRepository>();


            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
