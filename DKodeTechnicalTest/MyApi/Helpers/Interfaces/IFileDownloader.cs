
namespace MyApi.Helpers.Interfaces
{
    public interface IFileDownloader
    {
        public Task<bool> DownloadFile(BaseJob job);
    }
}
