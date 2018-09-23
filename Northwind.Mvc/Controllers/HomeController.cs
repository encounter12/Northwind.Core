using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Helpers;
using Northwind.Mvc.Models;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<IActionResult> Index()
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

            return View(customers);
        }

        public async Task<IActionResult> CustomerDetails(string id)
        {
            string url = "https://localhost:44377";
            string path = "customer/{id}/orders";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<List<OrderDetailsViewModel>> response = await this.httpHelpers
                .DoApiGet<List<OrderDetailsViewModel>>(url, path, CustomerIdParameter);

            List<OrderDetailsViewModel> orders = response.Data;

            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
            }

            return View(orders);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
