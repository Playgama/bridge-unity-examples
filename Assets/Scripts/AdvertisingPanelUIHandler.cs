using Playgama;
using Playgama.Modules.Advertisement;
using UnityEngine.UIElements;

public class AdvertisingPanelUIHandler : PanelUIHandler {
    private readonly Label isBannerSupportedLabel;
    private readonly Label lastBannerStateLabel;
    private readonly Label isInterstitialSupportedLabel;
    private readonly Label minimumDelayBetweenInterstitialLabel;
    private readonly Label lastInterstitialStateLabel;
    private readonly Label isRewardedSupportedLabel;
    private readonly Label lastRewardedStateLabel;
    private readonly Label rewardedPlacementLabel;
    private readonly Label adBlockDetectedLabel;
    
    private readonly Button showBannerButton;
    private readonly Button hideBannerButton;
    private readonly Button showInterstitialButton;
    private readonly Button showRewardedButton;
    private readonly Button adblockButton;

    public AdvertisingPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isBannerSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-banner-supported");
        lastBannerStateLabel = uiDocument.rootVisualElement.Q<Label>("last-banner-state");
        isInterstitialSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-interstitial-supported");
        minimumDelayBetweenInterstitialLabel = uiDocument.rootVisualElement.Q<Label>("minimum-delay-between-interstitial");
        lastInterstitialStateLabel = uiDocument.rootVisualElement.Q<Label>("last-interstitial-state");
        isRewardedSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-rewarded-supported");
        lastRewardedStateLabel = uiDocument.rootVisualElement.Q<Label>("last-rewarded-state");
        rewardedPlacementLabel = uiDocument.rootVisualElement.Q<Label>("rewarded-placement");
        adBlockDetectedLabel = uiDocument.rootVisualElement.Q<Label>("ad-block-detected");
        
        showBannerButton = uiDocument.rootVisualElement.Q<Button>("show-banner");
        hideBannerButton = uiDocument.rootVisualElement.Q<Button>("hide-banner");
        showInterstitialButton = uiDocument.rootVisualElement.Q<Button>("show-interstitial");
        showRewardedButton = uiDocument.rootVisualElement.Q<Button>("show-rewarded");
        adblockButton = uiDocument.rootVisualElement.Q<Button>("adblock");

        Bridge.advertisement.bannerStateChanged += OnChangeBannerState;
        Bridge.advertisement.interstitialStateChanged += OnChangeInterstitialState;
        Bridge.advertisement.rewardedStateChanged += OnChangeRewardedState;
        
        showBannerButton.RegisterCallback<ClickEvent>(_ => Bridge.advertisement.ShowBanner());
        hideBannerButton.RegisterCallback<ClickEvent>(_ => Bridge.advertisement.HideBanner());
        showInterstitialButton.RegisterCallback<ClickEvent>(_ => Bridge.advertisement.ShowInterstitial());
        showRewardedButton.RegisterCallback<ClickEvent>(_ => Bridge.advertisement.ShowRewarded());
        adblockButton.RegisterCallback<ClickEvent>(_ => CheckAddBlock());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        var isBannerSupported = Bridge.advertisement.isBannerSupported;
        var isRewardedSupported = Bridge.advertisement.isRewardedSupported;
        var isInterstitialSupported = Bridge.advertisement.isInterstitialSupported;
        isBannerSupportedLabel.text = isBannerSupported.ToString();
        isInterstitialSupportedLabel.text = isInterstitialSupported.ToString();
        minimumDelayBetweenInterstitialLabel.text = Bridge.advertisement.minimumDelayBetweenInterstitial.ToString();
        isRewardedSupportedLabel.text = isRewardedSupported.ToString();
        rewardedPlacementLabel.text = Bridge.advertisement.rewardedPlacement;
        showBannerButton.SetEnabled(isBannerSupported);
        hideBannerButton.SetEnabled(isBannerSupported);
        showInterstitialButton.SetEnabled(isInterstitialSupported);
        showRewardedButton.SetEnabled(isRewardedSupported);
        OnChangeBannerState(Bridge.advertisement.bannerState);
        OnChangeInterstitialState(Bridge.advertisement.interstitialState);
        OnChangeRewardedState(Bridge.advertisement.rewardedState);
        CheckAddBlock();
    }

    private void CheckAddBlock() {
        adBlockDetectedLabel.text = "Loading...";
        Bridge.advertisement.CheckAdBlock(hasAdBlock => adBlockDetectedLabel.text = hasAdBlock.ToString());
    }

    private void OnChangeBannerState(BannerState state) {
        lastBannerStateLabel.text = state.ToString();
    }

    private void OnChangeInterstitialState(InterstitialState state) {
        lastInterstitialStateLabel.text = state.ToString();
    }

    private void OnChangeRewardedState(RewardedState state) {
        lastRewardedStateLabel.text = state.ToString();
    }
}