using Northwind.Data;
using Northwind.Data.Models;

namespace Northwind.Services.Contracts
{
    public interface IDatabaseConfigurationService
    {
        void SeedData(NorthwindContext context, MasterContext masterContext);
    }
}
