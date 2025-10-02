using Playgama;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
{
    public class PlatformScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _platformId;
        [SerializeField] private PropertyTextView _language;
        [SerializeField] private PropertyTextView _payload;
        [SerializeField] private PropertyTextView _tld;
        [SerializeField] private PropertyTextView _isAudio;

        private void OnEnable()
        {
            _platformId.SetText(Bridge.platform.id);
            _language.SetText(Bridge.platform.language);
            _payload.SetText(Bridge.platform.payload);
            _tld.SetText(Bridge.platform.tld);
        }
    }
}