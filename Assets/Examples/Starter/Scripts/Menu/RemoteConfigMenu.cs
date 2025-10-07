using System;
using Newtonsoft.Json;
using Playgama.Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class RemoteConfigMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
#if UNITY_WEBGL
        public void GetRemoteConfig()
        {
            PlaygamaManager.GetRemoteConfig((success, data) =>
            {
                if (success)
                {
                    menuSettings.TextRemoteConfigResult.text = JsonConvert.SerializeObject(data, Formatting.Indented);
                }
                else
                {
                    Debug.Log("Failed to get remote config");
                }
            });
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextIsRemoteConfigSupported, "Is Remote Config Supported", PlaygamaManager.IsRemoteConfigSupported.ToString());
            menuSettings.ButtonGetRemoteConfig.interactable = PlaygamaManager.IsRemoteConfigSupported;
        }
#endif
        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsRemoteConfigSupported => textIsRemoteConfigSupported;
            public Button ButtonGetRemoteConfig => buttonGetRemoteConfig;
            public TextMeshProUGUI TextRemoteConfigResult => textRemoteConfigResult;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textIsRemoteConfigSupported;
            [SerializeField] private Button buttonGetRemoteConfig;
            [SerializeField] private TextMeshProUGUI textRemoteConfigResult;
        }
    }
}