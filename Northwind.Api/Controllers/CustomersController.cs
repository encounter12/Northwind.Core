using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Api.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        private readonly IOrderService orderService;

        public CustomersController(ICustomerService customerService, IOrderService orderService)
        {
            this.customerService = customerService;
            this.orderService = orderService;
        }

        [HttpGet("customers")]
        public ActionResult<IQueryable<CustomerListViewModel>> GetCustomers()
        {
            IQueryable<CustomerListViewModel> customers = this.customerService.GetCustomers();

            return Ok(customers);
        }

        [HttpGet("customer/{id}")]
        public ActionResult<CustomerDetails> GetCustomerDetails(string id)
        {
            CustomerDetails customer = this.customerService.GetCustomerDetails(id);
            return Ok(customer);
        }

        [HttpGet("customer/{id}/orders")]
        public ActionResult<IQueryable<OrderDetailsModel>> GetCustomerOrders(string id)
        {
            IEnumerable<OrderDetailsModel> orders = this.orderService.GetOrdersByCustomer(id);

            return Ok(orders);
        }
    }
}
