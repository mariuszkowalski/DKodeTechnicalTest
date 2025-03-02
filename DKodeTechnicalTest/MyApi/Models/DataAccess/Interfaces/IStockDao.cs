using MyApi.Models.DataTransfer;

namespace MyApi.Models.DataAccess.Interfaces
{
    public interface IStockDao
    {
        bool InsertIntoTable(StockDto dto);
        bool UpdateRecordBySKU(StockDto dto);
        StockDto SelectRecordBySKU(StockDto dto);
        HashSet<string> SelectAllSkus();
    }
}
