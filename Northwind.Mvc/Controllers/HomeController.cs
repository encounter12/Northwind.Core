using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Helpers;
using Northwind.Mvc.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpHelpers httpHelpers;

        public HomeController(IHttpHelpers httpHelpers)
        {
            this.httpHelpers = httpHelpers;
        }

        public async Task<IActionResult> Index(string currentFilter, string searchString)
        {
            string url = "https://localhost:44377";
            string path = "customers";

            Response<List<CustomerListViewModel>> response = await this.httpHelpers
                .DoApiGet<List<CustomerListViewModel>>(url, path);

            List<CustomerListViewModel> customers = response.Data;

            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
            }

            ViewBag.CurrentFilter = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.ContactName.Contains(searchString)).ToList();
            }

            customers = customers.OrderBy(c => c.ContactName).ToList();

            return View(customers);
        }

        public async Task<IActionResult> CustomerDetails(string id)
        {
            string url = "https://localhost:44377";
            string customerDetailsPath = "customer/{id}";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<CustomerDetails> customerDetailsResponse = await this.httpHelpers
                .DoApiGet<CustomerDetails>(url, customerDetailsPath, CustomerIdParameter);

            HttpStatusCode statusCode = customerDetailsResponse.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
            }

            var customerDetailsViewModel = new CustomerDetailsOrdersModel();

            customerDetailsViewModel.CustomerDetails = customerDetailsResponse.Data;

            string customerOrdersPath = "customer/{id}/orders";

            Response<List<OrderDetailsViewModel>> customerOrdersResponse = await this.httpHelpers
                .DoApiGet<List<OrderDetailsViewModel>>(url, customerOrdersPath, CustomerIdParameter);

            statusCode = customerOrdersResponse.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
            }

            customerDetailsViewModel.Orders = customerOrdersResponse.Data;

            return View(customerDetailsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
