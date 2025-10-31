using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using UnityEngine.UIElements;

public class RemoteConfigPanelUIHandler : PanelUIHandler {
    private readonly Label isRemoteConfigSupportedLabel;
    private readonly Label responseLabel;

    private readonly Button getButton;

    public RemoteConfigPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isRemoteConfigSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-remote-config-supported");
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");

        getButton = uiDocument.rootVisualElement.Q<Button>("get-button");

        getButton.RegisterCallback<ClickEvent>(_ => Bridge.remoteConfig.Get(new Dictionary<string, object> {
            { "name", "levels" },
            { "value", "5" },
        }, OnGetRemoteConfig));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        responseLabel.text = string.Empty;
        var isSupported = Bridge.remoteConfig.isSupported;
        isRemoteConfigSupportedLabel.text = isSupported.ToString();
        getButton.SetEnabled(isSupported);
    }

    private void OnGetRemoteConfig(bool success, Dictionary<string, string> data) {
        if (!success) return;
        responseLabel.text = JsonConvert.SerializeObject(data);
    }
}