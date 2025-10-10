using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Achievements.Requests
{
    public class UnlockAchievementBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _achievementKey;
        [SerializeField] private TMP_InputField _achievementName;

        public override void SendRequest()
        {
            var options = new Dictionary<string, object>()
            {
                ["achievement"] = _achievementName.text,
                ["achievementKey"] = _achievementKey.text,
            };

            Bridge.achievements.Unlock(options);
        }

        private void OnDisable()
        {
            _achievementKey.text = string.Empty;
            _achievementName.text = string.Empty;
        }
    }
}