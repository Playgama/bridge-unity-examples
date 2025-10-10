using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Base
{
    public abstract class BaseBridgeRequestWithPropertyValueResponseHandler : BaseBridgeRequestWithResponseHandler
    {
        [SerializeField] private PropertyTextView _property;

        protected sealed override void SetResponse(string response)
        {
            _property.SetText(response);
        }
    }
}