using CsvHelper.Configuration;
using CsvHelper;
using MyApi.Helpers.FileProcessors.Interfaces;
using MyApi.Helpers.FileProcessors.maps;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using System.Globalization;

namespace MyApi.Helpers.FileProcessors
{
    public class StockFileProcessor : IFileProcessor
    {
        private readonly ILogger<StockFileProcessor> _logger;
        private readonly IStockDao _dao;
        public StockFileProcessor(ILogger<StockFileProcessor> logger, IStockDao dao)
        {
            _logger = logger;
            _dao = dao;
        }

        public void ProcessFile(BaseJob job)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = false,
                Delimiter = ",",
                MissingFieldFound = null
            };

            int numberOfFails = 0;
            var skus = _dao.SelectAllSkus();

            using (var reader = new StreamReader(job.PathToFile))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.TypeConverterOptionsCache.GetOptions<string?>().NullValues.Add(null);
                csv.Context.TypeConverterOptionsCache.GetOptions<int?>().NullValues.Add(null);
                csv.Context.TypeConverterOptionsCache.GetOptions<Int64?>().NullValues.Add(null);
                csv.Context.RegisterClassMap<StockMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    StockDto? record = ReadCSVFile(csv);

                    if (record == null) { continue; }

                    StockDto? filtered = FilterData(record);

                    if (filtered == null) { continue; }
                    
                    if (!skus.Contains(record.Sku))
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

            _logger.LogDebug($"Finished inserting data into StockTable. Number of fails: {numberOfFails}");
        }

        private StockDto? FilterData(StockDto data)
        {
            return data.Shipping == "24h" ? data : null;
        }

        private StockDto? ReadCSVFile(CsvReader csv)
        {
            StockDto? record = null;

            try
            {
                record = csv.GetRecord<StockDto>();
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError($"There was malformed data row: {ex.Message}");
            }

            return record;
        }
    }
}
