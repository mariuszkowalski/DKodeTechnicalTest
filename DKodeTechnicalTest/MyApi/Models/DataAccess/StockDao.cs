using Dapper;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;

namespace MyApi.Models.DataAccess
{
    public class StockDao : BaseDao, IStockDao
    {
        private readonly ILogger<StockDao> _logger;
        public StockDao(ILogger<StockDao> logger, IConfiguration configuration) : base(configuration)
        {
            _logger = logger;
        }

        public bool InsertIntoTable(StockDto dto)
        {
            // Add update module. //
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@product_id", dto.ProductId);
            param.Add("@SKU", dto.Sku);
            param.Add("@unit", dto.Unit == "NULL" ? null : dto.Unit);
            param.Add("@qty", dto.Qty);                                                   
            param.Add("@manufacturer", dto.Manufacturer == "NULL" ? null : dto.Manufacturer);
            param.Add("@shipping", dto.Shipping);
            param.Add("@shipping_cost", dto.ShippingCost == 0.0 ? null : dto.ShippingCost);

            try
            {
                _con.Execute("InsertIntoStock", param, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed insterting data into StockTable: {JsonConvert.SerializeObject(dto)}");
                transaction.Rollback();
                isSuccess = false;
            }
            finally
            {
                _con.Close();
            }

            return isSuccess;
        }

        public HashSet<string> SelectAllSkus()
        {
            var result = new HashSet<string>();
            _con.Open();
            using var transaction = _con.BeginTransaction();

            try
            {
                result = _con.Query<string>("SelectAllSkusFromStock", transaction: transaction, commandType: CommandType.StoredProcedure).ToHashSet<string>();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                _con.Close();
            }

            return result;
        }

        public StockDto SelectRecordBySKU(StockDto dto)
        {
            var result = new StockDto();
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@SKU", dto.Sku);

            try
            {
                result = _con.Query<StockDto>("SelectFromStockBySKU", param, transaction, commandType: CommandType.StoredProcedure).First();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                _con.Close();
            }

            return result;
        }

        public bool UpdateRecordBySKU(StockDto dto)
        {
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@product_id", dto.ProductId);
            param.Add("@SKU", dto.Sku);
            param.Add("@unit", dto.Unit == "NULL" ? null : dto.Unit);
            param.Add("@qty", dto.Qty);
            param.Add("@manufacturer", dto.Manufacturer == "NULL" ? null : dto.Manufacturer);
            param.Add("@shipping", dto.Shipping);
            param.Add("@shipping_cost", dto.ShippingCost == 0.0 ? null : dto.ShippingCost);

            try
            {
                _con.Execute("UpdateStockBySKU", param, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                transaction.Rollback();
            }
            finally
            {
                _con.Close();
            }

            return isSuccess;
        }
    }
}
