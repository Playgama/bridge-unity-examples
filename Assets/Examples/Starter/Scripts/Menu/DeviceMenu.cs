using System;
using Playgama.Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class DeviceMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

#if UNITY_WEBGL
        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextDeviceType, "Device Type", PlaygamaManager.DeviceType.ToString());
        }
#endif

        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextDeviceType => textDeviceType;
            
            [SerializeField] private PlaygamaManager  playgamaManager;
            [SerializeField] private TextMeshProUGUI textDeviceType;
        }
    }
}