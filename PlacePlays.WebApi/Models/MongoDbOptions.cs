namespace PlacePlays.WebApi.Models;

public class MongoDbOptions
{
    public string ConnectionUri { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}