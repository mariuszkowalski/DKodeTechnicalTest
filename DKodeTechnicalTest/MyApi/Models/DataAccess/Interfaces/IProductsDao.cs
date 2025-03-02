using MyApi.Models.DataTransfer;

namespace MyApi.Models.DataAccess.Interfaces
{
    public interface IProductsDao
    {
        bool InsertIntoTable(ProductsDto dto);
        bool UpdateRecordBySKU(ProductsDto dto);
        ProductsDto SelectRecordBySKU(ProductsDto dto);
        HashSet<string> SelectAllSkus();
    }
}
