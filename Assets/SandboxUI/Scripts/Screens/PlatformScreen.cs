using Playgama;
using SandboxUI.Scripts.Requests;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
{
    public sealed class PlatformScreen : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private PropertyTextView _platformId;
        [SerializeField] private PropertyTextView _language;
        [SerializeField] private PropertyTextView _payload;
        [SerializeField] private PropertyTextView _tld;
        [SerializeField] private PropertyTextView _isAudio;
        [SerializeField] private GetServerTimeRequestHandler _getServerTimeHandler;
        [SerializeField] private PropertyTextView _isGetAllGamesSupported;
        [SerializeField] private PropertyTextView _isGetGameByIdSupported;

        private void OnEnable()
        {
            _platformId.SetText(Bridge.platform.id);
            _language.SetText(Bridge.platform.language);
            _payload.SetText(Bridge.platform.payload);
            _tld.SetText(Bridge.platform.tld);
            _isAudio.SetText(Bridge.platform.isAudioEnabled ? "enabled" : "disabled");
            _getServerTimeHandler.SendRequest();
            _isGetAllGamesSupported.SetText(Bridge.platform.isGetAllGamesSupported ? "supported" : "not supported");
            _isGetGameByIdSupported.SetText(Bridge.platform.isGetGameByIdSupported ? "supported" : "not supported");
        }
    }
}