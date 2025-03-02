using MyApi.Helpers.Factories.Interfaces;

namespace MyApi.Helpers.Handlers
{
    public class FileReaderHandler : HandlerBase
    {
        private readonly ILogger<FileReaderHandler> _logger;
        private readonly IFileProcessorFactory _fileProcessorFactory;

        public FileReaderHandler(ILogger<FileReaderHandler> logger, IFileProcessorFactory fileProcessorFactory)
        {
            _logger = logger;
            _fileProcessorFactory = fileProcessorFactory;
        }
        protected override bool CanHandle(BaseJob job)
        {
            if(!job.IsFileDownloaded)
            {
                _logger.LogError($"File was not downloaded correctly.");
                return false;
            }

            return true;
        }

        protected override void Handle(BaseJob job)
        {
            var fileProcessor = _fileProcessorFactory.CreateFileProcessor(job);
            fileProcessor.ProcessFile(job);

            _logger.LogDebug($"{this.GetType().Name} has finished succesfully");
        }

    }
}
