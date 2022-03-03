// Copyright 2022, Nederlandse Loterij

using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NederlandseLoterij.KrasLoterij.Api.Logging;
using Serilog;
using Serilog.Debugging;
using Serilog.Formatting.Compact;

namespace NederlandseLoterij.KrasLoterij.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = Startup.GetConfiguration();


        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <remarks>
        ///     There is a Serilog enricher which adds the correlation id to the log data.
        ///     It is possible to get the correlation id from the request header: See
        ///     https://github.com/ekmsystems/serilog-enrichers-correlation-id
        /// </remarks>
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich
                .With(new ThreadIdEnricher()) //Enricher adds the data to the requestlogger and the ILogger in the controller. Is Example. Remove for production.
                .WriteTo.Console(new RenderedCompactJsonFormatter()) //output log message as json
                .CreateLogger();

            SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}