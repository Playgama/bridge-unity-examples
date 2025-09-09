#if UNITY_WEBGL
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IAPButton : MonoBehaviour
{
    [SerializeField] private int buttonID;
    [SerializeField] private PlaygamaIAP playgamaIAP;
    [SerializeField] private string iap_name;

    [SerializeField] private TextMeshProUGUI purchaseName;
    [SerializeField] private TextMeshProUGUI purchasePrice;
    [SerializeField] private Button iapButton;

    private void Start()
    {
        // Get the Button component on this GameObject
        iapButton = GetComponent<Button>();
        if (iapButton != null)
        {
            iapButton.onClick.AddListener(IAPButtonClicked);
        }
        else
        {
            Debug.LogWarning("No Button component found on this GameObject!", this);
        }
    }

    private void OnEnable()
    {
        playgamaIAP.OnPurchasesLoaded.AddListener(RefreshButton);
    }

    private void OnDisable()
    {
        playgamaIAP.OnPurchasesLoaded.RemoveListener(RefreshButton);
    }


    public void RefreshButton()
    {
        purchaseName.text = playgamaIAP.GetPurchaseNameById(buttonID);
        purchasePrice.text = playgamaIAP.GetPurchasePriceById(buttonID);
    }
    
    public void IAPButtonClicked()
    {
        playgamaIAP.InitiatePurchase(iap_name);
    }
    
}

#endif
