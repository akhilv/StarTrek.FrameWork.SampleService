using DryIoc;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.DataAccess;

namespace StarTrek.FrameWork.SampleService.Api.DI
{
    public class CompositionRoot
    {
        public CompositionRoot(IRegistrator registrator)
        {
            registrator.Register<IOrderService, OrderService>();
            registrator.Register<ICustomerService, CustomerService>();

            //For components
            registrator.RegisterRepository();

            //For assembly scan
            //container.Register<ILogger, Serilog>() ;
            //    //var assemblies = new[] { typeof(ExportedService).GetAssembly() };
            //    //container.RegisterMany(assemblies);
        }

    }

    internal static class RegisterRepositoryExtension
    {
        public static void RegisterRepository(this IRegistrator registrator)
        {
            registrator.Register<IOrderRepository, OrderRepository>(Reuse.Singleton);
        }
    }
}