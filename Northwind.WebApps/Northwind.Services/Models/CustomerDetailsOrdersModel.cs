using System.Collections.Generic;

namespace Northwind.Services.Models
{
    public class CustomerDetailsOrdersModel
    {
        public CustomerDetails CustomerDetails { get; set; }

        public IEnumerable<OrderDetailsViewModel> Orders { get; set; }
    }
}
