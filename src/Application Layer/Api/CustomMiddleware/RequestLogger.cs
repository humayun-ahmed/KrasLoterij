// Copyright 2022, Nederlandse Loterij

using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using NederlandseLoterij.KrasLoterij.Repository.Entity;

namespace NederlandseLoterij.KrasLoterij.Api.CustomMiddleware
{
    public class RequestLogger
    {
        private readonly RequestDelegate m_next;

        public RequestLogger(RequestDelegate next)
        {
            m_next = next;
        }

        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
        {
            await m_next(httpContext);

            var repository = (IRepository)serviceProvider.GetService(typeof(IRepository));

            var requestLog = new RequestLog
            {
                RequestStatusCode = httpContext.Response.StatusCode,
                RequestTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                IsException = httpContext.Response.StatusCode == 500,
                RequestDetail = httpContext.Request.GetDisplayUrl()
            };
            repository.Add(requestLog);
            await repository.SaveChanges();
        }
    }
}