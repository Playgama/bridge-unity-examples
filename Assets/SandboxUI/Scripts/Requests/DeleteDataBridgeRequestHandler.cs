using Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Requests
{
    public class DeleteDataBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _coinsCountInput;
        [SerializeField] private TMP_InputField _levelIdInput;
        [SerializeField] private ToggleGroup _toggleGroup;

        public override void SendRequest()
        {
            var toggle = _toggleGroup.GetFirstActiveToggle();

            if (!toggle)
            {
                return;
            }

            var storageType = StorageToggleUtils.ConvertToggleToStorageType(toggle);

            Bridge.storage.Delete(Constants.Keys, HandleDeleteCompleted, storageType);
        }

        private void HandleDeleteCompleted(bool success)
        {
            if (!success)
            {
                return;
            }

            _coinsCountInput.text = string.Empty;
            _levelIdInput.text = string.Empty;
        }
    }
}