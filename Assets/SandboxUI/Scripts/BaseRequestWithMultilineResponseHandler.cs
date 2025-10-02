using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts
{
    public abstract class BaseRequestWithMultilineResponseHandler : BaseRequestHandler
    {
        [SerializeField] private TMP_InputField _responseField;

        private bool _isCaretRaycastRemoved;

        protected sealed override void SetResponse(string response)
        {
            _responseField.text = response;
            _responseField.gameObject.SetActive(true);
            TryRemoveRaycastFromCaret();
        }

        /// <summary>
        /// Need to fix scroll behavior that starting from input field
        /// </summary>
        private void TryRemoveRaycastFromCaret()
        {
            var selectionCaret = _responseField.GetComponentInChildren<TMP_SelectionCaret>();
            if (selectionCaret)
            {
                selectionCaret.raycastTarget = false;
            }

            _isCaretRaycastRemoved = true;
        }

        protected sealed override void OnDisableInternal()
        {
            _responseField.gameObject.SetActive(false);
        }
    }
}