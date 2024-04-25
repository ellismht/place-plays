namespace PlacePlays.WebApi.Endpoints;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}