using Microsoft.EntityFrameworkCore;
using multitracks.API;
using multitracks.API.Extensions;
using multitracks.Core.Interfaces;
using multitracks.Core.Services;
using multitracks.Core.Utilities;
using multitracks.Infrastructure;
using multitracks.Infrastructure.Repositories;
using multitracks.Infrastructure.Settings;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Configure serilog
//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));

try
{
    logger.Information("Application is starting");
    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options=>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //options.JsonSerializerOptions.WriteIndented = true;
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
    builder.Services.AddScoped<ISongRepository, SongRepository>();
    builder.Services.AddScoped<IArtistService, ArtistService>();
    builder.Services.AddScoped<ISongService, SongService>();
    builder.Services.AddAutoMapper(typeof(Mapping));
    builder.Services.AddScoped<IUploadImageToAzureRepository, UploadImageToAzureRepository>();
    var dataBaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.Configure<AzureOptions>(builder.Configuration.GetSection("Azure"));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(dataBaseConnectionString);
    });
    builder.Services.AddHealthChecks()
       .AddSqlServer(dataBaseConnectionString,
       name: "sqlServer",
       timeout: TimeSpan.FromSeconds(3),
       tags: new[] { "ready" });

    var app = builder.Build();

    // Configure global exception.
    ConfigurationMethod.ConfigureGlobalExceptionHandler(app);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    // Configure healthchecks.
    ConfigurationMethod.ConfigiureHealthChecks(app);

    app.UseRequestResponseLoggerMiddleware();
    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequestAsync);

    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    logger.Fatal(ex, "Application fail to start");
}
finally
{
    logger.Dispose();
}