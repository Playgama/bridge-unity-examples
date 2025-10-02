using Playgama;
using Playgama.Modules.Game;
using SandboxUI.Scripts.Base;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.Game
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _currentVisibilityState;
        [SerializeField] private PropertyTextView _lastVisibilityState;

        private void OnEnable()
        {
            var visibilityState = Bridge.game.visibilityState.ToString();
            _currentVisibilityState.SetText(visibilityState);
            _lastVisibilityState.AddValueAndSet(visibilityState);
            Bridge.game.visibilityStateChanged += HandleGameVisibilityStateChanged;
        }

        private void OnDisable()
        {
            Bridge.game.visibilityStateChanged -= HandleGameVisibilityStateChanged;
        }

        private void HandleGameVisibilityStateChanged(VisibilityState state)
        {
            _lastVisibilityState.AddValueAndSet(state.ToString());
        }
    }
}