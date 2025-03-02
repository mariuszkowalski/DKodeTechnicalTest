using MyApi.Helpers.Interfaces;
using System;

namespace MyApi.Helpers.Handlers
{
    public class DownloadHandler : HandlerBase
    {
        private readonly ILogger<DownloadHandler> _logger;
        private readonly IFileDownloader _downlader;
        private readonly bool Debug = false;

        public DownloadHandler(ILogger<DownloadHandler> logger, IFileDownloader downloader)
        {
            _logger = logger;
            _downlader = downloader;
        }

        protected override bool CanHandle(BaseJob job)
        {
            if (string.IsNullOrEmpty(job.Uri.ToString()))
            {
                _logger.LogError($"Url for file download was empty");
                return false;
            }

            return true;
        }

        protected override void Handle(BaseJob job)
        {
            if(!Debug)
            {
                _logger.LogDebug($"Downloading file from: {job.Uri.ToString()}");

                var fileName = Path.GetFileName(job.Uri.LocalPath);
                var targetDir = Path.Combine(AppContext.BaseDirectory, "Files");

                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                var pathToFile = Path.Combine(targetDir, $"{fileName}");

                job.PathToFile = pathToFile;
                job.IsFileDownloaded = _downlader.DownloadFile(job).GetAwaiter().GetResult();
            
                _logger.LogDebug($"{this.GetType().Name} has finished succesfully");
            }
            else
            {
                switch (job.FileType)
                {
                    case FileType.Products:
                        job.PathToFile = @"D:\****\dotnet\Zadanie\DKodeTechnicalTest\DKodeTechnicalTest\MyApi\bin\Debug\net7.0\Files\Products.csv";
                        break;
                    case FileType.Stock:
                        job.PathToFile = @"D:\****\dotnet\Zadanie\DKodeTechnicalTest\DKodeTechnicalTest\MyApi\bin\Debug\net7.0\Files\Inventory.csv";
                        break;
                    case FileType.Prices:
                        job.PathToFile = @"D:\****\dotnet\Zadanie\DKodeTechnicalTest\DKodeTechnicalTest\MyApi\bin\Debug\net7.0\Files\Prices.csv";
                        break;
                }
                job.IsFileDownloaded = true;
            }
        }
    }
}
