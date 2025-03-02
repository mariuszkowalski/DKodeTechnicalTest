namespace MyApi.Helpers
{
    public class BaseJob
    {
        public required Uri Uri { get; set; }
        public string PathToFile { get; set; } = string.Empty;
        public FileType FileType { get; set; }
        public bool IsFileDownloaded {get; set;}
    }
}
