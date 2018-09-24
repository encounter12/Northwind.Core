namespace Northwind.Data.DataTransferObjects
{
    public class OrderDetailsDTO
    {
        public decimal Total { get; set; }

        public int ProductsCount { get; set; }

        public bool PossibleIssue { get; set; }
    }
}
