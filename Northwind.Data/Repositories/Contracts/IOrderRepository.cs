using Northwind.Data.DataTransferObjects;
using Northwind.Data.Models;
using System.Collections.Generic;

namespace Northwind.Data.Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<Orders>
    {
        IEnumerable<OrderDetailsDTO> GetOrdersByCustomer(string customerId);
    }
}
