using Playgama;
using Playgama.Modules.Storage;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
{
    public class StorageScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _defaultStorageType;
        [SerializeField] private PropertyTextView _isLocalStorageSupported;
        [SerializeField] private PropertyTextView _isLocalStorageAvailable;
        [SerializeField] private PropertyTextView _isPlatformInternalSupported;
        [SerializeField] private PropertyTextView _isPlatformInternalAvailable;


        private void OnEnable()
        {
            _defaultStorageType.SetText(Bridge.storage.defaultType.ToString());
            _isLocalStorageSupported.SetText(Bridge.storage.IsSupported(StorageType.LocalStorage).ToString());
            _isLocalStorageAvailable.SetText(Bridge.storage.IsAvailable(StorageType.LocalStorage).ToString());
            _isPlatformInternalSupported.SetText(Bridge.storage.IsSupported(StorageType.PlatformInternal).ToString());
            _isPlatformInternalAvailable.SetText(Bridge.storage.IsAvailable(StorageType.PlatformInternal).ToString());
        }
    }
}