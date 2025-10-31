using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Playgama;
using Playgama.Modules.Platform;
using UnityEngine.UIElements;

public class PlatformPanelUIHandler : PanelUIHandler {
    private readonly Label platformIdLabel;
    private readonly Label languageLabel;
    private readonly Label payloadLabel;
    private readonly Label tldLabel;
    private readonly Label serverTimeLabel;
    private readonly Label audioLabel;
    private readonly Label allGamesSupportedLabel;
    private readonly Label gameByIdSupportedLabel;
    private readonly Label allGamesResponseLabel;
    private readonly Label gameResponseLabel;

    private readonly VisualElement sendGameReadyButton;
    private readonly VisualElement sendGameplayStartedButton;
    private readonly VisualElement sendInGameLoadingStartedButton;
    private readonly VisualElement sendPlayerGotAchievementButton;
    private readonly VisualElement sendGameplayStoppedButton;
    private readonly VisualElement sendInGameLoadingStoppedButton;
    private readonly VisualElement getServerTimeButton;
    private readonly VisualElement getAllGamesButton;
    private readonly VisualElement getGameByIdButton;

    private readonly TextField gameIdTextField;

    public PlatformPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        platformIdLabel = uiDocument.rootVisualElement.Q<Label>("platform-id");
        languageLabel = uiDocument.rootVisualElement.Q<Label>("language");
        payloadLabel = uiDocument.rootVisualElement.Q<Label>("payload");
        tldLabel = uiDocument.rootVisualElement.Q<Label>("tld");
        serverTimeLabel = uiDocument.rootVisualElement.Q<Label>("server-time");
        audioLabel = uiDocument.rootVisualElement.Q<Label>("is-audio");
        allGamesSupportedLabel = uiDocument.rootVisualElement.Q<Label>("all-games-supported");
        gameByIdSupportedLabel = uiDocument.rootVisualElement.Q<Label>("game-by-id-supported");
        allGamesResponseLabel = uiDocument.rootVisualElement.Q<Label>("all-games-response");
        gameResponseLabel = uiDocument.rootVisualElement.Q<Label>("game-response");

        sendGameReadyButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameReadyButton");
        sendGameplayStartedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameplayStartedButton");
        sendInGameLoadingStartedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameLoadingStartedButton");
        sendPlayerGotAchievementButton = uiDocument.rootVisualElement.Q<VisualElement>("SendPlayerGotAchievementButton");
        sendGameplayStoppedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameplayStoppedButton");
        sendInGameLoadingStoppedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameLoadingStoppedButton");
        getServerTimeButton = uiDocument.rootVisualElement.Q<VisualElement>("GetServerTimeButton");
        getAllGamesButton = uiDocument.rootVisualElement.Q<VisualElement>("GetAllGamesButton");
        getGameByIdButton = uiDocument.rootVisualElement.Q<VisualElement>("GetGameByIdButton");

        gameIdTextField = uiDocument.rootVisualElement.Q<TextField>("game-id-textfield");

        sendGameplayStoppedButton.SetEnabled(false);
        sendInGameLoadingStoppedButton.SetEnabled(false);
        sendGameReadyButton.RegisterCallback<ClickEvent>(_ => Bridge.platform.SendMessage(PlatformMessage.GameReady));
        sendGameplayStartedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStarted);
            sendGameplayStoppedButton.SetEnabled(true);
            sendGameplayStartedButton.SetEnabled(false);
        });
        sendInGameLoadingStartedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStarted);
            sendInGameLoadingStoppedButton.SetEnabled(true);
            sendInGameLoadingStartedButton.SetEnabled(false);
        });
        sendPlayerGotAchievementButton.RegisterCallback<ClickEvent>(_ => Bridge.platform.SendMessage(PlatformMessage.PlayerGotAchievement));
        sendGameplayStoppedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStopped);
            sendGameplayStartedButton.SetEnabled(true);
            sendGameplayStoppedButton.SetEnabled(false);
        });
        sendInGameLoadingStoppedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStopped);
            sendInGameLoadingStartedButton.SetEnabled(true);
            sendInGameLoadingStoppedButton.SetEnabled(false);
        });
        getServerTimeButton.RegisterCallback<ClickEvent>(_ => UpdateServerTime());
        getAllGamesButton.RegisterCallback<ClickEvent>(_ => GetAllGames());
        getGameByIdButton.RegisterCallback<ClickEvent>(_ => GetGameById());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        allGamesResponseLabel.text = string.Empty;
        gameResponseLabel.text = string.Empty;
        platformIdLabel.text = Bridge.platform.id;
        languageLabel.text = Bridge.platform.language;
        payloadLabel.text = string.IsNullOrWhiteSpace(Bridge.platform.payload) ? "<null>" : Bridge.platform.payload;
        tldLabel.text = string.IsNullOrWhiteSpace(Bridge.platform.tld) ? "<null>" : Bridge.platform.tld;
        audioLabel.text = Bridge.platform.isAudioEnabled.ToString();
        UpdateServerTime();
        allGamesSupportedLabel.text = Bridge.platform.isGetAllGamesSupported.ToString();
        gameByIdSupportedLabel.text = Bridge.platform.isGetGameByIdSupported.ToString();
        getAllGamesButton.SetEnabled(Bridge.platform.isGetAllGamesSupported);
        getGameByIdButton.SetEnabled(Bridge.platform.isGetGameByIdSupported);
    }

    private void UpdateServerTime() {
        serverTimeLabel.text = "Loading...";
        Bridge.platform.GetServerTime(time => serverTimeLabel.text = time.HasValue ? time.Value.ToString("F", CultureInfo.InvariantCulture) : "null");
    }

    private void GetAllGames() {
        Bridge.platform.GetAllGames((success, list) => {
            if (!success) {
                return;
            }

            allGamesSupportedLabel.text = JsonConvert.SerializeObject(list);
        });
    }

    private void GetGameById() {
        if (string.IsNullOrEmpty(gameIdTextField.value)) return;
        Bridge.platform.GetGameById(new Dictionary<string, object> { { "gameId", gameIdTextField.value } }, (success, gameInfo) => {
            if (!success) {
                return;
            }

            gameResponseLabel.text = JsonConvert.SerializeObject(gameInfo);
        });
    }
}