using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(json2record.func.Startup))]
namespace json2record.func
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
        }

    }
}