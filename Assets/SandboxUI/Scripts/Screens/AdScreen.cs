using Playgama;
using Playgama.Modules.Advertisement;
using SandboxUI.Scripts.Base;
using SandboxUI.Scripts.Requests;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
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

            Bridge.advertisement.bannerStateChanged += HandleBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged += HandleInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += HandleRewardedStateChanged;

            _adBlockBridgeRequestHandler.SendRequest();
        }

        private void OnDisable()
        {
            Bridge.advertisement.bannerStateChanged -= HandleBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged -= HandleInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged -= HandleRewardedStateChanged;
        }

        private void HandleBannerStateChanged(BannerState state)
        {
            _lastBannerState.AddValueAndSet(state.ToString());
        }

        private void HandleInterstitialStateChanged(InterstitialState state)
        {
            _lastInterstitialStates.AddValueAndSet(state.ToString());
        }

        private void HandleRewardedStateChanged(RewardedState state)
        {
            _lastRewardedStates.AddValueAndSet(state.ToString());
        }
    }
}