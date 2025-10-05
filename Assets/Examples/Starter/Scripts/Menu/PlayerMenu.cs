using System;
using Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Starter.Scripts.Menu
{
    public class PlayerMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

        public void Authorize()
        {
            PlaygamaManager.Authorize((success) =>
            {
                if (success)
                {
                    Debug.Log("Player successfully authorized");
                }
                else
                {
                    Debug.LogError("Player authorization failed");
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
            SetTextProperty(menuSettings.PropertyIsAuthSupported, "Is Auth Supported", PlaygamaManager.IsAuthSupported.ToString());
            SetTextProperty(menuSettings.PropertyIsAuthorized, "Is Authorized", PlaygamaManager.IsAuthorized.ToString());
            SetTextProperty(menuSettings.PropertyPlayerId, "Player ID", PlaygamaManager.PlayerId);
            SetTextProperty(menuSettings.PropertyPlayerName, "Player Name", PlaygamaManager.PlayerName);
            
            menuSettings.ButtonAuthorize.interactable = PlaygamaManager.IsAuthSupported;
            
            StartCoroutine(PlaygamaManager.LoadPlayerPhotos(photos =>
            {
                foreach (var photo in photos)
                {
                    var playerAvatar = Instantiate(menuSettings.PrefabPlayerAvatar, menuSettings.PlayerAvatarHolder);
                    playerAvatar.texture = photo;
                }
                menuSettings.PlayerAvatarHolder.gameObject.SetActive(menuSettings.PlayerAvatarHolder.childCount > 0);
            }));
        }
        
        private void SetTextProperty(TextMeshProUGUI text, string name, string value)
        {
            text.text = $"{name}: <color=#D8BBFF>{value}</color>";
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI propertyIsAuthSupported;
            [SerializeField] private TextMeshProUGUI propertyIsAuthorized;
            [SerializeField] private TextMeshProUGUI propertyPlayerId;
            [SerializeField] private TextMeshProUGUI propertyPlayerName;
            [SerializeField] private Button buttonAuthorize;
            [SerializeField] private RawImage prefabPlayerAvatar;
            [SerializeField] private RectTransform playerAvatarHolder;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI PropertyIsAuthSupported => propertyIsAuthSupported;
            public TextMeshProUGUI PropertyIsAuthorized => propertyIsAuthorized;
            public TextMeshProUGUI PropertyPlayerId => propertyPlayerId;
            public TextMeshProUGUI PropertyPlayerName => propertyPlayerName;
            public Button ButtonAuthorize => buttonAuthorize;
            public RawImage PrefabPlayerAvatar => prefabPlayerAvatar;
            public RectTransform PlayerAvatarHolder => playerAvatarHolder;
        }
    }
}