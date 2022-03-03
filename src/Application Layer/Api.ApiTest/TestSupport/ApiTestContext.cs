using System;
using System.Net.Http;

namespace NederlandseLoterij.KrasLoterij.Api.ApiTest.TestSupport
{
    public class ApiTestContext : IDisposable
    {
        private bool m_disposedValue;

        public ApiTestContext()
        {
            TestServerBuilder = new TestServerBuilder();
            Client = TestServerBuilder.Client;
        }

        public HttpClient Client { get; }

        private TestServerBuilder TestServerBuilder { get; }

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
                    if (TestServerBuilder != null)
                    {
                        TestServerBuilder.Dispose();
                    }
                }

                m_disposedValue = true;
            }
        }
    }
}