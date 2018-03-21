using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                .Build();
    }
}
