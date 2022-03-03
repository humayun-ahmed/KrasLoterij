using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NederlandseLoterij.KrasLoterij.Api.ApiTest.TestSupport;
using NederlandseLoterij.KrasLoterij.Api.Controllers.V1;

namespace NederlandseLoterij.KrasLoterij.Api.ApiTest
{
    [TestClass]
    public class LotteryControllerTests : ApiTestBase
    {
        public static HttpClient Client;

        private static TestServerBuilder TestServerBuilder;

        public LotteryControllerTests()
        {
            TestServerBuilder = new TestServerBuilder();
            Client = TestServerBuilder.Client;
        }

        [TestMethod]
        public async Task Get_Succeed()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiRoutes.Get}");

            var response = await Client.SendAsync(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Request should be success.");

            var result = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(result);
        }

        [ClassCleanup]
        public static void Clean()
        {
            TestServerBuilder.Dispose();
        }
    }
}