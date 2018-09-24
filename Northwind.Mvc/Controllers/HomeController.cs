using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Northwind.Mvc.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService customerService;

        private readonly IOrderService orderService;

        public HomeController(ICustomerService customerService, IOrderService orderService)
        {
            this.customerService = customerService;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<CustomerListViewModel> customers = null;

            try
            {
                customers = await this.customerService.GetCustomers(searchString);
                ViewBag.CurrentFilter = searchString;
            }
            catch (Exception)
            {
                RedirectToAction("Error") ;
            }

            return View(customers);
        }

        public async Task<IActionResult> CustomerDetails(string id)
        {
            var customerDetailsOrdersModel = new CustomerDetailsOrdersModel();

            try
            {
                customerDetailsOrdersModel.CustomerDetails = await this.customerService.GetCustomerDetails(id);
            }
            catch (Exception)
            {
                RedirectToAction("Error");
            }

            try
            {
                customerDetailsOrdersModel.Orders = await this.orderService.GetOrdersForCustomer(id);
            }
            catch (Exception)
            {
                RedirectToAction("Error");
            }

            return View(customerDetailsOrdersModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
