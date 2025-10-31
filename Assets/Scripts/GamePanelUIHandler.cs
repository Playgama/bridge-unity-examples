using Playgama;
using Playgama.Modules.Game;
using UnityEngine.UIElements;

public class GamePanelUIHandler : PanelUIHandler {
    private readonly Label currentVisibilityStateLabel;
    private readonly Label lastVisibilityStateLabel;
    
    public GamePanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        currentVisibilityStateLabel = uiDocument.rootVisualElement.Q<Label>("current-visibility-state");
        lastVisibilityStateLabel = uiDocument.rootVisualElement.Q<Label>("last-visibility-state");
        currentVisibilityStateLabel.text = string.Empty;
        lastVisibilityStateLabel.text = string.Empty;
        Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        OnGameVisibilityStateChanged(Bridge.game.visibilityState);
    }

    private void OnGameVisibilityStateChanged(VisibilityState state) {
        lastVisibilityStateLabel.text = lastVisibilityStateLabel.text == string.Empty ? state.ToString() : currentVisibilityStateLabel.text;
        currentVisibilityStateLabel.text = state.ToString();
    }
}