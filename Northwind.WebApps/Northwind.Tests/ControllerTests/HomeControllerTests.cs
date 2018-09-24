using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Mvc.Controllers;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_Should_ReturnViewResult_When_CustomersExist()
        {
            // Arrange
            var mockedCustomerService = new Mock<ICustomerService>();

            mockedCustomerService.Setup(customerService => customerService.GetCustomers(It.IsAny<string>()))
                .ReturnsAsync(this.CreateCustomersList());

            var controller = new HomeController(mockedCustomerService.Object, null);

            // Act
            var result = await controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerListViewModel>>(
                viewResult.ViewData.Model);
        }

        private IList<CustomerListViewModel> CreateCustomersList()
        {
            IList<CustomerListViewModel> customers = Builder<CustomerListViewModel>
                .CreateListOfSize(5)
                .Build();
            
            return customers;
        }
    }
}
