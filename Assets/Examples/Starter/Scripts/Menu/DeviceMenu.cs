using System;
using Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Examples.Starter.Scripts.Menu
{
    public class DeviceMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
        
        private void Start()
        {
            InitMenu();
        }
        
        private void InitMenu()
        {
            SetTextProperty(menuSettings.TextDeviceType, "Device Type", PlaygamaManager.DeviceType.ToString());
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager  playgamaManager;
            [SerializeField] private TextMeshProUGUI textDeviceType;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextDeviceType => textDeviceType;
        }
    }
}