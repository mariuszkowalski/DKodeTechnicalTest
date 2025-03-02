using CsvHelper.Configuration;
using MyApi.Models.DataTransfer;

namespace MyApi.Helpers.FileProcessors.maps
{
    public class StockMap : ClassMap<StockDto>
    {
        public StockMap()
        {

            Map(m => m.ProductId).Name("product_id");
            Map(m => m.Sku).Name("sku");
            Map(m => m.Unit).Name("unit").Default("NULL");
            Map(m => m.Qty).Name("qty").Default(0);
            Map(m => m.Manufacturer).Name("manufacturer_name").Default("NULL");
            Map(m => m.Shipping).Name("shipping");
            Map(m => m.ShippingCost).Name("shipping_cost").Default(0);

        }
    }
}
