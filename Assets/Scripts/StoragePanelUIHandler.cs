using Playgama;
using Playgama.Modules.Storage;
using UnityEngine.UIElements;

public class StoragePanelUIHandler : PanelUIHandler {
    private readonly Label storageTypeLabel;
    private readonly Label isLocalStorageSupportedLabel;
    private readonly Label isLocalStorageAvailableLabel;
    private readonly Label isPlatformInternalSupportedLabel;
    private readonly Label isPlatformInternalAvailableLabel;

    private readonly RadioButton localStorageRadioButton;
    private readonly RadioButton platformStorageRadioButton;

    private readonly Button loadDatabutton;
    private readonly Button saveDatabutton;
    private readonly Button deleteDatabutton;

    private readonly TextField coinsField;
    private readonly TextField levelField;

    public StoragePanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        storageTypeLabel = uiDocument.rootVisualElement.Q<Label>("storage-type");
        isLocalStorageAvailableLabel = uiDocument.rootVisualElement.Q<Label>("is-local-storage-available");
        isPlatformInternalAvailableLabel = uiDocument.rootVisualElement.Q<Label>("is-platform-internal-available");
        isLocalStorageSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-local-storage-supported");
        isPlatformInternalSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-platform-internal-supported");

        localStorageRadioButton = uiDocument.rootVisualElement.Q<RadioButton>("local-storage");
        platformStorageRadioButton = uiDocument.rootVisualElement.Q<RadioButton>("platform-storage");

        loadDatabutton = uiDocument.rootVisualElement.Q<Button>("load-data");
        saveDatabutton = uiDocument.rootVisualElement.Q<Button>("save-data");
        deleteDatabutton = uiDocument.rootVisualElement.Q<Button>("delete-data");

        coinsField = uiDocument.rootVisualElement.Q<TextField>("coins-field");
        levelField = uiDocument.rootVisualElement.Q<TextField>("level-field");

        localStorageRadioButton.RegisterValueChangedCallback(_ => UpdateSaveButtons());
        platformStorageRadioButton.RegisterValueChangedCallback(_ => UpdateSaveButtons());
        loadDatabutton.RegisterCallback<ClickEvent>(_ => LoadClicked());
        saveDatabutton.RegisterCallback<ClickEvent>(_ => SaveClicked());
        deleteDatabutton.RegisterCallback<ClickEvent>(_ => DeleteClicked());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        coinsField.value = string.Empty;
        levelField.value = string.Empty;
        storageTypeLabel.text = Bridge.storage.defaultType.ToString();
        var localStorageAvailable = Bridge.storage.IsAvailable(StorageType.LocalStorage);
        var localStorageSupported = Bridge.storage.IsSupported(StorageType.LocalStorage);
        isLocalStorageAvailableLabel.text = localStorageAvailable.ToString();
        var platformStorageAvailable = Bridge.storage.IsAvailable(StorageType.PlatformInternal);
        var platformStorageSupported = Bridge.storage.IsSupported(StorageType.PlatformInternal);
        isPlatformInternalAvailableLabel.text = platformStorageAvailable.ToString();
        isLocalStorageSupportedLabel.text = localStorageSupported.ToString();
        isPlatformInternalSupportedLabel.text = platformStorageSupported.ToString();

        localStorageRadioButton.SetEnabled(localStorageSupported && localStorageAvailable);
        platformStorageRadioButton.SetEnabled(platformStorageSupported && platformStorageAvailable);
    }

    private void UpdateSaveButtons() {
        var localStorageAvailable = Bridge.storage.IsAvailable(StorageType.LocalStorage);
        var localStorageSupported = Bridge.storage.IsSupported(StorageType.LocalStorage);
        var platformStorageAvailable = Bridge.storage.IsAvailable(StorageType.PlatformInternal);
        var platformStorageSupported = Bridge.storage.IsSupported(StorageType.PlatformInternal);
        if ((!localStorageAvailable || !localStorageSupported || !localStorageRadioButton.value) &&
            (!platformStorageSupported || !platformStorageAvailable || !platformStorageRadioButton.value)) {
            return;
        }

        loadDatabutton.SetEnabled(true);
        saveDatabutton.SetEnabled(true);
        deleteDatabutton.SetEnabled(true);
    }

    private void DeleteClicked() {
        Bridge.storage.Delete("level", storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
        Bridge.storage.Delete("coins", storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
    }

    private void SaveClicked() {
        Bridge.storage.Set("level", levelField.value, storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
        Bridge.storage.Set("coins", coinsField.value, storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
    }

    private void LoadClicked() {
        Bridge.storage.Get("level", (success, info) => {
            if (!success) return;
            levelField.value = info;
        }, storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
        Bridge.storage.Get("coins", (success, info) => {
            if (!success) return;
            coinsField.value = info;
        }, storageType: localStorageRadioButton.value ? StorageType.LocalStorage : StorageType.PlatformInternal);
    }
}