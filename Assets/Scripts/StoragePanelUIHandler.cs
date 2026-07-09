using Playgama;
using UnityEngine.UIElements;

public class StoragePanelUIHandler : PanelUIHandler {
    private readonly Button loadDatabutton;
    private readonly Button saveDatabutton;
    private readonly Button deleteDatabutton;

    private readonly TextField coinsField;
    private readonly TextField levelField;

    public StoragePanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        loadDatabutton = uiDocument.rootVisualElement.Q<Button>("load-data");
        saveDatabutton = uiDocument.rootVisualElement.Q<Button>("save-data");
        deleteDatabutton = uiDocument.rootVisualElement.Q<Button>("delete-data");

        coinsField = uiDocument.rootVisualElement.Q<TextField>("coins-field");
        levelField = uiDocument.rootVisualElement.Q<TextField>("level-field");

        loadDatabutton.RegisterCallback<ClickEvent>(_ => LoadClicked());
        saveDatabutton.RegisterCallback<ClickEvent>(_ => SaveClicked());
        deleteDatabutton.RegisterCallback<ClickEvent>(_ => DeleteClicked());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        coinsField.value = string.Empty;
        levelField.value = string.Empty;
    }

    private void DeleteClicked() {
        Bridge.storage.Delete("level");
        Bridge.storage.Delete("coins");
    }

    private void SaveClicked() {
        Bridge.storage.Set("level", levelField.value);
        Bridge.storage.Set("coins", coinsField.value);
    }

    private void LoadClicked() {
        Bridge.storage.Get("level", (success, info) => {
            if (!success) return;
            levelField.value = info;
        });
        Bridge.storage.Get("coins", (success, info) => {
            if (!success) return;
            coinsField.value = info;
        });
    }
}
