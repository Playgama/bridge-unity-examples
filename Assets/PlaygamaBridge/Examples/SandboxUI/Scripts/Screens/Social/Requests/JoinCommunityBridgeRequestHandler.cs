using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Social.Requests
{
    public class JoinCommunityBridgeRequestHandler : BaseBridgeRequestHandler
    {
        private readonly Dictionary<string, object> _options = new()
        {
            ["groupID"] = "testGroup"
        };

        public override void SendRequest()
        {
            Bridge.social.JoinCommunity(_options);
        }
    }
}