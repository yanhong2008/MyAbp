using Serilog;
using Serilog.Events;
using Zhaoxi.Forum.HttpApi.Host;

Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/logs.log"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac()
    .UseSerilog();

await builder.AddApplicationAsync<ForumHttpApiHostModule>();

var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();

