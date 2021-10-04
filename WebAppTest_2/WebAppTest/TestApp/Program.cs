using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TestLib;

namespace TestApp
{
    class Program
    {
        //Option 2: Testing via Swagger, saving in memory
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}