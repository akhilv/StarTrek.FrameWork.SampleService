using System.Collections.Generic;
using System.Threading.Tasks;
using StarTrek.FrameWork.SampleService.Models;

namespace StarTrek.FrameWork.SampleService.DataAccess
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderInformation>> GetOrderInformation(string id);
    }
}