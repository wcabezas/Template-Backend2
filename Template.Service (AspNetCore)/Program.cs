using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Template.Service
{
    /// <summary>
    /// ASP.Net Core Entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    
        /// <summary>
        /// Cretas the host
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
