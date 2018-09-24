using Northwind.Services.Helpers;
using Northwind.Services.Models;
using Northwind.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpHelpers httpHelpers;
        private readonly IConfigurationService configurationService;

        public CustomerService(IHttpHelpers httpHelpers, IConfigurationService configurationService)
        {
            this.httpHelpers = httpHelpers;
            this.configurationService = configurationService;
        }

        public async Task<IEnumerable<CustomerListViewModel>> GetCustomers(string searchString)
        {
            string path = "customers";

            Response<List<CustomerListViewModel>> response = await this.httpHelpers
                .DoApiGet<List<CustomerListViewModel>>(this.configurationService.NorthwindApiUrl, path);

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception($"The status code should be OK, but it currently is: {statusCode}");
            }

            List<CustomerListViewModel> customers = response.Data;

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.ContactName.Contains(searchString)).ToList();
            }

            customers = customers.OrderBy(c => c.ContactName).ToList();

            return customers;
        }

        public async Task<CustomerDetails> GetCustomerDetails(string id)
        {
            string customerDetailsPath = "customer/{id}";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<CustomerDetails> customerDetailsResponse = await this.httpHelpers
                .DoApiGet<CustomerDetails>(this.configurationService.NorthwindApiUrl, customerDetailsPath, CustomerIdParameter);

            HttpStatusCode statusCode = customerDetailsResponse.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception($"The response should have status OK, but currently it is: {statusCode}");
            }

            CustomerDetails customerDetails = customerDetailsResponse.Data;

            return customerDetails;
        }
    }
}
