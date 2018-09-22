using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Northwind.Data.Models;
using Northwind.Data.Repositories.Contracts;

namespace Northwind.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customers>, ICustomerRepository
    {
        private readonly NorthwindContext context;

        public CustomerRepository(NorthwindContext context) : base(context)
        {
            this.context = context;
        }

        public Customers GetCustomerById(string id)
        {
            Customers customer = this.GetById(id);

            return customer;
        }

        public IEnumerable<Orders> GetCustomerOrders(string id)
        {
            Customers customer = this.All()
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new Exception("No customer has been found with this id.");
            }

            return customer.Orders;
        }
    }
}
