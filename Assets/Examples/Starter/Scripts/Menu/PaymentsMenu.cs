using System;
using Newtonsoft.Json;
using Playgama.Examples.Starter.Scripts.Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playgama.Examples.Starter.Scripts.Menu
{
    public class PaymentsMenu : MenuSystem.Menu
    {
        [SerializeField] private MenuSettings menuSettings;

        private PlaygamaManager PlaygamaManager => menuSettings.PlaygamaManager;

#if UNITY_WEBGL
        public void Purchase()
        {
            PlaygamaManager.Purchase(
                menuSettings.InputFieldProductIdPurchase.text,
                (success, result) =>
                {
                    if (success)
                    {
                        Debug.Log($"Purchase completed successfully, id: {result["id"]}");
                    }
                    else
                    {
                        Debug.LogWarning($"Purchase failed");
                    }
                });
        }

        public void ConsumePurchase()
        {
            PlaygamaManager.ConsumePurchase(
                menuSettings.InputFieldProductIdConsume.text,
                (success, _) =>
                {
                    if (success)
                    {
                        Debug.Log($"Consume purchase completed successfully");
                    }
                    else
                    {
                        Debug.LogWarning($"Consume purchase failed");
                    }
                });
        }
        
        public void GetCatalog()
        {
            PlaygamaManager.GetCatalog((success, catalog) =>
                {
                    if (success)
                    {
                        Debug.Log($"Catalog retrieved successfully");
                        menuSettings.TextRequestResult.text = JsonConvert.SerializeObject(
                            catalog, Formatting.Indented);
                    }
                    else
                    {
                        Debug.LogWarning($"Catalog retrieval failed");
                    }
                }
            );
        }
        
        public void GetPurchases()
        {
            PlaygamaManager.GetPurchases((success, purchases) =>
                {
                    if (success)
                    {
                        Debug.Log($"Purchases retrieved successfully");
                        menuSettings.TextRequestResult.text = JsonConvert.SerializeObject(
                            purchases, Formatting.Indented);
                    }
                }
            );
        }

        protected override void InitMenu()
        {
            SetTextProperty(menuSettings.TextIsPaymentsSupported, "Is Payments Supported", PlaygamaManager.IsPaymentsSupported.ToString());
            
            menuSettings.InputFieldProductIdPurchase.interactable = PlaygamaManager.IsPaymentsSupported;
            menuSettings.InputFieldProductIdConsume.interactable = PlaygamaManager.IsPaymentsSupported;
            menuSettings.ButtonPurchase.interactable = PlaygamaManager.IsPaymentsSupported;
            menuSettings.ButtonConsumePurchase.interactable = PlaygamaManager.IsPaymentsSupported;
            menuSettings.ButtonGetCatalog.interactable = PlaygamaManager.IsPaymentsSupported;
            menuSettings.ButtonGetPurchases.interactable = PlaygamaManager.IsPaymentsSupported;
        }
#endif
        [Serializable]
        public class MenuSettings
        {
            public PlaygamaManager PlaygamaManager => playgamaManager;
            public TextMeshProUGUI TextIsPaymentsSupported => textIsPaymentsSupported;
            public TMP_InputField InputFieldProductIdPurchase => inputFieldProductIdPurchase;
            public TMP_InputField InputFieldProductIdConsume => inputFieldProductIdConsume;
            public Button ButtonPurchase => buttonPurchase;
            public Button ButtonConsumePurchase => buttonConsumePurchase;
            public Button ButtonGetCatalog => buttonGetCatalog;
            public Button ButtonGetPurchases => buttonGetPurchases;
            public TMP_Text TextRequestResult => textRequestResult;
            
            [SerializeField] private PlaygamaManager playgamaManager;
            [SerializeField] private TextMeshProUGUI textIsPaymentsSupported;
            [SerializeField] private TMP_InputField inputFieldProductIdPurchase;
            [SerializeField] private TMP_InputField inputFieldProductIdConsume;
            [SerializeField] private Button buttonPurchase;
            [SerializeField] private Button buttonConsumePurchase;
            [SerializeField] private Button buttonGetCatalog;
            [SerializeField] private Button buttonGetPurchases;
            [SerializeField] private TMP_Text textRequestResult;
        }
    }
}