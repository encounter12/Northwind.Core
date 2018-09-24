namespace Northwind.Services.Models
{
    public class OrderDetailsModel
    {
        public decimal Total { get; set; }

        public int ProductsCount { get; set; }

        public bool PossibleIssue { get; set; }
    }
}
