using Northwind.Data.Models;
using System.Collections.Generic;

namespace Northwind.Data.Repositories.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customers>
    {
        Customers GetCustomerById(string id);

        IEnumerable<Orders> GetCustomerOrders(string id);
    }
}
