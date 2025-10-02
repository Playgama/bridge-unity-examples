using System.Collections.Generic;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Requests
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