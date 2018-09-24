using Northwind.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Mvc.Services.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListViewModel>> GetCustomers(string searchString);

        Task<CustomerDetails> GetCustomerDetails(string id);
    }
}
