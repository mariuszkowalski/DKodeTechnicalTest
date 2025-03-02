using CsvHelper.Configuration;
using MyApi.Models.DataTransfer;

namespace MyApi.Helpers.FileProcessors.maps
{
    public class ProductsMap : ClassMap<ProductsDto>
    {
        public ProductsMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Sku).Name("SKU");
            Map(m => m.Name).Name("name").Default("NULL");
            Map(m => m.Ean).Name("EAN").Default(0);
            Map(m => m.ProducerName).Name("producer_name");
            Map(m => m.Category).Name("category").Default("NULL");
            Map(m => m.IsWire).Name("is_wire");
            Map(m => m.Shipping).Name("shipping");
            Map(m => m.Avaliable).Name("available");
            Map(m => m.IsVendor).Name("is_vendor");
            Map(m => m.DefaultImage).Name("default_image").Default("NULL");
        }

    }
}
