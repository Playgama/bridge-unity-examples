using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts.Requests
{
    public class GetGameByIdRequestHandler : BaseRequestWithMultilineResponseHandler
    {
        [SerializeField] private TMP_InputField _gameIdInput;

        public override void SendRequest()
        {
            if (string.IsNullOrEmpty(_gameIdInput.text))
            {
                SetResponse("Game ID is empty");
                return;
            }

            var options = new Dictionary<string, object>
            {
                ["gameId"] = _gameIdInput.text,
            };

            Bridge.platform.GetGameById(options, HandleGetGameByIdResponse);
        }

        private void HandleGetGameByIdResponse(bool success, Dictionary<string, string> result)
        {
            if (!success)
            {
                SetResponse("Failed to get game by id");
                return;
            }

            var strResult = JsonConvert.SerializeObject(result);
            SetResponse(strResult);
        }
    }
}