using Playgama;

namespace SandboxUI.Scripts.Requests
{
    public class ShowInterstitialBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowInterstitial();
        }
    }
}