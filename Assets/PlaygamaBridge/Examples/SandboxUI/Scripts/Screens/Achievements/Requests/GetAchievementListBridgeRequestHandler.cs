using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Achievements.Requests
{
    public class GetAchievementListBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.achievements.GetList(new Dictionary<string, object>(), OnRequestCompleted);
        }

        private void OnRequestCompleted(bool success, List<Dictionary<string, string>> response)
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