using Serilog.Core;
using MyApi.Helpers.Handlers.Interfaces;

namespace MyApi.Helpers.Handlers
{
    public abstract class HandlerBase : IHandler
    {
        private IHandler _nextHandler;

        public void SetNextHandler(IHandler handler)
        {
            _nextHandler = handler;
        }

        public virtual void CheckAndHandle(BaseJob job)
        {
            if(CanHandle(job))
            {
                Handle(job);
            }

            if(_nextHandler != null)
            {
                _nextHandler.CheckAndHandle(job);
            }
        }

        protected abstract bool CanHandle(BaseJob job);
        protected abstract void Handle(BaseJob job);
    }
}
