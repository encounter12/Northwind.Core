using Microsoft.EntityFrameworkCore;
using Northwind.Data.Models;
using Northwind.Data.Repositories.Contracts;
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

        public IQueryable<Orders> GetOrdersByCustomer(string customerId)
        {
            IQueryable<Orders> orders = this.All()
                .Where(o => o.CustomerId == customerId)
                .Include(c => c.OrderDetails);

            return orders;
        }
    }
}
