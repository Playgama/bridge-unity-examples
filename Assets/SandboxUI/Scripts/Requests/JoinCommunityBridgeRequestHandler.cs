using System.Collections.Generic;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
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