using System.Threading.Tasks;
using StarTrek.FrameWork.CustomerServiceClient.Model;

namespace StarTrek.FrameWork.CustomerServiceClient
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(int customerId);
    }
}