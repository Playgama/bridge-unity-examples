using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Payments.Requests
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

        private void OnDisable()
        {
            _productId.text = string.Empty;
        }
    }
}