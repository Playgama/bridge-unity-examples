using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Social.Requests
{
    public class ShareBridgeRequestHandler : BaseBridgeRequestHandler
    {
        private readonly Dictionary<string, object> _options = new()
        {
            ["text"] = "Test sharing text",
        };

        public override void SendRequest()
        {
            Bridge.social.Share(_options);
        }
    }
}