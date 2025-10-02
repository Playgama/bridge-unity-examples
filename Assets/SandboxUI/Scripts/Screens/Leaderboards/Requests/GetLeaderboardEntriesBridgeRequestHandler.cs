using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.Leaderboards.Requests
{
    public class GetLeaderboardEntriesBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        [SerializeField] private TMP_InputField _leaderboardIdInput;
        public override void SendRequest()
        {
            if (string.IsNullOrEmpty(_leaderboardIdInput.text))
            {
                SetResponse("Please enter a leaderboard ID.");
            }

            Bridge.leaderboards.GetEntries(_leaderboardIdInput.text, HandleRequestCompleted);
        }

        private void HandleRequestCompleted(bool success, List<Dictionary<string, string>> response)
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