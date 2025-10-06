using System;
using System.Collections.Generic;
using Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Starter.Scripts.Menu
{
    public class AchievementsMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

        public void UnlockAchievement()
        {
            if (string.IsNullOrEmpty(menuSettings.InputFieldName.text) ||
                string.IsNullOrEmpty(menuSettings.InputFiledKey.text))
            {
                Debug.LogWarning("Achievement name and key are required");
                return;           
            }
            
            var options = new Dictionary<string, object>()
            {
                { "achievement", menuSettings.InputFieldName.text },
                { "achievementKey", menuSettings.InputFiledKey.text }
            };

            PlaygamaManager.UnlockAchievement(options,
                (success) => Debug.Log($"Achievement unlocked: {menuSettings.InputFieldName.text}, Success: {success}"));
        }

        public void GetAchievementList()
        {
            var options = new Dictionary<string, object>();

            PlaygamaManager.GetAchievementList(options, (success, achievements) =>
            {
                Debug.Log($"Achievement list retrieved: Success: {success}");
                if (success)
                {
                    switch (PlaygamaManager.PlatformId)
                    {
                        case "y8":
                            foreach (var achievement in achievements)
                            {
                                Debug.Log("achievementid:" + achievement["achievementid"]);
                                Debug.Log("achievement:" + achievement["achievement"]);
                                Debug.Log("achievementkey:" + achievement["achievementkey"]);
                                Debug.Log("description:" + achievement["description"]);
                                Debug.Log("icon:" + achievement["icon"]);
                                Debug.Log("difficulty:" + achievement["difficulty"]);
                                Debug.Log("secret:" + achievement["secret"]);
                                Debug.Log("awarded:" + achievement["awarded"]);
                                Debug.Log("game:" + achievement["game"]);
                                Debug.Log("link:" + achievement["link"]);
                                Debug.Log("playerid:" + achievement["playerid"]);
                                Debug.Log("playername:" + achievement["playername"]);
                                Debug.Log("lastupdated:" + achievement["lastupdated"]);
                                Debug.Log("date:" + achievement["date"]);
                                Debug.Log("rdate:" + achievement["rdate"]);
                            }

                            break;
                    }
                }
            });
        }

        public void ShowNativePopup()
        {
            var options = new Dictionary<string, object>();
            PlaygamaManager.ShowNativePopup(options,
                (success) => { Debug.Log($"Show Native Popup Completed, Success: {success}"); }
            );
        }

        private void Start()
        {
            InitMenu();
        }

        private void InitMenu()
        {
            SetTextProperty(menuSettings.TextIsAchievementsSupported, "Is Achievements Supported",
                PlaygamaManager.IsAchievementsSupported.ToString());
            SetTextProperty(menuSettings.TextIsGetListSupported, "Is Get List Supported",
                PlaygamaManager.IsGetListSupported.ToString());
            SetTextProperty(menuSettings.TextIsNativePopupSupported, "Is Native Popup Supported",
                PlaygamaManager.IsNativePopupSupported.ToString());

            menuSettings.ButtonUnlock.interactable = PlaygamaManager.IsAchievementsSupported;
            menuSettings.ButtonGetList.interactable = PlaygamaManager.IsGetListSupported;
            menuSettings.ButtonShowNativePopup.interactable = PlaygamaManager.IsNativePopupSupported;
            menuSettings.InputFiledKey.interactable = PlaygamaManager.IsAchievementsSupported;
            menuSettings.InputFieldName.interactable = PlaygamaManager.IsAchievementsSupported;
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textIsAchievementsSupported;
            [SerializeField] private TextMeshProUGUI textIsGetListSupported;
            [SerializeField] private TextMeshProUGUI textIsNativePopupSupported;
            [SerializeField] private TMP_InputField inputFiledKey;
            [SerializeField] private TMP_InputField inputFieldName;
            [SerializeField] private Button buttonUnlock;
            [SerializeField] private Button buttonGetList;
            [SerializeField] private Button buttonShowNativePopup;

            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsAchievementsSupported => textIsAchievementsSupported;
            public TextMeshProUGUI TextIsGetListSupported => textIsGetListSupported;
            public TextMeshProUGUI TextIsNativePopupSupported => textIsNativePopupSupported;
            public TMP_InputField InputFiledKey => inputFiledKey;
            public TMP_InputField InputFieldName => inputFieldName;
            public Button ButtonUnlock => buttonUnlock;
            public Button ButtonGetList => buttonGetList;
            public Button ButtonShowNativePopup => buttonShowNativePopup;
        }
    }
}