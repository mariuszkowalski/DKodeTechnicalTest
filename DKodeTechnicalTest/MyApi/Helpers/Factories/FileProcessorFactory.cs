using MyApi.Helpers.Factories.Interfaces;
using MyApi.Helpers.FileProcessors;
using MyApi.Helpers.FileProcessors.Interfaces;

namespace MyApi.Helpers.Factories
{
    public class FileProcessorFactory : IFileProcessorFactory
    {
        private readonly ILogger<FileProcessorFactory> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IFileProcessor _fileProcessor;

        public FileProcessorFactory(ILogger<FileProcessorFactory> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IFileProcessor CreateFileProcessor(BaseJob job)
        {
            if (job.FileType == FileType.Products)
            {
                _fileProcessor = ActivatorUtilities.CreateInstance<ProductsFileProcessor>(_serviceProvider);
            }
            else if (job.FileType == FileType.Stock)
            {
                _fileProcessor = ActivatorUtilities.CreateInstance<StockFileProcessor>(_serviceProvider);
            }
            else if (job.FileType == FileType.Prices)
            {
                _fileProcessor = ActivatorUtilities.CreateInstance<PricesFileProcessor>(_serviceProvider);
            }

            return _fileProcessor;

        }
    }
}
