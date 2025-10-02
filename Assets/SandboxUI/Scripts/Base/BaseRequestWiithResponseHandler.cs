namespace SandboxUI.Scripts
{
    public abstract class BaseBridgeRequestWithResponseHandler : BaseBridgeRequestHandler
    {
        protected abstract void SetResponse(string response);
    }
}