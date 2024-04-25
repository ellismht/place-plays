using PlacePlays.WebApi.DAL;
using PlacePlays.WebApi.Endpoints;
using PlacePlays.WebApi.Models.Options;

const string MongoDbSectionName = "MongoDb";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(MongoDbSectionName));

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

//app.UseHttpsRedirection();

app.Run();