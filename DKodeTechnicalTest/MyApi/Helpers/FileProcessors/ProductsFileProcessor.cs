using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using MyApi.Helpers.FileProcessors.Interfaces;
using MyApi.Helpers.FileProcessors.maps;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using System.ComponentModel;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace MyApi.Helpers.FileProcessors
{
    public class ProductsFileProcessor : IFileProcessor
    {
        private readonly ILogger<ProductsFileProcessor> _logger;
        private readonly IProductsDao _dao;
        public ProductsFileProcessor(ILogger<ProductsFileProcessor> logger, IProductsDao dao)
        {
            _logger = logger;
            _dao = dao;
        }

        public void ProcessFile(BaseJob job)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = false,
                Delimiter = ";"
            };

            int numberOfFails = 0;
            var skus = _dao.SelectAllSkus();

            using (var reader = new StreamReader(job.PathToFile))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.TypeConverterOptionsCache.GetOptions<string?>().NullValues.Add(null);
                csv.Context.TypeConverterOptionsCache.GetOptions<int?>().NullValues.Add(null);
                csv.Context.TypeConverterOptionsCache.GetOptions<Int64?>().NullValues.Add(null);
                csv.Context.RegisterClassMap<ProductsMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    ProductsDto? record = ReadCSVFile(csv);

                    if(record == null) { continue; }

                    ProductsDto? filtered = FilterData(record);

                    if (filtered == null) { continue; }

                    if(!skus.Contains(filtered.Sku))
                    {
                        var success = _dao.InsertIntoTable(filtered);
                        numberOfFails += !success ? 1 : 0;
                        if (success) { skus.Add(filtered.Sku); }
                    }
                    else
                    {
                        numberOfFails += !_dao.UpdateRecordBySKU(filtered) ? 1 : 0;
                    }

                }
            }

            _logger.LogDebug($"Finished inserting data into ProductsTable. Number of fails: {numberOfFails}");
        }

        private ProductsDto? FilterData(ProductsDto data)
        {
            return !data.IsWire && data.Shipping == "24h" ? data : null;
        }

        private ProductsDto? ReadCSVFile(CsvReader csv)
        {
            ProductsDto? record = null;

            try
            {
                record = csv.GetRecord<ProductsDto>();
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError($"There was malformed data row: {ex.Message}");
            }

            return record;
        }
    }
}
