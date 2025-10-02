using Playgama;
using SandboxUI.Scripts.Base;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.RemoteConfig
{
    public class RemoteConfigScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isRemoteConfigSupported;

        private void OnEnable()
        {
            _isRemoteConfigSupported.SetText(Bridge.remoteConfig.isSupported.ToString());
        }
    }
}