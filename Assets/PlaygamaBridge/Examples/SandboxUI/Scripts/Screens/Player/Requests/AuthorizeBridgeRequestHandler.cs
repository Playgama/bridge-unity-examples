using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Player.Requests
{
    public class AuthorizeBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private PropertyTextView _isAuthorized;
        [SerializeField] private PropertyTextView _playerId;
        [SerializeField] private PropertyTextView _playerName;
        [SerializeField] private RawImage[] _playerAvatars;
        [SerializeField] private Texture2D _defaultAvatar;

        public override void SendRequest()
        {
            Bridge.player.Authorize(onComplete: OnAuthorizationCompleted);
        }

        private void OnAuthorizationCompleted(bool completed)
        {
            _isAuthorized.SetText(Bridge.player.isAuthorized.ToString());
            _playerId.SetText(Bridge.player.id);
            _playerName.SetText(Bridge.player.name);

            AvatarLoader.instance.InitializeAvatars(_playerAvatars, _defaultAvatar);
        }
    }
}