using System;
using Examples.Starter.Scripts.Playgama;
using TMPro;
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

        private void Start()
        {
            
        }

        private void InitMenu()
        {
            
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