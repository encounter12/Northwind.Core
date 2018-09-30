using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data;
using Northwind.Data.Models;
using Northwind.Data.Seed;
using System;
using System.Diagnostics;

namespace Northwind.Api
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var northwindContext = services.GetRequiredService<NorthwindContext>();
                    var masterContext = services.GetRequiredService<MasterContext>();

                    DatabaseInitializer.SeedData(northwindContext, masterContext);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("Configuration/appdata.json", optional: false, reloadOnChange: false);
                    config.AddCommandLine(args);
                })
                .UseStartup<Startup>();
        }
    }
}
