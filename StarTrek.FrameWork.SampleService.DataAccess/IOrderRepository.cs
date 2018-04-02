using System.Collections.Generic;
using System.Threading.Tasks;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;

namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderInformation>> GetOrderInformation(string id);
        Task<OrderInformation> CreateOrder(CreateOrderRequest createOrderRequest);

        Task<OrderInformation> UpdateOrder(UpdateOrderRequest updateOrderRequest);
        Task<bool> DeleteOrder(string id);
    }
}