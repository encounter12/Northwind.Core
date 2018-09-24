using System.ComponentModel.DataAnnotations;

namespace Northwind.Mvc.Models
{
    public class CustomerListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Orders Count")]
        public int OrdersCount { get; set; }
    }
}
