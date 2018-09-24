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
        private readonly IConfigurationService configurationService;

        public OrderService(IHttpHelpers httpHelpers, IConfigurationService configurationService)
        {
            this.httpHelpers = httpHelpers;
            this.configurationService = configurationService;
        }

        public async Task<List<OrderDetailsViewModel>> GetOrdersForCustomer(string id)
        {
            string customerOrdersPath = "customer/{id}/orders";

            Parameter CustomerIdParameter = new Parameter("id", id, ParameterType.UrlSegment);

            Response<List<OrderDetailsViewModel>> customerOrdersResponse = await this.httpHelpers.DoApiGet<List<OrderDetailsViewModel>>(
                this.configurationService.NorthwindApiUrl,
                customerOrdersPath,
                CustomerIdParameter);

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
