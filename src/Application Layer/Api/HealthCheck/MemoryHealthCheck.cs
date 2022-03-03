﻿using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace NederlandseLoterij.KrasLoterij.Api.HealthCheck
{
    #region snippet1

    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> m_options;

        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            m_options = options;
        }

        public string Name
        {
            get { return "memory_check"; }
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var options = m_options.Get(context.Registration.Name);

            // Include GC information in the reported diagnostics.
            var allocated = GC.GetTotalMemory(false);
            var data = new Dictionary<string, object>
            {
                {"AllocatedBytes", allocated},
                {"Gen0Collections", GC.CollectionCount(0)},
                {"Gen1Collections", GC.CollectionCount(1)},
                {"Gen2Collections", GC.CollectionCount(2)}
            };
            var status = allocated < options.Threshold ? HealthStatus.Healthy : context.Registration.FailureStatus;

            return Task.FromResult(new HealthCheckResult(
                status,
                "Reports degraded status if allocated bytes " +
                $">= {options.Threshold} bytes.",
                null,
                data));
        }
    }

    #endregion

    #region snippet2

    public static class GCInfoHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddMemoryHealthCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus = null,
            IEnumerable<string> tags = null,
            long? thresholdInBytes = null)
        {
            // Register a check of type GCInfo.
            builder.AddCheck<MemoryHealthCheck>(
                name, failureStatus ?? HealthStatus.Degraded, tags);

            // Configure named options to pass the threshold into the check.
            if (thresholdInBytes.HasValue)
            {
                builder.Services.Configure<MemoryCheckOptions>(name, options => { options.Threshold = thresholdInBytes.Value; });
            }

            return builder;
        }
    }

    #endregion

    #region snippet3

    public class MemoryCheckOptions
    {
        // Failure threshold (in bytes)
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }

    #endregion
}