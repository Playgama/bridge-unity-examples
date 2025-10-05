using System;
using System.Collections.Generic;
using Examples.Starter.Scripts.Playgama;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Starter.Scripts.Menu
{
    public class RemoteConfigMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

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

        public override void Open()
        {
            base.Open();
            InitMenu();
        }

        private void Start()
        {
            InitMenu();
        }

        private void InitMenu()
        {
            SetTextProperty(menuSettings.PropertyIsRemoteConfigSupported, "Is Remote Config Supported", PlaygamaManager.IsRemoteConfigSupported.ToString());
            menuSettings.ButtonGetRemoteConfig.interactable = PlaygamaManager.IsRemoteConfigSupported;
        }

        private void SetTextProperty(TextMeshProUGUI text, string propertyName, string propertyValue)
        {
            text.text = $"{propertyName}: <color=#D8BBFF>{propertyValue}</color>";
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI propertyIsRemoteConfigSupported;
            [SerializeField] private Button buttonGetRemoteConfig;
            [SerializeField] private TextMeshProUGUI textRemoteConfigResult;

            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI PropertyIsRemoteConfigSupported => propertyIsRemoteConfigSupported;
            public Button ButtonGetRemoteConfig => buttonGetRemoteConfig;
            public TextMeshProUGUI TextRemoteConfigResult => textRemoteConfigResult;
        }
    }
}