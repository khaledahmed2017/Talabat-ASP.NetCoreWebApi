using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalabatCore.Entities.Identity;
using TalabatRepository.Context;
using TalabatRepository.Context.ContextSeeding;
using TalabatRepository.Identity;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //update- Database
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();
                // this for add Admin user the first one the program run
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await SeedUserAsync.Seeduser(userManager);
                //this for seead the data the first one the program run(Product Data)
                await ContextSeeding.SeedAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
            host.Run();
        }
        // Startup configuration
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
