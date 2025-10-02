using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
{
    public class AddToFavoritesBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            Bridge.social.AddToFavorites();
        }
    }
}