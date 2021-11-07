using Microsoft.Extensions.Hosting;

namespace json2record.func {
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(c => {})
                .Build();
 
            host.Run();
        }
    }
}