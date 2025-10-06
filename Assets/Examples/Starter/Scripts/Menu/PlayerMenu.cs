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
                    InitMenu();
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
            SetTextProperty(menuSettings.TextIsAuthSupported, "Is Auth Supported", PlaygamaManager.IsAuthSupported.ToString());
            SetTextProperty(menuSettings.TextIsAuthorized, "Is Authorized", PlaygamaManager.IsAuthorized.ToString());
            SetTextProperty(menuSettings.TextPlayerId, "Player ID", PlaygamaManager.PlayerId ?? "<null>");
            SetTextProperty(menuSettings.TextPlayerName, "Player Name", PlaygamaManager.PlayerName ?? "<null>");
            
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

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textIsAuthSupported;
            [SerializeField] private TextMeshProUGUI textIsAuthorized;
            [SerializeField] private TextMeshProUGUI textPlayerId;
            [SerializeField] private TextMeshProUGUI textPlayerName;
            [SerializeField] private Button buttonAuthorize;
            [SerializeField] private RawImage prefabPlayerAvatar;
            [SerializeField] private RectTransform playerAvatarHolder;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsAuthSupported => textIsAuthSupported;
            public TextMeshProUGUI TextIsAuthorized => textIsAuthorized;
            public TextMeshProUGUI TextPlayerId => textPlayerId;
            public TextMeshProUGUI TextPlayerName => textPlayerName;
            public Button ButtonAuthorize => buttonAuthorize;
            public RawImage PrefabPlayerAvatar => prefabPlayerAvatar;
            public RectTransform PlayerAvatarHolder => playerAvatarHolder;
        }
    }
}