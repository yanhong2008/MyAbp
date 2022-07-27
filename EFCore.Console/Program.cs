// See https://aka.ms/new-console-template for more information
using Domain;
using EFCore.Console;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");


await CreateHostBuilder(args).RunConsoleAsync();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
         .ConfigureServices((context, service) =>
         {
             service.AddSingleton<IImporter, ExcelImporter>();
             var config = context.Configuration;
             var strCon = config.GetConnectionString("Default");

             service.AddDbContext<DomainDbContext>(builder =>
             {
                 builder.UseMySql(strCon, ServerVersion.AutoDetect(strCon), action => action.MigrationsAssembly("EFCore.Console"));
             }); 
             service.AddHostedService<MyHostService>();
             service.AddHostedService< MyHostService2>();
         });
}


//var builder = WebApplication.CreateBuilder(args);
//builder.Host.AddAppSettingsSecretsJson()
//    .UseAutofac()
//    .UseSerilog();
//// Add services to the container.
//await builder.AddApplicationAsync<ForumHttpApiHostModule>();

//var app = builder.Build();
//// Configure the HTTP request pipeline.
//await app.InitializeApplicationAsync();
//await app.RunAsync();