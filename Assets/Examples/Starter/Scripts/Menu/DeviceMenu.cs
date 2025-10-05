using System;
using Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;

namespace Examples.Starter.Scripts.Menu
{
    public class DeviceMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings settings;
        private PlaygamaManager PlaygamaManager => settings.PlaygamaManager;

        protected override void Awake()
        {
            base.Awake();
            settings.TextPropertyDevice.text = $"Device Type: <color=#D8BBFF>{PlaygamaManager.DeviceType}</color>";
            
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager  playgamaManager;
            [SerializeField] private TextMeshProUGUI textPropertyDevice;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextPropertyDevice => textPropertyDevice;
        }
    }
}