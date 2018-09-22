using Northwind.Data.Models;
using Northwind.Data.Repositories.Contracts;

namespace Northwind.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        NorthwindContext Context { get; }

        ICustomerRepository CustomerRepository { get; }
    }
}
