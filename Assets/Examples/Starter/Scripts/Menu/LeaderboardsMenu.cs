using System;
using Newtonsoft.Json;
using Playgama.Examples.Starter.Scripts.Playgama;
#if UNITY_WEBGL
using Playgama.Modules.Leaderboards;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class LeaderboardsMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;

        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
#if UNITY_WEBGL
        public void SetScore()
        {
            if (string.IsNullOrEmpty(menuSettings.InputFieldScore.text) ||
                string.IsNullOrEmpty(menuSettings.InputFieldLeaderboardId.text))
            {
                Debug.LogWarning("Score and Leaderboard ID are required");
                return;           
            }
            
            var leaderboardId = menuSettings.InputFieldLeaderboardId.text;
            var score = menuSettings.InputFieldScore.text;
            PlaygamaManager.SetScore(leaderboardId, int.Parse(score), (success) => {
                if (success)
                {
                    Debug.Log($"Score set successfully: {score}");
                }
                else
                {
                    Debug.Log("Failed to set score");
                }
            });
        }

        public void GetEntries()
        {
            if (string.IsNullOrEmpty(menuSettings.InputFieldLeaderboardIdGetEntries.text))
            {
                Debug.LogWarning("Leaderboard ID is required");
                return;           
            }
            
            var leaderboardId = menuSettings.InputFieldLeaderboardIdGetEntries.text;
            PlaygamaManager.GetEntries(leaderboardId, (success, entries) => {
                if (success)
                {
                    menuSettings.TextRequestResult.text = JsonConvert.SerializeObject(entries, Formatting.Indented);
                    Debug.Log($"Entries retrieved successfully: {entries.Count}");
                }
                else
                {
                    Debug.Log("Failed to retrieve entries");
                }
            });
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextLeaderboardsType, "Leaderboards Type", PlaygamaManager.LeaderboardType.ToString());
            menuSettings.InputFieldScore.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;
            menuSettings.InputFieldLeaderboardId.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;
            menuSettings.ButtonSetScore.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;

            menuSettings.InputFieldLeaderboardIdGetEntries.interactable = PlaygamaManager.LeaderboardType == LeaderboardType.InGame;
            menuSettings.ButtonGetEntries.interactable = PlaygamaManager.LeaderboardType == LeaderboardType.InGame;
        }
#endif
        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextLeaderboardsType => textLeaderboardsType;
            public TMP_InputField InputFieldScore => inputFieldScore;
            public TMP_InputField InputFieldLeaderboardId => inputFieldLeaderboardId;
            public TMP_InputField InputFieldLeaderboardIdGetEntries => inputFieldLeaderboardIdGetEntries;
            public Button ButtonSetScore => buttonSetScore;
            public Button ButtonGetEntries => buttonGetEntries;
            public TMP_Text TextRequestResult => textRequestResult;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textLeaderboardsType;
            [SerializeField] private TMP_InputField inputFieldScore;
            [SerializeField] private TMP_InputField inputFieldLeaderboardId;
            [SerializeField] private TMP_InputField inputFieldLeaderboardIdGetEntries;
            [SerializeField] private Button buttonSetScore;
            [SerializeField] private Button buttonGetEntries;
            [SerializeField] private TMP_Text textRequestResult;
        }
    }
}