using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PlacePlays.Infrastructure.DAL;
using PlacePlays.Infrastructure.DAL.Repositories;

namespace PlacePlays.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ISpotifyRepository, SpotifyRepository>(client =>
        {
            client.BaseAddress = new Uri("https://accounts.spotify.com/");
        });
        
        return services;
    } 
}