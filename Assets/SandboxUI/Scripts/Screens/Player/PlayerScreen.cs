using System.Collections.Generic;
using Playgama;
using SandboxUI.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Screens.Player
{
    public class PlayerScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isAuthSupported;
        [SerializeField] private PropertyTextView _isAuthorized;
        [SerializeField] private PropertyTextView _playerId;
        [SerializeField] private PropertyTextView _playerName;
        [SerializeField] private RawImage[] _playerAvatars;
        [SerializeField] private Texture2D _defaultAvatar;

        private readonly List<Coroutine> _coroutines = new();

        private void OnEnable()
        {
            _isAuthSupported.SetText(Bridge.player.isAuthorizationSupported.ToString());
            _isAuthorized.SetText(Bridge.player.isAuthorized.ToString());
            _playerId.SetText(Bridge.player.id);
            _playerName.SetText(Bridge.player.name);
            AvatarLoader.instance.InitializeAvatars(_playerAvatars, _defaultAvatar);
        }
    }
}