using Microsoft.AspNetCore.Mvc;
using Northwind.Data.Models;
using Northwind.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Api.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("customers")]
        public ActionResult<IQueryable<string>> GetCustomers()
        {
            IQueryable<string> customerNames = this.unitOfWork.CustomerRepository
                .All()
                .Select(c => c.ContactName);

            return Ok(customerNames);
        }

        [HttpGet("customer/{id}")]
        public ActionResult<string> GetSingleCustomer(string id)
        {
            Customers customer = this.unitOfWork.CustomerRepository.GetCustomerById(id);
            return customer.ContactName;
        }

        [HttpGet("customer/{id}/orders")]
        public ActionResult<IEnumerable<int>> GetCustomerOrders(string id)
        {
            IEnumerable<int> orderIDs = null;

            try
            {
                orderIDs = this.unitOfWork.CustomerRepository
                    .GetCustomerOrders(id)
                    .Select(o => o.OrderId);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return orderIDs.ToList();
        }
    }
}
