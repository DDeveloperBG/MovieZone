namespace MovieZone
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
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
/*
 Method 'GetServiceProviderHashCode' in type 'ExtensionInfo' from assembly 'Microsoft.EntityFrameworkCore.InMemory, Version=5.0.4.0, Culture=neutral, PublicKeyToken=adb9793829ddae60' does not have an implementation.
 */