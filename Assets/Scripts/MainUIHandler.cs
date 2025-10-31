using System;
using Playgama;
using Playgama.Modules.Platform;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUIHandler : MonoBehaviour {
    [SerializeField] private UIDocument mainUIDocument;
    [SerializeField] private UIDocument achievementsUIDocument;
    [SerializeField] private UIDocument advertisingUIDocument;
    [SerializeField] private UIDocument deviceUIDocument;
    [SerializeField] private UIDocument gameUIDocument;
    [SerializeField] private UIDocument leaderboardsUIDocument;
    [SerializeField] private UIDocument paymentsUIDocument;
    [SerializeField] private UIDocument platformUIDocument;
    [SerializeField] private UIDocument playerUIDocument;
    [SerializeField] private UIDocument remoteConfigUIDocument;
    [SerializeField] private UIDocument socialUIDocument;
    [SerializeField] private UIDocument storageUIDocument;
    [SerializeField] private Bridge bridge;

    private AchievementsPanelUIHandler achievements;
    private AdvertisingPanelUIHandler advertising;
    private DevicePanelUIHandler device;
    private GamePanelUIHandler game;
    private LeaderboardsPanelUIHandler leaderboards;
    private PaymentsPanelUIHandler payments;
    private PlatformPanelUIHandler platform;
    private PlayerPanelUIHandler player;
    private RemoteConfigPanelUIHandler remoteConfig;
    private SocialPanelUIHandler social;
    private StoragePanelUIHandler storage;

    private void Awake() {
        achievements = new AchievementsPanelUIHandler(achievementsUIDocument);
        advertising = new AdvertisingPanelUIHandler(advertisingUIDocument);
        device = new DevicePanelUIHandler(deviceUIDocument);
        game = new GamePanelUIHandler(gameUIDocument);
        leaderboards = new LeaderboardsPanelUIHandler(leaderboardsUIDocument);
        payments = new PaymentsPanelUIHandler(paymentsUIDocument);
        platform = new PlatformPanelUIHandler(platformUIDocument);
        player = new PlayerPanelUIHandler(playerUIDocument);
        remoteConfig = new RemoteConfigPanelUIHandler(remoteConfigUIDocument);
        social = new SocialPanelUIHandler(socialUIDocument);
        storage = new StoragePanelUIHandler(storageUIDocument);

        InitButton("PlatformButton", platform);
        InitButton("DeviceButton", device);
        InitButton("GameButton", game);
        InitButton("StorageButton", storage);
        InitButton("AdvertisementButton", advertising);
        InitButton("PlayerButton", player);
        InitButton("SocialButton", social);
        InitButton("LeaderboardsButton", leaderboards);
        InitButton("PaymentsButton", payments);
        InitButton("AchievementsButton", achievements);
        InitButton("RemoteConfigButton", remoteConfig);
        Toggle(true);
        Bridge.platform.SendMessage(PlatformMessage.GameReady);
    }

    private void InitButton(string buttonName, PanelUIHandler panelUIHandler) {
        var button = mainUIDocument.rootVisualElement.Q<VisualElement>(buttonName).ElementAt(0);
        button.focusable = true;
        button.RegisterCallback<ClickEvent>(_ => EnablePanel(panelUIHandler));
        panelUIHandler.BackPressed += () => Toggle(true);
    }

    private void Toggle(bool enable) {
        var root = mainUIDocument.rootVisualElement.Q<VisualElement>("Main-container").ElementAt(0);
        root.style.display = enable ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void EnablePanel(PanelUIHandler panel) {
        Toggle(false);
        panel.Toggle(true);
    }
}