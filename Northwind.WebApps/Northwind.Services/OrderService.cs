using Northwind.Services.Helpers;
using Northwind.Services.Models;
using Northwind.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpHelpers httpHelpers;
        private const string ApiUrl = "http://localhost:5000";

        public OrderService(IHttpHelpers httpHelpers)
        {
            this.httpHelpers = httpHelpers;
        }

        public async Task<List<OrderDetailsViewModel>> GetOrdersForCustomer(string id)
        {
            string customerOrdersPath = "customer/{id}/orders";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<List<OrderDetailsViewModel>> customerOrdersResponse = await this.httpHelpers
                .DoApiGet<List<OrderDetailsViewModel>>(ApiUrl, customerOrdersPath, CustomerIdParameter);

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
