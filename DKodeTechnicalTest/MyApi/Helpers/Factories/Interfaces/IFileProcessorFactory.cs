using MyApi.Helpers.FileProcessors.Interfaces;

namespace MyApi.Helpers.Factories.Interfaces
{
    public interface IFileProcessorFactory
    {
        public IFileProcessor CreateFileProcessor(BaseJob job);
    }
}
