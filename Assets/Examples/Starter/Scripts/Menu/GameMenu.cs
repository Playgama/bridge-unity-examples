using System;
using Examples.Starter.Scripts.Playgama;
using Playgama.Modules.Game;
using TMPro;
using UnityEngine;

namespace Examples.Starter.Scripts.Menu
{
    public class GameMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings settings;
        private VisibilityState _lastVisibleState;
        private PlaygamaManager PlaygamaManager => settings.PlaygamaManager;
        protected override void Awake()
        {
            base.Awake();
            PlaygamaManager.GameVisibilityStateChanged += OnGameVisibilityStateChanged;
            SetCurrentVisibleState(PlaygamaManager.CurrentVisibilityState);
            SetLastVisibleState(PlaygamaManager.CurrentVisibilityState);
        }

        private void OnGameVisibilityStateChanged(VisibilityState state)
        {
            SetCurrentVisibleState(state);
            if (_lastVisibleState != state)
            {
                SetLastVisibleState(state);
            }
        }

        private void SetCurrentVisibleState(VisibilityState state)
        {
            settings.PropertyCurrentVisibleState.text = $"Current Visibility State: <color=#D8BBFF>{state}</color>";
        }

        private void SetLastVisibleState(VisibilityState state)
        {
            settings.PropertyLastVisibleState.text = $"Last Visibility State: <color=#D8BBFF>{state}</color>";
        }

        protected override void OnDestroy()
        {
            PlaygamaManager.GameVisibilityStateChanged -= OnGameVisibilityStateChanged;
            base.OnDestroy();
        }

        [Serializable]
        public class MenuSettings
        {
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI propertyCurrentVisibleState;
            [SerializeField] private TextMeshProUGUI propertyLastVisibleState;
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI PropertyCurrentVisibleState => propertyCurrentVisibleState;
            public TextMeshProUGUI PropertyLastVisibleState => propertyLastVisibleState;
        }
    }
}