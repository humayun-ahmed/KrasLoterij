using System;

namespace NederlandseLoterij.KrasLoterij.Api.ApiTest.TestSupport
{
    public class ApiTestBase : IDisposable
    {
        private bool m_disposedValue;

        //public ApiTestContext TestContext { get; set; }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposedValue)
            {
                if (disposing)
                {
                    /*if (TestContext != null) 
					{
						TestContext.Dispose();
						TestContext = null;
					}*/
                }

                m_disposedValue = true;
            }
        }
    }
}