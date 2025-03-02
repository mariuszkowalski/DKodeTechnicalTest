namespace MyApi.Models.DataTransfer
{
    public class PricesDto
    {
        public int Id { get; set; }
        public string WarehouseId { get; set; }
        public string Sku { get; set; }
        public double NetProductPrice { get; set; }
        public double NetProductPriceAfterDiscount { get; set; }
        public int? VATRate { get; set; }
        public double? NetProductPriceAfterDiscountLogisticUnit { get; set; }
    }
}
