using Northwind.Data.Models;
using System.Linq;

namespace Northwind.Data.Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<Orders>
    {
        IQueryable<Orders> GetOrdersByCustomer(string customerId);
    }
}
