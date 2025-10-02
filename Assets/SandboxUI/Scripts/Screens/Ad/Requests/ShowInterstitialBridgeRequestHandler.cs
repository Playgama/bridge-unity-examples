using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Ad.Requests
{
    public class ShowInterstitialBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowInterstitial();
        }
    }
}