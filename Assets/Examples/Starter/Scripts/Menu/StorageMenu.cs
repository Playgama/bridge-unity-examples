using System;
using System.Collections.Generic;
using Examples.Starter.Scripts.Playgama;
using Playgama.Modules.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Starter.Scripts.Menu
{
    public class StorageMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

        private StorageType SelectedStorageType => menuSettings.TogglePlatformInternalStorageType.isOn
            ? StorageType.PlatformInternal
            : StorageType.LocalStorage;

        public void SelectLocalStorageType(bool isSelected)
        {
        }

        public void SelectPlatformInternalStorageType(bool isSelected)
        {
        }

        public void LoadData()
        {
            PlaygamaManager.GetStorageData(
                new List<string> { "coins", "level" }, 
                (success, data) =>
            {
                if (success)
                {
                    if (data[0] != null)
                    {
                        menuSettings.InputFieldCoinsCount.text = data[0];
                    }
                    else
                    {
                        menuSettings.InputFieldCoinsCount.text = String.Empty;
                        Debug.LogError("No data for the key 'coins'");
                    }

                    if (data[1] != null)
                    {
                        menuSettings.InputFieldLevelId.text = data[1];
                    }
                    else
                    {
                        menuSettings.InputFieldLevelId.text = String.Empty;
                        Debug.LogError("No data for the key 'level'");
                    }
                }
            }, SelectedStorageType);
        }

        public void SaveData()
        {
            PlaygamaManager.SetStorageData(
                new List<string> { "coins", "level" },
                new List<object>
                {
                    menuSettings.InputFieldCoinsCount.text,
                    menuSettings.InputFieldLevelId.text
                }, 
                (success) =>
                {
                    if (success)
                    {
                        Debug.Log("Data saved successfully");
                    }
                    else
                    {
                        Debug.LogError("Data saving failed");
                    }
                }, 
                SelectedStorageType);
        }

        public void DeleteData()
        {
            PlaygamaManager.DeleteStorageData(
                new List<string>()
                {
                    "coins",
                    "level"
                },
                (success) =>
                {
                    if (success)
                    {
                        Debug.Log("Data deleted successfully");
                    }
                    else
                    {
                        Debug.LogError("Data deletion failed");
                    }
                },
                SelectedStorageType);
        }

        protected override void Awake()
        {
            base.Awake();

            SetTextProperty(menuSettings.PropertyDefaultStorageType, "Default Storage Type",
                PlaygamaManager.DefaultStorageType.ToString());
            SetTextProperty(menuSettings.PropertyIsLocalStorageSupported, "Is Local Storage Supported",
                PlaygamaManager.IsLocalStorageSupported.ToString());
            SetTextProperty(menuSettings.PropertyIsLocalStorageAvailable, "Is Local Storage Available",
                PlaygamaManager.IsLocalStorageAvailable.ToString());
            SetTextProperty(menuSettings.PropertyIsPlatformInternalSupported, "Is Platform Internal Supported",
                PlaygamaManager.IsPlatformInternalSupported.ToString());
            SetTextProperty(menuSettings.PropertyIsPlatformInternalAvailable, "Is Platform Internal Available",
                PlaygamaManager.IsPlatformInternalAvailable.ToString());

            menuSettings.ToggleLocalStorageType.interactable =
                PlaygamaManager.IsLocalStorageSupported && PlaygamaManager.IsLocalStorageAvailable;
            menuSettings.TogglePlatformInternalStorageType.interactable = 
                PlaygamaManager.IsPlatformInternalSupported && PlaygamaManager.IsPlatformInternalAvailable;

            if (menuSettings.ToggleLocalStorageType.interactable)
            {
                menuSettings.ToggleLocalStorageType.isOn = true;
                menuSettings.ToggleLocalStorageType.Select();
            }
            else
            {
                menuSettings.TogglePlatformInternalStorageType.isOn =
                    menuSettings.TogglePlatformInternalStorageType.interactable;
                if (menuSettings.TogglePlatformInternalStorageType.interactable)
                {
                    menuSettings.TogglePlatformInternalStorageType.Select();
                }
            }
        }

        private void SetTextProperty(TextMeshProUGUI text, string name, string value)
        {
            text.text = $"{name}: <color=#D8BBFF>{value}</color>";
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI propertyDefaultStorageType;
            [SerializeField] private TextMeshProUGUI propertyIsLocalStorageSupported;
            [SerializeField] private TextMeshProUGUI propertyIsLocalStorageAvailable;
            [SerializeField] private TextMeshProUGUI propertyIsPlatformInternalSupported;
            [SerializeField] private TextMeshProUGUI propertyIsPlatformInternalAvailable;
            [SerializeField] private TMP_InputField inputFieldCoinsCount;
            [SerializeField] private TMP_InputField inputFieldLevelId;
            [SerializeField] private ToggleGroup toggleGroup;
            [SerializeField] private Toggle toggleLocalStorageType;
            [SerializeField] private Toggle togglePlatformInternalStorageType;

            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI PropertyDefaultStorageType => propertyDefaultStorageType;
            public TextMeshProUGUI PropertyIsLocalStorageSupported => propertyIsLocalStorageSupported;
            public TextMeshProUGUI PropertyIsLocalStorageAvailable => propertyIsLocalStorageAvailable;
            public TextMeshProUGUI PropertyIsPlatformInternalSupported => propertyIsPlatformInternalSupported;
            public TextMeshProUGUI PropertyIsPlatformInternalAvailable => propertyIsPlatformInternalAvailable;
            public TMP_InputField InputFieldCoinsCount => inputFieldCoinsCount;
            public TMP_InputField InputFieldLevelId => inputFieldLevelId;
            public ToggleGroup ToggleGroup => toggleGroup;
            public Toggle ToggleLocalStorageType => toggleLocalStorageType;
            public Toggle TogglePlatformInternalStorageType => togglePlatformInternalStorageType;
        }
    }
}