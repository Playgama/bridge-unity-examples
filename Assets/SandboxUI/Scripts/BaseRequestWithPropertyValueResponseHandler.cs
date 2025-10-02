using UnityEngine;

namespace SandboxUI.Scripts
{
    public abstract class BaseRequestWithPropertyValueResponseHandler : BaseRequestHandler
    {
        [SerializeField] private PropertyTextView _property;

        protected sealed override void SetResponse(string response)
        {
            _property.SetText(response);
        }

        protected sealed override void OnDisableInternal()
        {
        }
    }
}