using Playgama;
using SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.Payments.Requests
{
    public class PurchaseBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _productId;

        public override void SendRequest()
        {
            if (string.IsNullOrEmpty(_productId.text))
            {
                return;
            }

            Bridge.payments.Purchase(_productId.text);
        }
    }
}