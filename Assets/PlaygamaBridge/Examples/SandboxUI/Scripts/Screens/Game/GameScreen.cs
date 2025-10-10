using Playgama;
using Playgama.Modules.Game;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Game
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
            Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;
        }

        private void OnDisable()
        {
            Bridge.game.visibilityStateChanged -= OnGameVisibilityStateChanged;
        }

        private void OnGameVisibilityStateChanged(VisibilityState state)
        {
            _lastVisibilityState.AddValueAndSet(state.ToString());
        }
    }
}