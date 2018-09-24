using Northwind.Data.UnitOfWork;
using Northwind.Services.Contracts;
using Northwind.Services.Models;
using System.Collections.Generic;
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

        public IEnumerable<OrderDetailsModel> GetOrdersByCustomer(string customerId)
        {
            var orders = this.unitOfWork.OrderRepository
                .GetOrdersByCustomer(customerId)
                .Select(o => new OrderDetailsModel()
                {
                    Total = o.Total,
                    ProductsCount = o.ProductsCount,
                    PossibleIssue = o.PossibleIssue
                });

            return orders;
        }
    }
}
