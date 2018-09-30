using Microsoft.EntityFrameworkCore;

namespace Northwind.Data
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }
    }
}
