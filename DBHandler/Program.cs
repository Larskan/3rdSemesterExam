using DBHandler.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            try
            {
                await provider.GetRequiredService<IHttpClientServiceImplementation>().Execute();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Something went wrong: {e}");
            }
            
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientServiceImplementation, HttpClientCrudService>();
        }

        
    }
}
