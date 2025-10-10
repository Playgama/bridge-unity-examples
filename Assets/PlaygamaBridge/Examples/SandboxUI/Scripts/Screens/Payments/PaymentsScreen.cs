using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Payments
{
    public class PaymentsScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isPaymentsSupported;

        private void OnEnable()
        {
            _isPaymentsSupported.SetText(Bridge.payments.isSupported.ToString());
        }
    }
}