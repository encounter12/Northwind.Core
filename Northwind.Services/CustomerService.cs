using Northwind.Data.Models;
using Northwind.Data.UnitOfWork;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
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
                    ContactName = c.ContactName,
                    OrdersCount = c.Orders.Count()
                });

            return customers;
        }

        public CustomerDetails GetCustomerDetails(string id)
        {
            Customers customer = this.unitOfWork.CustomerRepository.GetCustomerById(id);

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
