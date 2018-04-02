using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using StarTrek.FrameWork.SampleService.Api.Controllers.V1;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;
using StarTrek.FrameWork.SampleService.Models.Exceptions;

namespace SampleService.Api.UnitTests.Controllers
{
    [TestFixture]
    public class OrdersControllerShould
    {
        private Mock<IOrderService> _orderServiceMock;

        [SetUp]
        public void SetUp()
        {
            _orderServiceMock = new Mock<IOrderService>();
        }

        [TestCase("TestId")]
        public async Task Get_Orders_For_Valid_Id(string id)
        {
            //Arrange
            var resDateTime = DateTime.Now.ToUniversalTime();
            var expectedResponse = new List<OrderInformation>
            {
                new OrderInformation {OrderId = 1, OrderRef = "ORDERREF", CreatedDateTime = resDateTime}
            };
            _orderServiceMock.Setup(os => os.GetOrderInformation(id)).ReturnsAsync(expectedResponse);
            var sut = new OrdersController(_orderServiceMock.Object);

            //Act
            var result =  await sut.GetOrdersById(id);

            //Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.That(expectedResponse == ((ObjectResult)result).Value);

        }

        [Test]
        public async Task Create_Order_With_Valid_Request()
        {
            //Arrange request
            var rq = new CreateOrderRequest
            {
                CustomerId = "CustomerId"
                
            };

            //response
            var resDateTime = DateTime.Now.ToUniversalTime();
            var expectedResponse =
                new OrderInformation {OrderId = 1, OrderRef = "ORDERREF", CreatedDateTime = resDateTime};

            _orderServiceMock.Setup(os => os.CreateOrder(rq)).ReturnsAsync(expectedResponse);
           var sut = new OrdersController(_orderServiceMock.Object);

            //Act
            var result = await sut.CreateOrder(rq);

            //Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.That(expectedResponse == ((ObjectResult)result).Value);

        }


        [Test]
        public async Task Throw_Error_When_CreatingOrder_With_Missing_OrderId()
        {
            //Arrange request
            var rq = new CreateOrderRequest
            {
                CustomerId = "CustomerId"
                
            };

            //response
            var resDateTime = DateTime.Now;
            var expectedResponse = new ErrorResponse(ErrorCode.ModelBindingException, "Bad Request");

            _orderServiceMock.Setup(os => os.CreateOrder(rq)).ThrowsAsync(new HttpStatusCodeException(HttpStatusCode.BadRequest, expectedResponse));
            var sut = new OrdersController(_orderServiceMock.Object);
            
            //Act
            var result = await sut.CreateOrder(rq);

            //Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.That(expectedResponse == ((ObjectResult)result).Value);

        }
    }
}

