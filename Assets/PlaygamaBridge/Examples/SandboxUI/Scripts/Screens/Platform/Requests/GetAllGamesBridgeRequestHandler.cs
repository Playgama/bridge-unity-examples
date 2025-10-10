using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Platform.Requests
{
    public class GetAllGamesBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.platform.GetAllGames(OnGetAllGamesResponse);
        }

        private void OnGetAllGamesResponse(bool success, List<Dictionary<string, string>> result)
        {
            if (!success)
            {
                SetResponse("Failed to get all games");
                return;
            }

            var strResult = JsonConvert.SerializeObject(result);
            SetResponse(strResult);
        }
    }
}
