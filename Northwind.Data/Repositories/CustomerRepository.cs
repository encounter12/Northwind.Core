using Northwind.Data.Models;
using Northwind.Data.Repositories.Contracts;

namespace Northwind.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customers>, ICustomerRepository
    {
        private readonly NorthwindContext context;

        public CustomerRepository(NorthwindContext context) : base(context)
        {
            this.context = context;
        }

        public Customers GetCustomerById(string id)
        {
            Customers customer = this.GetById(id);

            return customer;
        }
    }
}
