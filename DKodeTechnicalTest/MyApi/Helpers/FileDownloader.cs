using MyApi.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApi.Helpers
{
    public class FileDownloader : IFileDownloader
    {
        private readonly ILogger<FileDownloader> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public FileDownloader(ILogger<FileDownloader> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", _configuration.GetValue<string>("UserAgent"));
        }

        public async Task<bool> DownloadFile(BaseJob job)
        {            
            try
            {
                using var hm = new HttpRequestMessage(HttpMethod.Get, job.Uri);
                using var filestream = new FileStream(job.PathToFile, FileMode.Create);
                using var netstream = await _httpClient.GetStreamAsync(hm.RequestUri);
                
                _logger.LogDebug($"Finished downloading file: {job.Uri.ToString()}");
                
                await netstream.CopyToAsync(filestream);
                
                _logger.LogDebug($"Finished saving file: {job.PathToFile}");

            }
            catch(Exception ex)
            {
                _logger.LogError($"SOmething went wrong while downloading file {job.Uri.ToString()}");
                
                return false;
            }

            return true;

        }
    }
}
