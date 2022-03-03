// Copyright 2022, Nederlandse Loterij

using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.KrasLoterij.Api.CustomMiddleware;
using NederlandseLoterij.KrasLoterij.Api.HealthCheck;
using NederlandseLoterij.KrasLoterij.Api.Logging;
using NederlandseLoterij.KrasLoterij.Api.Options;
using NederlandseLoterij.KrasLoterij.Repository;
using NederlandseLoterij.KrasLoterij.Service;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;
using NederlandseLoterij.KrasLoterij.Validators;
using Serilog;

namespace NederlandseLoterij.KrasLoterij.Api
{
    public class Startup
    {
        public Startup()
        {
            Configuration = GetConfiguration();
        }

        public IConfiguration Configuration { get; }

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ".Development"}.json", true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });*/
            

            RegisterDependencies(services);

            services.AddHealthChecks()
                .AddCheck<HealthCheck.HealthCheck>("Kras Loterij service health check")
                .AddMemoryHealthCheck("memory");

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UserRequestLogger();

            //register first the global error handler!!!!
            app.ConfigureGlobalExceptionMiddleware();

            app.UseAuthentication();

            // Sequence of registration is important. Anything before this in not included in the logging.
            app.UseSerilogRequestLogging(options => { options.EnrichDiagnosticContext = LogDataEnricher.EnrichFromRequest; }
            );

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Welcome to the Kras Loterij service."); });
                endpoints.MapHealthChecks("/health"); //Add a health endpoint to return the status of your application.
                endpoints.MapControllers();
            });

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<KrasLoterijContext>();
                context.Database.Migrate();
            }
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddDependencies(Configuration);

            services.ConfigureSwagger();

            services.AddScoped<FluentValidation.IValidator<ScratchCommand>, ScratchCommandValidator>();
        }
    }
}