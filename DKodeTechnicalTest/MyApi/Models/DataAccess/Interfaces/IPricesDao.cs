using Microsoft.Extensions.ObjectPool;
using MyApi.Models.DataTransfer;

namespace MyApi.Models.DataAccess.Interfaces
{
    public interface IPricesDao
    {
        bool InsertIntoTable(PricesDto dto);
        bool UpdateRecordBySKU(PricesDto dto);
        PricesDto SelectRecordBySKU(PricesDto dto);
        HashSet<string> SelectAllSkus();
    }
}
