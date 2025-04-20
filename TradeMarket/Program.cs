using Microsoft.EntityFrameworkCore;
using TradeMarket.Data;
using TradeMarket.IRepository;
using TradeMarket.Repository;
using Serilog;
using System.Text.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
//
//NOTE: make a log level switch
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console()
    .WriteTo.File("logs/trademarket.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddSerilog();
builder.Services.AddControllers().AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseNodaTime())
);
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

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
