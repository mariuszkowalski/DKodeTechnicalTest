using Serilog.Core;

namespace MyApi.Helpers.Handlers.Interfaces
{
    public interface IHandler
    {
        void SetNextHandler(IHandler handler);
        void CheckAndHandle(BaseJob job);
    }
}
