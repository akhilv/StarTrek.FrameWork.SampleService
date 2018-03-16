using System.Threading.Tasks;
using StarTrek.FrameWork.CustomerServiceClient.Model;

namespace StarTrek.FrameWork.CustomerServiceClient
{
    public class CustomerService : ICustomerService
    {
        public async Task<Customer> GetCustomer(int customerId)
        {
            //User restsharp client to get the customer information
            return await Task.FromResult(new Customer());
        }
    }
}