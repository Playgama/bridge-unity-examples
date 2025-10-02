using Playgama;

namespace SandboxUI.Scripts.Requests
{
    public class HideBannerBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.HideBanner();
        }
    }
}