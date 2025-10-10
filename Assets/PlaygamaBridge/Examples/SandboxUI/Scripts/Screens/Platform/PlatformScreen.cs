using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Platform.Requests;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Platform
{
    public sealed class PlatformScreen : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private PropertyTextView _platformId;
        [SerializeField] private PropertyTextView _language;
        [SerializeField] private PropertyTextView _payload;
        [SerializeField] private PropertyTextView _tld;
        [SerializeField] private PropertyTextView _isAudio;
        [SerializeField] private GetServerTimeBridgeRequestHandler _getServerTimeBridgeHandler;
        [SerializeField] private PropertyTextView _isGetAllGamesSupported;
        [SerializeField] private PropertyTextView _isGetGameByIdSupported;

        private void OnEnable()
        {
            _platformId.SetText(Bridge.platform.id);
            _language.SetText(Bridge.platform.language);
            _payload.SetText(Bridge.platform.payload);
            _tld.SetText(Bridge.platform.tld);
            _isAudio.SetText(Bridge.platform.isAudioEnabled.ToString());
            _getServerTimeBridgeHandler.SendRequest();
            _isGetAllGamesSupported.SetText(Bridge.platform.isGetAllGamesSupported.ToString());
            _isGetGameByIdSupported.SetText(Bridge.platform.isGetGameByIdSupported.ToString());
        }
    }
}