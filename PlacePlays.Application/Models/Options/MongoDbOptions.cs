namespace PlacePlays.Application.Models.Options;

public class MongoDbOptions
{
    public string ConnectionUri { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}