using Northwind.Data.UnitOfWork;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System;
using System.Linq;

namespace Northwind.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<OrderDetailsViewModel> GetOrdersByCustomer(string customerId)
        {
            IQueryable<OrderDetailsViewModel> orders = this.unitOfWork.OrderRepository
                .GetOrdersByCustomer(customerId)
                .Select(o => new OrderDetailsViewModel()
                {
                    Total = o.OrderDetails.Sum(od => od.UnitPrice * (1M - Convert.ToDecimal(od.Discount)) * od.Quantity),
                    ProductsCount = o.OrderDetails.Count(),
                    PossibleIssue = o.OrderDetails.Any(od => 
                        od.Product.Discontinued == true || od.Product.UnitsInStock < od.Product.UnitsOnOrder)
                });

            return orders;
        }
    }
}
