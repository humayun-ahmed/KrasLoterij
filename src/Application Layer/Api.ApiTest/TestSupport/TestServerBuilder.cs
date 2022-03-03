using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace NederlandseLoterij.KrasLoterij.Api.ApiTest.TestSupport
{
    internal class TestServerBuilder : IDisposable
    {
        private bool m_disposedValue;

        public TestServerBuilder()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            var builder = new WebHostBuilder()
                .UseSerilog()
                .UseStartup<Startup>()
                .ConfigureServices(services => { })
                .ConfigureAppConfiguration((context, conf) =>
                {
                    //load the appsettings for the test run
                    //The TestServer is not loading the appsettings from the WebApi project!!!!
                    conf.AddJsonFile(configPath);
                });


            TestServerHost = new TestServer(builder);
            Client = TestServerHost.CreateClient();
        }

        public HttpClient Client { get; set; }

        /// <summary>
        ///     The TestServer
        /// </summary>
        public TestServer TestServerHost { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposedValue)
            {
                if (disposing)
                {
                    if (TestServerHost != null)
                    {
                        TestServerHost.Dispose();
                        TestServerHost = null;
                    }
                }

                m_disposedValue = true;
            }
        }
    }
}