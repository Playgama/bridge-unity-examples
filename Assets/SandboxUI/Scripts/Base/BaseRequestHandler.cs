using UnityEngine;

namespace SandboxUI.Scripts
{
    public abstract class BaseRequestHandler : MonoBehaviour
    {
        protected void OnDisable()
        {
            OnDisableInternal();
        }

        public abstract void SendRequest();
        protected abstract void OnDisableInternal();
    }
}