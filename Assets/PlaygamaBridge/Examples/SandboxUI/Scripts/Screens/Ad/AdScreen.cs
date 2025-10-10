using Playgama;
using Playgama.Modules.Advertisement;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Ad.Requests;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Ad
{
    public class AdScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isBannerSupported;
        [SerializeField] private PropertyTextView _lastBannerState;
        [SerializeField] private PropertyTextView _isInterstitialSupported;
        [SerializeField] private PropertyTextView _minimumDelayBetweenInterstitial;
        [SerializeField] private PropertyTextView _lastInterstitialStates;
        [SerializeField] private PropertyTextView _isRewardedSupported;
        [SerializeField] private PropertyTextView _lastRewardedStates;
        [SerializeField] private PropertyTextView _rewardedPlacement;
        [SerializeField] private CheckAdBlockBridgeRequestHandler _adBlockBridgeRequestHandler;

        private void OnEnable()
        {
            _isBannerSupported.SetText(Bridge.advertisement.isBannerSupported.ToString());
            _lastBannerState.SetText(string.Empty);
            _isInterstitialSupported.SetText(Bridge.advertisement.isInterstitialSupported.ToString());
            _minimumDelayBetweenInterstitial.SetText(Bridge.advertisement.minimumDelayBetweenInterstitial.ToString());
            _lastInterstitialStates.SetText(string.Empty);
            _isRewardedSupported.SetText(Bridge.advertisement.isRewardedSupported.ToString());
            _lastRewardedStates.SetText(string.Empty);
            _rewardedPlacement.SetText(Bridge.advertisement.rewardedPlacement);

            Bridge.advertisement.bannerStateChanged += OnBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;

            _adBlockBridgeRequestHandler.SendRequest();
        }

        private void OnDisable()
        {
            Bridge.advertisement.bannerStateChanged -= OnBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged -= OnInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged -= OnRewardedStateChanged;
        }

        private void OnBannerStateChanged(BannerState state)
        {
            _lastBannerState.AddValueAndSet(state.ToString());
        }

        private void OnInterstitialStateChanged(InterstitialState state)
        {
            _lastInterstitialStates.AddValueAndSet(state.ToString());
        }

        private void OnRewardedStateChanged(RewardedState state)
        {
            _lastRewardedStates.AddValueAndSet(state.ToString());
        }
    }
}