using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
{
    public class ShowRewardedBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.advertisement.ShowRewarded();
        }
    }
}