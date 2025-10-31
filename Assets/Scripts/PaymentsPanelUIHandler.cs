using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using UnityEngine.UIElements;

public class PaymentsPanelUIHandler : PanelUIHandler {
    private readonly Label isPaymentsSupportedLabel;
    private readonly Label responseLabel;

    private readonly Button purchaseButton;
    private readonly Button consumeButton;
    private readonly Button getCatalogButton;
    private readonly Button getPurchasesButton;
    
    private readonly TextField purchaseProductIDField;
    private readonly TextField consumeProductIDField;

    private bool paymentsIsSupported;

    public PaymentsPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isPaymentsSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-payments-supported");
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");
        
        purchaseButton = uiDocument.rootVisualElement.Q<Button>("purchase-button");
        consumeButton = uiDocument.rootVisualElement.Q<Button>("consume-button");
        getCatalogButton = uiDocument.rootVisualElement.Q<Button>("get-catalog-button");
        getPurchasesButton = uiDocument.rootVisualElement.Q<Button>("get-purchases-button");

        purchaseProductIDField = uiDocument.rootVisualElement.Q<TextField>("purchase-field");
        consumeProductIDField = uiDocument.rootVisualElement.Q<TextField>("consume-field");
        
        purchaseProductIDField.RegisterValueChangedCallback(evt => purchaseButton.SetEnabled(paymentsIsSupported && !string.IsNullOrWhiteSpace(evt.newValue)));
        consumeProductIDField.RegisterValueChangedCallback(evt => consumeButton.SetEnabled(paymentsIsSupported && !string.IsNullOrWhiteSpace(evt.newValue)));
        
        purchaseButton.RegisterCallback<ClickEvent>(_ => Bridge.payments.Purchase(purchaseProductIDField.value));
        consumeButton.RegisterCallback<ClickEvent>(_ => Bridge.payments.ConsumePurchase(consumeProductIDField.value));
        getCatalogButton.RegisterCallback<ClickEvent>(_ => Bridge.payments.GetCatalog(OnGetData));
        getPurchasesButton.RegisterCallback<ClickEvent>(_ => Bridge.payments.GetPurchases(OnGetData));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        purchaseProductIDField.value = string.Empty;
        consumeProductIDField.value = string.Empty;
        responseLabel.text = string.Empty;
        paymentsIsSupported = Bridge.payments.isSupported;
        isPaymentsSupportedLabel.text = paymentsIsSupported.ToString();
        
        purchaseButton.SetEnabled(paymentsIsSupported);
        consumeButton.SetEnabled(paymentsIsSupported);
        getCatalogButton.SetEnabled(paymentsIsSupported);
        getPurchasesButton.SetEnabled(paymentsIsSupported);
    }

    private void OnGetData(bool success, List<Dictionary<string, string>> data) {
        if (!success) return;
        responseLabel.text = JsonConvert.SerializeObject(data);
    }
}