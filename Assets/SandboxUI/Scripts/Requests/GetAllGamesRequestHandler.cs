using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;

namespace SandboxUI.Scripts.Requests
{
    public class GetAllGamesRequestHandler : BaseRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.platform.GetAllGames(HandleGetAllGamesResponse);
        }

        private void HandleGetAllGamesResponse(bool success, List<Dictionary<string, string>> result)
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
