using Dapper;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;

namespace MyApi.Models.DataAccess
{
    public class ProductsDao : BaseDao, IProductsDao
    {
        private readonly ILogger<ProductsDao> _logger;
        public ProductsDao(ILogger<ProductsDao> logger, IConfiguration configuration) : base(configuration)
        {
            _logger = logger;
        }

        public bool InsertIntoTable(ProductsDto dto)
        {
            // Add update module //
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();
            
            var param = new DynamicParameters();
            param.Add("@Id", dto.Id);
            param.Add("@SKU", dto.Sku);
            param.Add("@name", dto.Name == "NULL" ? null : dto.Name);
            param.Add("@EAN", dto.Ean == 0 ? null : dto.Ean);
            param.Add("@producer_name", dto.ProducerName);
            param.Add("@category", dto.Category == "NULL" ? null : dto.Category);
            param.Add("@is_wire", dto.IsWire);
            param.Add("@shipping", dto.Shipping);
            param.Add("@available", dto.Avaliable);
            param.Add("@is_vendor", dto.IsVendor);
            param.Add("@default_image", dto.DefaultImage == "NULL" ? null : dto.DefaultImage);

            try
            {
                _con.Execute("InsertIntoProducts", param, transaction);

                transaction.Commit();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed insterting data into ProductsTable: {JsonConvert.SerializeObject(dto)}");
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
                result = _con.Query<string>("SelectAllSkusFromProducts", transaction: transaction, commandType: CommandType.StoredProcedure).ToHashSet<string>();
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

        public ProductsDto SelectRecordBySKU(ProductsDto dto)
        {
            var result = new ProductsDto();
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@SKU", dto.Sku);

            try
            {
                result = _con.Query<ProductsDto>("SelectFromProductsBySKU", param, transaction, commandType: CommandType.StoredProcedure).First();
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

        public bool UpdateRecordBySKU(ProductsDto dto)
        {
            bool isSuccess = true;
            _con.Open();
            using var transaction = _con.BeginTransaction();

            var param = new DynamicParameters();
            param.Add("@SKU", dto.Sku);
            param.Add("@name", dto.Name == "NULL" ? null : dto.Name);
            param.Add("@EAN", dto.Ean == 0 ? null : dto.Ean);
            param.Add("@producer_name", dto.ProducerName);
            param.Add("@category", dto.Category == "NULL" ? null : dto.Category);
            param.Add("@is_wire", dto.IsWire);
            param.Add("@shipping", dto.Shipping);
            param.Add("@available", dto.Avaliable);
            param.Add("@is_vendor", dto.IsVendor);
            param.Add("@default_image", dto.DefaultImage == "NULL" ? null : dto.DefaultImage);

            try
            {
                 _con.Execute("UpdateProductBySKU", param, transaction, commandType: CommandType.StoredProcedure);
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
