using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
{
    public class ShowBannerBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowBanner();
        }
    }
}