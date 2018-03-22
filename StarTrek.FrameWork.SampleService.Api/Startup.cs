using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using StarTrek.FrameWork.SampleService.Api.Conventions;
using StarTrek.FrameWork.SampleService.Api.DI;
using StarTrek.FrameWork.SampleService.Api.MiddleWare;

namespace StarTrek.FrameWork.SampleService.Api
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(opt =>
            {
               opt.Conventions.Add(new FromBodyParameterModelConvention());
                //opt.Filters.Add(typeof(GlobalExceptionFilter)); 
            });

            var container = new Container();
            var newContainer = container.WithDependencyInjectionAdapter(services);

            var provider =  newContainer.ConfigureServiceProvider<CompositionRoot>();

            //Verify all DI setup is correct
            //TODO : This has stopped working in this version
            //https://bitbucket.org/dadhi/dryioc/wiki/ErrorDetectionAndResolution
            //if (newContainer.Validate().Any())
            //{
            //    throw new InvalidOperationException("Invalid DI Setup");
            //}
            ServiceLocator.SetLocatorProvider(() => new DryIocServiceLocator((Container)newContainer));
            return provider;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            //Example Middleware
            app.UseMiddleware<RequestCultureMiddleware>();
            
            //Sets path base
            app.UsePathBase("/sample-api/v1");


            //Three properties on the interface are cancellation tokens used to register Action methods that define startup and shutdown events.
            applicationLifetime.ApplicationStarted.Register(() => Console.WriteLine("Started"));
            applicationLifetime.ApplicationStopping.Register(() => Console.WriteLine("Stopping"));
            applicationLifetime.ApplicationStopped.Register(() => Console.WriteLine("Stopped"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configure default route
            app.UseMvc(routes => routes.MapRoute(
                name: "default_route",
                template: "{controller}/{id?}"));


            ////Another way of writing middleware
            //app.Use(async (context, next) =>
            //{
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});

            ////ShortCircuit
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
