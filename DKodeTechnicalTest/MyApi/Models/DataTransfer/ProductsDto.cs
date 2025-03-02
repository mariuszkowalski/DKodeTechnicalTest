using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApi.Models.DataTransfer
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string? Name { get; set; }
        public Int64? Ean { get; set; }
        public string ProducerName { get; set; }
        public string? Category { get; set; }
        public bool IsWire { get; set; }
        public string Shipping { get; set; }
        public bool Avaliable { get; set; }
        public bool IsVendor { get; set; }
        public string? DefaultImage { get; set; }
    }
}
