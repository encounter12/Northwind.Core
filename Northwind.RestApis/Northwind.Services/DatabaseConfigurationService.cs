using Northwind.Data;
using Northwind.Data.Models;
using Northwind.Data.Seed;
using Northwind.Services.Contracts;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class DatabaseConfigurationService : IDatabaseConfigurationService
    {
        public void SeedData(NorthwindContext context, MasterContext masterContext)
        {
            DatabaseInitializer.SeedData(context, masterContext);
        }
    }
}
