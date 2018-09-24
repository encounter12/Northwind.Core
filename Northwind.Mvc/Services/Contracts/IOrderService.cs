using Northwind.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Mvc.Services.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderDetailsViewModel>> GetOrdersForCustomer(string id);
    }
}
