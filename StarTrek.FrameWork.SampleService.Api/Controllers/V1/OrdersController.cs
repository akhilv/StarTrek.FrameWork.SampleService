using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Context;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;
using StarTrek.FrameWork.SampleService.Models.Exceptions;


namespace StarTrek.FrameWork.SampleService.Api.Controllers.V1
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            ObjectResult response;
            try
            {
                using (LogContext.PushProperty("AkhilCustomProperty", "custompropvalue"))
                {

                    _logger.LogInformation("Get Orders request recieved");
                    var responseTask = await _orderService.GetOrderInformation(String.Empty);
                    response = new ObjectResult(responseTask) {StatusCode = (int) HttpStatusCode.OK};
                    _logger.LogInformation("Get Orders request successfull");
                }
            }
            catch (HttpStatusCodeException e)
            {
                response = new ObjectResult(e.Error) {StatusCode = (int) e.StatusCode};
                _logger.LogError(e, e.Message);
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(string id)
        {
            ObjectResult response;
            try
            {
                var responseTask = await _orderService.GetOrderInformation(id);
                response = new ObjectResult(responseTask) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (HttpStatusCodeException e)
            {
                response = new ObjectResult(e.Error) { StatusCode = (int)e.StatusCode };
                _logger.LogError(e, e.Message);
            }
            return response;
        }

        //// POST sample-api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            ObjectResult response;
            try
            {
                var responseTask = await _orderService.CreateOrder(createOrderRequest);

                response = new ObjectResult(responseTask) { StatusCode = (int)HttpStatusCode.Created };
            }
            catch (HttpStatusCodeException e)
            {
                response = new ObjectResult(e.Error) { StatusCode = (int)e.StatusCode };
            }
            return response;
        }

        //// PUT sample-api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, UpdateOrderRequest updateOrderRequest)
        {
            ObjectResult response;
            try
            {
                var responseTask = await _orderService.UpdateOrder(updateOrderRequest);

                response = new ObjectResult(responseTask) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (HttpStatusCodeException e)
            {
                response = new ObjectResult(e.Error) { StatusCode = (int)e.StatusCode };
            }
            return response;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ObjectResult response;
            try
            {
                var responseTask = await _orderService.DeleteOrder(id);
                response = new ObjectResult(responseTask) { StatusCode = (int)HttpStatusCode.OK};
            }
            catch (HttpStatusCodeException e)
            {
                response = new ObjectResult(e.Error) { StatusCode = (int)e.StatusCode };
            }
            return response;
        }
    }
}
