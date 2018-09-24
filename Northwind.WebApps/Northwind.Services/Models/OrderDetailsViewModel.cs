using System.ComponentModel.DataAnnotations;

namespace Northwind.Services.Models
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "Total Amount")]
        public decimal Total { get; set; }

        [Display(Name = "Products Count")]
        public int ProductsCount { get; set; }

        [Display(Name = "Possible Issue")]
        public bool PossibleIssue { get; set; }
    }
}
