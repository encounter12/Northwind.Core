using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data.Models;

namespace Northwind.Api.Controllers
{
    //[Route("")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindContext context;

        public CustomersController(NorthwindContext context)
        {
            this.context = context;
        }

        [HttpGet("customers")]
        public ActionResult<IEnumerable<string>> GetCustomers()
        {
            List<string> customerNames = this.context.Customers
                .Select(c => c.ContactName)
                .ToList();

            return customerNames;
        }

        [HttpGet("customer/{id}")]
        public ActionResult<string> GetSingleCustomer(string id)
        {
            Customers customer = this.context.Customers.FirstOrDefault(c => c.CustomerId == id);

            return customer.ContactName;
        }

        [HttpGet("customer/{id}/orders")]
        public ActionResult<IEnumerable<int>> GetCustomerOrders(string id)
        {
            Customers customer = this.context.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                return BadRequest();
            }

            IEnumerable<int> orderIDs = customer.Orders.Select(o => o.OrderId);

            return orderIDs.ToList();
        }
    }
}
