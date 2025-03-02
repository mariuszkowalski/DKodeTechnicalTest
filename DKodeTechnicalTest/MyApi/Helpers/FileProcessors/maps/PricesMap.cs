using CsvHelper.Configuration;
using MyApi.Models.DataTransfer;

namespace MyApi.Helpers.FileProcessors.maps
{
    public class PricesMap : ClassMap<PricesDto>
    {
        public PricesMap()
        {
            Map(m => m.WarehouseId).Index(0);
            Map(m => m.Sku).Index(1);
            Map(m => m.NetProductPrice).Index(2);
            Map(m => m.NetProductPriceAfterDiscount).Index(3);
            Map(m => m.VATRate).Convert(n =>
            {
                var val = n.Row.GetField(4);
                if (int.TryParse(val, out int value))
                {
                    return value;
                }
                else if (val == "O")
                {
                    return 0;
                }
                else
                {
                    return 0;
                }
            }).Default(0);
            Map(m => m.NetProductPriceAfterDiscountLogisticUnit).Index(5).Default(0);
        }
    }
}
