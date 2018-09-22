using Northwind.Data.Models;
using Northwind.Data.Repositories;
using Northwind.Data.Repositories.Contracts;

namespace Northwind.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerRepository customerRepository;

        public UnitOfWork(NorthwindContext context)
        {
            this.Context = context;
        }

        public NorthwindContext Context { get; }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new CustomerRepository(this.Context);
                }

                return customerRepository;
            }
        }
    }
}
