using System;
using System.Linq;
using CommonServiceLocator;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.Api;
using StarTrek.FrameWork.SampleService.Api.DI;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.DataAccess;
using StarTrek.FrameWork.SampleService.Models;

namespace SampleService.Api.SpecTests
{
    /// <summary>
    /// Inherit StartUp to allow Stubs/Mocks to be configured for Integration Tests
    /// </summary>
    public class StartUpStub : Startup
    {
        public StartUpStub(IConfiguration configuration) : base(configuration){}

        public override IServiceProvider ConfigureDi(IServiceCollection services)
        {
            //var mockobj = new Mock<IOrderService>();
            //mockobj.Setup(os => os.CreateOrder(It.IsAny<CreateOrderRequest>())).ThrowsAsync(new InvalidOperationException());

            var container = new Container();
            var newContainer = container.WithDependencyInjectionAdapter(services);
            newContainer.RegisterDelegate(typeof(ISqlConfiguration), rs => rs.Resolve<IOptionsSnapshot<SqlConfiguration>>().Value, Reuse.Scoped);
            var provider = newContainer.ConfigureServiceProvider<CompositionRootStub>();

            //newContainer.UseInstance(typeof(IOrderService), mockobj.Object);

            return provider;
        }
    }

    public class CompositionRootStub
    {
        public CompositionRootStub(IRegistrator registrator)
        {
            registrator.Register<IOrderService, OrderService>();
            registrator.Register<ICustomerService, CustomerService>();
            registrator.Register<IOrderRepository, OrderRepository>(Reuse.Singleton);
        }
    }

}