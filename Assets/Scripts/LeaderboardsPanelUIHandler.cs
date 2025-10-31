using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using Playgama.Modules.Leaderboards;
using UnityEngine.UIElements;

public class LeaderboardsPanelUIHandler : PanelUIHandler {
    private readonly Label leaderboardTypeLabel;
    private readonly Label responseLabel;

    private readonly Button setScoreButton;
    private readonly Button getEntriesButton;

    private readonly TextField scoreField;
    private readonly TextField leaderboardNameField;
    private readonly TextField leaderboardIdField;

    private bool isAvailable;

    public LeaderboardsPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        leaderboardTypeLabel = uiDocument.rootVisualElement.Q<Label>("leaderboard-type");
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");

        setScoreButton = uiDocument.rootVisualElement.Q<Button>("set-score");
        getEntriesButton = uiDocument.rootVisualElement.Q<Button>("get-entries");

        scoreField = uiDocument.rootVisualElement.Q<TextField>("score-field");
        leaderboardNameField = uiDocument.rootVisualElement.Q<TextField>("leaderboard-name-field");
        leaderboardIdField = uiDocument.rootVisualElement.Q<TextField>("leaderboard-id-field");

        leaderboardNameField.RegisterValueChangedCallback(evt => setScoreButton.SetEnabled(isAvailable && !string.IsNullOrWhiteSpace(evt.newValue)));
        leaderboardIdField.RegisterValueChangedCallback(evt => getEntriesButton.SetEnabled(isAvailable && !string.IsNullOrWhiteSpace(evt.newValue)));

        setScoreButton.RegisterCallback<ClickEvent>(_ => Bridge.leaderboards.SetScore(leaderboardNameField.value, scoreField.value));
        getEntriesButton.RegisterCallback<ClickEvent>(_ => Bridge.leaderboards.GetEntries(leaderboardIdField.value, OnGetEntries));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        responseLabel.text = string.Empty;
        var leaderboardsType = Bridge.leaderboards.type;
        isAvailable = leaderboardsType != LeaderboardType.NotAvailable;
        leaderboardTypeLabel.text = leaderboardsType.ToString();

        scoreField.value = string.Empty;
        leaderboardIdField.value = string.Empty;
        leaderboardNameField.value = string.Empty;
        
        setScoreButton.SetEnabled(isAvailable);
        getEntriesButton.SetEnabled(isAvailable);
    }

    private void OnGetEntries(bool success, List<Dictionary<string, string>> entries) {
        if (!success) return;
        responseLabel.text = JsonConvert.SerializeObject(entries);
    }
}