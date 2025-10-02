using System;
using Playgama;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _currentVisibilityState;
        [SerializeField] private PropertyTextView _lastVisibilityState;

        private void OnEnable()
        {
            _currentVisibilityState.SetText(Bridge.game.visibilityState.ToString());
            _lastVisibilityState.SetText("true visible ->");
        }
    }
}