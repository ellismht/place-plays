using PlacePlays.WebApi.Endpoints.Tracks;

namespace PlacePlays.WebApi.Endpoints;

public static class EndpointExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/api")
            .MapEndpoint<InsertTrackLocationInfo>()
            .MapEndpoint<GetTracksInArea>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
}