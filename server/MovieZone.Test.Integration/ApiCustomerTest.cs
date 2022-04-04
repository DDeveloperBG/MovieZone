namespace MovieZone.Test.Integration
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using NUnit.Framework;

    public class ApiCustomerTest
    {
        [TestCase("Get", "api/v1/Customer")]
        [TestCase("Get", "api/v1/Customer/1")]
        [Ignore("Need to fix jwt setting value and handle 401 error")]
        public async Task GetAllCustomerTestAsync(string method, string url)
        {
            using var client = new TestClientProvider().Client;
            var request = new HttpRequestMessage(new HttpMethod(method), url);
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
