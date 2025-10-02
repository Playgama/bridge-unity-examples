using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Social.Requests
{
    public class AddToHomeScreenBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.social.AddToHomeScreen();
        }
    }
}