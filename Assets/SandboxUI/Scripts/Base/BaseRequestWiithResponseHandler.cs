namespace SandboxUI.Scripts
{
    public abstract class BaseRequestWithResponseHandler : BaseRequestHandler
    {
        protected abstract void SetResponse(string response);
    }
}