using Northwind.Data.DataTransferObjects;
using Northwind.Data.Models;
using Northwind.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data.Repositories
{
    public class OrderRepository : GenericRepository<Orders>, IOrderRepository
    {
        private readonly NorthwindContext context;

        public OrderRepository(NorthwindContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<OrderDetailsDTO> GetOrdersByCustomer(string customerId)
        {
            IEnumerable<OrderDetailsDTO> orders = this.All()
                .Where(o => o.CustomerId == customerId)
                .Select(o => new OrderDetailsDTO()
                {
                    Total = o.OrderDetails.Sum(od => od.UnitPrice * (1M - Convert.ToDecimal(od.Discount)) * od.Quantity),
                    ProductsCount = o.OrderDetails.Count(),
                    PossibleIssue = o.OrderDetails.Any(od =>
                        od.Product.Discontinued == true || od.Product.UnitsInStock < od.Product.UnitsOnOrder)
                }).ToList();

            return orders;
        }
    }
}
