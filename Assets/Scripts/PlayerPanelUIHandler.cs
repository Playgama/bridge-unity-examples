using System.Threading.Tasks;
using Playgama;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class PlayerPanelUIHandler : PanelUIHandler {
    private readonly Label isAuthSupportedLabel;
    private readonly Label isAuthorizedLabel;
    private readonly Label playerIDLabel;
    private readonly Label playerNameLabel;

    private readonly Button authorizeButton;

    private readonly VisualElement avatarContainer;

    public PlayerPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isAuthorizedLabel = uiDocument.rootVisualElement.Q<Label>("is-authorized");
        isAuthSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-auth-supported");
        playerIDLabel = uiDocument.rootVisualElement.Q<Label>("player-id");
        playerNameLabel = uiDocument.rootVisualElement.Q<Label>("player-name");

        authorizeButton = uiDocument.rootVisualElement.Q<Button>("authorize");

        avatarContainer = uiDocument.rootVisualElement.Q<VisualElement>("avatar-container");

        authorizeButton.RegisterCallback<ClickEvent>(_ => Authorize());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        var isAuthSupported = Bridge.player.isAuthorizationSupported;
        isAuthorizedLabel.text = Bridge.player.isAuthorized.ToString();
        isAuthSupportedLabel.text = isAuthSupported.ToString();
        playerIDLabel.text = Bridge.player.id;
        playerNameLabel.text = Bridge.player.name;

        authorizeButton.SetEnabled(isAuthSupported);
    }

    private void Authorize() {
        Bridge.player.Authorize(null, success => {
            if (!success) return;
            playerIDLabel.text = Bridge.player.id;
            playerNameLabel.text = Bridge.player.name;
            isAuthorizedLabel.text = Bridge.player.isAuthorized.ToString();
            avatarContainer.Clear();
            if (Bridge.player.photos.Count == 0) {
                var visualElement = new VisualElement();
                visualElement.AddToClassList("avatar");
                avatarContainer.Add(visualElement);
            } else {
                foreach (var playerPhoto in Bridge.player.photos) {
                    LoadAvatar(playerPhoto);
                }
            }
        });
    }

    private async void LoadAvatar(string url) {
        var www = UnityWebRequestTexture.GetTexture(url);
        var asyncOperation = www.SendWebRequest();
        while (!asyncOperation.isDone) {
            await Task.Yield();
        }

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        } else {
            var text = DownloadHandlerTexture.GetContent(www);
            var visualElement = new VisualElement();
            visualElement.AddToClassList("avatar");
            visualElement.style.backgroundImage = new StyleBackground(text);
            avatarContainer.Add(visualElement);
        }
    }
}