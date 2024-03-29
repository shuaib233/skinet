using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host =  CreateHostBuilder(args).Build();
           using(var scope = host.Services.CreateScope())
           {
               var Services = scope.ServiceProvider;
               var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
               var context = Services.GetRequiredService<StoreContext>();
               await context.Database.MigrateAsync();
               await SeedDataContext.SeedAsync(context,loggerFactory);
           }
           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
