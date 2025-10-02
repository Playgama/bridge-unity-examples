using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.RemoteConfig.Requests
{
    public class GetRemoteConfigBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.remoteConfig.Get(HandleRequestCompleted);
        }

        private void HandleRequestCompleted(bool success, Dictionary<string, string> response)
        {
            if (!success)
            {
                SetResponse("Request failed.");
                return;
            }

            var strResponse = JsonConvert.SerializeObject(response);
            SetResponse(strResponse);
        }
    }
}