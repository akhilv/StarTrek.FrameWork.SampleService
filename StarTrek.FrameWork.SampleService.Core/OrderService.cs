using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StarTrek.FrameWork.CustomerServiceClient;
using StarTrek.FrameWork.SampleService.DataAccess;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;
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
        public async Task<IEnumerable<OrderInformation>> GetOrderInformation(string id)
        {
            IEnumerable<OrderInformation> response = null;
            try
            {
                response = await _orderRepository.GetOrderInformation(id);
            }
            catch (SqlException sqlException) //catch handled exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, sqlException, new ErrorResponse(ErrorCode.SQLException, sqlException.Message));
            }
            return response;
        }

        public async Task<OrderInformation> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                createOrderRequest.OrderRef = Guid.NewGuid().ToString();

                return await _orderRepository.CreateOrder(createOrderRequest);
            }
            catch (SqlException sqlException) //catch handled exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, sqlException, new ErrorResponse(ErrorCode.SQLException, sqlException.Message));
            }
        }

        public async Task<OrderInformation> UpdateOrder(UpdateOrderRequest updateOrderRequest)
        {
            try
            {
                return await _orderRepository.UpdateOrder(updateOrderRequest);
            }
            catch (SqlException sqlException) //catch handled exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, sqlException, new ErrorResponse(ErrorCode.SQLException, sqlException.Message));
            }
        }

        public async Task<bool> DeleteOrder(string id)
        {
            try
            {
                return await _orderRepository.DeleteOrder(id);
            }
            catch (SqlException sqlException) //catch handled exceptions and relay httpstatuscodeexceptions to controller
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, sqlException, new ErrorResponse(ErrorCode.SQLException, sqlException.Message));
            }
        }
    }
}
