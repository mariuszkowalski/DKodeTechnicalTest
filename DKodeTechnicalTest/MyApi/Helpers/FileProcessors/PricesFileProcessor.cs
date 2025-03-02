using CsvHelper;
using CsvHelper.Configuration;
using MyApi.Helpers.FileProcessors.Interfaces;
using MyApi.Helpers.FileProcessors.maps;
using MyApi.Helpers.Interfaces;
using MyApi.Models.DataAccess.Interfaces;
using MyApi.Models.DataTransfer;
using System.Globalization;

namespace MyApi.Helpers.FileProcessors
{
    public class PricesFileProcessor : IFileProcessor
    {
        private readonly ILogger<PricesFileProcessor> _logger;
        private readonly IPricesDao _dao;
        public PricesFileProcessor(ILogger<PricesFileProcessor> logger, IPricesDao dao)
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
                csv.Context.RegisterClassMap<PricesMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    PricesDto? record = ReadCSVFile(csv);

                    if (record == null) { continue; }

                    if (!skus.Contains(record.Sku))
                    {
                        var success = _dao.InsertIntoTable(record);
                        numberOfFails += !success ? 1 : 0;
                        if (success) { skus.Add(record.Sku); }
                    }
                    else
                    {
                        numberOfFails += !_dao.UpdateRecordBySKU(record) ? 1 : 0;
                    }

                }
            }

            _logger.LogDebug($"Finished inserting data into PricesTable. Number of fails: {numberOfFails}");
        }

        private PricesDto? ReadCSVFile(CsvReader csv)
        {
            PricesDto? record = null;

            try
            {
                record = csv.GetRecord<PricesDto>();
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError($"There was malformed data row: {ex.Message}");
            }

            return record;
        }
    }
}
