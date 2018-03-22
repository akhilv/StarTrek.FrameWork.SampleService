using DryIoc;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.DataAccess;

namespace StarTrek.FrameWork.SampleService.Api.DI
{
    public class CompositionRoot
    {
        public CompositionRoot(IRegistrator container)
        {
            container.Register<IOrderService, OrderService>();
            container.Register<IOrderRepository, OrderRepository>();
            container.Register<ICustomerService, CustomerService>();

            //container.Register<ILogger, Serilog>() ;
            //    //var assemblies = new[] { typeof(ExportedService).GetAssembly() };
            //    //container.RegisterMany(assemblies);
        }

    }
}