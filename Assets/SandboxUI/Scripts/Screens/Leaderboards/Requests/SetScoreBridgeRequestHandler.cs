using Playgama;
using SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.Leaderboards.Requests
{
    public class SetScoreBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _scoreInput;
        [SerializeField] private TMP_InputField _leaderboardIdInput;

        public override void SendRequest()
        {
            if (string.IsNullOrEmpty(_scoreInput.text) || string.IsNullOrEmpty(_leaderboardIdInput.text))
            {
                return;
            }

            if (!int.TryParse(_scoreInput.text, out var score))
            {
                return;
            }

            Bridge.leaderboards.SetScore(_leaderboardIdInput.text, score);
        }

        private void OnDisable()
        {
            _scoreInput.text = string.Empty;
            _leaderboardIdInput.text = string.Empty;
        }
    }
}