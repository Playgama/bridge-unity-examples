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
        private readonly StateHistory<VisibilityState> _visibilityStates = new StateHistory<VisibilityState>(3);
        private PlaygamaManager PlaygamaManager => settings.PlaygamaManager;
        protected override void Awake()
        {
            base.Awake();
            PlaygamaManager.GameVisibilityStateChanged += OnGameVisibilityStateChanged;
        }

        private void Start()
        {
            InitMenu();
        }

        protected override void OnDestroy()
        {
            PlaygamaManager.GameVisibilityStateChanged -= OnGameVisibilityStateChanged;
            base.OnDestroy();
        }

        private void InitMenu()
        {
            SetTextProperty(settings.PropertyCurrentVisibleState, "Current Visibility State", PlaygamaManager.CurrentVisibilityState.ToString());
            SetTextProperty(settings.PropertyLastVisibleState, "Last Visibility State", PlaygamaManager.CurrentVisibilityState.ToString());
        }

        private void SetTextProperty(TextMeshProUGUI text, string name, string value)
        {
            text.text = $"{name}: <color=#D8BBFF>{value}</color>";
        }

        private void OnGameVisibilityStateChanged(VisibilityState state)
        {
            _visibilityStates.Enqueue(state);
            InitMenu();
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