using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.DataAccess;
using StarTrek.FrameWork.SampleService.Models;

namespace StarTrek.FrameWork.SampleService.Core
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerService _customerService;

        public OrderService(IOrderRepository orderRepository, ICustomerService customerService)
        {
            _orderRepository = orderRepository;
            _customerService = customerService;
        }
        public Task<IEnumerable<OrderInformation>> GetOrderInformation(string id)
        {
            throw new NotImplementedException();
        }
    }
}
