using Application.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Serilog;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//var cosmos = new CosmosConfiguration();
//builder.Configuration.GetSection("Cosmos").Bind(cosmos);
//builder.Services.AddDbContext<AppDbContext>(op => op.UseCosmos(cosmos.Uri, cosmos.Key, cosmos.DatabaseName));
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/ApplicationLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);
//builder.Services.AddServices(builder.Configuration);
//builder.Services.AddRepositories();
//builder.Services.Configure<BlobStorageConfiguration>(builder.Configuration.GetSection("BlobStorage"));
builder.Services.AddDistributedMemoryCache(); // cache

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
