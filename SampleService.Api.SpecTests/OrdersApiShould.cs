using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Owin;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using StarTrek.FrameWork.SampleService.Core;
using StarTrek.FrameWork.SampleService.Models;
using StarTrek.FrameWork.SampleService.Models.DTO;
using StarTrek.FrameWork.SampleService.Models.Exceptions;

namespace SampleService.Api.SpecTests
{
    [TestFixture]
    public class OrdersApiShould
    {
        private HttpClient _client;
        private const string BaseUrl = "http://localhost/sample-api/v1/orders";

        private readonly IConfigurationRoot _configuration;
        public OrdersApiShould()
        {
            //We want the test to use its own configuration file, hence configure the builder
            //and ensure the jsonfile properties is set to "Copy if newer"
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            var hostBuilder = new WebHostBuilder().UseStartup<StartUpStub>().UseConfiguration(_configuration);
            _client = CreateServer(hostBuilder);
            //TODO:Need to create a test data in Repositories
        }

        [Test]
        public async Task GetOrders()
        {
            //Arrange/Act
            var response = await _client.GetAsync(String.Empty);
            var result = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<IEnumerable<OrderInformation>>(result);
            
            //Assert
            Assert.That(orders.Any());
            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }


        [Test]
        public async Task GetOrders_For_OrderId()
        {
            //Arrange/Act
            
            //First get all
            //TODO : Needs to be removed once set up is done
            var res = await _client.GetAsync(String.Empty);
            var re = await res.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<IEnumerable<OrderInformation>>(re);

            //Now get selected
            var response = await _client.GetAsync(BaseUrl + $"/{o.First().OrderId}");
            var result = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<IEnumerable<OrderInformation>>(result);

            //Assert
            Assert.That(orders.Count() == 1);
            Assert.That(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Returns_HttpStatusCode_201_WhenCreatingOrderWith_ValidRequest()
        {
            //Arrange
            var request = JsonConvert.SerializeObject(new CreateOrderRequest { CustomerId = "TESTCUSTOMERID" });

            //Act
            var response = await _client.PostAsync(BaseUrl, new StringContent(request, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<OrderInformation>(result);

            //Assert
            Assert.IsNotNull(orders);
            Assert.That(response.StatusCode == HttpStatusCode.Created);
        }

        
        [TestCase("", Category = "ModelBinding")]
        [TestCase(null, Category = "ModelBinding")]
        public async Task Return_HttpStatusCode_400_WhenCreatingOrderWith_NullOREmpty_CustomerId(string customerId)
        {
            //Arrange
            var request = JsonConvert.SerializeObject(new CreateOrderRequest { CustomerId = customerId });

            //Act
            var response = await _client.PostAsync(BaseUrl, new StringContent(request, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);

            //Assert
            Assert.IsNotNull(errorResponse);
            Assert.That(errorResponse.ErrorCode == ErrorCode.ModelBindingException);
            Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
        }


        [Test]
        public async Task Return_HttpStatusCode_500_WhenCreatingOrder_Throws_AnyUnhandledException()
        {
            //Arrange
            var mockobj = new Mock<IOrderService>();
            mockobj.Setup(os => os.CreateOrder(It.IsAny<CreateOrderRequest>())).ThrowsAsync(new ArgumentNullException());

            //Create new Host with Mocks
            var hostBuilder = new WebHostBuilder().UseStartup<StartUpStub>().UseConfiguration(_configuration);
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IOrderService>(mockobj.Object);
            });
            var client = CreateServer(hostBuilder);

            var request = JsonConvert.SerializeObject(new CreateOrderRequest { CustomerId = "asd"});

            //Act
            var response = await client.PostAsync(BaseUrl, new StringContent(request, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);

            //Assert
            Assert.IsNotNull(errorResponse);
            Assert.That(errorResponse.ErrorCode == ErrorCode.UnhandledException);
            Assert.That(response.StatusCode == HttpStatusCode.InternalServerError);
        }

        private HttpClient CreateServer(IWebHostBuilder webHostBuilder)
        {
            var server = new TestServer(webHostBuilder);
            var client = server.CreateClient();
            client.BaseAddress = new Uri(BaseUrl);
            return client;
        }
    }

  
}
