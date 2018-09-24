using Northwind.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListViewModel>> GetCustomers(string searchString);

        Task<CustomerDetails> GetCustomerDetails(string id);
    }
}
