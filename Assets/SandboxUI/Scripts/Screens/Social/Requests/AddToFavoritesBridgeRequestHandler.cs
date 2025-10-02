using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Social.Requests
{
    public class AddToFavoritesBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.social.AddToFavorites();
        }
    }
}