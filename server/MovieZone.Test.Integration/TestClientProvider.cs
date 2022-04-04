namespace MovieZone.Test.Integration
{
    using System.IO;
    using System.Net.Http;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    public class TestClientProvider
    {
        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>());

            this.Client = server.CreateClient();
        }

        public HttpClient Client { get; set; }
    }
}
