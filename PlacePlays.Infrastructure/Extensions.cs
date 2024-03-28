using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PlacePlays.Application.Models.Options;

namespace PlacePlays.Infrastructure;

public static class Extensions
{
    private const string MongoDbSectionName = "MongoDb";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbSectionName));

        
        return services;
    } 
}