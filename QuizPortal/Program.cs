using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizPortal.Data;

namespace QuizPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                using (var scope = host.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    dbContext.Database.Migrate();
                }

                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                    // Required for Railway to listen on the assigned PORT
                    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
                    webBuilder.UseUrls($"http://0.0.0.0:{port}");
                });
    }
}
