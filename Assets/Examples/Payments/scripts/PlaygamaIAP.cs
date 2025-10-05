#if UNITY_WEBGL
using System.Collections.Generic;
using System.Text;
using Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlaygamaIAP : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Spinner shown while a purchase is in progress.")]
    [SerializeField] private GameObject iapLoading;
    [Tooltip("Panel that displays the catalog of products.")]
    [SerializeField] private GameObject catalogue;
    [Tooltip("Displays count of catalog items.")]
    [SerializeField] private TextMeshProUGUI amountText;
    [Tooltip("Lists the common IDs of catalog items.")]
    [SerializeField] private TextMeshProUGUI namesText;
    [Tooltip("Lists the prices of catalog items.")]
    [SerializeField] private TextMeshProUGUI pricesText;
    [Tooltip("Shows the user’s current coin balance.")]
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [Tooltip("Shows whether No-Ads has been purchased.")]
    [SerializeField] private TextMeshProUGUI noAdsAvailableText;

    [Header("Internal State")]
    [SerializeField] private int balance;           
    [SerializeField] private int coinAmount;         // your current coin total
    [SerializeField] private bool isNoAdsBought = false;

    public UnityEvent OnPurchasesLoaded = new UnityEvent();

    private List<Dictionary<string, string>> currentCatalog = new List<Dictionary<string, string>>();
    private int itemAmount = 0;
    private string currentPurchaseId;

    private void Start()
    {
        // Initialize No-Ads text based on saved flag
        UpdateNoAdsAvailableText(isNoAdsBought);

        // Fetch the product catalog
        Bridge.payments.GetCatalog(OnGetCatalogCompleted);
    }

    private void OnGetCatalogCompleted(bool success, List<Dictionary<string, string>> catalog)
    {
        Debug.Log($"OnGetCatalogCompleted, success: {success}");
        if (!success) return;

        currentCatalog = catalog;
        itemAmount = currentCatalog.Count;

        // Update UI: item count
        amountText.text = "Item Amount: " + itemAmount;

        // Build comma-separated name and price lists
        var namesBuilder = new StringBuilder("Item names: ");
        var pricesBuilder = new StringBuilder("Item prices: ");

        for (int i = 0; i < currentCatalog.Count; i++)
        {
            var dict = currentCatalog[i];
            if (dict.TryGetValue("commonId", out var id))
                namesBuilder.Append(id).Append(", ");
            if (dict.TryGetValue("price", out var price))
                pricesBuilder.Append(price).Append(", ");
        }

        namesText.text  = namesBuilder.ToString().TrimEnd(',', ' ');
        pricesText.text = pricesBuilder.ToString().TrimEnd(',', ' ');

        // Now load what the user already owns
        Bridge.payments.GetPurchases(OnGetPurchasesCompleted);
    }

    private void OnGetPurchasesCompleted(bool success, List<Dictionary<string, string>> purchases)
    {
        Debug.Log($"OnGetPurchasesCompleted, success: {success}");
        if (success)
        {
            foreach (var purchase in purchases)
            {
                if (purchase.TryGetValue("commonId", out var id) && id == "NO_Ads")
                {
                    isNoAdsBought = true;
                    UpdateNoAdsAvailableText(true);
                    break;
                }
            }
        }

        // Signal that both catalog + purchase data are ready
        OnPurchasesLoaded.Invoke();
    }

    public string GetPurchaseNameById(int id)
    {
        if (id < 0 || id >= currentCatalog.Count) return null;
        return currentCatalog[id].TryGetValue("commonId", out var name) ? name : null;
    }

    public string GetPurchasePriceById(int id)
    {
        if (id < 0 || id >= currentCatalog.Count) return null;
        return currentCatalog[id].TryGetValue("price", out var price) ? price : null;
    }

    public string GetPurchasePriceCurrencyCodeById(int id)
    {
        if (id < 0 || id >= currentCatalog.Count) return "unavailable_value";
        return currentCatalog[id].TryGetValue("priceCurrencyCode", out var code)
            ? code
            : "unavailable_value";
    }

    public void UpdateNoAdsAvailableText(bool status)
    {
        noAdsAvailableText.text = status
            ? "NO ADS: PURCHASED"
            : "NO ADS: AVAILABLE";
    }

    public void InitiatePurchase(string iapName)
    {
        // Show spinner & start the purchase flow
        iapLoading.SetActive(true);
        currentPurchaseId = iapName;
        Bridge.payments.Purchase(currentPurchaseId, OnPurchaseCompleted);
    }

    private void OnPurchaseCompleted(bool success, Dictionary<string, string> purchase)
    {
        Debug.Log($"OnPurchaseCompleted ({currentPurchaseId}), success: {success}");

        // Hide spinner after a moment
        Invoke(nameof(InvokeDisable), 1f);

        if (!success) return;

        if (currentPurchaseId == "NO_Ads")
        {
            // Non-consumable: just mark bought
            isNoAdsBought = true;
            UpdateNoAdsAvailableText(true);
        }
        else
        {
            // Consumable: consume to credit coins, etc.
            Bridge.payments.ConsumePurchase(currentPurchaseId, OnConsumePurchaseCompleted);
        }
    }
    
    private void OnConsumePurchaseCompleted(bool success, Dictionary<string, string> purchase)
    {
        Debug.Log($"OnConsumePurchaseCompleted ({currentPurchaseId}), success: {success}");
        if (!success) return;
        
        if (currentPurchaseId == "100_Coins")
        {
            coinAmount += 100;
            coinAmountText.text = "Balance: " + coinAmount + " Coins";
        }
    }


    private void InvokeDisable()
    {
        iapLoading.SetActive(false);
    }

  
    public void ChangeCatalogueVisibility(bool status)
    {
        catalogue.SetActive(status);
    }
}

#endif
