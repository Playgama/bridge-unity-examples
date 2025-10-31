using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using UnityEngine.UIElements;

public class AchievementsPanelUIHandler : PanelUIHandler {
    private readonly Label isAchievementsSupportedLabel;
    private readonly Label isGetListSupportedLabel;
    private readonly Label isNativePopupSupportedLabel;
    private readonly Label responseLabel;

    private readonly Button unlockButton;
    private readonly Button getListButton;
    private readonly Button showNativePopupButton;

    private readonly TextField keyField;
    private readonly TextField valueField;

    public AchievementsPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isAchievementsSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-achievements-supported");
        isGetListSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-get-list-supported");
        isNativePopupSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-native-popup-supported");
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");

        unlockButton = uiDocument.rootVisualElement.Q<Button>("unlock");
        getListButton = uiDocument.rootVisualElement.Q<Button>("get-list");
        showNativePopupButton = uiDocument.rootVisualElement.Q<Button>("show-native-popup");
        keyField = uiDocument.rootVisualElement.Q<TextField>("key-field");
        valueField = uiDocument.rootVisualElement.Q<TextField>("value-field");

        unlockButton.RegisterCallback<ClickEvent>(_ => Bridge.achievements.Unlock(new Dictionary<string, object> {
            { keyField.value, valueField.value }
        }));
        getListButton.RegisterCallback<ClickEvent>(_ => Bridge.achievements.GetList(new Dictionary<string, object>(), OnGetList));
        showNativePopupButton.RegisterCallback<ClickEvent>(_ => Bridge.achievements.ShowNativePopup(new Dictionary<string, object> {
            { keyField.value, valueField.value }
        }));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        keyField.value = string.Empty;
        valueField.value = string.Empty;
        ;
        responseLabel.text = string.Empty;
        var achievementsIsSupported = Bridge.achievements.isSupported;
        isAchievementsSupportedLabel.text = achievementsIsSupported.ToString();
        var getListSupported = Bridge.achievements.isGetListSupported;
        isGetListSupportedLabel.text = getListSupported.ToString();
        var nativePopupSupported = Bridge.achievements.isNativePopupSupported;
        isNativePopupSupportedLabel.text = nativePopupSupported.ToString();

        showNativePopupButton.SetEnabled(achievementsIsSupported && nativePopupSupported);
        unlockButton.SetEnabled(achievementsIsSupported);
        getListButton.SetEnabled(achievementsIsSupported && getListSupported);
    }

    private void OnGetList(bool success, List<Dictionary<string, string>> data) {
        if (!success) return;
        responseLabel.text = JsonConvert.SerializeObject(data);
    }
}