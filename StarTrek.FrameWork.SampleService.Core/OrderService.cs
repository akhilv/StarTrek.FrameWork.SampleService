using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.DataAccess;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.Exceptions;

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
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e) //catch custom exceptions and relay httpstatuscodeexceptions to controller
            {
               throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, new ErrorResponse(ErrorCode.NotImplementedException, e.Message));
            }
        }

        public Task<OrderInformation> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                return Task.FromResult(new OrderInformation());
                //throw new NotImplementedException();
            }
            catch (NotImplementedException e) //catch custom exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, new ErrorResponse(ErrorCode.NotImplementedException, e.Message));
            }
        }

        public Task<OrderInformation> UpdateOrder(UpdateOrderRequest updateOrderRequest)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e) //catch custom exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, new ErrorResponse(ErrorCode.NotImplementedException, e.Message));
            }
        }

        public Task<bool> DeleteOrder(string id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e) //catch custom exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, new ErrorResponse(ErrorCode.NotImplementedException, e.Message));
            }
        }
    }
}
