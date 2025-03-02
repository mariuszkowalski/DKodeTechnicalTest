using Dapper;
using Microsoft.Data.Sqlite;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using Newtonsoft.Json;
using System.Data;
using System.Xml;

namespace MyApi.Models.DataAccess
{
    public class PricesDao : BaseDao, IPricesDao
    {
        private readonly ILogger<PricesDao> _logger;
        public PricesDao(ILogger<PricesDao> logger, IConfiguration configuration) : base(configuration)
        {
            _logger = logger;
        }

        public bool InsertIntoTable(PricesDto dto)
        {
            // Add update module. //
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@warehouse_id", dto.WarehouseId);
            param.Add("@SKU", dto.Sku);
            param.Add("@net_product_price", dto.NetProductPrice);
            param.Add("@net_product_price_after_discount", dto.NetProductPriceAfterDiscount);
            param.Add("@VAT_rate", dto.VATRate == null ? 0 : dto.VATRate);
            param.Add("@net_product_price_after_discount_logistic_unit", dto.NetProductPriceAfterDiscountLogisticUnit == 0.0 ? null : dto.NetProductPriceAfterDiscountLogisticUnit);

            try
            {
                _con.Execute("InsertIntoPrices", param, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed insterting data into PricesTable: {JsonConvert.SerializeObject(dto)}");
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
                result = _con.Query<string>("SelectAllSkusFromPrices", transaction: transaction, commandType: CommandType.StoredProcedure).ToHashSet<string>();
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

        public PricesDto SelectRecordBySKU(PricesDto dto)
        {
            var result = new PricesDto();
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@SKU", dto.Sku);

            try
            {
                result = _con.Query<PricesDto>("SelectFromPricesBySKU", param, transaction, commandType: CommandType.StoredProcedure).First();
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

        public bool UpdateRecordBySKU(PricesDto dto)
        {
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@warehouse_id", dto.WarehouseId);
            param.Add("@SKU", dto.Sku);
            param.Add("@net_product_price", dto.NetProductPrice);
            param.Add("@net_product_price_after_discount", dto.NetProductPriceAfterDiscount);
            param.Add("@VAT_rate", dto.VATRate == null ? 0 : dto.VATRate);
            param.Add("@net_product_price_after_discount_logistic_unit", dto.NetProductPriceAfterDiscountLogisticUnit == 0.0 ? null : dto.NetProductPriceAfterDiscountLogisticUnit);

            try
            {
                _con.Execute("UpdatePriceBySKU", param, transaction, commandType: CommandType.StoredProcedure);
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
