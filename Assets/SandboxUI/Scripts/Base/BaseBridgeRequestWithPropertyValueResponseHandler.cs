using UnityEngine;

namespace SandboxUI.Scripts
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