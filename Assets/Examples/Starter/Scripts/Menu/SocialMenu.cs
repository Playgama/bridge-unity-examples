using System;
using System.Collections.Generic;
using Playgama.Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class SocialMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

        public void Share()
        {
            var options = new Dictionary<string, object>();
            
            switch (PlaygamaManager.PlatformId)
            {
                case "vk":
                    options.Add("link", "YOUR_LINK");
                    break;
            }
            
            PlaygamaManager.Share(options, (success) =>
            {
                Debug.Log($"Share success: {success}");           
            });
        }
        
        public void JoinCommunity()
        {
            var options = new Dictionary<string, object>();

            switch (PlaygamaManager.PlatformId)
            {
                case "vk":
                    options.Add("groupId", 199747461);
                    break;
                case "ok":
                    options.Add("groupId", 62984239710374);
                    break;
            }
            
            PlaygamaManager.JoinCommunity(options, (success) =>
            {
                Debug.Log($"Join community success: {success}");
            });
        }

        public void InviteFriends()
        {
            var options = new Dictionary<string, object>();

            switch (PlaygamaManager.PlatformId)
            {
                case "ok":
                    options.Add("text", "Hello World!");
                    break;
            }
            
            PlaygamaManager.InviteFriends(options, (success) =>
            {
                Debug.Log($"Invite friends success: {success}");
            });
        }

        public void CreatePost()
        {
            var options = new Dictionary<string, object>();

            switch (PlaygamaManager.PlatformId)
            {
                case "vk":
                    options.Add("message", "Hello World!");
                    options.Add("attachments", "photo-199747461_457239629");
                    break;
                
                case "ok":
                    var media = new object[]
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "text" },
                            { "text", "Hello World!" },
                        },
                        new Dictionary<string, object>
                        {
                            { "type", "link" },
                            { "url", "https://apiok.ru" },
                        },
                        new Dictionary<string, object>
                        {
                            { "type", "poll" },
                            { "question", "Do you like our API?" },
                            { 
                                "answers", 
                                new object[]
                                {
                                    new Dictionary<string, object>
                                    {
                                        { "text", "Yes" },
                                    },
                                    new Dictionary<string, object>
                                    {
                                        { "text", "No" },
                                    }
                                }
                            },
                            { "options", "SingleChoice,AnonymousVoting" },
                        },
                    };
                    
                    options.Add("media", media);
                    break;
            }
            
            PlaygamaManager.CreatePost(options, (success) =>
            {
                Debug.Log($"Create post success: {success}");
            });
        }

        public void AddFavorites()
        {
            PlaygamaManager.AddToFavorites((success) =>
            {
                Debug.Log($"Add to favorites success: {success}");
            });
        }
        
        public void AddToHomeScreen()
        {
            PlaygamaManager.AddToHomeScreen((success) =>
            {
                Debug.Log($"Add to home screen success: {success}");
            });
        }

        public void Rate()
        {
            PlaygamaManager.Rate((success) =>
                Debug.Log($"Rate success: {success}")
            );
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextIsShareSupported, "Is Share Supported", PlaygamaManager.IsShareSupported.ToString());
            SetTextProperty(menuSettings.TextIsJoinCommunitySupported, "Is Join Community Supported", PlaygamaManager.IsJoinCommunitySupported.ToString());
            menuSettings.ButtonShare.interactable = PlaygamaManager.IsShareSupported;
            menuSettings.ButtonJoinCommunity.interactable = PlaygamaManager.IsJoinCommunitySupported;
            
            SetTextProperty(menuSettings.TextIsInviteFriendsSupported, "Is Invite Friends Supported", PlaygamaManager.IsInviteFriendsSupported.ToString());
            SetTextProperty(menuSettings.TextIsCreatePostSupported, "Is Create Post Supported", PlaygamaManager.IsCreatePostSupported.ToString());
            menuSettings.ButtonInviteFriends.interactable = PlaygamaManager.IsInviteFriendsSupported;
            menuSettings.ButtonCreatePost.interactable = PlaygamaManager.IsCreatePostSupported;
            
            SetTextProperty(menuSettings.TextIsAddToFavoritesSupported, "Is Add To Favorites Supported", PlaygamaManager.IsAddToFavoritesSupported.ToString());
            SetTextProperty(menuSettings.TextIsAddToHomeScreenSupported, "Is Add To Home Screen Supported", PlaygamaManager.IsAddToHomeScreenSupported.ToString());
            menuSettings.ButtonAddToFavorites.interactable = PlaygamaManager.IsAddToFavoritesSupported;
            menuSettings.ButtonAddToHomeScreen.interactable = PlaygamaManager.IsAddToHomeScreenSupported;
            
            SetTextProperty(menuSettings.TextIsRateSupported, "Is Rate Supported", PlaygamaManager.IsRateSupported.ToString());
            SetTextProperty(menuSettings.TextIsExternalLinksAllowed, "Is External Links Allowed", PlaygamaManager.IsExternalLinksAllowed.ToString()); 
            menuSettings.ButtonRate.interactable = PlaygamaManager.IsRateSupported;
            
        }
        
        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsShareSupported => textIsShareSupported;
            public TextMeshProUGUI TextIsJoinCommunitySupported => textIsJoinCommunitySupported;
            public Button ButtonShare => buttonShare;
            public Button ButtonJoinCommunity => buttonJoinCommunity;
            public TextMeshProUGUI TextIsInviteFriendsSupported => textIsInviteFriendsSupported;
            public TextMeshProUGUI TextIsCreatePostSupported => textIsCreatePostSupported;
            public Button ButtonInviteFriends => buttonInviteFriends;
            public Button ButtonCreatePost => buttonCreatePost;
            public TextMeshProUGUI TextIsAddToFavoritesSupported => textIsAddToFavoritesSupported;
            public TextMeshProUGUI TextIsAddToHomeScreenSupported => textIsAddToHomeScreenSupported;
            public Button ButtonAddToFavorites => buttonAddToFavorites;
            public Button ButtonAddToHomeScreen => buttonAddToHomeScreen;
            public TextMeshProUGUI TextIsRateSupported => textIsRateSupported;
            public TextMeshProUGUI TextIsExternalLinksAllowed => textIsExternalLinksAllowed;
            public Button ButtonRate => buttonRate;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            
            [SerializeField] private TextMeshProUGUI textIsShareSupported;
            [SerializeField] private TextMeshProUGUI textIsJoinCommunitySupported;
            [SerializeField] private Button buttonShare;
            [SerializeField] private Button buttonJoinCommunity;

            [SerializeField] private TextMeshProUGUI textIsInviteFriendsSupported;
            [SerializeField] private TextMeshProUGUI textIsCreatePostSupported;
            [SerializeField] private Button buttonInviteFriends;
            [SerializeField] private Button buttonCreatePost;

            [SerializeField] private TextMeshProUGUI textIsAddToFavoritesSupported;
            [SerializeField] private TextMeshProUGUI textIsAddToHomeScreenSupported;
            [SerializeField] private Button buttonAddToFavorites;
            [SerializeField] private Button buttonAddToHomeScreen;

            [SerializeField] private TextMeshProUGUI textIsRateSupported;
            [SerializeField] private TextMeshProUGUI textIsExternalLinksAllowed;
            [SerializeField] private Button buttonRate;
        }
    }
}