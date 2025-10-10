using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Leaderboards.Requests
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

            Bridge.leaderboards.GetEntries(_leaderboardIdInput.text, OnRequestCompleted);
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

        private void OnDisable()
        {
            _leaderboardIdInput.text = string.Empty;
        }
    }
}