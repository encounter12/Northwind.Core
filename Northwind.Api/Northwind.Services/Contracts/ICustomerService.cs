using Northwind.Services.Models;
using System.Linq;

namespace Northwind.Services.Contracts
{
    public interface ICustomerService
    {
        IQueryable<CustomerListViewModel> GetCustomers();

        CustomerDetails GetCustomerDetails(string id);
    }
}
