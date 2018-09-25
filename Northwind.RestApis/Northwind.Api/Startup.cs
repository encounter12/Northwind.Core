using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data.Models;
using Northwind.Data.Repositories;
using Northwind.Data.Repositories.Contracts;
using Northwind.Data.UnitOfWork;
using Northwind.Services;
using Northwind.Services.Contracts;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Northwind.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Northwind REST API",
                    Description = "A simple API for working with Northwind database",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Ivaylo Botusharov",
                        Email = string.Empty,
                        Url = "https://twitter.com/ivo.botusharov"
                    },
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://example.com/license"
                    }
                });
            });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connection));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Northwind REST API";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind REST API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
