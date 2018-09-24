using Northwind.Data.Models;
using Northwind.Data.UnitOfWork;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System;
using System.Linq;

namespace Northwind.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<CustomerListViewModel> GetCustomers()
        {
            IQueryable<CustomerListViewModel> customers = this.unitOfWork.CustomerRepository
                .All()
                .Select(c => new CustomerListViewModel
                {
                    Id = c.CustomerId,
                    ContactName = c.ContactName,
                    OrdersCount = c.Orders.Count()
                });

            return customers;
        }

        public CustomerDetails GetCustomerDetails(string id)
        {
            Customers customer = this.unitOfWork.CustomerRepository.GetCustomerById(id);

            if (customer == null)
            {
                throw new Exception($"Cannot find customer with id: {id}");
            }

            var customerDetails = new CustomerDetails()
            {
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax
            };

            return customerDetails;
        }
    }
}
