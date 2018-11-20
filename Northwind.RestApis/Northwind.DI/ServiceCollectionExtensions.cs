using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data;
using Northwind.Data.Models;
using Northwind.Data.Repositories;
using Northwind.Data.Repositories.Contracts;
using Northwind.Data.UnitOfWork;
using Northwind.Infrastructure.Models;
using Northwind.Services;
using Northwind.Services.Contracts;

namespace Northwind.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services, DiContainers selectedContainer, AppData appData)
        {
            if (selectedContainer == DiContainers.AspNetCoreDependencyInjector)
            {
                BindRepositories(services);
                BindServices(services);
                BindDbContexts(services, appData);
            }

            return services;
        }

        public static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void BindServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDatabaseConfigurationService, DatabaseConfigurationService>();
        }

        public static void BindDbContexts(IServiceCollection services, AppData appData)
        {
            services.AddDbContext<MasterContext>(options => options.UseSqlServer(appData.MasterDbConnectionString));
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(appData.NorthwindDbConnectionString));
        }
    }
}
