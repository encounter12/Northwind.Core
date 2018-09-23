using Northwind.Data.Models;
using Northwind.Data.Repositories;
using Northwind.Data.Repositories.Contracts;

namespace Northwind.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerRepository customerRepository;

        private IOrderRepository orderRepository;

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

        public IOrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepository(this.Context);
                }

                return orderRepository;
            }
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
