namespace SandboxUI.Scripts.Base
{
    public abstract class BaseBridgeRequestWithResponseHandler : BaseBridgeRequestHandler
    {
        protected abstract void SetResponse(string response);
    }
}