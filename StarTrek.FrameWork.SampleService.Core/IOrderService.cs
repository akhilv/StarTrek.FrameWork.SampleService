using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;

namespace StarTrek.FrameWork.SampleService.Core
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderInformation>> GetOrderInformation(string id);

        Task<OrderInformation> CreateOrder(CreateOrderRequest createOrderRequest);

        Task<OrderInformation> UpdateOrder(UpdateOrderRequest updateOrderRequest);

        Task<bool> DeleteOrder(string id);
    }
}

