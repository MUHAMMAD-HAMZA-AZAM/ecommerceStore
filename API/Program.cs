using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfraStructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void  Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();

          //  var host = CreateHostBuilder(args).Build();
          //  New Learning Part
          // logs have been an essential part of troubleshooting application and infrastructure performance.

          //Log data contains information such as out of memory exception or hard disk errors.

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //    try
            //    {

            //        var context = services.GetRequiredService<StoreContext>();
            //         context.Database.MigrateAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = loggerFactory.CreateLogger<Program>();
            //        logger.LogError(ex, "An Exception Occurred During Migration");
            //    }
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
