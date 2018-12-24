using Customers.Domain.Services;
using Customers.Infrastructure.Entities;
using Customers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Helpers.Models.CommonEnums;

namespace Customers.Tests.UnitTests
{
    public class CustomerServiceShould
    {
        [InlineData("all")]
        [InlineData("")]
        [Theory]
        public void ReturnSuccessAndTwoItems(string searchTag)
        {
            //Arrange
            var customerVehicleServiceMoq = new Mock<ICustomerVehicleRepository>();
            var customerServiceMoq = new Mock<ICustomerRepository>();

            var moqList = new List<CustomerVehicle>() { new CustomerVehicle("Test123", "ABC123", "Yasser", "Assiut Egypt")
                ,new CustomerVehicle("Test2", "DEF123", "Ahmed", "Assiut Egypt")
            };
            DbSet<CustomerVehicle> myDbSet = GetQueryableMockDbSet(moqList);

            customerVehicleServiceMoq.Setup(m => m.DbSet).Returns(myDbSet);

            var customerService = new CustomerService(customerServiceMoq.Object, customerVehicleServiceMoq.Object);

            //Act

            var result = customerService.GetAllVehicles(searchTag);
            //Assert       
            Assert.Equal(ResponseStatusCode.Success, result.StatusCode);
            Assert.Equal(2, result.ItemsList.Count);
        }

        [InlineData("Yasser")]
        [InlineData("Ahmed")]
        [InlineData("ABC")]
        [InlineData("DEF")]
        [InlineData("Cairo")]
        [InlineData("Assiut")]
        [Theory]
        public void ReturnSuccessAndOneItem(string searchTag)
        {
            //Arrange
            var customerVehicleServiceMoq = new Mock<ICustomerVehicleRepository>();
            var customerServiceMoq = new Mock<ICustomerRepository>();

            var moqList = new List<CustomerVehicle>() { new CustomerVehicle("Test123", "ABC123", "Yasser", "Assiut Egypt")
                ,new CustomerVehicle("Test2", "DEF123", "Ahmed", "Cairo Egypt")
            };
            DbSet<CustomerVehicle> myDbSet = GetQueryableMockDbSet(moqList);

            customerVehicleServiceMoq.Setup(m => m.DbSet).Returns(myDbSet);

            var customerService = new CustomerService(customerServiceMoq.Object, customerVehicleServiceMoq.Object);

            //Act

            var result = customerService.GetAllVehicles(searchTag);
            //Assert       
            Assert.Equal(ResponseStatusCode.Success, result.StatusCode);
            Assert.Single(result.ItemsList);
        }

        [Fact]
        public void ReturnNotFound()
        {
            //Arrange
            var customerVehicleServiceMoq = new Mock<ICustomerVehicleRepository>();
            var customerServiceMoq = new Mock<ICustomerRepository>();
            var moqList = new List<CustomerVehicle>() { new CustomerVehicle("Test123", "ABC123", "Yasser", "Assiut Egypt")
                ,new CustomerVehicle("Test2", "DEF123", "Ahmed", "Cairo Egypt")
            };
            DbSet<CustomerVehicle> myDbSet = GetQueryableMockDbSet(moqList);
            customerVehicleServiceMoq.Setup(m => m.DbSet).Returns(myDbSet);
            var customerService = new CustomerService(customerServiceMoq.Object, customerVehicleServiceMoq.Object);

            //Act
            var result = customerService.GetAllVehicles("John");

            //Assert       
            Assert.Equal(ResponseStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public void ReturnServerError()
        {
            //Arrange
            var customerVehicleServiceMoq = new Mock<ICustomerVehicleRepository>();
            var customerServiceMoq = new Mock<ICustomerRepository>();

            customerVehicleServiceMoq.Setup(m => m.DbSet).Throws(new System.Exception()); ;

            var customerService = new CustomerService(customerServiceMoq.Object, customerVehicleServiceMoq.Object);

            //Act
            var result = customerService.GetAllVehicles("John");

            //Assert       
            Assert.Equal(ResponseStatusCode.ServerError, result.StatusCode);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        //private readonly TestServer _server;
        //private readonly HttpClient _client;

        //public HomeControllerTests()
        //{
        //    _server = new TestServer(WebHost.CreateDefaultBuilder()
        //        .UseStartup<Startup>());
        //    _client = _server.CreateClient();
        //}

        //[Fact]
        //public async Task home_controller_get_should_return_string_content()
        //{
        //    var response = await _client.GetAsync("/");
        //    response.EnsureSuccessStatusCode();

        //    var content = await response.Content.ReadAsStringAsync();

        //    content.Should().BeEquivalentTo("Hello from Actio.Services.Activites API!");
        //}
    }


}
