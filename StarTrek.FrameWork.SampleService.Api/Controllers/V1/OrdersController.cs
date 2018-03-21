using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.Models;


namespace StarTrek.FrameWork.SampleService.Api.Controllers.V1
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        public OrdersController(){}

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var responseTask = await _orderService.GetOrderInformation(String.Empty);
            return new ObjectResult(null) { StatusCode = (int)HttpStatusCode.OK };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersById(string id)
        {
            var responseTask = await _orderService.GetOrderInformation(id);
            return new ObjectResult(responseTask) { StatusCode = (int)HttpStatusCode.OK };
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
