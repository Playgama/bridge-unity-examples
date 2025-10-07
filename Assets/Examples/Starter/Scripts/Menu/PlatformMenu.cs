using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Playgama.Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class PlatformMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
#if UNITY_WEBGL
        public void SendGameReady()
        {
            PlaygamaManager.SendGameReady();
        }

        public void SendGameplayStarted()
        {
            PlaygamaManager.SendGameplayStarted();
        }

        public void SendInGameLoadingStarted()
        {
            PlaygamaManager.SendInGameLoadingStarted();
        }

        public void SendPlayerGotAchievement()
        {
            PlaygamaManager.SendPlayerGotAchievement();
        }

        public void SendGameplayStopped()
        {
            PlaygamaManager.SendGameplayStopped();
        }

        public void SendInGameLoadingStopped()
        {
            PlaygamaManager.SendInGameLoadingStopped();
        }
        
        public void GetServerTime()
        {
            PlaygamaManager.GetServerTime((serverTime) => 
                SetTextProperty(menuSettings.TextServerTime, "Server Time (UTC)", serverTime.HasValue ? serverTime.Value.ToString(CultureInfo.InvariantCulture) : "-"));
        }

        public void GetAllGames()
        {
            PlaygamaManager.GetAllGames((success, games) =>
            {
                Debug.Log($"Get All Games Completed, Success: {success}");

                menuSettings.TextRequestResult.text = success ? $"Request Result: \n {JsonConvert.SerializeObject(games, Formatting.Indented)}" : "Request Result: Failed";
            });
        }

        public void GetGameById()
        {
            if (string.IsNullOrEmpty(menuSettings.InputFieldGameId.text))
            {
                Debug.LogWarning("Game ID is required");
                return;           
            }
            
            var options = new Dictionary<string, object>()
            {
                { "gameId", menuSettings.InputFieldGameId.text }
            };
            PlaygamaManager.GetGameById(options, (success, game) =>
            {
                Debug.Log($"Get Game by ID Completed, Success: {success}");
                
                if (success)
                {
                    menuSettings.TextRequestResult.text = $"Request Result: \n {JsonConvert.SerializeObject(game, Formatting.Indented)}";
                }
                else
                {
                    menuSettings.TextRequestResult.text = "Request Result: Failed";
                }
            });
        }

        protected override void Awake()
        {
            base.Awake();
            PlaygamaManager.AudioStateChanged += OnAudioStateChanged;
            PlaygamaManager.PauseStateChanged += OnPauseStateChanged;
        }

        private void OnPauseStateChanged(bool isPaused)
        {
            Debug.Log($"Is Game Paused: {isPaused}");
        }

        private void OnAudioStateChanged(bool isAudioEnabled)
        {
            SetTextProperty(menuSettings.TextIsAudioEnabled, "Is Audio Enabled", isAudioEnabled.ToString() );
        }

        protected override void OnDestroy()
        {
            PlaygamaManager.AudioStateChanged -= OnAudioStateChanged;
            PlaygamaManager.PauseStateChanged -= OnPauseStateChanged;
            base.OnDestroy();
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextPlatformId, "Platform Id", PlaygamaManager.PlatformId);
            SetTextProperty(menuSettings.TextLanguage, "Language", PlaygamaManager.Language);
            SetTextProperty(menuSettings.TextPayload, "Payload", PlaygamaManager.Payload ?? "<null>");
            SetTextProperty(menuSettings.TextTld, "TLD", PlaygamaManager.Tld ?? "<null>");
            SetTextProperty(menuSettings.TextIsAudioEnabled, "Is Audio Enabled", PlaygamaManager.IsAudioEnabled.ToString());
            SetTextProperty(menuSettings.TextIsGetAllGamesSupported, "Is get All Games supported", PlaygamaManager.IsGetAllGamesSupported.ToString());
            SetTextProperty(menuSettings.TextIsGetGameByIdSupported, "Is get Game by ID supported", PlaygamaManager.IsGetGameByIdSupported.ToString());
            
            menuSettings.ButtonGetAllGames.interactable = PlaygamaManager.IsGetAllGamesSupported;
            menuSettings.ButtonGetGameById.interactable = PlaygamaManager.IsGetGameByIdSupported;
        }
#endif
        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textPlatformId;
            [SerializeField] private TextMeshProUGUI textLanguage;
            [SerializeField] private TextMeshProUGUI textPayload;
            [SerializeField] private TextMeshProUGUI textTld;
            [SerializeField] private TextMeshProUGUI textIsAudioEnabled;
            
            [SerializeField] private Button buttonSendGameReady;
            [SerializeField] private Button buttonSendGameplayStarted;
            [SerializeField] private Button buttonSendInGameLoadingStarted;
            [SerializeField] private Button buttonSendPlayerGotAchievement;
            [SerializeField] private Button buttonSendGameplayStopped;
            [SerializeField] private Button buttonSendInGameLoadingStopped;
            
            [SerializeField] private TextMeshProUGUI textServerTime;
            [SerializeField] private Button buttonGetServerTime;

            [SerializeField] private TextMeshProUGUI textIsGetAllGamesSupported;
            [SerializeField] private Button buttonGetAllGames;
            [SerializeField] private TextMeshProUGUI textRequestResult;

            [SerializeField] private TextMeshProUGUI textIsGetGameByIdSupported;
            [SerializeField] private TMP_InputField inputFieldGameId;
            [SerializeField] private Button buttonGetGameById;
            
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextPlatformId => textPlatformId;
            public TextMeshProUGUI TextLanguage => textLanguage;
            public TextMeshProUGUI TextPayload => textPayload;
            public TextMeshProUGUI TextTld => textTld;
            public TextMeshProUGUI TextIsAudioEnabled => textIsAudioEnabled;
            public Button ButtonSendGameReady => buttonSendGameReady;
            public Button ButtonSendGameplayStarted => buttonSendGameplayStarted;
            public Button ButtonSendInGameLoadingStarted => buttonSendInGameLoadingStarted;
            public Button ButtonSendPlayerGotAchievement => buttonSendPlayerGotAchievement;
            public Button ButtonSendGameplayStopped => buttonSendGameplayStopped;
            public Button ButtonSendInGameLoadingStopped => buttonSendInGameLoadingStopped;
            public TextMeshProUGUI TextServerTime => textServerTime;
            public Button ButtonGetServerTime => buttonGetServerTime;
            public TextMeshProUGUI TextIsGetAllGamesSupported => textIsGetAllGamesSupported;
            public Button ButtonGetAllGames => buttonGetAllGames;
            public TextMeshProUGUI TextRequestResult => textRequestResult;
            public TextMeshProUGUI TextIsGetGameByIdSupported => textIsGetGameByIdSupported;
            public TMP_InputField InputFieldGameId => inputFieldGameId;
            public Button ButtonGetGameById => buttonGetGameById;
        }
    }
}