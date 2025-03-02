namespace MyApi.Models.DataTransfer
{
    public class StockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string? Unit { get; set; }
        public double Qty { get; set; }
        public string? Manufacturer { get; set; }
        public string Shipping { get; set; }
        public double? ShippingCost { get; set; }
    }
}
