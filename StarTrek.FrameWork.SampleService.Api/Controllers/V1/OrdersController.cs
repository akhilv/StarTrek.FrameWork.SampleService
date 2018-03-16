using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarTrek.FrameWork.SampleService.Api.Controllers.V1
{
    [Route("[orders]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<OrderInformation>> GetOrders()
        {
            return await _orderService.GetOrderInformation(String.Empty);
        }

        [HttpGet("{id}")]
        public async Task<OrderInformation> GetOrdersById(int id)
        {
            var responseTask = await _orderService.GetOrderInformation(id.ToString());
            return responseTask.First();
        }

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
