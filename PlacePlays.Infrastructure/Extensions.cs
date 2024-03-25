﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PlacePlays.Application.Models.Options;
using PlacePlays.Infrastructure.DAL;
using PlacePlays.Infrastructure.DAL.LiteDB;
using PlacePlays.Infrastructure.DAL.Repositories;

namespace PlacePlays.Infrastructure;

public static class Extensions
{
    private const string LiteDbSectionName = "LiteDb";
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LiteDbOptions>(configuration.GetSection(LiteDbSectionName));
        services.AddScoped<ISpotifyRepository, SpotifyRepository>();

        services.AddSingleton<SpotifyLiteDbContext>();
        
        return services;
    } 
}