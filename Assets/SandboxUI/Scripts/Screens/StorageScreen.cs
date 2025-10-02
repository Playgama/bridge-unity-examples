using System;
using Playgama;
using Playgama.Modules.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Screens
{
    public class StorageScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _defaultStorageType;
        [SerializeField] private PropertyTextView _isLocalStorageSupported;
        [SerializeField] private PropertyTextView _isLocalStorageAvailable;
        [SerializeField] private PropertyTextView _isPlatformInternalSupported;
        [SerializeField] private PropertyTextView _isPlatformInternalAvailable;
        [SerializeField] private Toggle _localStorageToggle;
        [SerializeField] private Toggle _platformInternalToggle;


        private void OnEnable()
        {
            var storageDefaultType = Bridge.storage.defaultType;
            switch (storageDefaultType)
            {
                case StorageType.LocalStorage:
                    _localStorageToggle.isOn = true;
                    break;
                case StorageType.PlatformInternal:
                    _platformInternalToggle.isOn = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _defaultStorageType.SetText(storageDefaultType.ToString());
            _isLocalStorageSupported.SetText(Bridge.storage.IsSupported(StorageType.LocalStorage).ToString());
            _isLocalStorageAvailable.SetText(Bridge.storage.IsAvailable(StorageType.LocalStorage).ToString());
            _isPlatformInternalSupported.SetText(Bridge.storage.IsSupported(StorageType.PlatformInternal).ToString());
            _isPlatformInternalAvailable.SetText(Bridge.storage.IsAvailable(StorageType.PlatformInternal).ToString());
        }
    }
}