using System.Linq;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace NederlandseLoterij.KrasLoterij.Api.Logging
{
    public static class LogDataEnricher
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        }
    }
}