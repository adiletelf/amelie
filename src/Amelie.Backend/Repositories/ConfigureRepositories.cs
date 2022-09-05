using Amelie.Backend.Repositories.Implementations;

namespace Amelie.Backend.Repositories;

public static class ConfigureRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        return services;
    }
}
