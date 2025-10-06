using System;
using System.Collections;
using System.Collections.Generic;
using Playgama;
using Playgama.Modules.Advertisement;
using Playgama.Modules.Game;
using Playgama.Modules.Leaderboards;
using Playgama.Modules.Platform;
using Playgama.Modules.Storage;
using UnityEngine;
using UnityEngine.Networking;
using DeviceType = Playgama.Modules.Device.DeviceType;

namespace Examples.Starter.Scripts.Playgama
{
    public class PlaygamaManager : MonoBehaviour
    {
        #region Device
        public DeviceType DeviceType => Bridge.device.type;

        #endregion

        #region Game

        public VisibilityState CurrentVisibilityState => Bridge.game.visibilityState;

        #endregion

        #region Storage

        public StorageType DefaultStorageType => Bridge.storage.defaultType;
        public bool IsLocalStorageSupported => Bridge.storage.IsSupported(StorageType.LocalStorage);
        public bool IsLocalStorageAvailable => Bridge.storage.IsAvailable(StorageType.LocalStorage);
        public bool IsPlatformInternalSupported => Bridge.storage.IsSupported(StorageType.PlatformInternal);
        public bool IsPlatformInternalAvailable => Bridge.storage.IsAvailable(StorageType.PlatformInternal);

        #endregion

        #region Player

        public bool IsAuthSupported => Bridge.player.isAuthorizationSupported;
        public bool IsAuthorized => Bridge.player.isAuthorized;
        public string PlayerId => Bridge.player.id;
        public string PlayerName => Bridge.player.name;
        public List<string> PlayerPhotos => Bridge.player.photos;

        #endregion

        #region Remote Config

        public bool IsRemoteConfigSupported => Bridge.remoteConfig.isSupported;

        #endregion

        #region Achievements

        public bool IsAchievementsSupported => Bridge.achievements.isSupported;
        public bool IsGetListSupported => Bridge.achievements.isGetListSupported;
        public bool IsNativePopupSupported => Bridge.achievements.isNativePopupSupported;

        #endregion

        #region Payments

        public bool IsPaymentsSupported => Bridge.payments.isSupported;

        #endregion
        
        #region Leaderboards

        public LeaderboardType LeaderboardType => Bridge.leaderboards.type;

        #endregion
        
        #region  Advertisement

        public bool IsBannerSupported => Bridge.advertisement.isBannerSupported;
        public bool IsInterstitialSupported => Bridge.advertisement.isInterstitialSupported;
        public bool IsRewardedSupported => Bridge.advertisement.isRewardedSupported;
        public int MinimumDelayBetweenInterstitial => Bridge.advertisement.minimumDelayBetweenInterstitial;
        public string RewardedPlacement => Bridge.advertisement.rewardedPlacement;
        
        #endregion

        #region Social

        public bool IsShareSupported => Bridge.social.isShareSupported;
        public bool IsJoinCommunitySupported => Bridge.social.isJoinCommunitySupported;
        public bool IsInviteFriendsSupported => Bridge.social.isInviteFriendsSupported;
        public bool IsCreatePostSupported => Bridge.social.isCreatePostSupported;
        public bool IsAddToFavoritesSupported => Bridge.social.isAddToFavoritesSupported;
        public bool IsAddToHomeScreenSupported => Bridge.social.isAddToHomeScreenSupported;
        public bool IsRateSupported => Bridge.social.isRateSupported;
        public bool IsExternalLinksAllowed => Bridge.social.isExternalLinksAllowed;
        
        #endregion
        
        #region Platform

        public string PlatformId => Bridge.platform.id;
        public string Language => Bridge.platform.language;
        public string Payload => Bridge.platform.payload;
        public string Tld => Bridge.platform.tld;
        public bool IsAudioEnabled => Bridge.platform.isAudioEnabled;
        public bool IsGetAllGamesSupported => Bridge.platform.isGetAllGamesSupported;
        public bool IsGetGameByIdSupported => Bridge.platform.isGetGameByIdSupported;
        

        #endregion

        public event Action<VisibilityState> GameVisibilityStateChanged;
        public event Action<BannerState> AdvertisementBannerStateChanged;
        public event Action<InterstitialState> AdvertisementInterstitialStateChanged;
        public event Action<RewardedState> AdvertisementRewardedStateChanged;
        public event Action<bool> AudioStateChanged;
        public event Action<bool> PauseStateChanged;

        #region Storage

        public void GetStorageData(string key, Action<bool, string> onComplete, StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Get(key, onComplete, storageType);
        }

        public void GetStorageData(List<string> keys, Action<bool, List<string>> onComplete,
            StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Get(keys, onComplete, storageType);
        }

        public void SetStorageData(string key, string value, Action<bool> onComplete,
            StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Set(key, value, onComplete, storageType);
        }

        public void SetStorageData(List<string> keys, List<object> values, Action<bool> onComplete,
            StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Set(keys, values, onComplete, storageType);
        }

        public void DeleteStorageData(string key, Action<bool> onComplete,
            StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Delete(key, onComplete, storageType);
        }

        public void DeleteStorageData(List<string> keys, Action<bool> onComplete,
            StorageType storageType = StorageType.LocalStorage)
        {
            Bridge.storage.Delete(keys, onComplete, storageType);
        }

        #endregion

        #region Player

        public void Authorize(Action<bool> onComplete, Dictionary<string, object> options = null)
        {
            Bridge.player.Authorize(options, onComplete);
        }
        
        public IEnumerator LoadPlayerPhotos(Action<List<Texture2D>> onComplete)
        {
            var textures = new List<Texture2D>();

            foreach (var photoUrl in PlayerPhotos)
            {
                using var unityWebRequest = UnityWebRequestTexture.GetTexture(photoUrl);
                
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                    textures.Add(texture);
                }
                else
                {
                    Debug.LogError("Error loading player avatar: " + unityWebRequest.error);
                }
            }

