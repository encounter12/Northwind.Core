using Northwind.Mvc.Helpers;
using Northwind.Mvc.Models;
using Northwind.Mvc.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Northwind.Mvc.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpHelpers httpHelpers;

        public OrderService(IHttpHelpers httpHelpers)
        {
            this.httpHelpers = httpHelpers;
        }

        public async Task<List<OrderDetailsViewModel>> GetOrdersForCustomer(string id)
        {
            string url = "https://localhost:44377";
            string customerOrdersPath = "customer/{id}/orders";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<List<OrderDetailsViewModel>> customerOrdersResponse = await this.httpHelpers
                .DoApiGet<List<OrderDetailsViewModel>>(url, customerOrdersPath, CustomerIdParameter);

            HttpStatusCode statusCode = customerOrdersResponse.StatusCode;

            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception($"The status code should be OK, but it currently is: {statusCode}");
            }

            List<OrderDetailsViewModel> orderDetails = customerOrdersResponse.Data;

            return orderDetails;
        }
    }
}
