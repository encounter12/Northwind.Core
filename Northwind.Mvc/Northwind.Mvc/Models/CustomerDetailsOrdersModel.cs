using System.Collections.Generic;

namespace Northwind.Mvc.Models
{
    public class CustomerDetailsOrdersModel
    {
        public CustomerDetails CustomerDetails { get; set; }

        public IEnumerable<OrderDetailsViewModel> Orders { get; set; }
    }
}
