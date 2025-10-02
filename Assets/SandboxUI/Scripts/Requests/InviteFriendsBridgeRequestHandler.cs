using System.Collections.Generic;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
{
    public class InviteFriendsBridgeRequestHandler : BaseBridgeRequestHandler
    {
        private readonly Dictionary<string, object> _options = new()
        {
            ["text"] = "Test invite friends text"
        };

        public override void SendRequest()
        {
            Bridge.social.InviteFriends(_options);
        }
    }
}