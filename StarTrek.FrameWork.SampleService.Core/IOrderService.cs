using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StarTrek.FrameWork.SampleService.Models;

namespace StarTrek.FrameWork.SampleService.Core
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderInformation>> GetOrderInformation(string id);
    }
    }
