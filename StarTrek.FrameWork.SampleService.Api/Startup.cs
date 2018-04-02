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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StarTrek.FrameWork.SampleService.Api.Conventions;
using StarTrek.FrameWork.SampleService.Api.DI;
using StarTrek.FrameWork.SampleService.Api.Filters;
using StarTrek.FrameWork.SampleService.Api.MiddleWare;

namespace StarTrek.FrameWork.SampleService.Api
{
    public interface ISqlConfiguration
    {
        string ConnectionString { get; set; }

        int ConnectionTimeOut { get; set; }
    }
    public class SqlConfiguration : ISqlConfiguration
    {
        public string ConnectionString { get; set; }
        public int ConnectionTimeOut { get; set; }
    }

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(opt =>
            {
               opt.Conventions.Add(new FromBodyParameterModelConvention());
               opt.Filters.Add(typeof(ValidateModelAttribute)); 
            });

            //Configure the custom configurations
            services.Configure<SqlConfiguration>(_configuration.GetSection("SQLConfiguration"));


            //USING DRYIOC
            return ConfigureDi(services);
        }

        public virtual IServiceProvider ConfigureDi(IServiceCollection services)
        {
            var container = new Container();
            var newContainer = container.WithDependencyInjectionAdapter(services);
            //For custom configuration DI, 
            //Using inbuilt container
            //services.AddScoped(typeof(ISqlOptionConfiguration),  sp => sp.GetService<IOptionsSnapshot<SqlOptionConfiguration>>().Value);
            //Using DRYIOC 
            newContainer.RegisterDelegate(typeof(ISqlConfiguration), rs => rs.Resolve<IOptionsSnapshot<SqlConfiguration>>().Value, Reuse.Scoped);

            //Rest of the DI
            var provider = newContainer.ConfigureServiceProvider<CompositionRoot>();

            //Verify all DI setup is correct
            //TODO : This has stopped working in this version
            //https://bitbucket.org/dadhi/dryioc/wiki/ErrorDetectionAndResolution
            //if (newContainer.Validate().Any())
            //{
            //    throw new InvalidOperationException("Invalid DI Setup");
            //}
            //Configure ServiceLocator 
            ServiceLocator.SetLocatorProvider(() => new DryIocServiceLocator((Container)newContainer));
            return provider;
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            //Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

            //Sets path base
            app.UsePathBase("/sample-api/v1");


            //Three properties on the interface are cancellation tokens used to register Action methods that define startup and shutdown events.
            applicationLifetime.ApplicationStarted.Register(() => Console.WriteLine("Started"));
            applicationLifetime.ApplicationStopping.Register(() => Console.WriteLine("Stopping"));
            applicationLifetime.ApplicationStopped.Register(() => Console.WriteLine("Stopped"));

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
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
