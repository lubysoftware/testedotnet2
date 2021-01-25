using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

namespace Luby.TimeManager.API
{
    public class Program
    {
        private static IConfiguration _configuration;
        public static void Main(string[] args)
        {
            setConfiguration();
            GetLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
                    {
                        //builder.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();

                
        private static void setConfiguration(){
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        private static void GetLogger()
        {
            var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            //.Enrich.WithProperty("Environment", this.EnvironmentName)
            .Enrich.WithProperty("App", "Luby.TimeManager.API")
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(_configuration["Elk:Host"]))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                ModifyConnectionSettings = a => a.BasicAuthentication(_configuration["Elk:User"], _configuration["Elk:Secret"])
            });
            Log.Logger = logger.CreateLogger();
        }
    }
}
