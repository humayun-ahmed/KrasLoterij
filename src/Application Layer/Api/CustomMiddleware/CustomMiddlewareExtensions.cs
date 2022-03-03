// Copyright 2022, Nederlandse Loterij

using Microsoft.AspNetCore.Builder;

namespace NederlandseLoterij.KrasLoterij.Api.CustomMiddleware
{
    public static class CustomMiddlewareExtensions
    {
        public static void ConfigureGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }

        public static IApplicationBuilder UserRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogger>();
        }
    }
}