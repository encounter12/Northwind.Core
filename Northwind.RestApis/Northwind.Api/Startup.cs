using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.DI;
using Northwind.Infrastructure.Models;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 44346;
            });

            AppData appData = Configuration.GetSection("AppData").Get<AppData>();

            services.AddDependencyInjection(DiContainers.AspNetCoreDependencyInjector, appData);

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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

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
