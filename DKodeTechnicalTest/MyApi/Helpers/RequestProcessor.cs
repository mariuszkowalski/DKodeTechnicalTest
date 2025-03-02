using MyApi.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using MyApi.Helpers.Handlers;
using MyApi.Helpers.Handlers.Interfaces;

namespace MyApi.Helpers
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly ILogger<RequestProcessor> _logger;
        private readonly IServiceProvider _serviceProvider;
        public RequestProcessor(ILogger<RequestProcessor> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public int GetAllData()
        {
            _logger.LogDebug("Processing request for all files.");

            /*
            - Products.csv - https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv
            - Inventory.csv - https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv
            - Prices.csv - https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv
             */
            var jobs = new List<BaseJob>()
            {
                new BaseJob
                {
                    Uri = new Uri("http://127.0.0.1:8000/Products_small.csv"),
                    FileType = FileType.Products
                },
                new BaseJob
                {
                    Uri = new Uri("http://127.0.0.1:8000/Inventory_small.csv"),
                    FileType = FileType.Stock
                },
                new BaseJob
                {
                    Uri = new Uri("http://127.0.0.1:8000/Prices_small.csv"),
                    FileType = FileType.Prices
                }
            };

            var downloadHandler = ActivatorUtilities.CreateInstance<DownloadHandler>(_serviceProvider);
            var fileReaderHandler = ActivatorUtilities.CreateInstance<FileReaderHandler>(_serviceProvider);

            downloadHandler.SetNextHandler(fileReaderHandler);
            foreach(BaseJob job in jobs)
            {
                downloadHandler.CheckAndHandle(job);
            }

            return 1;
        }
    }
}
