using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PanelUIHandler {
    private readonly UIDocument uiDocument;
    private readonly ScrollView scrollView;

    private float lastScrollPos;
    private float maxScrollableHeight;

    public event Action BackPressed = delegate { };

    protected PanelUIHandler(UIDocument uiDocument) {
        this.uiDocument = uiDocument;
        uiDocument.enabled = true;
        scrollView = uiDocument.rootVisualElement.Q<ScrollView>();
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        FixWebGLScrollSpeed();
        InitBackButton();
    }

    private void FixWebGLScrollSpeed() {
#if !UNITY_EDITOR
        if (scrollView == null) return;
        scrollView.RegisterCallback<WheelEvent>(ScrollCallback, TrickleDown.TrickleDown);
        maxScrollableHeight = (float)typeof(ScrollView).GetProperty("scrollableHeight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(scrollView, null)!;
    }

    private void ScrollCallback(WheelEvent evt) {
        evt.StopPropagation();
        if (float.IsNaN(maxScrollableHeight)) {
            maxScrollableHeight = (float)typeof(ScrollView).GetProperty("scrollableHeight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(scrollView, null)!;
        }

        uiDocument.rootVisualElement.focusController?.IgnoreEvent(evt);
        lastScrollPos += evt.delta.y * 0.1f;
        lastScrollPos = Mathf.Min(maxScrollableHeight + 200f, Mathf.Max(0, lastScrollPos));
        scrollView.scrollOffset = new Vector2(0, lastScrollPos);
    }
#else
    }
#endif

    private void InitBackButton() {
        var button = uiDocument.rootVisualElement.Q<VisualElement>(className: "header-btn__back-button");
        button.focusable = true;
        button.RegisterCallback<ClickEvent>(_ => {
            Toggle(false);
            BackPressed();
        });
    }

    public virtual void Toggle(bool enable) {
        uiDocument.rootVisualElement.style.display = enable ? DisplayStyle.Flex : DisplayStyle.None;
    }
}