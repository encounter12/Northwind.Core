using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Api.Controllers;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Northwind.Tests.ControllerTests
{
    public class CustomersControllerTests
    {
        private readonly CustomersController customersController;
        private Mock<ICustomerService> mockedCustomerService;
        private Mock<IOrderService>  mockedOrderService;

        public CustomersControllerTests()
        {
            mockedCustomerService = new Mock<ICustomerService>();
            mockedOrderService = new Mock<IOrderService>();
            customersController = new CustomersController(
                mockedCustomerService.Object, mockedOrderService.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(6)]
        public void GetCustomers_Should_ReturnOkResult_When_NoError_AndCustomersExist(int customersCount)
        {
            // Arrange
            this.mockedCustomerService.Setup(customerService => customerService.GetCustomers())
                .Returns(this.CreateCustomersList(customersCount).AsQueryable());

            // Act
            var okResult = customersController.GetCustomers();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetCustomers_Should_ReturnAllCustomers_When_CustomersExist(int customersCount)
        {
            // Arrange
            this.mockedCustomerService.Setup(customerService => customerService.GetCustomers())
                .Returns(this.CreateCustomersList(customersCount).AsQueryable());

            // Act
            var okResult = customersController.GetCustomers().Result as OkObjectResult;

            // Assert
            var customers = Assert.IsAssignableFrom<IQueryable<CustomerListViewModel>>(okResult.Value);
            Assert.Equal(customersCount, customers.Count());
        }

        private IList<CustomerListViewModel> CreateCustomersList(int customersCount)
        {
            IList<CustomerListViewModel> customers = Builder<CustomerListViewModel>
                .CreateListOfSize(customersCount)
                .Build();

            return customers;
        }
    }
}
