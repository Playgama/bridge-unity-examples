using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using UnityEngine.UIElements;

public class AchievementsPanelUIHandler : PanelUIHandler {
    private readonly Label responseLabel;

    private readonly Button unlockButton;
    private readonly Button getAchievementsButton;

    private readonly TextField keyField;

    public AchievementsPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");

        unlockButton = uiDocument.rootVisualElement.Q<Button>("unlock");
        getAchievementsButton = uiDocument.rootVisualElement.Q<Button>("get-achievements");
        keyField = uiDocument.rootVisualElement.Q<TextField>("key-field");

        unlockButton.RegisterCallback<ClickEvent>(_ => Bridge.achievements.Unlock(keyField.value));
        getAchievementsButton.RegisterCallback<ClickEvent>(_ => Bridge.achievements.GetAchievements(OnGetAchievements));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        keyField.value = string.Empty;
        responseLabel.text = string.Empty;
    }

    private void OnGetAchievements(bool success, List<Dictionary<string, string>> data) {
        if (!success) return;
        responseLabel.text = JsonConvert.SerializeObject(data);
    }
}
