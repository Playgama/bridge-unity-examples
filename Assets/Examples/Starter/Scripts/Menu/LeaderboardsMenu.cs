using System;
using Examples.Starter.Scripts.Playgama;
using Newtonsoft.Json;
using Playgama.Modules.Leaderboards;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Examples.Starter.Scripts.Menu
{
    public class LeaderboardsMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;

        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

        public override void Open()
        {
            base.Open();
            InitMenu();
        }

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

        private void Start()
        {
            InitMenu();
        }

        private void InitMenu()
        {
            SetTextProperty(menuSettings.TextLeaderboardsType, "Leaderboards Type", PlaygamaManager.LeaderboardType.ToString());
            menuSettings.InputFieldScore.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;
            menuSettings.InputFieldLeaderboardId.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;
            menuSettings.ButtonSetScore.interactable = PlaygamaManager.LeaderboardType != LeaderboardType.NotAvailable;

            menuSettings.InputFieldLeaderboardIdGetEntries.interactable = PlaygamaManager.LeaderboardType == LeaderboardType.InGame;
            menuSettings.ButtonGetEntries.interactable = PlaygamaManager.LeaderboardType == LeaderboardType.InGame;
        }
        
        private void SetTextProperty(TextMeshProUGUI text, string name, string value)
        {
            text.text = $"{name}: <color=#D8BBFF>{value}</color>";
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textLeaderboardsType;
            [SerializeField] private TMP_InputField inputFieldScore;
            [SerializeField] private TMP_InputField inputFieldLeaderboardId;
            [SerializeField] private TMP_InputField inputFieldLeaderboardIdGetEntries;
            [SerializeField] private Button buttonSetScore;
            [SerializeField] private Button buttonGetEntries;
            [SerializeField] private TMP_Text textRequestResult;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextLeaderboardsType => textLeaderboardsType;
            public TMP_InputField InputFieldScore => inputFieldScore;
            public TMP_InputField InputFieldLeaderboardId => inputFieldLeaderboardId;
            public TMP_InputField InputFieldLeaderboardIdGetEntries => inputFieldLeaderboardIdGetEntries;
            public Button ButtonSetScore => buttonSetScore;
            public Button ButtonGetEntries => buttonGetEntries;
            public TMP_Text TextRequestResult => textRequestResult;
        }
    }
}