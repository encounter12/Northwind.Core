using Northwind.Services.Models;
using System.Collections.Generic;

namespace Northwind.Services.Contracts
{
    public interface IOrderService
    {
        IEnumerable<OrderDetailsModel> GetOrdersByCustomer(string customerId);
    }
}
