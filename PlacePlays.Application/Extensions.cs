using Microsoft.Extensions.DependencyInjection;
using PlacePlays.Application.Services.Spotify;

namespace PlacePlays.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISpotifyService, SpotifyService>();
        
        return services;
    } 
}