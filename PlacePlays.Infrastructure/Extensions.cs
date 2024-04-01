using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PlacePlays.Application.Abstractions;
using PlacePlays.Application.Models.Options;
using PlacePlays.Infrastructure.DAL;
using PlacePlays.Infrastructure.DAL.Repositories;

namespace PlacePlays.Infrastructure;

public static class Extensions
{
    private const string MongoDbSectionName = "MongoDb";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbSectionName));

        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IRepository, SpotifyRepository>();
        
        return services;
    } 
}