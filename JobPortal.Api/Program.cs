using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.IO;

namespace JobPortal.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        public static void Main(string[] args)
        {
                string connectionStrings = Configuration.GetConnectionString("ConnectionString");

                var informationSinkOpt = new MSSqlServerSinkOptions
                {
                    TableName = "WebApiLogs",
                };

                var debugSinkOpt = new MSSqlServerSinkOptions
                {
                    TableName = "ErrorLogs",
                };

             Log.Logger = new LoggerConfiguration()
                              .MinimumLevel.Debug()
                              .WriteTo.Conditional(
                ev =>
                {
                    // We want to only log information level logs to this table
                    bool isInformation = ev.Level == LogEventLevel.Information;
                    if (isInformation) { return true; }
                    return false;
                },
                wt => wt.MSSqlServer(
                    connectionString: connectionStrings,
                    sinkOptions: informationSinkOpt
                    )
            )
            .WriteTo.Conditional(
                ev => {
                    // We want to only log debug level logs to this table
                    bool isDebug = ev.Level == LogEventLevel.Error;
                    if (isDebug) { return true; }
                    return false;
                },
                wt => wt.MSSqlServer(
                    connectionString: connectionStrings,
                    sinkOptions: debugSinkOpt))
                    .CreateLogger();

                    CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
