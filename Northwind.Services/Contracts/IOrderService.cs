using Northwind.Services.Models;
using System.Linq;

namespace Northwind.Services.Contracts
{
    public interface IOrderService
    {
        IQueryable<OrderDetailsViewModel> GetOrdersByCustomer(string customerId);
    }
}
