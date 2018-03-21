using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace StarTrek.FrameWork.SampleService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            app.UsePathBase("/sample-api/v1");

            //Three properties on the interface are cancellation tokens used to register Action methods that define startup and shutdown events.
            applicationLifetime.ApplicationStarted.Register(() => Console.WriteLine("I am started"));
            applicationLifetime.ApplicationStopping.Register(() => Console.WriteLine("I am stopping"));
            applicationLifetime.ApplicationStopped.Register(() => Console.WriteLine("I am stopped"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configure default route
            app.UseMvc(routes => routes.MapRoute(
                name: "default_route",
                template: "{controller}/{id?}"));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
