using System;
using Playgama.Examples.Starter.Scripts.Playgama;
using Playgama.Modules.Game;
using TMPro;
using UnityEngine;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class GameMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
        private readonly StateHistory<VisibilityState> _visibilityStates = new StateHistory<VisibilityState>(3);
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
        
        protected override void Awake()
        {
            base.Awake();
            PlaygamaManager.GameVisibilityStateChanged += OnGameVisibilityStateChanged;
        }

        protected override void OnDestroy()
        {
            PlaygamaManager.GameVisibilityStateChanged -= OnGameVisibilityStateChanged;
            base.OnDestroy();
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextCurrentVisibleState, "Current Visibility State", PlaygamaManager.CurrentVisibilityState.ToString());
            SetTextProperty(menuSettings.TextLastVisibleState, "Last Visibility State", _visibilityStates.ToString());
        }

        private void OnGameVisibilityStateChanged(VisibilityState state)
        {
            _visibilityStates.Enqueue(state);
            SetTextProperty(menuSettings.TextLastVisibleState, "Last Visibility State", _visibilityStates.ToString());
        }

        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextCurrentVisibleState => textCurrentVisibleState;
            public TextMeshProUGUI TextLastVisibleState => textLastVisibleState;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textCurrentVisibleState;
            [SerializeField] private TextMeshProUGUI textLastVisibleState;
        }
    }
}