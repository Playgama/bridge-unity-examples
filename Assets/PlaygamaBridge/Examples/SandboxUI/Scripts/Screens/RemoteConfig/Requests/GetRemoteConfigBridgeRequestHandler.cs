using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.RemoteConfig.Requests
{
    public class GetRemoteConfigBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.remoteConfig.Get(OnRequestCompleted);
        }

        private void OnRequestCompleted(bool success, Dictionary<string, string> response)
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