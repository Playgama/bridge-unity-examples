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
    private readonly Label sendMessageResponseLabel;

    private readonly VisualElement sendGameReadyButton;
    private readonly VisualElement sendInGameLoadingStartedButton;
    private readonly VisualElement sendPlayerGotAchievementButton;
    private readonly VisualElement sendInGameLoadingStoppedButton;
    private readonly VisualElement sendLevelStartedButton;
    private readonly VisualElement sendLevelCompletedButton;
    private readonly VisualElement sendLevelFailedButton;
    private readonly VisualElement sendLevelPausedButton;
    private readonly VisualElement sendLevelResumedButton;
    private readonly VisualElement sendLevelStartedWithOptionsButton;
    private readonly VisualElement sendLevelCompletedWithOptionsButton;
    private readonly VisualElement sendLevelFailedWithOptionsButton;
    private readonly VisualElement sendLevelPausedWithOptionsButton;
    private readonly VisualElement sendLevelResumedWithOptionsButton;
    private readonly VisualElement getServerTimeButton;
    private readonly VisualElement getAllGamesButton;
    private readonly VisualElement getGameByIdButton;

    private readonly TextField gameIdTextField;
    private readonly TextField optionsTextField;

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
        sendMessageResponseLabel = uiDocument.rootVisualElement.Q<Label>("send-message-response");

        sendGameReadyButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameReadyButton");
        sendInGameLoadingStartedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameLoadingStartedButton");
        sendPlayerGotAchievementButton = uiDocument.rootVisualElement.Q<VisualElement>("SendPlayerGotAchievementButton");
        sendInGameLoadingStoppedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendGameLoadingStoppedButton");
        sendLevelStartedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelStartedButton");
        sendLevelCompletedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelCompletedButton");
        sendLevelFailedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelFailedButton");
        sendLevelPausedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelPausedButton");
        sendLevelResumedButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelResumedButton");
        sendLevelStartedWithOptionsButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelStartedWithOptionsButton");
        sendLevelCompletedWithOptionsButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelCompletedWithOptionsButton");
        sendLevelFailedWithOptionsButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelFailedWithOptionsButton");
        sendLevelPausedWithOptionsButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelPausedWithOptionsButton");
        sendLevelResumedWithOptionsButton = uiDocument.rootVisualElement.Q<VisualElement>("SendLevelResumedWithOptionsButton");
        getServerTimeButton = uiDocument.rootVisualElement.Q<VisualElement>("GetServerTimeButton");
        getAllGamesButton = uiDocument.rootVisualElement.Q<VisualElement>("GetAllGamesButton");
        getGameByIdButton = uiDocument.rootVisualElement.Q<VisualElement>("GetGameByIdButton");

        gameIdTextField = uiDocument.rootVisualElement.Q<TextField>("game-id-textfield");
        optionsTextField = uiDocument.rootVisualElement.Q<TextField>("options-textfield");

        sendInGameLoadingStoppedButton.SetEnabled(false);
        sendLevelCompletedButton.SetEnabled(false);
        sendLevelFailedButton.SetEnabled(false);
        sendLevelPausedButton.SetEnabled(false);
        sendLevelResumedButton.SetEnabled(false);
        sendGameReadyButton.RegisterCallback<ClickEvent>(_ => Bridge.platform.SendMessage(PlatformMessage.GameReady));
        sendInGameLoadingStartedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStarted);
            sendInGameLoadingStoppedButton.SetEnabled(true);
            sendInGameLoadingStartedButton.SetEnabled(false);
        });
        sendPlayerGotAchievementButton.RegisterCallback<ClickEvent>(_ => Bridge.platform.SendMessage(PlatformMessage.PlayerGotAchievement));
        sendInGameLoadingStoppedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.InGameLoadingStopped);
            sendInGameLoadingStartedButton.SetEnabled(true);
            sendInGameLoadingStoppedButton.SetEnabled(false);
        });

        sendLevelStartedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.LevelStarted);
            sendLevelStartedButton.SetEnabled(false);
            sendLevelCompletedButton.SetEnabled(true);
            sendLevelFailedButton.SetEnabled(true);
            sendLevelPausedButton.SetEnabled(true);
        });
        sendLevelCompletedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.LevelCompleted);
            ResetLevelButtons();
        });
        sendLevelFailedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.LevelFailed);
            ResetLevelButtons();
        });
        sendLevelPausedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.LevelPaused);
            sendLevelPausedButton.SetEnabled(false);
            sendLevelResumedButton.SetEnabled(true);
        });
        sendLevelResumedButton.RegisterCallback<ClickEvent>(_ => {
            Bridge.platform.SendMessage(PlatformMessage.LevelResumed);
            sendLevelPausedButton.SetEnabled(true);
            sendLevelResumedButton.SetEnabled(false);
        });
        sendLevelStartedWithOptionsButton.RegisterCallback<ClickEvent>(_ => SendMessageWithOptions(PlatformMessage.LevelStarted));
        sendLevelCompletedWithOptionsButton.RegisterCallback<ClickEvent>(_ => SendMessageWithOptions(PlatformMessage.LevelCompleted));
        sendLevelFailedWithOptionsButton.RegisterCallback<ClickEvent>(_ => SendMessageWithOptions(PlatformMessage.LevelFailed));
        sendLevelPausedWithOptionsButton.RegisterCallback<ClickEvent>(_ => SendMessageWithOptions(PlatformMessage.LevelPaused));
        sendLevelResumedWithOptionsButton.RegisterCallback<ClickEvent>(_ => SendMessageWithOptions(PlatformMessage.LevelResumed));

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

    private void ResetLevelButtons() {
        sendLevelStartedButton.SetEnabled(true);
        sendLevelCompletedButton.SetEnabled(false);
        sendLevelFailedButton.SetEnabled(false);
        sendLevelPausedButton.SetEnabled(false);
        sendLevelResumedButton.SetEnabled(false);
    }

    private void SendMessageWithOptions(PlatformMessage message) {
        var optionsJson = optionsTextField.value;
        if (string.IsNullOrWhiteSpace(optionsJson)) {
            sendMessageResponseLabel.text = "Please enter options JSON";
            return;
        }

        try {
            var options = JsonConvert.DeserializeObject<Dictionary<string, object>>(optionsJson);
            Bridge.platform.SendMessage(message, options);
            sendMessageResponseLabel.text = $"Sent {message} with options: {optionsJson}";
        }
        catch (System.Exception e) {
            sendMessageResponseLabel.text = $"Invalid JSON: {e.Message}";
        }
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