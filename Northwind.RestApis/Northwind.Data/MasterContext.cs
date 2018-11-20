using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Infrastructure.Models;

namespace Northwind.Data
{
    public class MasterContext : DbContext
    {
        private readonly IConfiguration configuration;

        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppData appData = this.configuration.GetSection("AppData").Get<AppData>();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(appData.MasterDbConnectionString);
            }
        }
    }
}
