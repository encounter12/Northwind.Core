using Northwind.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderDetailsViewModel>> GetOrdersForCustomer(string id);
    }
}
