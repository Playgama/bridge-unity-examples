using System;
using Playgama.Examples.Starter.Scripts.Playgama;
#if UNITY_WEBGL
using Playgama.Modules.Advertisement;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class AdvertisementMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;
#if UNITY_WEBGL
        private readonly StateHistory<BannerState> _bannerStates = new StateHistory<BannerState>(3);
        private readonly StateHistory<InterstitialState> _interstitialStates = new StateHistory<InterstitialState>(3);
        private readonly StateHistory<RewardedState> _rewardedStates = new StateHistory<RewardedState>(3);
#endif        
        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;
#if UNITY_WEBGL
        public void ShowBanner()
        {
            PlaygamaManager.ShowBanner();
        }

        public void HideBanner()
        {
            PlaygamaManager.HideBanner();
        }

        public void ShowInterstitial()
        {
            PlaygamaManager.ShowInterstitial();
        }
        
        public void ShowRewarded()
        {
            PlaygamaManager.ShowRewarded();
        }
        
        public void CheckAdBlock()
        {
            PlaygamaManager.CheckAdBlock((result) =>
                {
                    Debug.Log($"AdBlock Detected: {result}");
                });
        }

        protected override void Awake()
        {
            base.Awake();
            PlaygamaManager.AdvertisementBannerStateChanged += PlaygamaManagerOnAdvertisementBannerStateChanged;
            PlaygamaManager.AdvertisementInterstitialStateChanged += PlaygamaManagerOnAdvertisementInterstitialStateChanged;
            PlaygamaManager.AdvertisementRewardedStateChanged += PlaygamaManagerOnAdvertisementRewardedStateChanged;
        }

        protected override void OnDestroy()
        {
            PlaygamaManager.AdvertisementBannerStateChanged -= PlaygamaManagerOnAdvertisementBannerStateChanged;
            PlaygamaManager.AdvertisementInterstitialStateChanged -= PlaygamaManagerOnAdvertisementInterstitialStateChanged;
            PlaygamaManager.AdvertisementRewardedStateChanged -= PlaygamaManagerOnAdvertisementRewardedStateChanged;
            base.OnDestroy();
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextIsBannerSupported, "Is Banner Supported", PlaygamaManager.IsBannerSupported.ToString());
            SetTextProperty(menuSettings.TextLastBannerState, "Last Banner States", _bannerStates.ToString());
            menuSettings.ButtonShowBanner.interactable = PlaygamaManager.IsBannerSupported;
            menuSettings.ButtonHideBanner.interactable = PlaygamaManager.IsBannerSupported;
            
            SetTextProperty(menuSettings.TextIsInterstitialSupported, "Is Interstitial Supported", PlaygamaManager.IsInterstitialSupported.ToString());
            SetTextProperty(menuSettings.TextMinimumDelayBetweenInterstitial, "Minimum Delay Between Interstitial",PlaygamaManager.MinimumDelayBetweenInterstitial.ToString());
            SetTextProperty(menuSettings.TextLastInterstitialStates, "Last Interstitial States", _interstitialStates.ToString());
            menuSettings.ButtonShowInterstitial.interactable = PlaygamaManager.IsInterstitialSupported;
            
            SetTextProperty(menuSettings.TextIsRewardedSupported, "Is Rewarded Supported", PlaygamaManager.IsRewardedSupported.ToString());
            SetTextProperty(menuSettings.TextLastRewardedStates, "Last Rewarded States", _rewardedStates.ToString());
            SetTextProperty(menuSettings.TextRewardedPlacement, "Rewarded Placement", PlaygamaManager.RewardedPlacement ?? "<null>");
            menuSettings.ButtonShowRewarded.interactable = PlaygamaManager.IsRewardedSupported;
        }

        private void PlaygamaManagerOnAdvertisementBannerStateChanged(BannerState state)
        {
            _bannerStates.Enqueue(state);
            SetTextProperty(menuSettings.TextLastBannerState, "Last Banner States", _bannerStates.ToString());
        }

        private void PlaygamaManagerOnAdvertisementInterstitialStateChanged(InterstitialState state)
        {
            _interstitialStates.Enqueue(state);
            SetTextProperty(menuSettings.TextLastInterstitialStates, "Last Interstitial States", _interstitialStates.ToString());
        }

        private void PlaygamaManagerOnAdvertisementRewardedStateChanged(RewardedState state)
        {
            _rewardedStates.Enqueue(state);
            SetTextProperty(menuSettings.TextLastRewardedStates, "Last Rewarded States", _rewardedStates.ToString());
        }
#endif

        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsBannerSupported => textIsBannerSupported;
            public TextMeshProUGUI TextLastBannerState => textLastBannerState;
            public Button ButtonShowBanner => buttonShowBanner;
            public Button ButtonHideBanner => buttonHideBanner;
            public TextMeshProUGUI TextIsInterstitialSupported => textIsInterstitialSupported;
            public TextMeshProUGUI TextMinimumDelayBetweenInterstitial => textMinimumDelayBetweenInterstitial;
            public TextMeshProUGUI TextLastInterstitialStates => textLastInterstitialStates;
            public Button ButtonShowInterstitial => buttonShowInterstitial;
            public TextMeshProUGUI TextIsRewardedSupported => textIsRewardedSupported;
            public TextMeshProUGUI TextLastRewardedStates => textLastRewardedStates;
            public TextMeshProUGUI TextRewardedPlacement => textRewardedPlacement;
            public Button ButtonShowRewarded => buttonShowRewarded;
            public Button ButtonCheckAdBlock => buttonCheckAdBlock;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textIsBannerSupported;
            [SerializeField] private TextMeshProUGUI textLastBannerState;
            [SerializeField] private Button buttonShowBanner;
            [SerializeField] private Button buttonHideBanner;
            
            [SerializeField] private TextMeshProUGUI textIsInterstitialSupported;
            [SerializeField] private TextMeshProUGUI textMinimumDelayBetweenInterstitial;
            [SerializeField] private TextMeshProUGUI textLastInterstitialStates;
            [SerializeField] private Button buttonShowInterstitial;
            
            [SerializeField] private TextMeshProUGUI textIsRewardedSupported;
            [SerializeField] private TextMeshProUGUI textLastRewardedStates;
            [SerializeField] private TextMeshProUGUI textRewardedPlacement;
            [SerializeField] private Button buttonShowRewarded;

            [SerializeField] private Button buttonCheckAdBlock;
        }
    }
}