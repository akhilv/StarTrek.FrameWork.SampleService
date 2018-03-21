using System.Collections.Generic;
using System.Threading.Tasks;
using StarTrek.FrameWork.SampleService.Models;

namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<IEnumerable<OrderInformation>> GetOrderInformation(string id)
        {
            var res = new List<OrderInformation>();


            //Implement dapper layer to retrieve the order information 
            return await Task.FromResult(res);
        }
    }
}