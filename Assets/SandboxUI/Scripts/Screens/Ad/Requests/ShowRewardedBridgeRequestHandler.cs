using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Ad.Requests
{
    public class ShowRewardedBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowRewarded();
        }
    }
}