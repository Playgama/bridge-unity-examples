using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Ad.Requests
{
    public class ShowInterstitialBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowInterstitial();
        }
    }
}