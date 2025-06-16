using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TradeMarket.Data;
using TradeMarket.IRepository;
using TradeMarket.Repository;
using TradeMarket.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//NOTE: make a log level switch and move this somewhere
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // From appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Information,
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .WriteTo.File("logs/trademarket.txt", Serilog.Events.LogEventLevel.Information,
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Logging.AddConsole();
builder.Services.AddSerilog();
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    });

var secretKey = config["JwtSettings:SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = builder.Configuration["Jwt:Issuer"],
              ValidAudience = builder.Configuration["Jwt:Audience"],
              RoleClaimType = "role",
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
          };
      });
builder.Services.AddAuthorization();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins().AllowAnyOrigin().AllowAnyHeader();
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            o => o.UseNodaTime()
        )
        .EnableSensitiveDataLogging()
);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
