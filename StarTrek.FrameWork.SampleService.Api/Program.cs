using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace StarTrek.FrameWork.SampleService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //The Run method starts the web app and blocks the calling thread until the host is shutdown
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //Default kestrel listens on port 5000    
                //Enable below if want to run multiple kestrel apps on same IP, port 0 will dynamically bind to a port.
                //.UseUrls("http://*:0")
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    logging.ClearProviders();
                //})

                //https://github.com/serilog/serilog-aspnetcore
                //https://github.com/serilog/serilog-sinks-console
                //https://github.com/serilog/serilog-settings-configuration
                //https://github.com/serilog/serilog/wiki/Enrichment  - Enrichers (can make use for logging and generating TxId i.e. CorrelationId, ThreadId or AnythingElse
                //https://github.com/serilog/serilog-enrichers-thread
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        //[{Timestamp: HH:mm: ss} {Level:u3}] {Message:lj}{NewLine}{Exception}
                        //[{Timestamp: HH:mm: ss} {Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}  - if want just the threadid
                        .WriteTo.Console(
                            outputTemplate:
                            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}")
                        .WriteTo.ApplicationInsightsTraces(hostingContext.Configuration["ApplicationInsights:InstrumentationKey"])
                        .Enrich.WithThreadId().Enrich.FromLogContext();
                })
                //.UseApplicationInsights()
                .Build();
    }
}