            onComplete?.Invoke(textures);
        }

        #endregion

        #region Remote Config

        public void GetRemoteConfig(Action<bool, Dictionary<string, string>> onComplete)
        {
            Bridge.remoteConfig.Get(onComplete);
        }

        #endregion

        #region Achievements

        public void UnlockAchievement(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.achievements.Unlock(options, onComplete);
        }

        public void GetAchievementList(Dictionary<string, object> options, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.achievements.GetList(options, onComplete);
        }
        
        public void ShowNativePopup(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.achievements.ShowNativePopup(options, onComplete);
        }

        #endregion

        #region Payments

        public void Purchase(string productId, Action<bool, Dictionary<string, string>> onComplete)
        {
            Bridge.payments.Purchase(productId, onComplete);
        }

        public void ConsumePurchase(string productId, Action<bool, Dictionary<string, string>> onComplete)
        {
            Bridge.payments.ConsumePurchase(productId, onComplete);
        }

        public void GetCatalog(Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.payments.GetCatalog(onComplete);
        }

        public void GetPurchases(Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.payments.GetPurchases(onComplete);
        }

        #endregion
        
        #region Leaderboards

        public void SetScore(string leaderboardId, int score, Action<bool> onComplete)
        {
            Bridge.leaderboards.SetScore(leaderboardId, score, onComplete);
        }

        public void GetEntries(string leaderboardId, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.leaderboards.GetEntries(leaderboardId, onComplete);
        }
        #endregion

        #region Advertisement

        public void ShowBanner(BannerPosition bannerPosition = BannerPosition.Bottom, string placementId = null)
        {
            Bridge.advertisement.ShowBanner(bannerPosition, placementId);
        }

        public void HideBanner()
        {
            Bridge.advertisement.HideBanner();
        }
        
        public void ShowInterstitial(string placementId = null)
        {
            Bridge.advertisement.ShowInterstitial(placementId);
        }

        public void ShowRewarded(string placementId = null)
        {
            Bridge.advertisement.ShowRewarded(placementId);
        }

        public void CheckAdBlock(Action<bool> onResult)
        {
            Bridge.advertisement.CheckAdBlock(onResult);
        }
        
        #endregion
        
        #region Social

        public void Share(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.social.Share(options, onComplete);
        }

        public void JoinCommunity(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.social.JoinCommunity(options, onComplete);
        }
        
        public void InviteFriends(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.social.InviteFriends(options, onComplete);
        }
        
        public void CreatePost(Dictionary<string, object> options, Action<bool> onComplete)
        {
            Bridge.social.CreatePost(options, onComplete);
        }

        public void AddToFavorites(Action<bool> onComplete = null)
        {
            Bridge.social.AddToFavorites(onComplete);
        }

        public void AddToHomeScreen(Action<bool> onComplete = null)
        {
            Bridge.social.AddToHomeScreen(onComplete);
        }
        
        public void Rate(Action<bool> onComplete = null)
        {
            Bridge.social.Rate(onComplete);
        }
        
        #endregion

        #region Platform

        public void SendGameReady()
        {
            Bridge.platform.SendMessage(PlatformMessage.GameReady);
        }
        
        public void SendGameplayStarted()
        {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStarted);
        }

        public void SendPlayerGotAchievement()
        {
            Bridge.platform.SendMessage(PlatformMessage.PlayerGotAchievement);
        }
        
        public void SendGameplayStopped()
        {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStopped);
        }

        public void SendInGameLoadingStarted()
        {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStarted);
        }

        public void SendInGameLoadingStopped()
        {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStopped);
        }

        public void GetServerTime(Action<DateTime?> onComplete)
        {
            Bridge.platform.GetServerTime(onComplete);
        }

        public void GetAllGames(Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            Bridge.platform.GetAllGames(onComplete);
        }

        public void GetGameById(Dictionary<string, object> options, Action<bool, Dictionary<string, string>> onComplete)
        {
            Bridge.platform.GetGameById(options, onComplete);
        }

        #endregion

        private void OnVisibilityStateChanged(VisibilityState state)
        {
            GameVisibilityStateChanged?.Invoke(state);
        }
        
        private void OnAdvertisementBannerStateChanged(BannerState state)
        {
            AdvertisementBannerStateChanged?.Invoke(state);
        }
        
        private void OnAdvertisementInterstitialStateChanged(InterstitialState state)
        {
            AdvertisementInterstitialStateChanged?.Invoke(state);
        }
        
        private void OnAdvertisementRewardedStateChanged(RewardedState state)
        {
            AdvertisementRewardedStateChanged?.Invoke(state);
        }
        
        private void OnAudioStateChanged(bool state)
        {
            AudioStateChanged?.Invoke(state);
        }
        
        private void OnPauseStateChanged(bool state)
        {
            PauseStateChanged?.Invoke(state);
        }

        private void Awake()
        {
            Bridge.game.visibilityStateChanged += OnVisibilityStateChanged;
            Bridge.advertisement.bannerStateChanged += OnAdvertisementBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged += OnAdvertisementInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += OnAdvertisementRewardedStateChanged;
        }

        private void OnDestroy()
        {
            if (Bridge.instance != null)
            {
                Bridge.game.visibilityStateChanged -= OnVisibilityStateChanged;
                Bridge.advertisement.bannerStateChanged -= OnAdvertisementBannerStateChanged;
                Bridge.advertisement.interstitialStateChanged -= OnAdvertisementInterstitialStateChanged;
                Bridge.advertisement.rewardedStateChanged -= OnAdvertisementRewardedStateChanged;
            }
        }
    }
}