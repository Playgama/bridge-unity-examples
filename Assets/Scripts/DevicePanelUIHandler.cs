using Playgama;
using UnityEngine.UIElements;

public class DevicePanelUIHandler : PanelUIHandler {
    public DevicePanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        var deviceNameLabel = uiDocument.rootVisualElement.Q<Label>("device-name");
        deviceNameLabel.text = Bridge.device.type.ToString();
    }
}