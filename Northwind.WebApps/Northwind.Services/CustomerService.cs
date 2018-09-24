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

        public CustomerService(IHttpHelpers httpHelpers)
        {
            this.httpHelpers = httpHelpers;
        }

        public async Task<IEnumerable<CustomerListViewModel>> GetCustomers(string searchString)
        {
            string url = "https://localhost:44377";
            string path = "customers";

            Response<List<CustomerListViewModel>> response = await this.httpHelpers
                .DoApiGet<List<CustomerListViewModel>>(url, path);

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
            string url = "https://localhost:44377";
            string customerDetailsPath = "customer/{id}";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<CustomerDetails> customerDetailsResponse = await this.httpHelpers
                .DoApiGet<CustomerDetails>(url, customerDetailsPath, CustomerIdParameter);

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
