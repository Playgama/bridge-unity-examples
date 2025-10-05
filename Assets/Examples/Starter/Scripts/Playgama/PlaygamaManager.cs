using System;
using System.Collections;
using System.Collections.Generic;
using Playgama;
using Playgama.Modules.Game;
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
        
        #region Platform

        public string PlatformId => Bridge.platform.id;

        #endregion

        public event Action<VisibilityState> GameVisibilityStateChanged;

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

        public void ConsumePurchase(string productId, Action<bool> onComplete)
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

        private void OnVisibilityStateChanged(VisibilityState state)
        {
            GameVisibilityStateChanged?.Invoke(state);
        }

        private void Awake()
        {
            Bridge.game.visibilityStateChanged += OnVisibilityStateChanged;
        }

        private void OnDestroy()
        {
            if (Bridge.instance != null)
            {
                Bridge.game.visibilityStateChanged -= OnVisibilityStateChanged;
            }
        }
    }
}